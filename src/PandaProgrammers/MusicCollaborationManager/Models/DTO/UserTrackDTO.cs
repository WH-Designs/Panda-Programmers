namespace MusicCollaborationManager.Models.DTO
{
    public class UserTrackDTO
    {
        public string LinkToTrack { get; set; }  //Intended for a Spotify link
        public string Title { get; set; }
        public string Artist { get; set; }
        public string? ImageURL { get; set; } = null;
        public string Uri { get; set; }

    }
}
