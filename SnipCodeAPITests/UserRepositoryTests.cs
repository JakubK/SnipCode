using NUnit.Framework;
using NSubstitute;
using SnipCodeAPI.Repositories;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SnipCodeAPITests
{
    [TestFixture]
    public class UserRepositoryTests
    {
      [Test]
      public void GetUsers_WhenNoUsers_ReturnsEmptyCollection()
      {
        IDataGateway gateway = Substitute.For<IDataGateway>();

        UserRepository repository = new UserRepository(gateway);
        var users = repository.GetUsers();

        Assert.AreEqual(new List<User>(),users);
      }

      [Test]
      public void GetUsers_WhenUsersAreAvailable_ReturnsUsers()
      {
        IDataGateway gateway = Substitute.For<IDataGateway>();
        gateway.GetAllUsers().Returns(new List<User>{
          new User()
        });

        UserRepository repository = new UserRepository(gateway);
        var users = repository.GetUsers();
        Assert.AreEqual(1,users.Count());
      }
    }
}