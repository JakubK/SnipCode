using SnipCodeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnipCodeAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
    }
}
