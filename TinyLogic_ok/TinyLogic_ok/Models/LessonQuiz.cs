using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class LessonQuiz
    {
        [Key] public int IdQuiz { get; set; }

        public string Title { get; set; }
        public string QuizJson { get; set; } 

        public int LessonId { get; set; }
        public Lessons Lesson { get; set; }
    }
}
