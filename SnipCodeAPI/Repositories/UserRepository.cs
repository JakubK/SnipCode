using System.Collections.Generic;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;

using System.Linq;

namespace SnipCodeAPI.Repositories
{
  public class UserRepository : IUserRepository
  {
    IDataGateway DataGateway;
    public UserRepository(IDataGateway dataGateway)
    {
      this.DataGateway = dataGateway;
    }

    public void DeleteUser(int id)
    {
      DataGateway.RemoveUser(id);
    }

    public User GetUserById(int userId)
    {
      return DataGateway.GetUserByID(userId);
    }

    public IEnumerable<User> GetUsers()
    {
      return DataGateway.GetAllUsers();
    }

    public void InsertUser(User user)
    {
      DataGateway.InsertUser(user);
    }

    public void UpdateUser(User user)
    {
      DataGateway.UpdateUser(user);
    }
  }
}