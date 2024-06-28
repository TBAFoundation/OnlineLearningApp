using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp.Controllers;

public class QuestionController : Controller
{
    private readonly OnlineLearningAppDbContext _context;

    public QuestionController(OnlineLearningAppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var allQuestions = _context.Questions.ToListAsync();
        return View();
    }
}