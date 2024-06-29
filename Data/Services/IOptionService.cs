namespace OnlineLearningApp;

public interface IOptionService
{
    Task<IEnumerable<OptionDto>> GetAllOptionsAsync();
    Task<OptionDto?> GetOptionByIdAsync(int id);
    Task AddOptionAsync(OptionDto optionDto);
    Task UpdateOptionAsync(OptionDto optionDto);
    Task DeleteOptionAsync(int id);
}
