using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public interface IModuleService
{
    Task<IEnumerable<Module>> GetAllModulesAsync();
    Task<Module?> GetModuleByIdAsync(int id);
    Task AddModuleAsync(Module module);
    Task UpdateModuleAsync(Module module);
    Task DeleteModuleAsync(int id);

}
