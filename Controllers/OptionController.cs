using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp.Controllers;
public class OptionController : Controller
{
    private readonly OnlineLearningAppDbContext _context;

    public OptionController(OnlineLearningAppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var allOptions = await _context.Options.ToListAsync();
        return View(allOptions);
    }
}