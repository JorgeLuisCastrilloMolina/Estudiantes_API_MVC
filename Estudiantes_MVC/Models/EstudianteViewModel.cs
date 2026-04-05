using System.ComponentModel.DataAnnotations;

namespace Estudiantes_MVC.Models;

public class EstudianteViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nombre requerido")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email requerido")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;
}
