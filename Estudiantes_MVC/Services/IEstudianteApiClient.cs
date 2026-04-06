using Estudiantes_MVC.Models;

namespace Estudiantes_MVC.Services;

public interface IEstudianteApiClient
{
    Task<IReadOnlyList<EstudianteViewModel>> GetAllAsync();
    Task<EstudianteViewModel?> GetByIdAsync(int id);
    Task<bool> CreateAsync(EstudianteViewModel estudiante);
    Task<bool> UpdateAsync(EstudianteViewModel estudiante);
    Task<bool> DeleteAsync(int id);
}
