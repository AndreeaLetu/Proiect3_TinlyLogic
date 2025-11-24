namespace TinyLogic_ok.Models
{
    /// <summary>
    /// Structura JSON pentru o întrebare din test
    /// </summary>
    public class TestQuestion
    {
        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        /// <summary>
        /// Tipul întrebării: "MultipleChoice", "Code", "TrueFalse"
        /// </summary>
        public string QuestionType { get; set; }

        /// <summary>
        /// Opțiunile de răspuns (pentru MultipleChoice)
        /// </summary>
        public List<string> Options { get; set; }

        public string CorrectAnswer { get; set; }

        public int Points { get; set; } = 10;
    }
}