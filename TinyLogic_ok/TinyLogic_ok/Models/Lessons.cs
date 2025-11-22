using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class Lessons
    {
        [Key] public int IdLesson { get; set; }

        public string LessonName { get; set; }
        public string Description { get; set; }
        public string PdfPath { get; set; }
        public int OrderIndex { get; set; }
        public string ContentJson { get; set; }

        // 👉 Lecția aparține unui curs
        public int CourseId { get; set; }
        public Courses Course { get; set; }

        // 👉 Lecția are EXACT un quiz
        public LessonQuiz Quiz { get; set; }

       
        public ICollection<UserLessons> UserProgress { get; set; } = new List<UserLessons>();


    }
}
