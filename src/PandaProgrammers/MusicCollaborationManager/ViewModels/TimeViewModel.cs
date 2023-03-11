using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.ViewModels
{
    public class TimeViewModel
    {
        public string timeCategory { get; set; }

        public List<string> bedGenres = new List<string> { "ambient", "study", "chill" };

        public List<string> workGenres = new List<string> { "classical", "chill", "jazz", "ambient", "study", "piano", "soundtracks" };

        public List<string> partyGenres = new List<string> { "salsa", "tango", "dance", "disco", "hip-hop", "dancehall", "club", "party", "reggaeton", "pop" };

        public List<string> exerciseGenres = new List<string> { "work-out", "rock-n-roll", "pop", "hip-hop", "metal", "brazil", "anime", "dubstep", "electronic", "heavy-metal", "techno" };

        public List<string> upbeatGenres = new List<string> { "pop", "alt-rock", "rock", "summer", "groove", "happy", "hip-hop", "metal", "rock-n-roll", "reggae" };

        public List<string> chillGenres = new List<string> { "classical", "chill", "jazz", "ambient", "study", "blues" };

        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string coverImageInput { get; set; }

    }
}
