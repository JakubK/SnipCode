using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories;
using SnipCodeAPI.Repositories.Interfaces;

namespace SnipCodeAPI.Controllers
{
  public class HomeController : Controller
  {   
    private ISnippetRepository snippetRepository; 
    private ISnippetFileRepository snippetFileRepository; 

    private IUserRepository UserRepository;

    public HomeController(IUserRepository userRepository)
    {
      this.UserRepository = userRepository;

    }
    public IActionResult Users()
    {
      return View(UserRepository.GetUsers());
    }

    public IActionResult DeleteUser()
    {
      UserRepository.DeleteUser(1);
      System.Diagnostics.Debug.WriteLine("DELETION COMPLETE");
      return Redirect("Users");
    }

    public IActionResult Snippets()
    {
      return View(snippetRepository.GetSnippets());
    }
    public IActionResult SnippetFiles()
    {
      return View(snippetFileRepository.GetSnippetFiles());
    }
  }
}