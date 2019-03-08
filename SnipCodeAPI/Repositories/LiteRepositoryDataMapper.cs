using LiteDB;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using System.Collections.Generic;

namespace SnipCodeAPI.Repositories
{
    public class LiteRepositoryDataMapper : IDataGateway
    {
        #region constructor and fields
        private readonly LiteRepository _liteRepository;
        public LiteRepositoryDataMapper(string connectionString) => _liteRepository = new LiteRepository(connectionString);
        #endregion
        #region User
        public List<User> GetAllUsers() => _liteRepository.Query<User>().ToList();
        public User GetUserByEmail(string email) => _liteRepository.Query<User>().Where(x => x.Email == email).FirstOrDefault();
        public void RemoveUser(int id) => _liteRepository.Delete<User>(x => x.ID == id);
        public void InsertUser(User user) => _liteRepository.Insert<User>(user);
        public bool UpdateUser(User user) => _liteRepository.Update<User>(user);
        #endregion
        #region Snippet
        public List<Snippet> GetAllSnippets() => _liteRepository.Query<Snippet>().ToList();
        public Snippet GetSnippetByHash(string hash) => _liteRepository.Query<Snippet>().Where(x => x.Hash == hash).FirstOrDefault();
        public void RemoveSnippet(int id) => _liteRepository.Delete<Snippet>(x => x.Id == id);
        public void InsertSnippet(Snippet snippet) => _liteRepository.Insert<Snippet>(snippet);
        public bool UpdateSnippet(Snippet snippet) => _liteRepository.Update<Snippet>(snippet);

    
    #endregion
  }
}