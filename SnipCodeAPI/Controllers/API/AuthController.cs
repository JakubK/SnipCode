using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Models.Requests;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private readonly IUserRepository userRepository;
        public AuthController(IAuthService authServiceParam,
                             IJWTService jwtServiceParam, IUserRepository userRepositoryParam)
        {
            authService = authServiceParam;
            jwtService = jwtServiceParam;
            userRepository = userRepositoryParam;
        }

        /// <summary>
        /// Regenerate token
        /// </summary>
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

        [Authorize]
        [HttpPut("change/password")]
        public IActionResult ChangePassword([FromHeader] string Authorization, ChangePasswordRequest changePasswordRequest)
        {
            string tokenString = Authorization.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            
            bool passwordChanged = authService.TryChangePassword(email,changePasswordRequest);

            if(passwordChanged)
                return Ok();
            else
                return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]LoginViewModel credentials)
        {
            if(authService.Authenticate(credentials) != null)
                return Unauthorized();

            userRepository.InsertUser(new User
            {
                Email = credentials.Email,
                Password = credentials.Password
            });

            return Ok();
        }

        /// <summary>
        /// Authorize user and generate a token 
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel credentials)
        {
            var jwt = authService.Authenticate(credentials);//check in the db
            if(jwt == null)
                return Unauthorized();
            return Ok(jwt);
        }

        [Authorize]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok();
        }
    }
}