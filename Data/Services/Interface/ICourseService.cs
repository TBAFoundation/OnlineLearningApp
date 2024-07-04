using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public interface ICourseService
{
    Task<Course> GetCourseByIdAsync(int id);
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task CreateCourseAsync(Course course);
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(int id);
}
