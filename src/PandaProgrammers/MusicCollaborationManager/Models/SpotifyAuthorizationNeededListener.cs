using System;
using System.Collections.Generic;

namespace MusicCollaborationManager.Models;

public partial class SpotifyAuthorizationNeededListener
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public int ListenerId { get; set; }

    public virtual Listener Listener { get; set; }
}
