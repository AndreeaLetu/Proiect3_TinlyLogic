using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TinyLogic_ok.Models;

namespace TinyLogic_ok.Controllers
{
    [Authorize]
    public class TestsController : Controller
    {
        private readonly TinyLogicDbContext _context;
        private readonly UserManager<User> _userManager;

        public TestsController(TinyLogicDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        // GET: Lista de teste
        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // 1. Încărcăm toate testele și lecțiile asociate (o singură interogare mare)
            var tests = await _context.Tests
                .Include(t => t.Course)
                    .ThenInclude(c => c.Lessons)
                .ToListAsync();

            // 2. Colectăm toate ID-urile de lecții din toate cursurile
            var allLessonIds = tests.SelectMany(t => t.Course.Lessons.Select(l => l.IdLesson)).Distinct().ToList();

            // 3. Încărcăm TOATE progresele lecțiilor utilizatorului (o singură interogare)
            var allLessonProgress = await _context.LessonProgresses
                .Where(lp => lp.UserId == userId.ToString() && lp.IsCompleted && allLessonIds.Contains(lp.LessonId))
                .ToListAsync();

            // 4. Încărcăm TOATE progresele testelor utilizatorului (o singură interogare)
            var allTestProgress = await _context.TestProgresses
                .Where(tp => tp.UserId == userId)
                .OrderByDescending(tp => tp.CompletedAt)
                .ToListAsync();


            var testVMs = new List<TestVM>();

            // 5. Acum, iterăm și calculăm totul în memorie (C#), fără await-uri costisitoare
            foreach (var test in tests)
            {
                // Găsim ID-urile lecțiilor CURENTE
                var currentTestLessonIds = test.Course.Lessons.Select(l => l.IdLesson).ToList();
                var totalLessons = currentTestLessonIds.Count;

                // Calculăm lecțiile finalizate prin filtrarea listei din memorie (allLessonProgress)
                var completedLessons = allLessonProgress
                    .Count(lp => currentTestLessonIds.Contains(lp.LessonId));

                bool isLocked = completedLessons < totalLessons;

                // Găsim cel mai recent progres al testului din lista din memorie (allTestProgress)
                var testProgress = allTestProgress
                    .FirstOrDefault(tp => tp.TestId == test.IdTest); // Primul rezultat va fi cel mai recent datorită OrderByDescending de mai sus.

                testVMs.Add(new TestVM
                {
                    Test = test,
                    IsLocked = isLocked,
                    IsCompleted = testProgress != null && testProgress.IsPassed,
                    LastScore = testProgress?.Score,
                    RequiredCourse = test.Course,
                    CompletedLessons = completedLessons,
                    TotalLessons = totalLessons
                });
            }

            return View(testVMs);
        }

        // GET: Pagina unui test specific
        public async Task<IActionResult> TakeTest(int testId, LessonProgress lp)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var test = await _context.Tests
                .Include(t => t.Course)
                    .ThenInclude(c => c.Lessons)
                .FirstOrDefaultAsync(t => t.IdTest == testId);

            if (test == null)
                return NotFound();

            var totalLessons = test.Course.Lessons.Count;

            var completedLessons = await _context.UserLessons
                .Where(ul => ul.UserId == userId
                    && ul.IsCompleted
                    && test.Course.Lessons.Select(l => l.IdLesson).Contains(ul.LessonId))
                .CountAsync();


            if (completedLessons < totalLessons)
            {
                TempData["Error"] = "Trebuie să finalizezi cursul pentru a debloca acest test!";
                return RedirectToAction("Index");
            }

            var parsedContent = JsonConvert.DeserializeObject<TestContent>(test.TestJson);

            var testProgress = await _context.TestProgresses
                .Where(tp => tp.UserId == userId && tp.TestId == test.IdTest)
                .OrderByDescending(tp => tp.CompletedAt)
                .FirstOrDefaultAsync();

            var vm = new TestVM
            {
                Test = test,
                ParsedContent = parsedContent,
                IsLocked = false,
                IsCompleted = testProgress != null && testProgress.IsPassed,
                LastScore = testProgress?.Score,
                RequiredCourse = test.Course,
                CompletedLessons = completedLessons,
                TotalLessons = totalLessons
            };

            return View(vm);
        }

        // POST: Verifică răspunsurile
        [HttpPost]
        public async Task<IActionResult> SubmitTest([FromBody] SubmitTestRequest request)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var test = await _context.Tests.FindAsync(request.TestId);
            if (test == null)
                return Json(new { success = false, message = "Testul nu există!" });

            var parsedContent = JsonConvert.DeserializeObject<TestContent>(test.TestJson);

            int totalScore = 0;
            int maxScore = parsedContent.Questions.Sum(q => q.Points);

            foreach (var answer in request.Answers)
            {
                // 🔥 CORECȚIE: Ne asigurăm că ambele părți ale comparației sunt string-uri.
                // Acest lucru rezolvă eroarea 'string' și 'int'.
                string expectedQuestionNumber = answer.QuestionNumber.ToString();
                
                var question = parsedContent.Questions
                    .FirstOrDefault(q => q.QuestionNumber.ToString() == expectedQuestionNumber);

                if (question != null)
                {
                    string userAnswer = NormalizeDiacritics(answer.Answer.Trim().ToLower());
                    string correctAnswer = NormalizeDiacritics(question.CorrectAnswer.Trim().ToLower());

                    if (userAnswer == correctAnswer)
                        totalScore += question.Points;
                }
            }

            int percentage = (int)((double)totalScore / maxScore * 100);
            bool isPassed = percentage >= test.PassingScore;

            var testProgress = new TestProgress
            {
                UserId = userId,
                TestId = request.TestId,
                Score = percentage,
                IsPassed = isPassed,
                CompletedAt = DateTime.Now,
                AnswersJson = JsonConvert.SerializeObject(request.Answers)
            };

            _context.TestProgresses.Add(testProgress);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                score = percentage,
                passed = isPassed,
                message = isPassed
                    ? $"Felicitări! Ai trecut testul cu {percentage}%!"
                    : $"Ai obținut {percentage}%. Nota minimă este {test.PassingScore}%."
            });
        }

        private string NormalizeDiacritics(string text)
        {
            return text
                .Replace("ă", "a").Replace("â", "a").Replace("î", "i")
                .Replace("ș", "s").Replace("ş", "s")
                .Replace("ț", "t").Replace("ţ", "t");
        }

        public class SubmitTestRequest
        {
            public int TestId { get; set; }
            public List<TestAnswer> Answers { get; set; }
        }

        public class TestAnswer
        {
            public int QuestionNumber { get; set; }
            public string Answer { get; set; }
        }
    }
}