using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;
using System.Diagnostics;

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

        // Check if user exists by UserName
        var user = await _userManager.FindByNameAsync(loginVM.Email);
        if (user == null)
        {
            TempData["Error"] = "User not found. Please, try again!";
            return View(loginVM);
        }

        // Attempt to sign in using the email (assuming username is the email)
        var result = await _signInManager.PasswordSignInAsync(user.UserName, loginVM.Password, false, false);
        if (result.Succeeded)
        {
            // Redirect based on user role
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index", "Course");
            }
            else if (await _userManager.IsInRoleAsync(user, "Instructor"))
            {
                return RedirectToAction("InstructorDashboard", "Instructor");
            }
            else if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                return RedirectToAction("StudentDashboard", "Student");
            }
        }
        else
        {
            TempData["Error"] = "Wrong credentials. Please, try again!";
            Debug.WriteLine($"Login failed for user {loginVM.Email}. Result: {result}");
        }

        return View(loginVM);
    }

    // Register View
    public IActionResult Register() => View(new RegisterViewModel());

    // Register POST action
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVM)
    {
        if (!ModelState.IsValid) return View(registerVM);

        // Check if email is already registered
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
