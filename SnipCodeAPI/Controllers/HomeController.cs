using Microsoft.AspNetCore.Mvc;

namespace SnipCodeAPI.Controllers
{
  public class HomeController : Controller
  {    
    public IActionResult Index()
    {
      return View();
    }
  }
}