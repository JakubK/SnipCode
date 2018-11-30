using System;
using System.Text;

namespace SnipCodeAPI.Models
{
  public class LoginViewModel
  {
    public string Email {get;set;}
    public string Password {get;set;}

    public static LoginViewModel Decode(string authString)
    {
      var credValue = authString.Substring("Basic ".Length).Trim();
      var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //login:password
      var usernameAndPass = usernameAndPassenc.Split(":");
      
      return new LoginViewModel{
        Email = usernameAndPass[0],
        Password = usernameAndPass[1]
      };
    }
  }
}