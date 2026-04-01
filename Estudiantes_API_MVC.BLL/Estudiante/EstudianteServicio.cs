using Estudiantes_API_MVC.DLL.Repositorio.Estudiante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudiantes_API_MVC.BLL.Estudiante
{
    public class EstudianteServicio : IEstudianteServicio
    {
        private readonly IEstudianteRepositorio _repo;

        public EstudianteServicio(IEstudianteRepositorio repo)
        {
            _repo = repo;
        }

        public IEnumerable<DLL.Entidades.Estudiante> GetEstudiantes()
        {
            return _repo.ObtenerEstudiantes();
        }

        public DLL.Entidades.Estudiante GetEstudiante(int id)
        {
            return _repo.ObtenerEstudiantePorId(id);
        }

        public void AddEstudiante(DLL.Entidades.Estudiante estudiante)
        {
            _repo.AgregarEstudiante(estudiante);
        }

        public void UpdateEstudiante(int id, DLL.Entidades.Estudiante estudiante)
        {
            // Aquí puedes decidir si validas que el id coincida con estudiante.Id
            _repo.ActualizarEstudiante(estudiante);
        }

        public void DeleteEstudiante(int id)
        {
            _repo.EliminarEstudiante(id);
        }
    }

}
