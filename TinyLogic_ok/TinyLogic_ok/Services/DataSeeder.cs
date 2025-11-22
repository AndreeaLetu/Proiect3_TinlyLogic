using System.Text.Json;
using TinyLogic_ok.Models;
using Microsoft.EntityFrameworkCore;

namespace TinyLogic_ok.Services
{
    public class DataSeeder
    {
        private readonly TinyLogicDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DataSeeder(TinyLogicDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task SeedAsync()
        {
            Console.WriteLine("=== START SEED ===");

            await SeedCoursesAsync();
            await SeedLessonsAsync();

            Console.WriteLine("=== END SEED ===");
        }

        private async Task SeedCoursesAsync()
        {
            if (await _context.Courses.AnyAsync())
            {
                Console.WriteLine("Cursurile există deja – skip.");
                return;
            }

            var pythonCourse = new Courses
            {
                CourseName = "Python pentru copii",
                Description = "Curs Python interactiv pentru începători",
                Difficulty = "Ușor"
            };

            _context.Courses.Add(pythonCourse);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Creat cursul cu ID = {pythonCourse.CourseId}");
        }

        private async Task SeedLessonsAsync()
        {
            if (await _context.Lessons.AnyAsync())
            {
                Console.WriteLine("Lecțiile există deja – skip.");
                return;
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseName == "Python pentru copii");

            if (course == null)
            {
                Console.WriteLine("NU există cursul Python – nu pot importa lecțiile.");
                return;
            }

            string jsonPath = Path.Combine(_env.ContentRootPath, "Data/python_lessons.json");
            Console.WriteLine("Caut json la: " + jsonPath);

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("!!! JSON-ul pentru lecții NU există.");
                return;
            }

            string json = await File.ReadAllTextAsync(jsonPath);

            List<LessonJsonModel>? lessons;

            try
            {
                lessons = JsonSerializer.Deserialize<List<LessonJsonModel>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare la parsarea JSON: " + ex.Message);
                return;
            }

            if (lessons == null)
            {
                Console.WriteLine("JSON-ul este gol.");
                return;
            }

            foreach (var item in lessons)
            {
                var lessonEntity = new Lessons
                {
                    LessonName = item.LessonName,
                    OrderIndex = item.OrderIndex,
                    Description = item.ContentJson.Title,
                    CourseId = course.CourseId,
                    ContentJson = JsonSerializer.Serialize(item.ContentJson)
                };

                _context.Lessons.Add(lessonEntity);
                Console.WriteLine($"Adaug lecția: {item.LessonName}");
            }

            await _context.SaveChangesAsync();
        }
    }
}
