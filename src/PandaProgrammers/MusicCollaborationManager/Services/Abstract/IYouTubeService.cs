using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Services.Abstract
{
    public interface IYouTubeService
    {
        public Task<IEnumerable<MusicVideoDTO>> GetPopularMusicVideosAsync();
    }
}
