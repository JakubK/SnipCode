using Microsoft.AspNetCore.Identity;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services.Interfaces;
using System;
using System.Linq;

namespace SnipCodeAPI.Services
{
  public class AuthService : IAuthService
  {
    private IUserRepository userRepository;
    private readonly IPasswordHasher<User> passwordHasher;
    private IJWTService jwtService;
    public AuthService(IUserRepository userRepositoryParam,
      IPasswordHasher<User> passwordHasherParam,
      ISeedService seedService,
      IJWTService jwtServiceParam)
    {
      this.userRepository = userRepositoryParam;
      this.jwtService = jwtServiceParam;
      this.passwordHasher = passwordHasherParam;

      seedService.Seed();
    }
    public JsonWebToken Authenticate(LoginViewModel login)
    {
      User user = userRepository.GetUsers().FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

      if(user == null)
        return null;

      var jwt = jwtService.Generate(user.Email);
      var refreshToken = passwordHasher.HashPassword(user, new Guid().ToString())
            .Replace("+", string.Empty)
            .Replace("=", string.Empty)
            .Replace("/", string.Empty);

      jwt.RefreshToken = refreshToken;
      jwtService.RefreshTokens.Add(new RefreshToken { Email = user.Email, Token = refreshToken });

      return jwt;
    }
  }
}