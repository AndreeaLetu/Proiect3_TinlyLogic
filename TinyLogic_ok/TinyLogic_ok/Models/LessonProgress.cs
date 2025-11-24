using System;

namespace TinyLogic_ok.Models
{
    public class LessonProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LessonId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
