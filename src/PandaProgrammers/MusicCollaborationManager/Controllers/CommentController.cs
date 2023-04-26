using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("{SpotifyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetComments(string SpotifyId)
        {
            List<Comment> comments;

            comments = _commentRepository.GetAllOfCommentsForPlaylist(SpotifyId);

            List<CommentDTO> commentDTOList = new List<CommentDTO>();

            if (comments.IsNullOrEmpty())
            {
                return NotFound();
            }

            foreach(Comment comment in comments)
            {
                CommentDTO commentDTO = new CommentDTO(comment);
                commentDTOList.Add(commentDTO);
            }

            return Ok(commentDTOList);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Comment> PostComment([Bind("Message,Likes,ListenerId,SpotifyId")] Comment comment)
        {
            //comment.Id = 0;

            Console.WriteLine(comment.Likes);
            Console.WriteLine(comment.Message);
            Console.WriteLine(comment.ListenerId);
            Console.WriteLine(comment.SpotifyId);

            Comment c = _commentRepository.AddOrUpdate(comment);
            return CreatedAtAction("GetComments", "Comment", c);
        }
    }
}
