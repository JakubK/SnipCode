namespace SnipCodeAPI.Models.Requests
{
  public class CreateSnippetRequest
  {
    public string Name { get; set; }
    public string CreatorEmail {get;set;}
    public string Content {get;set;}
  }
}