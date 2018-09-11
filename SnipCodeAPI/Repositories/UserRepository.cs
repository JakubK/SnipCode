using System.Collections.Generic;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;

using System.Linq;

namespace SnipCodeAPI.Repositories
{
  public class UserRepository : IUserRepository
  {
    private IDataGateway DataGateway;
    public UserRepository(IDataGateway DataGateway) => this.DataGateway = DataGateway;
    public void DeleteUser(int id) => DataGateway.RemoveUser(id);
    public User GetUserById(int userId) => DataGateway.GetUserByID(userId);
    public IEnumerable<User> GetUsers() => DataGateway.GetAllUsers() ?? new List<User>();
    public void InsertUser(User user) => DataGateway.InsertUser(user);
    public bool UpdateUser(User user) => DataGateway.UpdateUser(user);
  }
}