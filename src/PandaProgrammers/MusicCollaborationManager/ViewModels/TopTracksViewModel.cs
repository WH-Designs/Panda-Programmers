using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.ViewModels
{
    public class TopTracksViewModel
    {

        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string coverImageInput { get; set; }
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string descriptionInput { get; set; }
    }
}
