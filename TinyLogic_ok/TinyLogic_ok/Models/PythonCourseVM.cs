namespace TinyLogic_ok.Models
{
    public class PythonCourseVM
    {
        public Courses? Course { get; set; }
        public List<Lessons>? Lessons { get; set; }
        public Lessons? SelectedLesson { get; set; }
        public LessonContent? ParsedContent { get; set; }
    }

}
