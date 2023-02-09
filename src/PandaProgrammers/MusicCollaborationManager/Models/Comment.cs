using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicCollaborationManager.Models;

[Table("Comment")]
public partial class Comment
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int Likes { get; set; }

    [StringLength(300)]
    public string Message { get; set; } = null!;

    [Column("ListenerID")]
    public int ListenerId { get; set; }

    [Column("PlaylistID")]
    public int PlaylistId { get; set; }

    [ForeignKey("ListenerId")]
    [InverseProperty("Comments")]
    public virtual Listener Listener { get; set; } = null!;

    [ForeignKey("PlaylistId")]
    [InverseProperty("Comments")]
    public virtual Playlist Playlist { get; set; } = null!;
}
