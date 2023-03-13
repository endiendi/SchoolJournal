namespace SchoolJournal.Test
{
    public class SchoolJournalTests
    {
        [Test]
        public void WhenInvokedToFetchStatisticsForAnEmptyValueIt_ShouldReturnAnAverageOfZero()
        {
            //arrange
            var student = new StudentInMemory("Adam", "Kowalski", "Polski");
            student.AddGrade(6);
            student.AddGrade(5);
            student.AddGrade(4);
            student.AddGrade(3);
            student.AddGrade(2);
            //act
            var statistics = student.GetStatistics();
            //assert
            Assert.AreEqual(Math.Round(4.0, 1), Math.Round(statistics.Average, 1));
        }

        [Test]
        public void WhenGetStatisticsCalled_ShouldReturnCorrectMax()
        {
            //arrange
            var student = new StudentInMemory("Adam", "Kowalski", "polski");
            student.AddGrade(6);
            student.AddGrade(2);
            student.AddGrade(6);
            student.AddGrade(2);
            student.AddGrade(6);
            //act
            var statistics = student.GetStatistics();
            //assert
            Assert.AreEqual(6, statistics.Max);
        }
        [Test]
        public void WhenGetStatisticsCalledLetters_ShouldReturnCorrectMax()
        {
            //arrange
            var student = new StudentInMemory("Adam", "Kowalski", "polski");
            student.AddGrade("A");
            student.AddGrade("E");
            student.AddGrade("A");
            student.AddGrade("E");
            student.AddGrade("A");
            //act
            var statistics = student.GetStatistics();
            //assert
            Assert.AreEqual(6, statistics.Max);
        }
        [Test]
        public void WhenGetStatisticsCalled_ShouldReturnCorrectMin()
        {
            //arrange
            var student = new StudentInMemory("Adam", "Kowalski", "polski");
            student.AddGrade(6);
            student.AddGrade(5);
            student.AddGrade(4);
            student.AddGrade(3);
            student.AddGrade(2);
            //act
            var statistics = student.GetStatistics();
            //assert
            Assert.AreEqual(2, statistics.Min);
        }
        [Test]
        public void WhenGetStatisticsCalled_ShouldReturnCorrectAverage()
        {
            var student = new StudentInMemory("Adam", "Kowalski", "polski");
            //arrange
            student.AddGrade("+A");
            student.AddGrade("E-");
            student.AddGrade("+A");
            student.AddGrade("E-");
            student.AddGrade("A");
            //act
            var statistics = student.GetStatistics();
            //assert
            Assert.AreEqual(Math.Round(4.4f, 1), Math.Round(statistics.Average, 1));
        }

    }
}
