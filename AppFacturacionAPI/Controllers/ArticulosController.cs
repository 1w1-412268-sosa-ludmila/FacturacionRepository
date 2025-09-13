using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppFacturacion25.Domain.Services;
using AppFacturacion25.Domain.Domain;

namespace AppFacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly ArticuloService _service;

        public ArticulosController(ArticuloService service)
        {
            _service = service;
        }

        // GET api/articulos
        [HttpGet]
        public ActionResult<List<Articulo>> GetAll()
        {
            return Ok(_service.ObtenerTodos());
        }

        // GET api/articulos/5
        [HttpGet("{id}")]
        public ActionResult<Articulo> GetById(int id)
        {
            var art = _service.ObtenerPorId(id);
            if (art == null) return NotFound();
            return Ok(art);
        }

        // POST api/articulos
        [HttpPost]
        public IActionResult Create([FromBody] Articulo articulo)
        {
            _service.Guardar(articulo);
            return CreatedAtAction(nameof(GetById), new { id = articulo.IdArticulo }, articulo);
        }

        // PUT api/articulos/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Articulo articulo)
        {
            if (id != articulo.IdArticulo) return BadRequest();
            _service.Guardar(articulo);
            return NoContent();
        }

        // DELETE api/articulos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Podés implementar un SP_DELETE_ARTICULO
            return StatusCode(501, "Delete no implementado todavía.");
        }
    }
}

