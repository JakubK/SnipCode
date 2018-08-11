using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories.Interfaces;

namespace SnipCodeAPI.Controllers
{
  public class HomeController : Controller
  {   
    private IRepository repository; 
    public HomeController(IRepository repo)
    {
      this.repository = repo;
    }
    public IActionResult Users()
    {
      return View(repository.Database.Query<User>().ToList());
    }
    public IActionResult Snippets()
    {
      return View(repository.Database.Query<Snippet>().ToList());
    }
    public IActionResult SnippetFiles()
    {
      return View(repository.Database.Query<SnippetFile>().ToList());
    }
  }
}