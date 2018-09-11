using System.Collections.Generic;
using LiteDB;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using System.Linq;

namespace SnipCodeAPI.Repositories
{
    public class LiteRepositoryDataMapper : IDataGateway
    {
        #region constructor and fields
        private LiteRepository LiteRepository;
        public LiteRepositoryDataMapper(string connectionString) => LiteRepository = new LiteRepository(connectionString);
        #endregion
        #region User
        public List<User> GetAllUsers() => LiteRepository.Query<User>().ToList();
        public User GetUserByID(int id) => LiteRepository.Query<User>().Where(x => x.Id == id).FirstOrDefault();
        public void RemoveUser(int id) => LiteRepository.Delete<User>(x => x.Id == id);
        public void InsertUser(User user) => LiteRepository.Insert<User>(user);
        public bool UpdateUser(User user) => LiteRepository.Update<User>(user);
        #endregion
        #region Snippet
        public List<Snippet> GetAllSnippets() => LiteRepository.Query<Snippet>().ToList();
        public Snippet GetSnippetById(int id) => LiteRepository.Query<Snippet>().Where(x => x.Id == id).SingleOrDefault();
        public void RemoveSnippet(int id) => LiteRepository.Delete<Snippet>(x => x.Id == id);
        public void InsertSnippet(Snippet snippet) => LiteRepository.Insert<Snippet>(snippet);
        public bool UpdateSnippet(Snippet snippet) => LiteRepository.Update<Snippet>(snippet);
        #endregion
        #region SnippetFiles
        public List<SnippetFile> GetAllSnippetFiles() => LiteRepository.Query<SnippetFile>().ToList();
        public SnippetFile GetSnippetFileById(int id) => LiteRepository.Query<SnippetFile>().Where(x => x.Id == id).SingleOrDefault();
        public void RemoveSnippetFile(int id) => LiteRepository.Delete<SnippetFile>(x => x.Id == id);
        public void InsertSnippetFile(SnippetFile snippetFile) => LiteRepository.Insert<SnippetFile>(snippetFile);
        public bool UpdateSnippetFile(SnippetFile snippetFile) => LiteRepository.Update<SnippetFile>(snippetFile);
        #endregion
    }
}