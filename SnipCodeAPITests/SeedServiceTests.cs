using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System;
using SnipCodeAPI.Services;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Models;

namespace SnipCodeAPITests
{
  [TestFixture]
  public class SeedServiceTests
  {
    [Test]
    public void Seed_WhenCalled_ClearsDBAndCreatesAdminAccount()
    {
      IUserRepository userRepository = Substitute.For<IUserRepository>();
      userRepository.GetUsers().Returns(new List<User>
      {
        new User{
          Email = "example@example.com"
        }, new User{
          Email = "test@test.com"
        }
      });

      SeedService seedService = new SeedService(userRepository);
      seedService.Seed();
      userRepository.Received().DeleteUser(Arg.Any<int>());
      userRepository.Received().InsertUser(Arg.Is<User>(x => x.Email == "Admin" && x.Password == "admin"));
    }
  }
}