﻿using SnipCodeAPI.Models;
using SnipCodeAPI.Models.Requests;
using System.Collections.Generic;

namespace SnipCodeAPI.Services.Interfaces
{
    public interface ISnippetService
    {
        Snippet Create(CreateSnippetRequest snippet);
        List<Snippet> GetSnippets();
        Snippet GetSnippetByHash(string hash);
        bool DeleteSnippet(string hash);
        bool UpdateSnippet(string hash, UpdateSnippetRequest updateSnippetRequest);
        List<Snippet> GetUserSnippets(string userEmail);
        List<Snippet> GetSharedUserSnippets(string userEmail);
    }
}
