namespace TinyLogic_ok.Models
{
    public class LessonDetailsVM
    {
        public Lessons Lesson { get; set; }      // lecția din DB
        public LessonContent ParsedContent { get; set; } // JSON-ul deserializat
    }

    public class LessonContent
    {
        public string Title { get; set; }
        public List<LessonSection> Sections { get; set; }
    }

    public class LessonSection
    {
        public string Heading { get; set; }
        public string Text { get; set; }
    }
}
