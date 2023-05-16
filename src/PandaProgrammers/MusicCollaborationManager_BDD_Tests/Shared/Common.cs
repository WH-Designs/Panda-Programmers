﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.Shared
{
    // Sitewide definitions and useful methods
    public class Common
    {
        public const string BaseUrl = "http://127.0.0.1:5000";     // copied from launchSettings.json
  /*      public const string BaseUrl = "https://localhost:5000"; */            //Just in case the one above doesn't work.


        // File to store browser cookies in
        public const string CookieFile = "..\\..\\..\\..\\..\\..\\..\\MCMCookies.txt";

        // Page names that everyone should use
        // A handy way to look these up
        public static readonly Dictionary<string, string> Paths = new()
        {
            {"Home" , "/"},
            {"Login", "/Identity/Account/Login"},
            {"GeneratorIndex", "/Generator" },
            {"QGenerator", "/Generator/Mood" },
            {"Settings", "/Listener/Settings"},
            {"Search", "/Search/Search"},
            {"TopArtist", "/Generator/TopArtist"},
            {"RelatedGenerator", "/Generator/RelatedArtists" },
            {"FAQ", "/Generator/FAQ"},
            {"EmailConfirmation", "/Identity/Account/ResendEmailConfirmation" },
            {"ResetPassword", "/Identity/Account/ResetPassword" }
        };

        public static string PathFor(string pathName) => Paths[pathName];
        public static string UrlFor(string pathName) => BaseUrl + Paths[pathName];
    }
}
