using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class GeneratorsViewModel
    {
        public List<FullTrack> fullResult { get; set; }
        public string PlaylistCoverImageUrl { get; set; }
        public string PlaylistDescription { get; set; }
        public string? PlaylistImgBase64 { get; set; } = null;
        public bool IsPlaylistPublicOnSpotify { get; set; }

        //https://stackoverflow.com/questions/35406457/load-an-image-from-url-as-base64-string -- Ronald Babu's answer.
        public static async Task<string> ImageUrlToBase64(string imageUrl)
        {
            using var httClient = new HttpClient();
            var imageBytes = await httClient.GetByteArrayAsync(imageUrl);
            return Convert.ToBase64String(imageBytes);
        }
        public string PlaylistTitle { get; set; }
    }
}
