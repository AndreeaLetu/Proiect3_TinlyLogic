namespace TinyLogic_ok.Models
{
    public class PythonCourseVM
    {
        public Courses? Course { get; set; }
        public List<Lessons>? Lessons { get; set; }
        public Lessons? SelectedLesson { get; set; }
        public LessonContent? ParsedContent { get; set; }
        // Lecțiile finalizate de utilizator
        public List<int>? CompletedLessonIds { get; set; }

        // Status curs
        public bool IsCourseCompleted { get; set; }
        public bool IsSelectedLessonCompleted { get; set; }

        public int HighestCompletedOrder { get; set; }



    }

}
