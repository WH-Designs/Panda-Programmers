using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Execution;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class MoodViewModel
    {
        [Required]
        public string mood { get; set; }

        public List<string> moodList = new List<string> {"Happy", "Sad", "Angry", "Chill", "Energetic", "Dancing" };

        //Lists to populate genre pools for different moods
        public List<string> happyGenreList = new List<string> { "acoustic", "reggae", "pop", "alt-rock", "guitar", "rock", "summer", "groove", "happy", "world-music" };

        public List<string> angryGenreList = new List<string> { "death-metal", "hardcore", "heavy-metal", "black-metal", "metalcore" };

        //public List<string> sadGenreList = new List<string> { "country", "sad", "blues", "acoustic", "emo", "bluegrass", "goth", "opera" };
        public List<string> sadGenreList = new List<string> { "sad"};

        public List<string> calmGenreList = new List<string> { "classical", "chill", "jazz", "ambient", "study", "piano", "guitar" };

        public List<string> energyGenreList = new List<string> { "work-out", "rock-n-roll", "pop", "hip-hop", "metal", "brazil", "anime", "dubstep", "electronic", "heavy-metal", "techno" };

        public List<string> danceGenreList = new List<string> { "salsa", "tango", "dance", "disco", "hip-hop", "dancehall", "club", "party", "reggaeton" };

        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string coverImageInput { get; set; }
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string descriptionInput { get; set; }
    }
}
