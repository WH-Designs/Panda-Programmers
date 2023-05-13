using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.ViewModels
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel() 
        {
        }
        public int NumPlaylistFollowers { get; set; }
        public FullPlaylistDTO PlaylistContents = new FullPlaylistDTO();
        public string MCMUsername { get; set; }
    }
}
