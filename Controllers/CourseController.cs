using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp.Controllers;
public class CourseController : Controller
{
    private readonly OnlineLearningAppDbContext _context;

    public CourseController(OnlineLearningAppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var allCourses = _context.Courses.ToListAsync();
        return View(allCourses);
    }
}