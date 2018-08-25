using LiteDB;
using System;
using System.Collections.Generic;

namespace SnipCodeAPI.Models
{
    public class User
    {
        private List<Snippet> _snippets = new List<Snippet>();
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Snippet> Snippets { get => _snippets; set => _snippets = value; }
    }
}