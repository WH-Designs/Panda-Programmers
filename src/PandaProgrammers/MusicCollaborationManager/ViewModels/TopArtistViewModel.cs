using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.ViewModels
{
    public class TopArtistViewModel
    {
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string coverImageInput { get; set; }
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string descriptionInput { get; set; }
        [RegularExpression("^[\\w ]*[^\\W_][\\w ]")]
        public string titleInput { get; set; }
        public bool generateTitle { get; set; }
    }
}
