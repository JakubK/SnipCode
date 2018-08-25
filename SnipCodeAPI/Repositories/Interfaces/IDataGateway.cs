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

     List<Snippet> Snippets {get;set;}
     List<SnippetFile> SnippetFiles {get;set;}
  }
}