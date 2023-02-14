using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicCollaborationManager.Models;

[Table("Theme")]
public partial class Theme
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(6)]
    public string PrimaryColor { get; set; } = null!;

    [StringLength(6)]
    public string SecondaryColor { get; set; } = null!;

    [StringLength(32)]
    public string Font { get; set; } = null!;

    [Column("ListenerID")]
    public int ListenerId { get; set; }

    [ForeignKey("ListenerId")]
    [InverseProperty("Themes")]
    public virtual Listener Listener { get; set; } = null!;
}
