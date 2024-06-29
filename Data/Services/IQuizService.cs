namespace OnlineLearningApp;

public interface IQuizService
{
    Task<IEnumerable<QuizDto>> GetAllQuizzesAsync();
    Task<QuizDto?> GetQuizByIdAsync(int id);
    Task AddQuizAsync(QuizDto quizDto);
    Task UpdateQuizAsync(QuizDto quizDto);
    Task DeleteQuizAsync(int id);
}
