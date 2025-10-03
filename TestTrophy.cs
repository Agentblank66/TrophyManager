using TrophyManager;

namespace TestTrophyManager
{
    /// <summary>
    /// This class contains unit tests for the Trophy class.
    /// </summary>
    [TestClass]
    public class TestTrophy
    {
        /// <summary>
        /// This method tests the constructor of the Trophy class with various valid and invalid inputs.
        /// </summary>
        [TestMethod]
        public void TestTrophyConstructor()
        {
            // Test valid inputs
            var trophy = new Trophy(1, "Champions League", 2020);
            Assert.AreEqual(1, trophy.Id);
            Assert.AreEqual("Champions League", trophy.Competition);
            Assert.AreEqual(2020, trophy.Year);

            // Test invalid competition name (too short)
            Assert.ThrowsException<ArgumentException>(() => new Trophy(2, "AB", 2020));
            // Test invalid competition name (null or empty)
            Assert.ThrowsException<ArgumentException>(() => new Trophy(3, "", 2020));
            // Test invalid year (too low)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trophy(4, "Champions League", 1960));
            // Test invalid year (too high)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trophy(5, "Champions League", 2030));
        }

        /// <summary>
        /// This method tests the default constructor of the Trophy class.
        /// </summary>
        [TestMethod]
        public void TestTrophyDefaultConstructor()
        {
            // Test default constructor initializes with default values.
            var trophy = new Trophy();
            Assert.AreEqual(0, trophy.Id);
            Assert.AreEqual("Unknown Competition", trophy.Competition);
            Assert.AreEqual(2000, trophy.Year);
        }

        /// <summary>
        /// This method tests the ToString method of the Trophy class.
        /// </summary>
        [TestMethod]
        public void TestTrophyToString()
        {
            // Test ToString method returns the correct string representation.
            var trophy = new Trophy(1, "Champions League", 2020);
            string expectedString = "Trophy ID: 1, Competition: Champions League (2020)";
            Assert.AreEqual(expectedString, trophy.ToString());
        }

    }

    /// <summary>
    /// This class contains unit tests for the TrophiesRepository class.
    /// </summary>
    [TestClass]
    public class TestTrophiesRepository
    {
        /// <summary>
        /// Instance of TrophiesRepository to be used in tests.
        /// </summary>
        private TrophiesRepository _repository;

        /// <summary>
        /// This method sets up the test environment before each test method is run.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _repository = new TrophiesRepository();
        }

        /// <summary>
        /// This method tests the Get method of the TrophiesRepository class.
        /// </summary>
        [TestMethod]
        public void TestGet()
        {
            // Test that there are 5 trophies in the repository.
            var trophies = _repository.Get();
            Assert.AreEqual(5, trophies.Count);
        }

        /// <summary>
        /// This method tests the GetFilteredByYear method of the TrophiesRepository class.
        /// </summary>
        [TestMethod]
        public void TestGetFilteredByYear()
        {
            // Test filtering by year 2020.
            var trophies2020 = _repository.GetFilteredByYear(2020);
            Assert.AreEqual(1, trophies2020.Count);
            Assert.AreEqual("Champions League", trophies2020[0].Competition);
            // Test filtering by year 2019.
            var trophies2019 = _repository.GetFilteredByYear(2019);
            Assert.AreEqual(1, trophies2019.Count);
            Assert.AreEqual("Premier League", trophies2019[0].Competition);
            // Test filtering by a year with no trophies (2023).
            var trophies2023 = _repository.GetFilteredByYear(2023);
            Assert.AreEqual(0, trophies2023.Count);
        }

        /// <summary>
        /// This method tests the GetSorted method of the TrophiesRepository class by competition.
        /// </summary>
        [TestMethod]
        public void TestGetSortedByCompetition()
        {
            // Test sorting by competition name.
            var sortedByCompetition = _repository.GetSorted("competition");
            Assert.AreEqual(5, sortedByCompetition.Count);
            Assert.AreEqual("Champions League", sortedByCompetition[0].Competition);
            Assert.AreEqual("FA Cup", sortedByCompetition[1].Competition);
            Assert.AreEqual("La Liga", sortedByCompetition[2].Competition);
            Assert.AreEqual("Premier League", sortedByCompetition[3].Competition);
            Assert.AreEqual("Serie A", sortedByCompetition[4].Competition);
        }

        /// <summary>
        /// This method tests the GetSorted method of the TrophiesRepository class by year.
        /// </summary>
        [TestMethod]
        public void TestGetSortedByYear()
        {
            // Test sorting by year.
            var sortedByYear = _repository.GetSorted("year");
            Assert.AreEqual(5, sortedByYear.Count);
            Assert.AreEqual(2018, sortedByYear[0].Year);
            Assert.AreEqual(2019, sortedByYear[1].Year);
            Assert.AreEqual(2020, sortedByYear[2].Year);
            Assert.AreEqual(2021, sortedByYear[3].Year);
            Assert.AreEqual(2022, sortedByYear[4].Year);
        }

        /// <summary>
        /// This method tests the GetById method of the TrophiesRepository class.
        /// </summary>
        [TestMethod]
        public void TestGetById()
        {
            // Test getting trophy by valid ID.
            var trophy = _repository.GetById(1);
            Assert.IsNotNull(trophy);
            Assert.AreEqual("Champions League", trophy.Competition);
            Assert.AreEqual(2020, trophy.Year);
            // Test getting trophy by invalid ID (should throw exception).
            Assert.ThrowsException<ArgumentException>(() => _repository.GetById(999));
        }

        /// <summary>
        /// This method tests the Add method of the TrophiesRepository class.
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            // Test adding a new trophy.
            var newTrophy = new Trophy(6, "Bundesliga", 2023);
            _repository.Add(newTrophy);
            // Verify that the trophy was added.
            var trophies = _repository.Get();
            Assert.AreEqual(6, trophies.Count);
            // Verify that the added trophy can be retrieved by its ID.
            var addedTrophy = _repository.GetById(6);
            Assert.IsNotNull(addedTrophy);
            Assert.AreEqual("Bundesliga", addedTrophy.Competition);
            Assert.AreEqual(2023, addedTrophy.Year);
        }

        /// <summary>
        /// This method tests the Remove method of the TrophiesRepository class.
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            // Test removing a trophy by valid ID.
            _repository.Remove(1);
            // Verify that the trophy was removed.
            var trophies = _repository.Get();
            Assert.AreEqual(4, trophies.Count);
            // Verify that the removed trophy cannot be retrieved by its ID (should throw exception).
            Assert.ThrowsException<ArgumentException>(() => _repository.GetById(1));
            // Test removing a trophy by invalid ID (should throw exception).
            Assert.ThrowsException<ArgumentException>(() => _repository.Remove(999));
        }

        /// <summary>
        /// This method tests the Update method of the TrophiesRepository class.
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            // Test updating a trophy by valid ID.
            var updatedTrophy = new Trophy(1, "Updated League", 2024);
            _repository.Update(1, updatedTrophy);
            // Verify that the trophy was updated.
            var trophy = _repository.GetById(1);
            Assert.IsNotNull(trophy);
            Assert.AreEqual("Updated League", trophy.Competition);
            Assert.AreEqual(2024, trophy.Year);
            // Test updating a trophy by invalid ID (should throw exception).
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(6,new Trophy(999, "Nonexistent", 2020)));
        }
    }
}