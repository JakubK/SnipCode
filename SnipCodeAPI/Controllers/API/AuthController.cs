using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Services.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace SnipCodeAPI.Controllers.API
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        public AuthController(IAuthService authService,
                             IPasswordHasher<User> passwordHasher,
                             IJwtService jwtService)
        {
            _authService = authService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;

            System.Diagnostics.Debug.WriteLine("CLEAR");
        }

        /// <summary>
        /// Regenerate token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpPost("refresh")]
        [Route("refresh/{token}")]
        public IActionResult RefreshToken([FromHeader] string refreshToken)
        {
            var token = _jwtService.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            if (token == null)
                return NotFound();
            var jwtToken = _jwtService.Generate(token.Email);
            jwtToken.RefreshToken = refreshToken;
            return Ok(jwtToken);
        }

        /// <summary>
        /// Get new token 
        /// </summary>
        /// <returns></returns>
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var header = Request.Headers["Authorization"];

            if (!header.ToString().StartsWith("Basic"))
            {
                return BadRequest("Wrong Request");
            }

            var credValue = header.ToString().Substring("Basic ".Length).Trim();
            var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
            var usernameAndPass = usernameAndPassenc.Split(":");

            //check in the db

            var user = _authService.Authenticate(new LoginViewModel
            {
                Email = usernameAndPass[0],
                Password = usernameAndPass[1]
            });

            if (user == null)
            {
                return BadRequest("Bad Credentials");
            }

            var jwtToken = _jwtService.Generate(usernameAndPass[0]);

            var refreshToken = _passwordHasher.HashPassword(user, new Guid().ToString())
            .Replace("+", string.Empty)
            .Replace("=", string.Empty)
            .Replace("/", string.Empty);

            jwtToken.RefreshToken = refreshToken;

            _jwtService.RefreshTokens.Add(new RefreshToken { Email = usernameAndPass[0], Token = refreshToken });
            System.Diagnostics.Debug.WriteLine(_jwtService.RefreshTokens.Count);
            return Ok(jwtToken);
        }
    }
}