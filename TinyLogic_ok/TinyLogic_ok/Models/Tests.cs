using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class Tests
    {
        [Key] public int IdTest { get; set; }

        public string TestName { get; set; }
        public string Description { get; set; }

        public string TestJson { get; set; } // testul final salvat ca JSON

        // 👉 Testul aparține unui curs
        public int CourseId { get; set; }
        public Courses Course { get; set; }
    }
}
