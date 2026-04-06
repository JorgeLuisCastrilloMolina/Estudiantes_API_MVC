using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiantes_API_MVC.BLL.Estudiante
{
    public interface IEstudianteServicio
    {
        IEnumerable<DLL.Entidades.Estudiante> GetEstudiantes();
        DLL.Entidades.Estudiante? GetEstudiante(int id);
        void AddEstudiante(DLL.Entidades.Estudiante estudiante);
        void UpdateEstudiante(int id, DLL.Entidades.Estudiante estudiante);
        void DeleteEstudiante(int id);
    }

}
