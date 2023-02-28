using System;
using System.Collections.Generic;

namespace MusicCollaborationManager.Models;

public partial class Theme
{
    public int Id { get; set; }

    public string PrimaryColor { get; set; }

    public string SecondaryColor { get; set; }

    public string Font { get; set; }

    public int ListenerId { get; set; }

    public virtual Listener Listener { get; set; }
}
