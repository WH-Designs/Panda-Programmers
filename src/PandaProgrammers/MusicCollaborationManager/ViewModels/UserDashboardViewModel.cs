using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.ViewModels
{
    public class UserDashboardViewModel
    {
        public Listener listener { get; set; }
        public ClaimsPrincipal aspUser { get; set; }
        public string fullName { get; set; }
        public List<string> tracks { get; set; }
        public List<string> playlists { get; set; }
    }
}
