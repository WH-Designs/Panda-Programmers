using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicCollaborationManager.Models;

[Table("Listener")]
public partial class Listener
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(64)]
    public string FirstName { get; set; } = null!;

    [StringLength(64)]
    public string LastName { get; set; } = null!;

    [Column("FriendID")]
    public int FriendId { get; set; }

    [Column("ASPNetIdentityID")]
    [StringLength(64)]
    public string AspnetIdentityId { get; set; } = null!;

    [InverseProperty("Listener")]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    [InverseProperty("Listener")]
    public virtual ICollection<Theme> Themes { get; } = new List<Theme>();
}
