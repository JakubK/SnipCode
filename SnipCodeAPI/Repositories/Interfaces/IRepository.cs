using LiteDB;

namespace SnipCodeAPI.Repositories.Interfaces
{
  public interface IRepository
  {
   LiteRepository Database {get;set;}
  }
}