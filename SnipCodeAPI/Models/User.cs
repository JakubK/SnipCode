using AspNetCore.Identity.LiteDB.Models;
using System;
using System.Collections.Generic;

namespace SnipCodeAPI.Models
{
    public class User : ApplicationUser
    {
        public int ID { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Snippet> Snippets { get; set; } = new List<Snippet>();
    }
}