using Estudiantes_MVC.Models;
using Estudiantes_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estudiantes_MVC.Controllers;

public class EstudiantesController : Controller
{
    private readonly IEstudianteApiClient _apiClient;

    public EstudiantesController(IEstudianteApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var estudiantes = await _apiClient.GetAllAsync();
            return View(estudiantes);
        }
        catch (ApiUnavailableException ex)
        {
            ViewData["ApiError"] = ex.Message;
            return View(Array.Empty<EstudianteViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var estudiante = await _apiClient.GetByIdAsync(id);
            if (estudiante is null)
            {
                return NotFound();
            }

            return View(estudiante);
        }
        catch (ApiUnavailableException ex)
        {
            TempData["StatusMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new EstudianteViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EstudianteViewModel estudiante)
    {
        if (!ModelState.IsValid)
        {
            return View(estudiante);
        }

        try
        {
            var created = await _apiClient.CreateAsync(estudiante);
            if (!created)
            {
                ModelState.AddModelError(string.Empty, "No se pudo registrar el estudiante en el API.");
                return View(estudiante);
            }

            TempData["StatusMessage"] = "Estudiante registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (ApiUnavailableException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(estudiante);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var estudiante = await _apiClient.GetByIdAsync(id);
            if (estudiante is null)
            {
                return NotFound();
            }

            return View(estudiante);
        }
        catch (ApiUnavailableException ex)
        {
            TempData["StatusMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EstudianteViewModel estudiante)
    {
        if (id != estudiante.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(estudiante);
        }

        try
        {
            var updated = await _apiClient.UpdateAsync(estudiante);
            if (!updated)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el estudiante en el API.");
                return View(estudiante);
            }

            TempData["StatusMessage"] = "Estudiante actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (ApiUnavailableException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(estudiante);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var estudiante = await _apiClient.GetByIdAsync(id);
            if (estudiante is null)
            {
                return NotFound();
            }

            return View(estudiante);
        }
        catch (ApiUnavailableException ex)
        {
            TempData["StatusMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var deleted = await _apiClient.DeleteAsync(id);
            if (!deleted)
            {
                TempData["StatusMessage"] = "No se pudo eliminar el estudiante.";
                return RedirectToAction(nameof(Index));
            }

            TempData["StatusMessage"] = "Estudiante eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (ApiUnavailableException ex)
        {
            TempData["StatusMessage"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
