using SpotifyAPI.Web;
using MusicCollaborationManager.Services.Concrete;

namespace MusicCollaborationManager.Models.DTO
{

    public class UserProfileDTO
    {
        private readonly SpotifyClientBuilder _spotifyClientBuilder;
        public UserProfileDTO(SpotifyClientBuilder spotifyClientBuilder)
        {
            _spotifyClientBuilder = spotifyClientBuilder;
        }

        public PrivateUser Me { get; set; }

        public async Task OnGet()
        {
            var spotify = await _spotifyClientBuilder.BuildClient();

            Me = await spotify.UserProfile.Current();
        }
    }
}