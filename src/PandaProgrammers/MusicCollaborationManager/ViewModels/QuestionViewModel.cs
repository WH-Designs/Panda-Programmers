using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.ViewModels
{
    public class QuestionViewModel
    {
        public List<SelectListItem> genresSelect;
        [Required]
        public string genre { get; set; }
        [Range(1, 10)]
        public double acousticness { get; set; }
        [Range(1, 10)]
        public double danceability { get; set; }
        [Range(1, 10)]
        public double energy { get; set; }
        [Range(1, 10)]
        public double instrumentalness { get; set; }
        [Range(1, 10)]
        public double liveness { get; set; }
        [Range(1, 10)]
        public int popularity { get; set; }
        [Range(1, 10)]
        public double speechiness { get; set; }
        [Range(30, 250)]
        public double tempo { get; set; }
        [Range(1, 10)]
        public double valence { get; set; } 
        

        public QuestionViewModel SeedGenres(QuestionViewModel vm ,Task<RecommendationGenresResponse> recommendation)
        {
            vm.genresSelect = new List<SelectListItem>();
            foreach (string genre in recommendation.Result.Genres)
            {
                var item = new SelectListItem()
                {
                    Text = genre,
                    Value = genre
                };
                vm.genresSelect.Add(item);
            }
            return vm;
        }
    }
}