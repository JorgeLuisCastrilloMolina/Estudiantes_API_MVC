using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiantes_API_MVC.DLL.Entidades
{

        public record Estudiante
    {
        public int Id { get; set; }             

        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }       

        [Required(ErrorMessage = "Email requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }        
    }
}

