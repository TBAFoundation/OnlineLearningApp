using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;
public class QuestionService : IQuestionService
{
    private readonly OnlineLearningAppDbContext _context;

    public QuestionService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync()
    {
        return await _context.Questions
            .Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                QuizId = q.QuizId,
                Options = q.Options.Select(o => new OptionDto
                {
                    OptionId = o.OptionId,
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect,
                    QuestionId = o.QuestionId
                }).ToList()
            }).ToListAsync();
    }

    public async Task<QuestionDto?> GetQuestionByIdAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null) return null;

        return new QuestionDto
        {
            QuestionId = question.QuestionId,
            QuestionText = question.QuestionText,
            QuestionType = question.QuestionType,
            QuizId = question.QuizId,
            Options = question.Options.Select(o => new OptionDto
            {
                OptionId = o.OptionId,
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                QuestionId = o.QuestionId
            }).ToList()
        };
    }

    public async Task AddQuestionAsync(QuestionDto questionDto)
    {
        var question = new Question
        {
            QuestionText = questionDto.QuestionText,
            QuestionType = questionDto.QuestionType,
            QuizId = questionDto.QuizId
        };

        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        foreach (var optionDto in questionDto.Options)
        {
            var option = new Option
            {
                OptionText = optionDto.OptionText,
                IsCorrect = optionDto.IsCorrect,
                QuestionId = question.QuestionId
            };
            _context.Options.Add(option);
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdateQuestionAsync(QuestionDto questionDto)
    {
        var question = await _context.Questions.FindAsync(questionDto.QuestionId);
        if (question == null) return;

        question.QuestionText = questionDto.QuestionText;
        question.QuestionType = questionDto.QuestionType;
        question.QuizId = questionDto.QuizId;

        _context.Questions.Update(question);
        await _context.SaveChangesAsync();

        foreach (var optionDto in questionDto.Options)
        {
            var option = await _context.Options.FindAsync(optionDto.OptionId);
            if (option == null)
            {
                option = new Option
                {
                    OptionText = optionDto.OptionText,
                    IsCorrect = optionDto.IsCorrect,
                    QuestionId = question.QuestionId
                };
                _context.Options.Add(option);
            }
            else
            {
                option.OptionText = optionDto.OptionText;
                option.IsCorrect = optionDto.IsCorrect;
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteQuestionAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}