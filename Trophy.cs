namespace TrophyManager
{
    public class Trophy
    {
        /// <summary>
        /// Intansefields.
        /// </summary>
        private string _competition;
        private int _year;

        /// <summary>
        /// Properties.
        /// </summary>
        public int Id { get; set; }
        public string Competition
        {
            get { return _competition; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Competition name cannot be null or empty.");
                }
              
                if (value.Length < 3)
                {
                    throw new ArgumentException("Competition name must be at least 3 characters long.");
                }

                _competition = value;
            }
        }
        public int Year
        {
            get { return _year; }
            set
            {
                if (value <= 1970 || value >= 2025)
                {
                    throw new ArgumentOutOfRangeException($"Year must be between 1850 and 2025.");
                }
                _year = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="competition"></param>
        /// <param name="year"></param>
        public Trophy(int id, string competition, int year)
        {
            Id = id;
            Competition = competition;
            Year = year;
        }

        /// <summary>
        /// Default constructor that initializes the trophy with default values.
        /// </summary>
        public Trophy():this(0, "Unknown Competition", 2000) { }

        /// <summary>
        /// Returns a string representation of the object, including its identifier, competition name, and year.
        /// </summary>
        /// <returns> A string in the format "Id: Competition (Year)".</returns>
        public override string ToString()
        {
            return $"Trophy ID: {Id}, Competition: {Competition} ({Year})";
        }
    }
}
