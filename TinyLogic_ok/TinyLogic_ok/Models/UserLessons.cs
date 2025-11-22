using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class UserLessons
    {
        [Key] public int IdUserLesson { get; set; }

        // Legătura cu user-ul
        public int UserId { get; set; }
        public User User { get; set; }

        // Legătura cu lecția
        public int LessonId { get; set; }
        public Lessons Lesson { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }



    }
}
