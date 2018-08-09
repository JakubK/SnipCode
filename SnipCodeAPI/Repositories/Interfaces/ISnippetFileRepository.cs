using SnipCodeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnipCodeAPI.Repositories.Interfaces
{
    interface ISnippetFileRepository
    {
        IEnumerable<SnippetFile> GetSnippetFiles();
        SnippetFile GetSnippetFileById(int snippetFileId);
        void InsertSnippetFile(SnippetFile snippetFile);
        void DeleteSnippetFile(int snippetFileId);
        void UpdateSnippetFile(SnippetFile snippetFile);
    }
}
