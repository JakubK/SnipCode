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
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IJWTService jwtService;
        public AuthController(IAuthService authServiceParam,
                             IJWTService jwtServiceParam)
        {
            authService = authServiceParam;
            jwtService = jwtServiceParam;
        }

        /// <summary>
        /// Regenerate token
        /// </summary>
        [Authorize]
        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromHeader] string refreshToken)
        {
            var token = jwtService.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            if (token == null)
                return NotFound();
                
            var jwtToken = jwtService.Generate(token.Email);
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
        public IActionResult Login([FromBody] LoginViewModel credentials)
        {
            System.Diagnostics.Debug.WriteLine(credentials.Email + " : " + credentials.Password);
            var jwt = authService.Authenticate(credentials);//check in the db
            if(jwt == null)
                return Unauthorized();
            return Ok(jwt);
        }
    }
}