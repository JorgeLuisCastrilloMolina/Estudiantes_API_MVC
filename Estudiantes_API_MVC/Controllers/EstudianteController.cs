using Microsoft.AspNetCore.Mvc;
using Estudiantes_API_MVC.BLL.Estudiante;
using Estudiantes_API_MVC.DLL.Entidades;

namespace Estudiantes_API_MVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteServicio _servicio;

        public EstudiantesController(IEstudianteServicio servicio)
        {
            _servicio = servicio;
        }

        // GET: /Estudiantes
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Estudiante>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Estudiante>> GetEstudiantes()
        {
            var estudiantes = _servicio.GetEstudiantes();
            return Ok(estudiantes);
        }

        // GET: /Estudiantes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Estudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Estudiante> GetEstudiante(int id)
        {
            var estudiante = _servicio.GetEstudiante(id);
            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        // POST: /Estudiantes
        [HttpPost]
        [ProducesResponseType(typeof(Estudiante), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Estudiante> AddEstudiante([FromBody] Estudiante estudiante)
        {
            if (estudiante == null)
                return BadRequest();

            _servicio.AddEstudiante(estudiante);
            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudiante);
        }

        // PUT: /Estudiantes/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Estudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Estudiante> UpdateEstudiante(int id, [FromBody] Estudiante estudiante)
        {
            if (estudiante == null || estudiante.Id != id)
                return BadRequest();

            var existing = _servicio.GetEstudiante(id);
            if (existing == null)
                return NotFound();

            _servicio.UpdateEstudiante(id, estudiante);
            return Ok(estudiante);
        }

        // DELETE: /Estudiantes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteEstudiante(int id)
        {
            var existing = _servicio.GetEstudiante(id);
            if (existing == null)
                return NotFound();

            _servicio.DeleteEstudiante(id);
            return Ok();
        }
    }
}