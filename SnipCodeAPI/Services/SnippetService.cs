using SnipCodeAPI.Models;
using SnipCodeAPI.Models.Requests;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SnipCodeAPI.Services
{
    public class SnippetService : ISnippetService
    {
        private readonly ISnippetRepository _snippetRepository;
        private readonly IDateTime _dateTime;

        public SnippetService(IDateTime dateTime, ISnippetRepository snippetRepository)
        {
            _dateTime = dateTime;
            _snippetRepository = snippetRepository;
        }

        public Snippet Create(CreateSnippetRequest request)
        {
            Snippet snippet = new Snippet();

            int snippetCount = _snippetRepository.GetSnippets().Count();

            snippet.Name = request.Name;
            snippet.CreatorEmail = request.CreatorEmail;
            snippet.Hash = GenerateHash(request.CreatorEmail + (snippetCount + 1));
            snippet.Content = request.Content;
            snippet.CreationTime = _dateTime.Now.ToString("g");
            snippet.ExpirationTime = _dateTime.Now.AddMinutes(10).ToString("g");

            _snippetRepository.InsertSnippet(snippet);

            return snippet;
        }

        public List<Snippet> GetSnippets() => _snippetRepository.GetSnippets().ToList();

        public Snippet GetSnippetByHash(string hash, out Snippet snippet) => snippet = _snippetRepository.GetSnippetByHash(hash);

        public bool DeleteSnippet(string hash)
        {
            var snippet = _snippetRepository.GetSnippetByHash(hash);
            if (snippet == null)
                return false;
    
            _snippetRepository.DeleteSnippet(snippet.Id);
            return true;
        }

        public bool UpdateSnippet(string hash, Snippet snippet) =>
            _snippetRepository.GetSnippetByHash(hash) != null && _snippetRepository.UpdateSnippet(snippet);

        private static string GenerateHash(string text)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(text);
            var hash = md5.ComputeHash(inputBytes);
            var stringBuilder = new StringBuilder();
            foreach (var value in hash)
            {
                stringBuilder.Append(value.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
