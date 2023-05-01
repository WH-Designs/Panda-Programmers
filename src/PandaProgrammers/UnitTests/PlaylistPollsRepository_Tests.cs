using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Models;
using Microsoft.Data.Sqlite;
using NuGet.ContentModel;
using RemindersTest;


namespace UnitTests
{
    public class PlaylistPollsRepository_Tests
    {

        private Mock<MCMDbContext> _mockContext;
        private Mock<DbSet<Poll>> _mockPollDbSet;
        private List<Poll> _polls;

        [SetUp]
        public void Setup()
        {          
            _polls = new List<Poll>
            {
                new Poll { Id = 1, PollId = "apwbYwQItyK648wmeNcqP51", SpotifyPlaylistId = "pwbYwQItyK648wmeNcqP51", SpotifyTrackUri = "twbYwQItyK648wmeNcqP51"},
                new Poll { Id = 2, PollId = "apwbYwQItyK648wmeNcqP52", SpotifyPlaylistId = "pwbYwQItyK648wmeNcqP52", SpotifyTrackUri = "twbYwQItyK648wmeNcqP52"}
            };

            _mockContext = new Mock<MCMDbContext>();
            _mockPollDbSet = MockHelpers.GetMockDbSet(_polls.AsQueryable());
            _mockContext.Setup(ctx => ctx.Polls).Returns(_mockPollDbSet.Object);

            _mockContext.Setup(ctx => ctx.Set<Poll>()).Returns(_mockPollDbSet.Object);
        }


            [Test]
        public void GetPollDetailsBySpotifyPlaylistID_WithMoreThanOneEntry_ReturnsCorrectPoll()
        {
            //Arrange
            IPlaylistPollRepository PollsRepo = new PlaylistPollRepository(_mockContext.Object);
            const string ExpectedPlaylistID = "pwbYwQItyK648wmeNcqP51";

            Poll ExpectedPoll = new Poll();
            ExpectedPoll.Id = 1;
            ExpectedPoll.PollId = "apwbYwQItyK648wmeNcqP51";
            ExpectedPoll.SpotifyPlaylistId = "pwbYwQItyK648wmeNcqP51";
            ExpectedPoll.SpotifyTrackUri = "twbYwQItyK648wmeNcqP51";

            //Act
            Poll ActualPoll = PollsRepo.GetPollDetailsBySpotifyPlaylistID(ExpectedPlaylistID);


            //Assert
            Assert.That(ActualPoll.Id.Equals(ExpectedPoll.Id));
            Assert.That(ActualPoll.PollId.Equals(ExpectedPoll.PollId));
            Assert.That(ActualPoll.SpotifyPlaylistId.Equals(ExpectedPoll.SpotifyPlaylistId));
            Assert.That(ActualPoll.SpotifyTrackUri.Equals(ExpectedPoll.SpotifyTrackUri));
        }

        [Test]
        public void GetPollDetailsBySpotifyPlaylistID_WithNoMatchingEntries_ShouldReturnNull()
        {
            // Arrange
            //Arrange
            IPlaylistPollRepository PollsRepo = new PlaylistPollRepository(_mockContext.Object);
            const string NonExistantPlaylistID = "I_DONT_EXIST";

            Poll ExpectedPoll = new Poll();
            ExpectedPoll.Id = 1;
            ExpectedPoll.PollId = "apwbYwQItyK648wmeNcqP51";
            ExpectedPoll.SpotifyPlaylistId = "pwbYwQItyK648wmeNcqP51";
            ExpectedPoll.SpotifyTrackUri = "twbYwQItyK648wmeNcqP51";

            //Act
            Poll ActualPoll = PollsRepo.GetPollDetailsBySpotifyPlaylistID(NonExistantPlaylistID);


            //Assert
            Assert.That(ActualPoll == null);
            //Assert.That(ActualPoll.Id.Equals(ExpectedPoll.Id));
            //Assert.That(ActualPoll.PollId.Equals(ExpectedPoll.PollId));
            //Assert.That(ActualPoll.SpotifyPlaylistId.Equals(ExpectedPoll.SpotifyPlaylistId));
            //Assert.That(ActualPoll.SpotifyTrackUri.Equals(ExpectedPoll.SpotifyTrackUri));
        }
    }
}
