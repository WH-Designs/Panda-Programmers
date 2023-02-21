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
using RemindersTest;

namespace UnitTests
{
    public class ListenerRepository_Tests
    {
        private Mock<MCMDbContext> _mockContext;
        private Mock<DbSet<Listener>> _mockListenerDbSet;
        private List<Listener> _listeners;

        [SetUp]
        public void Setup()
        {
            _listeners = new List<Listener>
            {
                new Listener {Id= 1, FirstName = "chad", LastName = "bass", AspnetIdentityId = "", FriendId = 0},
                new Listener {Id= 2, FirstName = "tiffany", LastName = "fox", AspnetIdentityId = "", FriendId = 0},
                new Listener {Id= 3, FirstName = "dale", LastName = "morse", AspnetIdentityId = "", FriendId = 0},
            };

            _mockContext = new Mock<MCMDbContext>();
            _mockListenerDbSet = MockHelpers.GetMockDbSet(_listeners.AsQueryable());
            _mockContext.Setup(ctx => ctx.Listeners).Returns(_mockListenerDbSet.Object);
        }

        [Test]
        public void GetListenerFullName_WithAtLeast1Person_ReturnsCorrectFullName()
        {
            // Arrange
            IListenerRepository listenerRepository = new ListenerRepository(_mockContext.Object);
            string expected = "chad bass";

            // Act
            string actual = listenerRepository.GetListenerFullName(1);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetListenerFullName_WithId0_ReturnsNull()
        {
            // Arrange
            IListenerRepository listenerRepository = new ListenerRepository(_mockContext.Object);
            string expected = null;

            // Act
            string actual = listenerRepository.GetListenerFullName(0);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetListenerFullName_WithNewPersonId_ReturnsNull()
        {
            // Arrange
            IListenerRepository listenerRepository = new ListenerRepository(_mockContext.Object);
            string expected = null;

            // Act
            string actual = listenerRepository.GetListenerFullName(5);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
