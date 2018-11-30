using NUnit.Framework;
using SnipCodeAPI.Models;

namespace SnipCodeAPITests
{
  //Class with unit tests for extension & static methods
  [TestFixture]
  public class ExtensionTests
  {
    [Test]
    public void DecodeCredentials_ReturnsProperLoginViewModel_WhenValidBasicAuthStringIsGiven()
    {
      string testVal = "Basic QWRtaW46YWRtaW4=";
      LoginViewModel result = LoginViewModel.Decode(testVal);
      Assert.IsTrue(result.Email == "Admin" && result.Password == "admin");
    }
  }
}