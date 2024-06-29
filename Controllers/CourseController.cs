using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Models;

namespace OnlineLearningApp.Controllers;
public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("CourseId,CourseName,Description,Category,StartDate,EndDate,ImageURL,InstructorId")] Course course)
    {
        if (ModelState.IsValid)
        {
            await _courseService.AddCourseAsync(course);
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,Description,Category,StartDate,EndDate,ImageURL,InstructorId")] Course course)
    {
        if (id != course.CourseId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _courseService.UpdateCourseAsync(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingCourse = await _courseService.GetCourseByIdAsync(course.CourseId);
                if (existingCourse == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _courseService.DeleteCourseAsync(id);
        return RedirectToAction(nameof(Index));
    }
}