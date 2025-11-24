using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyLogic_ok.Models
{
    public class Tests
    {
        [Key]
        public int IdTest { get; set; }

        public string TestName { get; set; }

        public string Description { get; set; }

        public string TestJson { get; set; }

        public int PassingScore { get; set; } = 70; // Nota minimă de trecere

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Courses Course { get; set; }

        // Relație cu progresul utilizatorilor
        public ICollection<TestProgress> TestProgresses { get; set; }
    }
}