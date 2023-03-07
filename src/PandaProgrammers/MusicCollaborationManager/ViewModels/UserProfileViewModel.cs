using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MusicCollaborationManager.Models;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class UserProfileViewModel
    {
        public Listener listener { get; set; }
        public ClaimsPrincipal aspUser { get; set; }
        public string fullName { get; set; }
        public string spotifyName { get; set; }
        public string accountType { get; set; }
        public string country { get; set; }
        public int followerCount { get; set; } 
        public string profilePic { get; set; }

    }
}