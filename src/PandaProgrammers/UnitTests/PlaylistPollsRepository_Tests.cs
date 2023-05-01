using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class PlaylistPollsRepository_Tests
    {

        private static readonly string _seedFile = @"..\..\..\DATA\SEED.sql";

        private InMemoryDbHelper<MCMDbContext> _dbHelper = new InMemoryDbHelper<MCMDbContext>(
            _seedFile,
            DbPersistence.OneDbPerTest
        );


        //[Test]
        //public void GetAllOfCommentsForPlaylist_WithAtLeast1Comment_ReturnsAListWithAtLeast1Element() 
        //{
        //    // Arrange
        //    using MCMDbContext context = _dbHelper.GetContext();
        //    ICommentRepository repo = new CommentRepository(context);

        //    int expectedLength = 2;

        //    List<Comment> expectedList = new List<Comment>
        //    {
        //        new Comment {Id = 1,
        //        Message = "I like this playlist",
        //        SpotifyId = "0wbYwQItyK648wmeNcqP5z",
        //        Likes = 10,
        //        ListenerId = 1},

        //        new Comment {Id = 2,SpotifyId = "0wbYwQItyK648wmeNcqP5z",
        //        Message = "I dislike this playlist",
        //        Likes = 20,
        //        ListenerId = 2}
        //    };

        //    // Act
        //    List<Comment> actualList = repo.GetAllOfCommentsForPlaylist("0wbYwQItyK648wmeNcqP5z");

        //    // Assert
        //    Assert.That(expectedLength, Is.EqualTo(actualList.Count));
        //}
    }
}
