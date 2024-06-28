using Microsoft.AspNetCore.Mvc;
using OnlineLearningApp.Data;

namespace OnlineLearningApp.Controllers;

public class UserController : Controller
{
    private readonly OnlineLearningAppDbContext _context;

    public UserController(OnlineLearningAppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var data = _context.Users.ToList();
        return View(data);
    }
}