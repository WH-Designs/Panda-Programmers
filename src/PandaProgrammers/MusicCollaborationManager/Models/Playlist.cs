using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicCollaborationManager.Models;

[Table("Playlist")]
public partial class Playlist
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ServiceID")]
    public int ServiceId { get; set; }

    [InverseProperty("Playlist")]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
}
