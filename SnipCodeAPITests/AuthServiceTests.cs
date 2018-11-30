using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services;
using SnipCodeAPI.Services.Interfaces;

namespace SnipCodeAPITests
{
  [TestFixture]
  public class AuthServiceTests
  {
    [Test]
    public void Authenticate_WhenInvalidCredentialsGiven_ReturnsNull()
    {
      IUserRepository userRepositoryMock = Substitute.For<IUserRepository>();
      userRepositoryMock.GetUsers().Returns(new List<User>()
      {
        new User(){
          Email = "Admin",
          Password = "admin"
        }
      });

      ISeedService seedServiceMock = Substitute.For<ISeedService>();
      AuthService authService = new AuthService(userRepositoryMock,null,seedServiceMock,null);
      LoginViewModel model = new LoginViewModel();
      model.Email = "wrong@email.com";
      model.Password = "wrongpassword";
      
      JsonWebToken result = authService.Authenticate(model);

      Assert.IsTrue(result == null);
    }

    [Test]
    public void Authenticate_WhenValidCredentialsGiven_ReturnsJsonWebToken()
    {
      IUserRepository userRepositoryMock = Substitute.For<IUserRepository>();
      userRepositoryMock.GetUsers().Returns(new List<User>()
      {
        new User(){
          Email = "Admin",
          Password = "admin"
        }
      });

      IPasswordHasher<User> passwordHasherMock = Substitute.For<IPasswordHasher<User>>();
      ISeedService seedServiceMock = Substitute.For<ISeedService>();
      IJWTService jwtServiceMock = Substitute.For<IJWTService>();
      jwtServiceMock.Generate(Arg.Any<string>()).Returns(new JsonWebToken());
      jwtServiceMock.RefreshTokens.Returns(new List<RefreshToken>());

      AuthService authService = new AuthService(userRepositoryMock,passwordHasherMock,seedServiceMock,jwtServiceMock);
      LoginViewModel model = new LoginViewModel();
      model.Email = "Admin";
      model.Password = "admin";

      JsonWebToken result = authService.Authenticate(model);

      Assert.IsTrue(result != null);
    }
  }
}