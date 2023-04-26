using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicCollaborationManager.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int Likes { get; set; }

    public string Message { get; set; }

    public int ListenerId { get; set; }

    public string SpotifyId { get; set; }

    [JsonIgnore]
    public virtual Listener Listener { get; set; }
}
