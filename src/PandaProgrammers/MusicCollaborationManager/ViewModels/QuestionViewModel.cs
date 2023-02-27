using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.ViewModels
{
    public class QuestionViewModel
    {
        public List<SelectListItem> genresSelect;
        public string genre { get; set; }
        public double acousticness { get; set; } 
        public double danceability { get; set; }
        public double energy { get; set; }
        public double instrumentalness { get; set; }
        public double liveness { get; set; }
        public int popularity { get; set; }
        public double speechiness { get; set; }
        public double tempo { get; set; }
        public double valence { get; set; }
        
    }
}