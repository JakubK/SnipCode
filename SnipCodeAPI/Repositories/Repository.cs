using LiteDB;
using SnipCodeAPI.Repositories.Interfaces;

namespace SnipCodeAPI.Repositories
{
  public class Repository : IRepository
  {
    public LiteRepository Database
    {
      get { return repo; }
      set { repo = value; }
    }
    private LiteRepository repo;

    public Repository(string connectionString)
    {
      Database = new LiteRepository(connectionString);
    }
  }
}