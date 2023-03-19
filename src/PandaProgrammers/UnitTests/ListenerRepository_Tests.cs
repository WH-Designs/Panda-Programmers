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

namespace UnitTests
{
    public class ListenerRepository_Tests
    {
        private static readonly string _seedFile = @"..\..\..\DATA\SEED.sql";

        private InMemoryDbHelper<MCMDbContext> _dbHelper = new InMemoryDbHelper<MCMDbContext>(
            _seedFile,
            DbPersistence.OneDbPerTest
        );

        [Test]
        public void GetListenerFullName_WithAtLeast1Person_ReturnsCorrectFullName()
        {
            // Arrange
            using MCMDbContext context = _dbHelper.GetContext();
            IListenerRepository repo = new ListenerRepository( context );

            string expected = "Chad Bass";

            // Act
            string actual = repo.GetListenerFullName(1);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void GetListenerFullName_WithId0_ReturnsNull()
        {
            // Arrange
            using MCMDbContext context = _dbHelper.GetContext();
            IListenerRepository repo = new ListenerRepository(context);

            // Act
            string actual = repo.GetListenerFullName(0);

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void GetListenerFullName_WithNewPersonId_ReturnsNull()
        {
            // Arrange
            using MCMDbContext context = _dbHelper.GetContext();
            IListenerRepository repo = new ListenerRepository(context);

            // Act
            string actual = repo.GetListenerFullName(6);

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void FindListenerByAspId_WithCorrectId_ReturnsCorrectListener()
        {
            // Arrange
            using MCMDbContext context = _dbHelper.GetContext();
            IListenerRepository repo = new ListenerRepository(context);

            Listener expected = repo.FindById(1);

            // Act
            Listener actual = repo.FindListenerByAspId("4b7959dc-2e9f-4fa9-ad38-d49ea70c8d32");

            // Assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void FindListenerByAspId_WithIncorrectId_ReturnsNull()
        {
            // Arrange
            using MCMDbContext context = _dbHelper.GetContext();
            IListenerRepository repo = new ListenerRepository(context);

            // Act
            Listener actual = repo.FindListenerByAspId("5b7959dc-2e9f-4fa9-ad38-d49ea70c8d30");

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void FindListenerByAspId_WithNullAspId_ReturnsNull()
        {
            // Arrange
            using MCMDbContext context = _dbHelper.GetContext();
            IListenerRepository repo = new ListenerRepository(context);

            // Act
            Listener actual = repo.FindListenerByAspId(null);

            // Assert
            Assert.Null(actual);
        }
    }
}
