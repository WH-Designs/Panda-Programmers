using System;
using System.Collections.Generic;

namespace MusicCollaborationManager.Models;

public partial class Poll
{
    public int Id { get; set; }

    public string PollId { get; set; }

    public string SpotifyPlaylistId { get; set; }

    public string SpotifyTrackUri { get; set; }
}
