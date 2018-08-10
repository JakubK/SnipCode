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
        public void DeleteSnippet(int snippetId)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Delete<Snippet>(snippetId);
            }
        }

        public Snippet GetSnippetById(int snippetId)
        {
            using (var db = new LiteRepository("database.db"))
            {
                return db.Query<Snippet>().Include(x => x.Files)
                    .SingleById(snippetId);
            }
        }

        public IEnumerable<Snippet> GetSnippets()
        {
            using (var db = new LiteRepository("database.db"))
            {
                return db.Query<Snippet>()
                    .Include(x => x.Files)
                    .ToList();
            }
        }

        public void InsertSnippet(Snippet snippet)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Insert(snippet);
            }
        }

        public void UpdateSnippet(Snippet snippet)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Update(snippet);
            }
        }
    }
}
