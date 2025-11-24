namespace TinyLogic_ok.Services
{
    public interface ILessonProgressService
    {
        Task MarkLessonCompletedAsync(string userId, int lessonId);
    }
}
