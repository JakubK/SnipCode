using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Services.Interfaces;
using System.Collections.Generic;

namespace SnipCodeAPI.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SnippetController : ControllerBase
    {
        private readonly ISnippetService _snippetService;

        public SnippetController(ISnippetService snippetService) => _snippetService = snippetService;

        /// <summary>
        /// Get all snippets 
        /// </summary>
        /// <response code="200">Return list of Snippets</response>
        /// <response code="204">If list is empty</response>
        [HttpGet, ActionName("GetSnippets")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public ActionResult<List<Snippet>> GetAll()
        {
            var snippets = _snippetService.GetSnippets();
            if (snippets.Count == 0)
                return NoContent();
            return Ok(snippets);
        }

        /// <summary>
        /// Get a specified Snippet
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return specified Snippet</response>
        /// <response code="404">If snippet doesn't exist</response>
        [HttpGet("{id}", Name = "GetSnippet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Snippet> GetSnippet(int id)
        {
            if (_snippetService.GetSnippetById(id, out var snippet) == null)
                return NotFound();

            return snippet;
        }

        /// <summary>
        /// Create new snippet with provided snippetFiles content
        /// </summary>
        /// <param name="snippet"></param>
        /// <response code="201">Return the newly created snippet</response>
        /// <response code="400">If the snippet is null</response>
        [HttpPost(Name = "CreateSnippet")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Snippet> Create(Snippet snippet)
        {
            _snippetService.Create(snippet);
            return CreatedAtRoute("GetSnippet", new { id = snippet.Id }, snippet);
        }

        /// <summary>
        /// Update specific Snippet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="snippet"></param>
        /// <response code="200">Return updated snippet</response>
        /// <response code="404">Snippet not found in collection</response>
        [HttpPut("{id}", Name = "UpdateSnippet")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<Snippet> Update(int id, Snippet snippet)
        {
            if (!_snippetService.UpdateSnippet(id, snippet))
                return NotFound();
            return Ok(snippet);
        }

        /// <summary>
        /// Delete a specific Snippet
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Snippet has been removed</response>
        /// <response code="404">Snippet not found in collection</response>
        [HttpDelete("{id}", Name = "DeleteSnippet")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult Delete(int id)
        {
            if (!_snippetService.DeleteSnippet(id))
                return NotFound();
            return NoContent();
        }
    }
}
