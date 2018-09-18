using System.Collections.Generic;
using SnipCodeAPI.Repositories.Interfaces;
using System;
using SnipCodeAPI.Models;
using System.Linq;
using System.Threading.Tasks;


namespace SnipCodeAPI.Repositories
{
    public class SnippetRepository : ISnippetRepository
    {
        private IDataGateway DataGateway;
        public SnippetRepository(IDataGateway dataGateway) => DataGateway = dataGateway;
        public void DeleteSnippet(int snippetId) => DataGateway.RemoveSnippet(snippetId);
        public Snippet GetSnippetById(int snippetId) => DataGateway.GetSnippetById(snippetId);
        public IEnumerable<Snippet> GetSnippets() => DataGateway.GetAllSnippets() ?? new List<Snippet>();
        public void InsertSnippet(Snippet snippet) => DataGateway.InsertSnippet(snippet);
        public bool UpdateSnippet(Snippet snippet) => DataGateway.UpdateSnippet(snippet);
    }
}