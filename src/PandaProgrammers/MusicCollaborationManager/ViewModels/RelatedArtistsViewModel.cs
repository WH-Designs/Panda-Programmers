using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.ViewModels
{
    public class RelatedArtistsViewModel
    {
        public List<SelectListItem> RelatedSelect;
        
        public List<string> RelatedArtists;

        [Required]
        public string Artist { get; set; }
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string coverImageInput { get; set; }
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string descriptionInput { get; set; }

        //public RelatedArtistsViewModel SeedArtists(RelatedArtistsViewModel vm, Task<RelatedArtistsResponse> artists)
        //{
        //    vm.RelatedSelect = new List<SelectListItem>();

        //    foreach(var artist in artists.Result.artists)
        //    {
        //        var item = new SelectListItem()
        //        {
        //            Text = artist,
        //            Value = artist
        //        };
        //        vm.RelatedSelect.Add(item);
        //    }
        //    return vm;
        //}
    }
}
