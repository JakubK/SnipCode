using Microsoft.AspNetCore.Identity;
using SnipCodeAPI.Models;
using SnipCodeAPI.Models.Requests;
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
      var token = jwtService.RefreshTokens.FirstOrDefault(x => x.Email == user.Email);
      if(token == null)
      {
        jwtService.RefreshTokens.Add(new RefreshToken { Email = user.Email, Token = refreshToken });
      }
      else
      {
        token.Token = refreshToken;
      }

      return jwt;
    }

    public bool TryChangePassword(string email, ChangePasswordRequest changePasswordRequest)
    {
      User user = userRepository.GetUsers().FirstOrDefault(x => x.Email == email);

      if(user == null)
        return false;

      if(user.Password == changePasswordRequest.OldPassword)
      {
        user.Password = changePasswordRequest.NewPassword;
        userRepository.UpdateUser(user);
        return true;
      }
      
      return false;
    }
  }
}