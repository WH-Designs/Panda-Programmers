using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetComments(string SpotifyId)
        {
            List<Comment> comments;

            comments = _commentRepository.GetAllOfCommentsForPlaylist(SpotifyId);

            if (comments is null)
            {
                return NotFound();
            }
            else if (!comments.Any())
            {
                return Ok();
            }

            return Ok(comments);
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Task> PostTask([Bind("Message,Likes,ListenerId,SpotifyId")] Comment comment)
        {
            comment.Id = 0;

            Comment c = _commentRepository.AddOrUpdate(comment);
            return CreatedAtAction("GetComments", "Comment", c);
        }
    }
}
