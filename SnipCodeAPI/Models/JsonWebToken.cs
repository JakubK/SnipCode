namespace SnipCodeAPI.Models
{
  public class JsonWebToken
  {
    public string RefreshToken {get;set;}
    public string AccessToken {get;set;}
    public long Expires {get;set;}
  }
}