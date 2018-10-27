using System.Collections.Generic;
using SnipCodeAPI.Models;

namespace SnipCodeAPI.Services.Interfaces
{
  public interface IJWTService
  {
      List<RefreshToken> RefreshTokens { get; set; }
      JsonWebToken Generate(string email);
  }
}