using SnipCodeAPI.Models;
using System.Collections.Generic;

namespace SnipCodeAPI.Repositories.Interfaces
{
    public interface ISnippetRepository
    {
        IEnumerable<Snippet> GetSnippets();
        Snippet GetSnippetById(int snippetId);
        void InsertSnippet(Snippet snippet);
        void DeleteSnippet(int snippetId);
        bool UpdateSnippet(Snippet snippet);
    }
}
