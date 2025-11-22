using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TinyLogic_ok.Models
{
    public class User : IdentityUser<int>
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public byte[]? Photo { get; set; }

        public int? PhoneNumber { get; set; }

        public string? Role { get; set; }

        public DateTime? BirthDate { get; set; }

        public ICollection<UserLessons>? LessonsProgress { get; set; } = new List<UserLessons>();



    }
}
