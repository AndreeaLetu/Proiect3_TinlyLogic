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

        // Simulare a unui obiect care ar trebui să fie citit din lessons.json sau BD
        // Într-o aplicație reală, ați folosi un Lesson model puternic tipizat.
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

        // Metoda care afișează pagina cu nivelurile cursului (Views/Courses/PythonCourse.cshtml)
        // Mapează la URL-ul /Courses/PythonCourse
        public IActionResult PythonCourse()
        {
            return View();
        }

        // Metoda care afișează pagina detaliată a lecției (Views/Courses/LessonDetails.cshtml)
        // Mapează la URL-ul /Courses/Lesson?id=X
        public IActionResult Lesson(int id)
        {
            // --- SIMULARE DE ÎNCĂRCARE A DATELOR DIN JSON/BD ---
            // În acest loc, ar trebui să folosești 'id' pentru a căuta
            // în baza de date sau în fișierul JSON detaliile lecției.

            // Simulare pentru Lecția 1
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

                // Returnează View-ul LessonDetails, trimițând modelul de date.
                return View("LessonDetails", lessonData);
            }

            // Simulare pentru Lecția 2
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

            // Dacă ID-ul nu este găsit
            return NotFound();
        }

        // --- Restul metodelor din CoursesController rămân neschimbate (Index, Details, Create, Edit, Delete) ---
        // ...

        // (Copiază/Lipește restul codului tău, inclusiv Index(), Details(), Create(), etc.)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }
        // ... (etc.)
    }
}