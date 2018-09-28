using AspNetCore.Identity.LiteDB.Models;
using LiteDB;
using System;
using System.Collections.Generic;

namespace SnipCodeAPI.Models
{
    public class User : ApplicationUser
    {
        private List<Snippet> _snippets = new List<Snippet>();
        public int ID { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Snippet> Snippets { get => _snippets; set => _snippets = value; }
    }
}