using LiteDB;
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
        public void DeleteSnippet(int snippetId)
        {
            using (var db = new LiteRepository(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                db.Delete<Snippet>(snippetId);
            }
        }

        public Snippet GetSnippetById(int snippetId)
        {
            using (var db = new LiteRepository(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                return db.Query<Snippet>().Include(x => x.Files)
                    .SingleById(snippetId);
            }
        }

        public IEnumerable<Snippet> GetSnippets()
        {
            using (var db = new LiteRepository(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                return db.Query<Snippet>()
                    .Include(x => x.Files)
                    .ToList();
            }
        }

        public void InsertSnippet(Snippet snippet)
        {
            using (var db = new LiteRepository(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                db.Insert(snippet);
            }
        }

        public void UpdateSnippet(Snippet snippet)
        {
            using (var db = new LiteRepository(ConfigurationManager.ConnectionStrings["LiteDB"].ConnectionString))
            {
                db.Update(snippet);
            }
        }
    }
}
