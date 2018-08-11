using LiteDB;
using Microsoft.IdentityModel.Protocols;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SnipCodeAPI.Repositories
{
  public class SnippetRepository : ISnippetRepository
  {
    private IRepository repository;
    public SnippetRepository(IRepository repoParam)
    {
      this.repository = repoParam;
    }
    public void DeleteSnippet(int snippetId)
    {
      repository.Database.Delete<Snippet>(snippetId);
    }

    public Snippet GetSnippetById(int snippetId)
    {
      return repository.Database.Query<Snippet>().Include(x => x.Files).SingleById(snippetId);
    }

    public IEnumerable<Snippet> GetSnippets()
    {
      return repository.Database.Query<Snippet>()
            .Include(x => x.Files)
            .ToList();
    }

    public void InsertSnippet(Snippet snippet)
    {
      repository.Database.Insert(snippet);
    }

    public void UpdateSnippet(Snippet snippet)
    {
      repository.Database.Update(snippet);
    }
  }
}
