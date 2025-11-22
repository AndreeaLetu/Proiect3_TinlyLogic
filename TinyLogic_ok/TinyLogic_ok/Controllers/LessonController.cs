using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyLogic_ok.Models;
using System.Text.Json;


public class LessonsController : Controller
{
    private readonly TinyLogicDbContext _context;

    public LessonsController(TinyLogicDbContext context)
    {
        _context = context;
    }

    // GET: Lessons/Create
    public IActionResult Create(int courseId)
    {
        return View(new Lessons { CourseId = courseId });
    }

    // POST: Lessons/Create
    [HttpPost]
    public async Task<IActionResult> Create(Lessons lesson)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction("PythonCourse", "Courses", new { courseId = lesson.CourseId });
        }

        return View(lesson);
    }
    [HttpPost]
    public async Task<IActionResult> ImportFromJson(int courseId)
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Lessons/python_lessons.json");

        if (!System.IO.File.Exists(jsonPath))
            return Content("Fisierul JSON nu exista!");

        var jsonData = await System.IO.File.ReadAllTextAsync(jsonPath);

        var lessons = JsonSerializer.Deserialize<List<LessonJsonModel>>(jsonData);

        foreach (var item in lessons)
        {
            var lesson = new Lessons
            {
                CourseId = courseId,
                LessonName = item.LessonName,
                OrderIndex = item.OrderIndex,
                Description = item.ContentJson.Title,
                ContentJson = JsonSerializer.Serialize(item.ContentJson)
            };

            _context.Lessons.Add(lesson);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("PythonCourse", "Courses", new { courseId });
    }
}
