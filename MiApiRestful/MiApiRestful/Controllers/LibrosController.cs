using MiApiRestful.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiApiRestful.Controllers
{
    // Define la ruta base para los endpoints de la API
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        // Lista estática de libros para probar, sin base de datos
        private static List<Libro> libros = new List<Libro>
        {
            new Libro { Id = 1, Titulo = "El Quijote", Autor = "Miguel de Cervantes", Año = 1605 },
            new Libro { Id = 2, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", Año = 1967 }
        };

        // GET: api/libros
        [HttpGet]
        public ActionResult<List<Libro>> Get()
        {
            return Ok(libros);  // Devuelve la lista de libros
        }

        // GET: api/libros/1
        [HttpGet("{id}")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);  // Busca el libro por su ID
            if (libro == null)
            {
                return NotFound();  // Si no se encuentra el libro, devuelve NotFound
            }
            return Ok(libro);  // Si lo encuentra, devuelve el libro
        }

        // POST: api/libros
        [HttpPost]
        public ActionResult<Libro> Post([FromBody] Libro libro)
        {
            libro.Id = libros.Max(l => l.Id) + 1;  // Asigna un nuevo ID al libro
            libros.Add(libro);  // Agrega el libro a la lista
            return CreatedAtAction(nameof(Get), new { id = libro.Id }, libro);  // Devuelve el libro creado
        }

        // PUT: api/libros/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            var libroExistente = libros.FirstOrDefault(l => l.Id == id);  // Busca el libro por ID
            if (libroExistente == null)
            {
                return NotFound();  // Si no existe, devuelve NotFound
            }

            // Actualiza el libro
            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.Año = libro.Año;

            return NoContent();  // Respuesta exitosa sin contenido
        }

        // DELETE: api/libros/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);  // Busca el libro por ID
            if (libro == null)
            {
                return NotFound();  // Si no existe, devuelve NotFound
            }

            libros.Remove(libro);  // Elimina el libro
            return NoContent();  // Respuesta exitosa sin contenido
        }
    }
}