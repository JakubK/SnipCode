using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Extensions;
using SnipCodeAPI.Models;
using SnipCodeAPI.Services.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace SnipCodeAPI.Controllers.API
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJWTService _jwtService;
        public AuthController(IAuthService authService,
                             IJWTService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Regenerate token
        /// </summary>
        [Authorize]
        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromHeader] string refreshToken)
        {
            var token = _jwtService.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            if (token == null)
                return NotFound();
                
            var jwtToken = _jwtService.Generate(token.Email);
            jwtToken.RefreshToken = refreshToken;
            return Ok(jwtToken);
        }

        [HttpPost("register")]
        public IActionResult Register()
        {
            return Ok("aaa");
        }

        /// <summary>
        /// Authorize user and generate a token 
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromHeader] string authorization)
        {
            if (!authorization.StartsWith("Basic"))
                return BadRequest("Wrong Request");

            LoginViewModel credentials = authorization.DecodeCredentials();
            //check in the db
            var jwt = _authService.Authenticate(credentials);
            if(jwt == null)
                return BadRequest("Wrong credentials");
            return Ok(jwt);
        }
    }
}