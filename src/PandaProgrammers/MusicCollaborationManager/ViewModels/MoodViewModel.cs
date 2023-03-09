using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class MoodViewModel
    {
        [Required]
        public string mood { get; set; }

        public List<string> moodList = new List<string> {"Happy", "Sad", "Angry", "Calming", "Energetic", "Dancing" };

        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string coverImageInput { get; set; }
    }
}
