using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp.Controllers;
[Authorize(Roles = UserRoles.Admin)]
public class CourseController : Controller
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var allCourses = await _service.GetAllCoursesAsync();
        return View(allCourses);
    }

    public IActionResult Create()
    {
        var dropdowns = new NewCourseDropdownViewModel
        {
            Instructors = _context.Users.ToList(),
            Categories = _context.Categories.Select(c => c.Name).ToList()
        };
        var viewModel = new NewCourseViewModel
        {
            Instructors = dropdowns.Instructors,
            Categories = dropdowns.Categories
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewCourseViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newCourse = new Course
            {
                CourseName = model.CourseName,
                Description = model.Description,
                Category = model.Category,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ImageURL = model.ImageURL,
                InstructorId = model.InstructorId
            };
            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        var dropdowns = new NewCourseDropdownViewModel
        {
            Instructors = _context.Users.ToList(),
            Categories = _context.Categories.Select(c => c.Name).ToList()
        };
        model.Instructors = dropdowns.Instructors;
        model.Categories = dropdowns.Categories;
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        var viewModel = new NewCourseViewModel
        {
            CourseName = course.CourseName,
            Description = course.Description,
            Category = course.Category,
            StartDate = course.StartDate,
            EndDate = course.EndDate,
            ImageURL = course.ImageURL,
            InstructorId = course.InstructorId,
            Instructors = _context.Users.ToList(),
            Categories = _context.Categories.Select(c => c.Name).ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, NewCourseViewModel model)
    {
        if (ModelState.IsValid)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.CourseName = model.CourseName;
            course.Description = model.Description;
            course.Category = model.Category;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            course.ImageURL = model.ImageURL;
            course.InstructorId = model.InstructorId;

            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        var dropdowns = new NewCourseDropdownViewModel
        {
            Instructors = _context.Users.ToList(),
            Categories = _context.Categories.Select(c => c.Name).ToList()
        };
        model.Instructors = dropdowns.Instructors;
        model.Categories = dropdowns.Categories;
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _context.Courses.Include(c => c.Instructor).FirstOrDefaultAsync(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}