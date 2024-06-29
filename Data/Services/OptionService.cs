using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class OptionService : IOptionService
{
    private readonly OnlineLearningAppDbContext _context;

    public OptionService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OptionDto>> GetAllOptionsAsync()
    {
        return await _context.Options
            .Select(o => new OptionDto
            {
                OptionId = o.OptionId,
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                QuestionId = o.QuestionId
            }).ToListAsync();
    }

    public async Task<OptionDto?> GetOptionByIdAsync(int id)
    {
        var option = await _context.Options.FindAsync(id);
        if (option == null) return null;

        return new OptionDto
        {
            OptionId = option.OptionId,
            OptionText = option.OptionText,
            IsCorrect = option.IsCorrect,
            QuestionId = option.QuestionId
        };
    }

    public async Task AddOptionAsync(OptionDto optionDto)
    {
        var option = new Option
        {
            OptionText = optionDto.OptionText,
            IsCorrect = optionDto.IsCorrect,
            QuestionId = optionDto.QuestionId
        };

        _context.Options.Add(option);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOptionAsync(OptionDto optionDto)
    {
        var option = await _context.Options.FindAsync(optionDto.OptionId);
        if (option == null) return;

        option.OptionText = optionDto.OptionText;
        option.IsCorrect = optionDto.IsCorrect;

        _context.Options.Update(option);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOptionAsync(int id)
    {
        var option = await _context.Options.FindAsync(id);
        if (option != null)
        {
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
        }
    }
}
