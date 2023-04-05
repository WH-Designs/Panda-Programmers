using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCollaborationManager.Models;

public partial class Listener
{
    public int Id { get; set; }

    [StringLength(64)]
    [RegularExpression("[a-zA-Z]+", ErrorMessage = "Characters are not allowed.")]
    [Required]
    public string FirstName { get; set; }

    [StringLength(64)]
    [RegularExpression("[a-zA-Z]+", ErrorMessage = "Characters are not allowed.")]
    [Required]
    public string LastName { get; set; }

    public int FriendId { get; set; }

    [Required]
    public string AspnetIdentityId { get; set; }

    public string SpotifyId { get; set; }

    public string AuthToken { get; set; }

    public string AuthRefreshToken { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Theme> Themes { get; } = new List<Theme>();
}
