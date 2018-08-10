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
    public class SnippetFileRepository : ISnippetFileRepository
    {
        public void DeleteSnippetFile(int snippetFileId)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Delete<SnippetFile>(snippetFileId);
            }
        }

        public SnippetFile GetSnippetFileById(int snippetFileId)
        {
            using (var db = new LiteRepository("database.db"))
            {
                return db.Query<SnippetFile>().SingleById(snippetFileId);
            }
        }

        public IEnumerable<SnippetFile> GetSnippetFiles()
        {
            using (var db = new LiteRepository("database.db"))
            {
                return db.Query<SnippetFile>().ToList();
            }
        }

        public void InsertSnippetFile(SnippetFile snippetFile)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Insert(snippetFile);
            }
        }

        public void UpdateSnippetFile(SnippetFile snippetFile)
        {
            using (var db = new LiteRepository("database.db"))
            {
                db.Update(snippetFile);
            }
        }
    }
}
