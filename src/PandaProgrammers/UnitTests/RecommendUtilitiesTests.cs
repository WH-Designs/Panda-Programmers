using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Utilities;
using MusicCollaborationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class RecommendUtilitiesTests
    {
        private RecommendDTO _recommendDTO;
        private GeneratorUtilities _generatorUtilities;
        private QuestionViewModel _questionViewModel;

        [SetUp]
        public void Setup()
        {
            _generatorUtilities = new GeneratorUtilities();
            _questionViewModel = new QuestionViewModel();
        }

        [Test]
        public void TestRNGValueShouldReturnANumberBetween1And10()
        {
            // Arrange
            // Act
            int result = _generatorUtilities.rngValue();
            // Assert
            Assert.That(result, Is.GreaterThanOrEqualTo(1));
            Assert.That(result, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void TestRNGValueInputShouldReturnANumberBetweenMinAndMax()
        {
            int min = 1;
            int max = 10;

            int result = _generatorUtilities.rngValueInput(min, max);

            Assert.That(result, Is.GreaterThanOrEqualTo(1));
            Assert.That(result, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void TestRNGValueInputShouldReturnANumberBetweenMinAndMaxWithNegatives()
        {
            int min = -50;
            int max = -10;

            int result = _generatorUtilities.rngValueInput(min, max);

            Assert.That(result, Is.GreaterThanOrEqualTo(-50));
            Assert.That(result, Is.LessThanOrEqualTo(-10));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringSunday()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 12, 12, 0, 0);
            string expected = "sunDay";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringSunMorning()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 12, 4, 0, 0);
            string expected = "endMorning";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringSundayEvening()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 12, 19, 0, 0);
            string expected = "workEvening";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringSundayBedtime()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 12, 21, 0, 0);
            string expected = "bedTime";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringFriday()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 10, 12, 0, 0);
            string expected = "workDay";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringFridayMorning()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 10, 6, 0, 0);
            string expected = "workMorning";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringFridayEvening()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 10, 20, 0, 0);
            string expected = "friEvening";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringFridayBedTime()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 10, 23, 5, 0);
            string expected = "bedTime";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringWeekDay()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 13, 10, 0, 0);
            string expected = "workDay";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringWeekMorning()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 14, 7, 0, 0);
            string expected = "workMorning";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryStringWeekEvening()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 15, 18, 0, 0);
            string expected = "workEvening";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryWeekBedTime()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 16, 21, 0, 0);
            string expected = "bedTime";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryWeekendDay()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 18, 12, 0, 0);
            string expected = "endDay";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryWeekendEvening()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 18, 20, 0, 0);
            string expected = "endEvening";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryWeekendBedtime()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 18, 23, 0, 0);
            string expected = "bedTime";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetTimeValueShouldReturnCorrectTimeCategoryWeekendMorning()
        {
            DateTime fakeDateTime = new DateTime(2023, 3, 18, 4, 0, 0);
            string expected = "endMorning";

            string result = _generatorUtilities.getTimeValue(fakeDateTime);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
