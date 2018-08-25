using System.Collections.Generic;
using SnipCodeAPI.Repositories.Interfaces;
using System;
using SnipCodeAPI.Models;
using System.Linq;
using System.Threading.Tasks;


namespace SnipCodeAPI.Repositories
{
    public class SnippetFileRepository : ISnippetFileRepository
    {
        private IDataGateway DataGateway;
        public SnippetFileRepository(IDataGateway dataGateway) => DataGateway = dataGateway;
        public void DeleteSnippetFile(int snippetFileId) => DataGateway.RemoveSnippet(snippetFileId);
        public SnippetFile GetSnippetFileById(int snippetFileId) => DataGateway.GetSnippetFileById(snippetFileId);
        public IEnumerable<SnippetFile> GetSnippetFiles() => DataGateway.GetAllSnippetFiles();
        public void InsertSnippetFile(SnippetFile snippetFile) => DataGateway.InsertSnippetFile(snippetFile);
        public void UpdateSnippetFile(SnippetFile snippetFile) => DataGateway.UpdateSnippetFile(snippetFile);
    }
}