using Microsoft.AspNetCore.Mvc;

namespace SnipCodeAPI.Controllers
{
  [Route("home/index")]
  public class HomeController : Controller
  {    
    public IActionResult Index()
    {
      return View();
    }
  }
}