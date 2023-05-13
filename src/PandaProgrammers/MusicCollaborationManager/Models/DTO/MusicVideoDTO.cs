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
            JObject? jObject = null;
            try
            {
                jObject = JObject.Parse((string)obj);
            }
            catch (JsonReaderException)
            {
                Debug.WriteLine("Error parsing JSON. (MusicVideoDTO)");
            }
            if (jObject != null)
            {
                IEnumerable<MusicVideoDTO> VideoList = jObject["items"].Select(mv => new MusicVideoDTO()
                {
                    VideoID = (string)mv["id"],
                    VideoTitle = (string)mv["snippet"]["title"],
                    YouTubeChannelName = (string)mv["snippet"]["channelTitle"],
                    ThumbnailURL = (string)mv["snippet"]["thumbnails"]["default"]["url"],
                    ThumbnailWidth = (string)mv["snippet"]["thumbnails"]["default"]["width"],
                    ThumbnailHeight = (string)mv["snippet"]["thumbnails"]["default"]["height"]
                });
                return VideoList;
            }

            return Enumerable.Empty<MusicVideoDTO>();
        }
    }
}
