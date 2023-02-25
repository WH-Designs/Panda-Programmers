using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Moq;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.ViewModels;
using RemindersTest;

namespace UnitTests
{
    public class RecommendDTO_Tests
    {
        private RecommendDTO _recommendDTO;
        private QuestionViewModel _questionViewModel;

        [SetUp]
        public void Setup()
        {
            _recommendDTO = new RecommendDTO();
            _questionViewModel = new QuestionViewModel();
        }

        [Test]
        public void TestRNGValueShouldReturnANumberBetween1And10()
        {
            // Arrange
            // Act
            int result = _recommendDTO.rngValue();
            // Assert
            Assert.That(result, Is.GreaterThanOrEqualTo(1));
            Assert.That(result, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void TestConvertToDTOShouldReasignValuesInCorrectFormatForSpotifyAPI()
        {
            // Arrange
            _questionViewModel.acousticness = 5;
            _questionViewModel.danceability= 5;
            _questionViewModel.energy= 5;
            _questionViewModel.instrumentalness= 5;
            _questionViewModel.liveness= 5;
            _questionViewModel.popularity= 5;
            _questionViewModel.speechiness= 5;
            _questionViewModel.valence= 5;
            _questionViewModel.genre = "classical";
            // Act
            _recommendDTO = _recommendDTO.convertToDTO(_questionViewModel);
            // Assert
            Assert.That(_recommendDTO.target_acousticness, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.target_danceability, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.target_energy, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.target_instrumentalness, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.target_liveness, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.target_popularity, Is.EqualTo(50));
            Assert.That(_recommendDTO.target_speechiness, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.target_valence, Is.EqualTo(0.5));
            Assert.That(_recommendDTO.genre, Is.EqualTo("classical"));
            Assert.That(_recommendDTO.market, Is.EqualTo("US"));
            Assert.That(_recommendDTO.limit, Is.EqualTo(20));

        }

        [Test]
        public void TestConvertToDTOShouldReasignValuesInCorrectFormatForSpotifyAPIWhenPassedLowValues()
        {
            // Arrange
            _questionViewModel.acousticness = -5;
            _questionViewModel.danceability = -5;
            _questionViewModel.energy = -5;
            _questionViewModel.instrumentalness = -5;
            _questionViewModel.liveness = -5;
            _questionViewModel.popularity = -5;
            _questionViewModel.speechiness = -5;
            _questionViewModel.valence = -5;
            _questionViewModel.genre = "classical";
            // Act
            _recommendDTO = _recommendDTO.convertToDTO(_questionViewModel);
            // Assert
            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_instrumentalness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_instrumentalness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(0));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_speechiness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_speechiness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.genre, Is.EqualTo("classical"));
            Assert.That(_recommendDTO.market, Is.EqualTo("US"));
            Assert.That(_recommendDTO.limit, Is.EqualTo(20));

        }

        [Test]
        public void TestConvertToDTOShouldReasignValuesInCorrectFormatForSpotifyAPIWhenPassedHighValues()
        {
            // Arrange
            _questionViewModel.acousticness = 50;
            _questionViewModel.danceability = 50;
            _questionViewModel.energy = 50;
            _questionViewModel.instrumentalness = 50;
            _questionViewModel.liveness = 50;
            _questionViewModel.popularity = 50;
            _questionViewModel.speechiness = 50;
            _questionViewModel.valence = 50;
            _questionViewModel.genre = "classical";
            // Act
            _recommendDTO = _recommendDTO.convertToDTO(_questionViewModel);
            // Assert
            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_instrumentalness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_instrumentalness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(0));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_speechiness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_speechiness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.genre, Is.EqualTo("classical"));
            Assert.That(_recommendDTO.market, Is.EqualTo("US"));
            Assert.That(_recommendDTO.limit, Is.EqualTo(20));

        }

        [Test]
        public void TestConvertToDTOShouldReasignValuesInCorrectFormatForSpotifyAPIWhenPassedNoValues()
        {
            // Arrange
            _questionViewModel.genre = "classical";
            // Act
            _recommendDTO = _recommendDTO.convertToDTO(_questionViewModel);
            // Assert
            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_instrumentalness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_instrumentalness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(0));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_speechiness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_speechiness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.genre, Is.EqualTo("classical"));
            Assert.That(_recommendDTO.market, Is.EqualTo("US"));
            Assert.That(_recommendDTO.limit, Is.EqualTo(20));

        }
    }
}
