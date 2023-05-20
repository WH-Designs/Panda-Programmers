using Microsoft.Build.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusicCollaborationManager.Models.DTO
{
    public class SavePlaylistDTO
    {
        public List<string> NewTrackUris { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(100)]
        public string NewPlaylistName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public bool NewPlaylistIsVisible { get; set; }

        [StringLength(300)]
        public string NewPlaylistDescription { get; set; }

        
    }
}
