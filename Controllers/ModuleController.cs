using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;

namespace OnlineLearningApp.Controllers;
public class ModuleController : Controller
{
    private readonly OnlineLearningAppDbContext _context;

    public ModuleController(OnlineLearningAppDbContext context)
    {
        _context = context;
    }
    public async Task <IActionResult> Index()
    {
        var modules = _context.Modules.ToListAsync();
        return View();
    }
}