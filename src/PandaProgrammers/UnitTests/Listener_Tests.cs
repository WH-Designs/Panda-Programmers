using Microsoft.EntityFrameworkCore.Infrastructure;
using MusicCollaborationManager.Models;

namespace UnitTests;

public class Listener_Tests
{
    private Listener MakeValidListener()
    {
        Listener listener = new Listener
        {
            Id = 1,
            FirstName = "Chad",
            LastName = "Bass",
            FriendId = 0,
            AspnetIdentityId = "4b7959dc-2e9f-4fa9-ad38-d49ea70c8d32",
            SpotifyId = "0",
            AuthToken = "0",
            AuthRefreshToken = "0",
            Comments = {}
        };

        return listener;
    }

    [Test]
    public void ValidListener_IsValid()
    {
        // Arrange
        Listener listener = MakeValidListener();

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.That(mv.Valid, Is.True);
    }

    [Test]
    public void Listener_WithMissingFirstName_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.FirstName = null;

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("FirstName"), Is.True);
        });
    }

    [Test]
    public void Listener_WithEmptyStringFirstName_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.FirstName = "";

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("FirstName"), Is.True);
        });
    }

    [Test]
    public void Listener_WithToLongOfFirstName_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.FirstName = "aihfhfusfbnjlisbdfuyfgyesfgyugsebfsgdfuygshbgfyugeyfhesgfyugsfygsyufgyufgyuegfsgyfugsyfaguyegfyuagefyugyuefgyugbsyfuag";

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("FirstName"), Is.True);
        });
    }

    [Test]
    public void Listener_WithMissingLastName_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.LastName = null;

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("LastName"), Is.True);
        });
    }

    [Test]
    public void Listener_WithEmptyStringLastName_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.LastName = "";

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("LastName"), Is.True);
        });
    }

    [Test]
    public void Listener_WithToLongOfLastName_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.LastName = "aihfhfusfbnjlisbdfuyfgyesfgyugsebfsgdfuygshbgfyugeyfhesgfyugsfygsyufgyufgyuegfsgyfugsyfaguyegfyuagefyugyuefgyugbsyfuag";

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("LastName"), Is.True);
        });
    }

    [Test]
    public void Listener_WithMissingAspIdentityId_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.AspnetIdentityId = null;

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("AspnetIdentityId"), Is.True);
        });
    }

    [Test]
    public void Listener_WithEmptyStringAspIdentityId_IsInvalid()
    {
        // Arrange
        Listener listener = MakeValidListener();
        listener.AspnetIdentityId = "";

        // Act
        ModelValidator mv = new ModelValidator(listener);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mv.Valid, Is.False);
            Assert.That(mv.ContainsFailureFor("AspnetIdentityId"), Is.True);
        });
    }
}