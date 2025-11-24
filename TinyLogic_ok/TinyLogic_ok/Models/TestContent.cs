namespace TinyLogic_ok.Models
{
    /// <summary>
    /// Structura JSON pentru conținutul complet al unui test
    /// </summary>
    public class TestContent
    {
        public string Title { get; set; }

        public string Instructions { get; set; }

        /// <summary>
        /// Timp limită în minute
        /// </summary>
        public int TimeLimit { get; set; }

        public List<TestQuestion> Questions { get; set; }
    }
}