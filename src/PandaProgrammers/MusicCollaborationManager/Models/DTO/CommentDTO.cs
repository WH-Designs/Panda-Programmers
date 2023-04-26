namespace MusicCollaborationManager.Models.DTO
{
    public class CommentDTO
    {
        public int Likes { get; set; }

        public string Message { get; set; }

        public int ListenerId { get; set; }

        public string SpotifyId { get; set; }

        public string ListenerName { get; set; }

        public CommentDTO(Comment comment)
        {
            Likes = comment.Likes;
            Message = comment.Message;
            ListenerId = comment.ListenerId;
            SpotifyId = comment.SpotifyId;
            ListenerName = comment.Listener.FirstName;
        }
    }
}
