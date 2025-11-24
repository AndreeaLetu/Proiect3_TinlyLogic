namespace TinyLogic_ok.Models
{
    /// <summary>
    /// ViewModel pentru paginile de teste
    /// </summary>
    public class TestVM
    {
        public Tests Test { get; set; }

        public TestContent ParsedContent { get; set; }

        /// <summary>
        /// True dacă testul este blocat (cursul nu e finalizat)
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// True dacă utilizatorul a trecut deja testul
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Ultimul scor obținut de utilizator
        /// </summary>
        public int? LastScore { get; set; }

        public Courses RequiredCourse { get; set; }

        /// <summary>
        /// Numărul de lecții finalizate din cursul necesar
        /// </summary>
        public int CompletedLessons { get; set; }

        /// <summary>
        /// Numărul total de lecții din cursul necesar
        /// </summary>
        public int TotalLessons { get; set; }
    }
}