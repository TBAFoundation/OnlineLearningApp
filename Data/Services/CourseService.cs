using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp;

public class CourseService : ICourseService
{
    private readonly OnlineLearningAppDbContext _context;

    public CourseService(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Modules)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task AddCourseAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
