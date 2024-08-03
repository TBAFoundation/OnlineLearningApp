using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly OnlineLearningAppDbContext _context;

    public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, OnlineLearningAppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    // Display the list of users
    public async Task<IActionResult> Users()
    {
        var users = await _context.Accounts.ToListAsync();
        return View(users);
    }

    // Login View
    public IActionResult SignIn() => View(new LoginViewModel());

    // Login POST action
    [HttpPost]
    public async Task<IActionResult> SignIn(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid) return View(loginVM);

        var user = await _userManager.FindByEmailAsync(loginVM.Email);
        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Course");
                }
            }
            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        TempData["Error"] = "Wrong credentials. Please, try again!";
        return View(loginVM);
    }

    // Register View
    public IActionResult Register() => View(new RegisterViewModel());

    // Register POST action
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVM)
    {
        if (!ModelState.IsValid) return View(registerVM);

        var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(registerVM);
        }

        var newUser = new Account()
        {
            FullName = registerVM.FullName,
            Email = registerVM.EmailAddress,
            UserName = registerVM.EmailAddress
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

        if (newUserResponse.Succeeded)
        {
            var roles = new List<string> { UserRoles.Admin, UserRoles.Instructor, UserRoles.Student };
            await _userManager.AddToRolesAsync(newUser, roles);

            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return RedirectToAction("RegisterCompleted");
        }

        foreach (var error in newUserResponse.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(registerVM);
    }

    // Logout POST action
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Course");
    }

    // Registration completed view
    public IActionResult RegisterCompleted() => View();

    // Access denied view
    public IActionResult AccessDenied() => View();
}
