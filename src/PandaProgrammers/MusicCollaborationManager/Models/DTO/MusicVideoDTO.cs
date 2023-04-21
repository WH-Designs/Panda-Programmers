using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MusicCollaborationManager.Models.DTO
{
    public class MusicVideoDTO
    {
        public string VideoID { get; set; }
        public string VideoTitle { get; set; }
        public string ThumbnailURL { get; set; }
        public string ThumbnailHeight { get; set; }
        public string ThumbnailWidth { get; set; }
        public string YouTubeChannelName { get; set; }

        public MusicVideoDTO()
        {
        }

        public static IEnumerable<MusicVideoDTO> FromJSON(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
