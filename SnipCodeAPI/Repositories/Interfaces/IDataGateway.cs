using System.Collections.Generic;
using SnipCodeAPI.Models;

namespace SnipCodeAPI.Repositories.Interfaces
{
    public interface IDataGateway
    {
        List<User> GetAllUsers();
        User GetUserByID(int id);
        void RemoveUser(int id);
        void InsertUser(User user);
        void UpdateUser(User user);

        List<Snippet> GetAllSnippets();
        Snippet GetSnippetById(int id);
        void RemoveSnippet(int id);
        void InsertSnippet(Snippet snippet);
        void UpdateSnippet(Snippet snippet);

        List<SnippetFile> GetAllSnippetFiles();
        SnippetFile GetSnippetFileById(int id);
        void RemoveSnippetFile(int id);
        void InsertSnippetFile(SnippetFile snippet);
        void UpdateSnippetFile(SnippetFile snippet);
    }
}