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
    public class RecommendTimeDTO_Tests
    {
        private RecommendDTO _recommendDTO;
        private TimeViewModel _timeViewModel;

        [SetUp]
        public void Setup()
        {
            _recommendDTO = new RecommendDTO();
            _timeViewModel= new TimeViewModel();
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForWorkDay()
        {
            _timeViewModel.timeCategory = "workDay";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(0.4));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(40));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(100));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForWorkMorning()
        {
            _timeViewModel.timeCategory = "workMorning";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(100));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(250));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForEndMorning()
        {
            _timeViewModel.timeCategory = "endMorning";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.5));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForFridayEvening()
        {
            _timeViewModel.timeCategory = "friEvening";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.5));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForEndEvening()
        {
            _timeViewModel.timeCategory = "endEvening";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_danceability, Is.GreaterThanOrEqualTo(0.7));
            Assert.That(_recommendDTO.target_danceability, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.target_valence, Is.GreaterThanOrEqualTo(0.5));
            Assert.That(_recommendDTO.target_valence, Is.LessThanOrEqualTo(1));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForWorkEvening()
        {
            _timeViewModel.timeCategory = "workEvening";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(0.5));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(80));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForBedTime()
        {
            _timeViewModel.timeCategory = "bedTime";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_energy, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_energy, Is.LessThanOrEqualTo(0.4));

            Assert.That(_recommendDTO.target_tempo, Is.GreaterThanOrEqualTo(10));
            Assert.That(_recommendDTO.target_tempo, Is.LessThanOrEqualTo(80));

            Assert.That(_recommendDTO.limit, Is.EqualTo(20));
        }

        [Test]
        public void TestconvertToTimeDTOShouldSetValuesForSunDay()
        {
            _timeViewModel.timeCategory = "sunDay";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

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
        public void TestconvertToTimeDTOShouldSetValuesForWeekendDay()
        {
            _timeViewModel.timeCategory = "endDay";

            _recommendDTO = _recommendDTO.convertToTimeDTO(_timeViewModel);

            Assert.That(_recommendDTO.target_acousticness, Is.GreaterThanOrEqualTo(0.1));
            Assert.That(_recommendDTO.target_acousticness, Is.LessThanOrEqualTo(1));

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
    }
}