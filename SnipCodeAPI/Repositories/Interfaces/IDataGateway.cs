using SnipCodeAPI.Models;
using System.Collections.Generic;

namespace SnipCodeAPI.Repositories.Interfaces
{
    public interface IDataGateway
    {
        List<User> GetAllUsers();
        User GetUserByEmail(string email);

        void RemoveUser(int id);
        void InsertUser(User user);
        bool UpdateUser(User user);

        List<Snippet> GetAllSnippets();
        Snippet GetSnippetByHash(string hash);
        void RemoveSnippet(int id);
        void InsertSnippet(Snippet snippet);
        bool UpdateSnippet(Snippet snippet);
    }
}