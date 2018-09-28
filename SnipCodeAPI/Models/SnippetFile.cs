using LiteDB;

namespace SnipCodeAPI.Models
{
    public class SnippetFile
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Code { get; set; }
    }
}