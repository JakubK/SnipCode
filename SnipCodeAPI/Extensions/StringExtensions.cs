using System;
using System.Text;
using SnipCodeAPI.Models;

namespace SnipCodeAPI.Extensions
{
  public static class StringExtensions
  {
    public static LoginViewModel DecodeCredentials(this string param)
    {
      var credValue = param.Substring("Basic ".Length).Trim();
      var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //login:password
      var usernameAndPass = usernameAndPassenc.Split(":");
      
      return new LoginViewModel{
        Email = usernameAndPass[0],
        Password = usernameAndPass[1]
      };
    }
  }
}