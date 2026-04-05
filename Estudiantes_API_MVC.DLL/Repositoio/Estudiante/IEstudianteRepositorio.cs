using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estudiantes_API_MVC.DLL.Entidades;


namespace Estudiantes_API_MVC.DLL.Repositorio.Estudiante

{
    public interface IEstudianteRepositorio
    {
        List<Entidades.Estudiante> ObtenerEstudiantes();
        Entidades.Estudiante? ObtenerEstudiantePorId(int id);
        bool AgregarEstudiante(Entidades.Estudiante estudiante);
        bool ActualizarEstudiante(Entidades.Estudiante estudiante);
        bool EliminarEstudiante(int id);
    }


}
