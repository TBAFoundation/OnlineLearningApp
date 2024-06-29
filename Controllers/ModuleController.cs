using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningApp.Data;
using OnlineLearningApp.Models;

namespace OnlineLearningApp.Controllers;
public class ModuleController : Controller
{
    private readonly IModuleService _moduleService;

    public ModuleController(IModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    public async Task<IActionResult> Index()
    {
        var modules = await _moduleService.GetAllModulesAsync();
        return View(modules);
    }

    public async Task<IActionResult> Details(int id)
    {
        var module = await _moduleService.GetModuleByIdAsync(id);
        if (module == null)
        {
            return NotFound();
        }
        return View(module);
    }

    [Authorize(Roles = "Admin,Instructor")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> Create([Bind("ModuleId,ModuleName,Content,CourseId")] Module module)
    {
        if (ModelState.IsValid)
        {
            await _moduleService.AddModuleAsync(module);
            return RedirectToAction(nameof(Index));
        }
        return View(module);
    }

    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> Edit(int id)
    {
        var module = await _moduleService.GetModuleByIdAsync(id);
        if (module == null)
        {
            return NotFound();
        }
        return View(module);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> Edit(int id, [Bind("ModuleId,ModuleName,Content,CourseId")] Module module)
    {
        if (id != module.ModuleId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _moduleService.UpdateModuleAsync(module);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingModule = await _moduleService.GetModuleByIdAsync(module.ModuleId);
                if (existingModule == null)
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
        return View(module);
    }

    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> Delete(int id)
    {
        var module = await _moduleService.GetModuleByIdAsync(id);
        if (module == null)
        {
            return NotFound();
        }
        return View(module);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _moduleService.DeleteModuleAsync(id);
        return RedirectToAction(nameof(Index));
    }
}