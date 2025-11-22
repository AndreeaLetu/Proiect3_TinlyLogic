/* Exista mai multe cursuri cu mai multe lectii 
 * -> legatura intre cursuri si lectii */


using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class Courses
    {
        [Key] public int CourseId { get; set; }
        public string CourseName { get; set; }

        public string Description { get; set; }
        public string Difficulty { get; set; }

        // Relații
        public ICollection<Lessons> Lessons { get; set; } = new List<Lessons>();

        public Tests Test { get; set; }  // fiecare curs are 1 test final
    }
}
