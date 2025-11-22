using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyLogic_ok.Models;
using System.Collections.Generic;

namespace TinyLogic_ok.Controllers
{
    public class CoursesController : Controller
    {
        private readonly TinyLogicDbContext _context;

        public CoursesController(TinyLogicDbContext context)
        {
            _context = context;
        }

      
        private class LessonViewModel
        {
            public int Id { get; set; }
            public string Level { get; set; }
            public string Title { get; set; }
            public string ContentHtml { get; set; }
            public string ExampleCode { get; set; }
            public string TaskDescription { get; set; }
            public string InitialCode { get; set; }
        }

        
    
        public async Task<IActionResult> PythonCourse(int courseId, int? lessonId)
        {
            var course = await _context.Courses
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
                return NotFound();

            var lessons = course.Lessons.OrderBy(l => l.OrderIndex).ToList();

            Lessons selectedLesson = null;
            LessonContent parsed = null;

            if (lessonId != null)
            {
                selectedLesson = lessons.FirstOrDefault(l => l.IdLesson == lessonId);

                if (selectedLesson != null && selectedLesson.ContentJson != null)
                {
                    parsed = System.Text.Json.JsonSerializer.Deserialize<LessonContent>(selectedLesson.ContentJson);
                }
            }

            var vm = new PythonCourseVM
            {
                Course = course,
                Lessons = lessons,
                SelectedLesson = selectedLesson,
                ParsedContent = parsed
            };

            return View(vm);
        }


      
        public IActionResult Lesson(int id)
        {
          
            if (id == 1)
            {
                var lessonData = new LessonViewModel
                {
                    Id = 1,
                    Level = "Nivelul 1: Începător",
                    Title = "Lecția 1: Primul tău program Python!",
                    ContentHtml = "Bine ai venit în Python! Folosim comanda <strong><code>print()</code></strong> pentru a afișa text pe ecran. Este prima ta linie de cod!",
                    ExampleCode = "print(\"Salut, lume!\")",
                    TaskDescription = "Rulează codul existent în editorul de mai jos și apasă \"Rulează Codul\" pentru a vedea rezultatul! (Output-ul trebuie să fie 'Salut, lume!')",
                    InitialCode = "# Scrie codul tău Python aici\nprint(\"Salut, lume!\")"
                };

              
                return View("LessonDetails", lessonData);
            }

            if (id == 2)
            {
                var lessonData = new LessonViewModel
                {
                    Id = 2,
                    Level = "Nivelul 1: Începător",
                    Title = "Lecția 2: Variabile și date",
                    ContentHtml = "O <strong>variabilă</strong> este ca o cutie în care poți stoca informații (texte, numere etc.).",
                    ExampleCode = "nume = \"Ion\"\nvarsta = 10\nprint(nume)",
                    TaskDescription = "Creează o variabilă nouă numită **`tara`** și dă-i valoarea **\"Romania\"**. Apoi, afișează valoarea acesteia.",
                    InitialCode = "# Creează o variabilă numită 'oras'\noras = \"Bucuresti\"\n"
                };
                return View("LessonDetails", lessonData);
            }

           
            return NotFound();
        }

   
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }
      
    }
}