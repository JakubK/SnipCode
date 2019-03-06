using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SnipCodeAPI.Models;
using SnipCodeAPI.Services.Interfaces;

namespace SnipCodeAPI.Services
{
  public class JWTService : IJWTService
  {
    public List<RefreshToken> RefreshTokens { get; set; }
    private IConfiguration configuration;
    public JWTService(IConfiguration configurationParam)
    {
      RefreshTokens = new List<RefreshToken>();
      this.configuration = configurationParam;
    }
    public JsonWebToken Generate(string email)
    {
      var claims = new [] { new Claim(ClaimTypes.Email, email)};

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Keys")["Jwt"]));
      var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

      var token = new JwtSecurityToken(issuer: "mysite.com",
        audience: "mysite.com",
        expires: DateTime.Now.AddMinutes(5),
        claims: claims,
        signingCredentials: signInCredentials);

      JsonWebToken jwtToken = new JsonWebToken();
      jwtToken.Expires = DateTime.Now.AddMinutes(5).Ticks;
      jwtToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

      return jwtToken;
    }
  }
}