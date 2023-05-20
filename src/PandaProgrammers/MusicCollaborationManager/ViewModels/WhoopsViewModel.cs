using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class WhoopsViewModel
    {
        public Listener listener { get; set; }
        public SpotifyAuthorizationNeededListener authNeededListener { get; set; }
    }
}