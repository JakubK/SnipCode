using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AspNetCore.Identity.LiteDB.Data;
using AspNetCore.Identity.LiteDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SnipCodeAPI.Models;
using SnipCodeAPI.Services.Interfaces;

namespace SnipCodeAPI.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {   
        private IAuthService AuthService;
        private IPasswordHasher<User> PasswordHasher;
        private IJWTService JWTService;
        public AuthController(IAuthService authService,
                             IPasswordHasher<User> passwordHasher,
                             IJWTService jwtService)
        {
            this.AuthService = authService;
            this.PasswordHasher = passwordHasher;
            this.JWTService = jwtService;

            System.Diagnostics.Debug.WriteLine("CLEAR");
        }


        [Authorize]
        [HttpPost("refresh")]
        [Route("refresh/{token}")]
        public IActionResult RefreshToken([FromHeader] string refreshToken)
        {           
            var token = JWTService.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            if(token != null)
            {
                var jwtToken = JWTService.Generate(token.Email);
                jwtToken.RefreshToken = refreshToken;
                return Ok(jwtToken);
            }
            return NotFound();
        }

        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var header = Request.Headers["Authorization"];

            if(!header.ToString().StartsWith("Basic"))
            {
                return BadRequest("Wrong Request");
            }

            var credValue = header.ToString().Substring("Basic ".Length).Trim();
            var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
            var usernameAndPass = usernameAndPassenc.Split(":");

            //check in the db

            var user = AuthService.Authenticate(new LoginViewModel
            {
                Email = usernameAndPass[0],
                Password = usernameAndPass[1]
            });

            if(user == null)
            {
                return BadRequest("Bad Credentials");
            }

            var jwtToken = JWTService.Generate(usernameAndPass[0]);

            var RefreshToken = PasswordHasher.HashPassword(user,new Guid().ToString())
            .Replace("+", string.Empty)
            .Replace("=", string.Empty)
            .Replace("/", string.Empty);

            jwtToken.RefreshToken = RefreshToken;

            JWTService.RefreshTokens.Add(new RefreshToken { Email = usernameAndPass[0], Token = RefreshToken});
            System.Diagnostics.Debug.WriteLine(JWTService.RefreshTokens.Count);
            return Ok(jwtToken);
        }
    }
}