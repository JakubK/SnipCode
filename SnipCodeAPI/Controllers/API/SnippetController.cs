﻿using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SnipCodeAPI.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SnipCodeAPI.Repositories.Interfaces;
using System;
using Hangfire;

namespace SnipCodeAPI.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SnippetController : ControllerBase
    {
        private readonly ISnippetService _snippetService;
        private readonly IUserRepository _userRepository;

        public SnippetController(ISnippetService snippetService, IUserRepository userRepository)
        {
            _snippetService = snippetService;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Get all snippets 
        /// </summary>
        /// <response code="200">Return list of Snippets</response>
        /// <response code="204">If list is empty</response>
        [HttpGet(Name = "GetSnippets")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public ActionResult<List<Snippet>> GetAllSnippets()
        {
            var snippets = _snippetService.GetSnippets();
            if (snippets.Count == 0)
                return NoContent();
            return Ok(snippets);
        }

        /// <summary>
        /// Get a specified Snippet
        /// </summary>
        /// <param name="hash"></param>
        /// <response code="200">Return specified Snippet</response>
        /// <response code="404">If snippet doesn't exist with specified hash</response>
        [HttpGet("{hash}", Name = "GetSnippet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Snippet> GetSnippet(string hash)
        {
            if (_snippetService.GetSnippetByHash(hash) == null)
                return NotFound(hash);

            return _snippetService.GetSnippetByHash(hash);
        }
        
        [Authorize]
        [HttpGet("user")]
        public ActionResult<List<Snippet>> GetUserSnippets([FromHeader] string Authorization)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

            return _snippetService.GetUserSnippets(email);
        }

        [Authorize]
        [HttpGet("user/shared")]
        public ActionResult<List<Snippet>> GetSharedUserSnippets([FromHeader] string Authorization)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

            return _snippetService.GetSharedUserSnippets(email);
        }

        /// <summary>
        /// Create new snippet with provided snippetFiles content
        /// </summary>
        /// <param name="snippetRequest"></param>
        /// <response code="201">Return the url to newly Snippet</response>
        /// <response code="400">If model doesn't contain required fields</response>
        [HttpPost(Name = "CreateSnippet")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult CreateSnippet(CreateSnippetRequest snippetRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Snippet snippet = _snippetService.Create(snippetRequest);

            //if it has no creatorEmail then create a job to destroy if after 10 minutes
            if(string.IsNullOrEmpty(snippetRequest.CreatorEmail))
            {
                BackgroundJob.Schedule(
                () => _snippetService.DeleteSnippet(snippet.Hash),
                TimeSpan.FromMinutes(10));
            }
            
            return new JsonResult(new {status = HttpStatusCode.Created, hash = snippet.Hash});
        }

        /// <summary>
        /// Update specific Snippet
        /// </summary>
        /// <param name="updateSnippetRequest"></param>
        /// <param name="Authorization"></param>
        /// <response code="200">Return request from input if it has been accepted and executed</response>
        /// <response code="404">Snippet not found in collection with specified hash code</response>
        [HttpPut(Name = "UpdateSnippet")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Authorize]
        public ActionResult<Snippet> UpdateSnippet(UpdateSnippetRequest updateSnippetRequest, [FromHeader] string Authorization)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            
            if(_snippetService.GetSnippetByHash(updateSnippetRequest.Hash).CreatorEmail == email)
                if (!_snippetService.UpdateSnippet(updateSnippetRequest.Hash, updateSnippetRequest))
                    return NotFound(updateSnippetRequest.Hash);
            return Ok(updateSnippetRequest);
        }

        /// <summary>
        /// Delete a specific Snippet
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="Authorization"></param>
        /// <response code="204">Snippet has been removed</response>
        /// <response code="404">Snippet not found in collection with specified hash code</response>
        [HttpDelete("{hash}", Name = "DeleteSnippet")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [Authorize]
        public IActionResult DeleteSnippet(string hash,[FromHeader] string Authorization)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

            if(_snippetService.GetSnippetByHash(hash).CreatorEmail == email)
                if (!_snippetService.DeleteSnippet(hash))
                    return NotFound(hash);
            return NoContent();
        }
        [Authorize]
        [HttpPut("share")]
        public IActionResult ShareSnippet(ShareSnippetRequest shareSnippetRequest, [FromHeader] string Authorization)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            if(_snippetService.GetSnippetByHash(shareSnippetRequest.Hash).CreatorEmail == email) //check if email of creator is equal to email of sender
            {
                User user = _userRepository.GetUserByEmail(shareSnippetRequest.UserEmail);
                if(user == default(User))
                {
                    return NotFound();
                }
                if(!user.SharedSnippets.Any(x => x.Hash == shareSnippetRequest.Hash))
                {
                    user.SharedSnippets.Add(_snippetService.GetSnippetByHash(shareSnippetRequest.Hash));
                    _userRepository.UpdateUser(user);
                }
            }

            return Ok();
        }

        [Authorize]
        [HttpPut("removeShared")]
        public IActionResult RemoveSharedSnippet(ShareSnippetRequest shareSnippetRequest, [FromHeader] string Authorization)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            User user = _userRepository.GetUserByEmail(shareSnippetRequest.UserEmail);

            user.SharedSnippets.RemoveAll(x => x.Hash == shareSnippetRequest.Hash);
            _userRepository.UpdateUser(user);
            
            return Ok();
        }

        private static string GenerateUrl(HttpRequest request, string hash)
        {
            return $"{request.Host}{request.Path.ToUriComponent()}{hash}";
        }
    }
}