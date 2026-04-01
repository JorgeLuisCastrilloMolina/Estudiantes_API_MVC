using Estudiantes_API_MVC.DLL.Data;
using Microsoft.EntityFrameworkCore;
using Estudiantes_API_MVC.DLL.Repositorio.Estudiante;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace Estudiantes_API_MVC.DLL.Repositoio.Estudiante
{
    public class EstudianteRepositorio : IEstudianteRepositorio
    {
        private readonly EstudiantesDbContext _context;

        public EstudianteRepositorio(EstudiantesDbContext context)
        {
            _context = context;
        }

        public bool AgregarEstudiante(Entidades.Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            return _context.SaveChanges() > 0;
        }

        public bool ActualizarEstudiante(Entidades.Estudiante estudiante)
        {
            var existing = _context.Estudiantes.Find(estudiante.Id);
            if (existing == null) return false;

            existing.Nombre = estudiante.Nombre;
            existing.Email = estudiante.Email;

            _context.Estudiantes.Update(existing);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarEstudiante(int id)
        {
            var existing = _context.Estudiantes.Find(id);
            if (existing == null) return false;

            _context.Estudiantes.Remove(existing);
            return _context.SaveChanges() > 0;
        }

        public Entidades.Estudiante ObtenerEstudiantePorId(int id)
        {
            return _context.Estudiantes
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }

        public List<Entidades.Estudiante> ObtenerEstudiantes()
        {
            return _context.Estudiantes
                .AsNoTracking()
                .ToList();
        }
    }

}
