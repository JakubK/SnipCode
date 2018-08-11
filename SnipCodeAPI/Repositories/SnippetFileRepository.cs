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
        private IRepository repository;
        public SnippetFileRepository(IRepository repoParam)
        {
            this.repository = repoParam;
        }
        public void DeleteSnippetFile(int snippetFileId)
        {
            repository.Database.Delete<SnippetFile>(snippetFileId);
        }

        public SnippetFile GetSnippetFileById(int snippetFileId)
        {
            return repository.Database.Query<SnippetFile>().SingleById(snippetFileId);
        }

        public IEnumerable<SnippetFile> GetSnippetFiles()
        {
            return repository.Database.Query<SnippetFile>().ToList();
        }

        public void InsertSnippetFile(SnippetFile snippetFile)
        {
            repository.Database.Insert(snippetFile);
        }

        public void UpdateSnippetFile(SnippetFile snippetFile)
        {
            repository.Database.Update(snippetFile);
        }
    }
}
