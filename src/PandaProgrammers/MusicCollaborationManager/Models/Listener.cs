using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.Models;

public partial class Listener
{
    public int Id { get; set; }
    [Required]
    [StringLength(64)]
    [RegularExpression("^[\\w]*[^\\W][\\w]")]
    public string FirstName { get; set; }
    [Required]
    [StringLength(64)]
    [RegularExpression("^[\\w]*[^\\W][\\w]")]
    public string LastName { get; set; }

    public int FriendId { get; set; }

    public string AspnetIdentityId { get; set; }

    public string SpotifyId { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Theme> Themes { get; } = new List<Theme>();
}
