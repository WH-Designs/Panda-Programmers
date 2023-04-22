using SpotifyAPI.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.ViewModels
{
    public class TrackInputViewModel
    {
        public List<FullTrack> seedTracks { get; set; } = new List<FullTrack>();
        public string trackID { get; set; }
        public string trackName { get; set; }
        public string artistName { get; set; }
    }
}
