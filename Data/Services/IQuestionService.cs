namespace OnlineLearningApp;

public interface IQuestionService
{
    Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync();
    Task<QuestionDto?> GetQuestionByIdAsync(int id);
    Task AddQuestionAsync(QuestionDto questionDto);
    Task UpdateQuestionAsync(QuestionDto questionDto);
    Task DeleteQuestionAsync(int id);
}
