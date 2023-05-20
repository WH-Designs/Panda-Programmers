using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using SpotifyAPI.Web;
using System.Diagnostics;

namespace MusicCollaborationManager.ViewModels
{
    public class GeneratorsViewModel
    {
        public List<FullTrack> fullResult { get; set; }
        public string PlaylistCoverImageUrl { get; set; }
        public string PlaylistDescription { get; set; }
        public string? PlaylistImgBase64 { get; set; } = null;

        //https://stackoverflow.com/questions/35406457/load-an-image-from-url-as-base64-string -- Ronald Babu's answer.
        public static async Task<string> ImageUrlToBase64(string imageUrl)
        {
            using var httClient = new HttpClient();
            var imageBytes = await httClient.GetByteArrayAsync(imageUrl);
            return Convert.ToBase64String(imageBytes);
        }
        public string PlaylistTitle { get; set; }

        public static string EnsurePlaylistDescriptionSize(string playlistDescription) 
        {
            if (playlistDescription.IsNullOrEmpty()) 
            {
                return null;
            }

            else if (playlistDescription.Length > 300)
            {
                string newPlaylistDescription = playlistDescription;

                newPlaylistDescription =  newPlaylistDescription.Substring(0, 300);
                int lastPeriodIndex = newPlaylistDescription.LastIndexOf('.');

                if(lastPeriodIndex == newPlaylistDescription.Length -1)
                {
                    return newPlaylistDescription;
                }
                else 
                {
                    if(lastPeriodIndex < 1) 
                    {
                        return null;
                    }

                    newPlaylistDescription = newPlaylistDescription.Substring(0, lastPeriodIndex + 1);
                    Debug.WriteLine($"Length of playlist description: \n{newPlaylistDescription.Length}\n Description: \n {newPlaylistDescription}");
                    return newPlaylistDescription;
                }
            }

            Debug.WriteLine($"No description trimming needed. Description char count: {playlistDescription.Length}");
            return playlistDescription;
        }

        //public string GetUserInputPhrase(string userDescriptionInput)
        //{
        //    userDescriptionInput.
        //}
    }
}
