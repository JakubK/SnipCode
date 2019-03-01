using LiteDB;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SnipCodeAPI.Models
{
    public class Snippet
    {
        [BsonId]
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Content {get;set;}
        public string CreatorEmail {get;set;}
        public string CreationTime { get; set; }
        public string LastModified {get;set;}
    }
}