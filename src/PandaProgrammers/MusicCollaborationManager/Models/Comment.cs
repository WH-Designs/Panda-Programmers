using System;
using System.Collections.Generic;

namespace MusicCollaborationManager.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int Likes { get; set; }

    public string Message { get; set; }

    public int ListenerId { get; set; }

    public int PlaylistId { get; set; }

    public virtual Listener Listener { get; set; }

    public virtual Playlist Playlist { get; set; }
}
