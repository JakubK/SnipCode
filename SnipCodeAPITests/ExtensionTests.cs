using NUnit.Framework;
using SnipCodeAPI.Extensions;
using SnipCodeAPI.Models;

namespace SnipCodeAPITests
{
  //Class with unit tests for extension methods
  [TestFixture]
  public class ExtensionTests
  {
    [Test]
    public void DecodeCredentials_ReturnsProperLoginViewModel_WhenValidBasicAuthStringIsGiven()
    {
      string testVal = "Basic QWRtaW46YWRtaW4=";
      LoginViewModel result = testVal.DecodeCredentials();
      Assert.IsTrue(result.Email == "Admin" && result.Password == "admin");
    }
  }
}