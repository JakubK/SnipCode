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
        public void DeleteSnippetFile(int snippetFileId) => DataGateway.RemoveSnippetFile(snippetFileId);
        public SnippetFile GetSnippetFileById(int snippetFileId) => DataGateway.GetSnippetFileById(snippetFileId);
        public IEnumerable<SnippetFile> GetSnippetFiles() => DataGateway.GetAllSnippetFiles() ?? new List<SnippetFile>();
        public void InsertSnippetFile(SnippetFile snippetFile) => DataGateway.InsertSnippetFile(snippetFile);
        public bool UpdateSnippetFile(SnippetFile snippetFile) => DataGateway.UpdateSnippetFile(snippetFile);
    }
}