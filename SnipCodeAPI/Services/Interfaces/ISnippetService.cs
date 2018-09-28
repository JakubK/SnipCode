using SnipCodeAPI.Models;
using System.Collections.Generic;

namespace SnipCodeAPI.Services.Interfaces
{
    public interface ISnippetService
    {
        void Create(Snippet snippet);
        List<Snippet> GetSnippets();
        Snippet GetSnippetById(int id, out Snippet snippet);
        bool DeleteSnippet(int id);
        bool UpdateSnippet(int id, Snippet snippet);
    }
}
