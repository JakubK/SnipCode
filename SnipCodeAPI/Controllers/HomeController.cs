using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories;
using SnipCodeAPI.Repositories.Interfaces;

namespace SnipCodeAPI.Controllers
{
  public class HomeController : Controller
  {   
    private IUserRepository userRepository;
    private ISnippetRepository snippetRepository; 
    private ISnippetFileRepository snippetFileRepository; 

    public HomeController(IUserRepository userRepo, ISnippetRepository snippetRepo, ISnippetFileRepository snippetFileRepo)
    {
      this.userRepository = userRepo;
      this.snippetRepository = snippetRepo;
      this.snippetFileRepository = snippetFileRepo;
    }
    public IActionResult Users()
    {
      return View(userRepository.GetUsers());
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