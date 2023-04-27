using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicCollaborationManager.Models;

public partial class Listener
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int FriendId { get; set; }

    public string AspnetIdentityId { get; set; }

    public string Theme { get; set; }

    public string SpotifyId { get; set; }

    public string AuthToken { get; set; }

    public string AuthRefreshToken { get; set; }

    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
}
