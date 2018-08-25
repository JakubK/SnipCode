using Microsoft.AspNetCore.Mvc;
using SnipCodeAPI.Models;
using SnipCodeAPI.Repositories;
using SnipCodeAPI.Repositories.Interfaces;
using System;

namespace SnipCodeAPI.Controllers
{
    public class HomeController : Controller
    {
        private ISnippetRepository SnippetRepository;
        private ISnippetFileRepository SnippetFileRepository;
        private IUserRepository UserRepository;

        public HomeController(IUserRepository UserRepository, ISnippetRepository SnippetRepository, ISnippetFileRepository SnippetFileRepository)
        {
            this.UserRepository = UserRepository;
            this.SnippetRepository = SnippetRepository;
            this.SnippetFileRepository = SnippetFileRepository;

        }
        public IActionResult Users()
        {
            return View(UserRepository.GetUsers());
        }

        public IActionResult DeleteUser(int id)
        {
            UserRepository.DeleteUser(id);
            System.Diagnostics.Debug.WriteLine("DELETION COMPLETE");
            return Redirect("Users");
        }

        public IActionResult Snippets()
        {
            return View(SnippetRepository.GetSnippets());
        }
        public IActionResult SnippetFiles()
        {
            return View(SnippetFileRepository.GetSnippetFiles());
        }

        public IActionResult InsertSnippetFile()
        {
            SnippetFile snippetFile = new SnippetFile()
            {
                Name = "TestFile",
                Extension = "csharp",
                Code = "xxx",
                Snippet = SnippetRepository.GetSnippetById(3)
            };
            SnippetFileRepository.InsertSnippetFile(snippetFile);
            var snippet = SnippetRepository.GetSnippetById(3);
            snippet.Files.Add(snippetFile);
            SnippetRepository.UpdateSnippet(snippet);
            return Redirect("SnippetFiles");
        }

        public IActionResult InsertSnippet()
        {
            Snippet snippet = new Snippet()
            {
                Hash = "123",
                Name = "Test",
                Creator = UserRepository.GetUserById(2),
                CreationTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddDays(3)
            };
            SnippetRepository.InsertSnippet(snippet);
            var user = UserRepository.GetUserById(2);
            user.Snippets.Add(snippet);
            UserRepository.UpdateUser(user);
            return Redirect("Snippets");
        }
        public IActionResult InsertUser()
        {
            User user = new User()
            {
                Email = "test@test.test",
                CreationTime = DateTime.Now
            };
            UserRepository.InsertUser(user);
            return Redirect("Users");
        }
    }
}