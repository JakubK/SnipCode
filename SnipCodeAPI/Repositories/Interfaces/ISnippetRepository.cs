using SnipCodeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnipCodeAPI.Repositories.Interfaces
{
    interface ISnippetRepository
    {
        IEnumerable<Snippet> GetSnippets();
        Snippet GetSnippetById(int snippetId);
        void InsertSnippet(Snippet snippet);
        void DeleteSnippet(int snippetId);
        void UpdateSnippet(Snippet snippet);
    }
}
