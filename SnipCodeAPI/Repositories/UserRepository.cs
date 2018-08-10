using LiteDB;
using Microsoft.IdentityModel.Protocols;
using SnipCodeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnipCodeAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers()
        {
            using (var db = new LiteRepository("database.db"))
            {
                return db.Query<User>().Include(x => x.Snippets).ToList();
            }
        }
        public User GetUserById(int userId)
        {
            using (var db = new LiteRepository("database.db"))
            {
                return db.Query<User>().Include(x => x.Snippets)
                    .SingleById(userId);
            }
        }
        public void InsertUser(User user)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Insert(user);
            }
        }
        public void DeleteUser(int userId)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Delete<User>(userId);
            }
        }
        public void UpdateUser(User user)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Update(user);
            }
        }
    }
}
