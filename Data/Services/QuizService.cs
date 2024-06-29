using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class QuizService : IQuizService
{
    private readonly OnlineLearningAppDbContext _context;

    public QuizService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
    {
        return await _context.Quizzes
            .Select(q => new QuizDto
            {
                QuizId = q.QuizId,
                QuizName = q.QuizName,
                Description = q.Description,
                DateCreated = q.DateCreated,
                ModuleId = q.ModuleId,
                Questions = q.Questions.Select(q => new QuestionDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    QuizId = q.QuizId
                }).ToList()
            }).ToListAsync();
    }

    public async Task<QuizDto?> GetQuizByIdAsync(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null) return null;

        return new QuizDto
        {
            QuizId = quiz.QuizId,
            QuizName = quiz.QuizName,
            Description = quiz.Description,
            DateCreated = quiz.DateCreated,
            ModuleId = quiz.ModuleId,
            Questions = quiz.Questions.Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                QuizId = q.QuizId
            }).ToList()
        };
    }

    public async Task AddQuizAsync(QuizDto quizDto)
    {
        var quiz = new Quiz
        {
            QuizName = quizDto.QuizName,
            Description = quizDto.Description,
            DateCreated = quizDto.DateCreated,
            ModuleId = quizDto.ModuleId
        };

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuizAsync(QuizDto quizDto)
    {
        var quiz = await _context.Quizzes.FindAsync(quizDto.QuizId);
        if (quiz == null) return;

        quiz.QuizName = quizDto.QuizName;
        quiz.Description = quizDto.Description;
        quiz.DateCreated = quizDto.DateCreated;
        quiz.ModuleId = quizDto.ModuleId;

        _context.Quizzes.Update(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteQuizAsync(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz != null)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }
    }
}
