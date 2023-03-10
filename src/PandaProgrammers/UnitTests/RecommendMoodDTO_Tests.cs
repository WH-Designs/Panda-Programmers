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
    public class RecommendMoodDTO_Tests
    {
        private RecommendDTO _recommendDTO;
        private MoodViewModel _moodViewModel;

        [SetUp]
        public void Setup()
        {
            _recommendDTO = new RecommendDTO();
            _moodViewModel = new MoodViewModel();
        }
        [Test]
        public void TestRNGValueInputShouldReturnANumberBetweenMinAndMax()
        {
            int min = 1;
            int max = 10;

            int result = _recommendDTO.rngValueInput(min, max);

            Assert.That(result, Is.GreaterThanOrEqualTo(1));
            Assert.That(result, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void TestRNGValueInputShouldReturnANumberBetweenMinAndMaxWithNegatives()
        {
            int min = -50;
            int max = -10;

            int result = _recommendDTO.rngValueInput(min, max);

            Assert.That(result, Is.GreaterThanOrEqualTo(-50));
            Assert.That(result, Is.LessThanOrEqualTo(-10));
        }

        [Test]
        public void TestConvertToMoodDTOShouldSetValuesForHappy()
        {
            _moodViewModel.mood = "1";

            _recommendDTO = _recommendDTO.convertToMoodDTO(_moodViewModel);

            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(0.7));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.3));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_speechiness, Is.GreaterThanOrEqualTo(0.3));
            Assert.That(_recommendDTO.target_speechiness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(60));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(170));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestConvertToMoodDTOShouldSetValuesForAngry()
        {
            _moodViewModel.mood = "3";

            _recommendDTO = _recommendDTO.convertToMoodDTO(_moodViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(0.3));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_speechiness, Is.GreaterThanOrEqualTo(0.5));
            Assert.That(_recommendDTO.target_speechiness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(0.3));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(120));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(250));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestConvertToMoodDTOShouldSetValuesForSad()
        {
            _moodViewModel.mood = "2";

            _recommendDTO = _recommendDTO.convertToMoodDTO(_moodViewModel);

            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_instrumentalness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_instrumentalness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(0.3));           

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_speechiness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_speechiness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(0.3));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestConvertToMoodDTOShouldSetValuesForCalming()
        {
            _moodViewModel.mood = "4";

            _recommendDTO = _recommendDTO.convertToMoodDTO(_moodViewModel);

            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_instrumentalness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_instrumentalness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(0.3));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(30));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(90));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestConvertToMoodDTOShouldSetValuesForEnergetic()
        {
            _moodViewModel.mood = "5";

            _recommendDTO = _recommendDTO.convertToMoodDTO(_moodViewModel);

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.5));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.8));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(0.2));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(110));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(250));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestConvertToMoodDTOShouldSetValuesForDancing()
        {
            _moodViewModel.mood = "6";

            _recommendDTO = _recommendDTO.convertToMoodDTO(_moodViewModel);

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.8));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.6));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_liveness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_liveness, Is.LessThanOrEqualTo(0.3));

            Assert.That(_recommendDTO.target_popularity, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_popularity, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.5));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }
    }
}