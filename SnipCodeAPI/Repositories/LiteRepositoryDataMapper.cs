using System.Collections.Generic;
using LiteDB;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using System.Linq;

namespace SnipCodeAPI.Repositories
{
  public class LiteRepositoryDataMapper : IDataGateway
  {
    private LiteRepository LiteRepository;

    public LiteRepositoryDataMapper(string connectionString)
    {
      LiteRepository = new LiteRepository(connectionString);
    }

    public List<User> GetAllUsers()
    {
      return LiteRepository.Query<User>().ToList();
    }

    public User GetUserByID(int id)
    {
      return LiteRepository.Query<User>().Where(x => x.Id == id).FirstOrDefault();
    }

    public void RemoveUser(int id)
    {
      LiteRepository.Delete<User>(x => x.Id == id);
    }

    public void InsertUser(User user)
    {
      LiteRepository.Insert<User>(user);
    }

    public void UpdateUser(User user)
    {
      LiteRepository.Update<User>(user);
    }

    public List<Snippet> Snippets
    {
      get
      {
        return LiteRepository.Query<Snippet>().ToList();
      }
      set
      {
        LiteRepository.Update(value);
      }
    } 

    public List<SnippetFile> SnippetFiles
    {
      get
      {
        return LiteRepository.Query<SnippetFile>().ToList();
      }
      set
      {
        LiteRepository.Update(value);
      }
    }


  }
}