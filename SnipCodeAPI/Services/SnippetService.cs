using SnipCodeAPI.Models;
using SnipCodeAPI.Models.Requests;
using SnipCodeAPI.Repositories.Interfaces;
using SnipCodeAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HashidsNet;

namespace SnipCodeAPI.Services
{
    public class SnippetService : ISnippetService
    {
        private readonly ISnippetRepository _snippetRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateTime _dateTime;
        private readonly Hashids hashids;

        public SnippetService(IDateTime dateTime, ISnippetRepository snippetRepository, IUserRepository userRepository)
        {
            _dateTime = dateTime;
            _snippetRepository = snippetRepository;
            _userRepository = userRepository;

            hashids = new Hashids("randomSalt",8);
        }

        public Snippet Create(CreateSnippetRequest request)
        {
            Snippet snippet = new Snippet();

            int snippetCount = _snippetRepository.GetSnippets().Count();

            snippet.Name = request.Name;
            snippet.CreatorEmail = request.CreatorEmail;
            snippet.Hash = GenerateHash(snippetCount);
            snippet.Content = request.Content;
            snippet.CreationTime = _dateTime.Now.ToString("g");
            snippet.LastModified = snippet.CreationTime;

            _snippetRepository.InsertSnippet(snippet);

            return snippet;
        }

        public List<Snippet> GetSnippets() => _snippetRepository.GetSnippets().ToList();

        public Snippet GetSnippetByHash(string hash) => _snippetRepository.GetSnippetByHash(hash);

        public bool DeleteSnippet(string hash)
        {
            var snippet = _snippetRepository.GetSnippetByHash(hash);
            if (snippet == null)
                return false;

            //foreach user check if he has this snippet shared
            var users = _userRepository.GetUsers();
            foreach(var user in users)
            {
                if(user.SharedSnippets.Any(x => x.Hash == hash))
                {
                    user.SharedSnippets.RemoveAll(x => x.Hash == hash);
                    System.Diagnostics.Debug.WriteLine("REMOVED " + hash);
                    _userRepository.UpdateUser(user);
                }
            }
    
            _snippetRepository.DeleteSnippet(snippet.Id);

            return true;
        }

        public List<Snippet> GetUserSnippets(string userEmail)
        {
            List<Snippet> result = new List<Snippet>();
            List<Snippet> allSnippets = GetSnippets();
            foreach(var snippet in allSnippets)
            {
                if(snippet.CreatorEmail == userEmail)
                {
                    result.Add(snippet);
                }
            }

            return result;
        }

        public List<Snippet> GetSharedUserSnippets(string userEmail)
        {
            return _userRepository.GetUserByEmail(userEmail).SharedSnippets;
        }

        public bool UpdateSnippet(string hash, UpdateSnippetRequest updateSnippetRequest)
        {
           Snippet snippet = _snippetRepository.GetSnippetByHash(hash);
           if(snippet != null)
           {
            snippet.Content = updateSnippetRequest.NewContent;
            snippet.LastModified = _dateTime.Now.ToString("g");
            _snippetRepository.UpdateSnippet(snippet);
            return true;
           }
           
           return false;
        }

        private string GenerateHash(int id)
        {
            return hashids.Encode(id);
        }
  }
}