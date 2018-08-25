
using LiteDB;
using System;
using System.Collections.Generic;

namespace SnipCodeAPI.Models
{
    public class Snippet
    {
        private List<SnippetFile> _files = new List<SnippetFile>();
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public User Creator {get;set;}
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public List<SnippetFile> Files { get => _files; set => _files = value; }
    }
}