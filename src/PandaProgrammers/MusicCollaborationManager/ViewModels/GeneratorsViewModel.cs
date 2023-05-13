using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class GeneratorsViewModel
    {
        public List<FullTrack> fullResult { get; set; }
        public string PlaylistCoverImageUrl { get; set; }
        public string PlaylistDescription { get; set; }
        public string PlaylistTitle { get; set; }
    }
}
