using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class UserDashboardViewModel
    {
        public Listener listener { get; set; }
        public ClaimsPrincipal aspUser { get; set; }
        public string fullName { get; set; }
        public List<string> tracks { get; set; }
        public List<string> playlists { get; set; }
        public List<FullTrack> TopTracks { get; set; }
        public List<SimplePlaylist> FeatPlaylists { get; set; }
        public List<SimplePlaylist> UserPlaylists { get; set; }
    }
}
