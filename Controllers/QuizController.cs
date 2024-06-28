using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp.Controllers;

public class QuizController : Controller
{
    private readonly OnlineLearningAppDbContext _context;

    public QuizController(OnlineLearningAppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var allQuizzes = _context.Quizzes.ToListAsync();
        return View();
    }
}