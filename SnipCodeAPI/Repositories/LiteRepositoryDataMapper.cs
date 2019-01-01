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
        public List<User> GetAllUsers() => _liteRepository.Query<User>().Include(x=>x.Snippets).ToList();
        public User GetUserById(int id) => _liteRepository.Query<User>().Include(x => x.Snippets).Where(x => x.ID == id).FirstOrDefault();
        public void RemoveUser(int id) => _liteRepository.Delete<User>(x => x.ID == id);
        public void InsertUser(User user) => _liteRepository.Insert<User>(user);
        public bool UpdateUser(User user) => _liteRepository.Update<User>(user);
        #endregion
        #region Snippet
        public List<Snippet> GetAllSnippets() => _liteRepository.Query<Snippet>().ToList();
        public Snippet GetSnippetByHash(string hash) => _liteRepository.Query<Snippet>().Where(x => x.Hash == hash).SingleOrDefault();
        public void RemoveSnippet(int id) => _liteRepository.Delete<Snippet>(x => x.Id == id);
        public void InsertSnippet(Snippet snippet) => _liteRepository.Insert<Snippet>(snippet);
        public bool UpdateSnippet(Snippet snippet) => _liteRepository.Update<Snippet>(snippet);
        #endregion
    }
}