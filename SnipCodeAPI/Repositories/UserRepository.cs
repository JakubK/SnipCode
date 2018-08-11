using LiteDB;
using Microsoft.IdentityModel.Protocols;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnipCodeAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IRepository repository;
        public UserRepository(IRepository repoParam)
        {
            this.repository = repoParam;
        }
        public IEnumerable<User> GetUsers()
        {
            return repository.Database.Query<User>().Include(x => x.Snippets).ToList();
        }
        public User GetUserById(int userId)
        {
            return repository.Database.Query<User>().Include(x => x.Snippets).SingleById(userId);
        }
        public void InsertUser(User user)
        {
            repository.Database.Insert(user);
        }
        public void DeleteUser(int userId)
        {
            repository.Database.Delete<User>(userId);
        }
        public void UpdateUser(User user)
        {
            repository.Database.Update(user);
        }
    }
}
