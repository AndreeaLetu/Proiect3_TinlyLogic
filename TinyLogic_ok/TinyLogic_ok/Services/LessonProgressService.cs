
using TinyLogic_ok.Models;
using TinyLogic_ok.Services;

public class LessonProgressService : ILessonProgressService
{
    private readonly TinyLogicDbContext _context;

    public LessonProgressService(TinyLogicDbContext context)
    {
        _context = context;
    }

    public async Task MarkLessonCompletedAsync(string userId, int lessonId)
    {
        var progress = new LessonProgress
        {
            UserId = userId,
            LessonId = lessonId,
            IsCompleted = true,
            CompletedAt = DateTime.UtcNow
        };

        _context.LessonProgresses.Add(progress);
        await _context.SaveChangesAsync();
    }
}
