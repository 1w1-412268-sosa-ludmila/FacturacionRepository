using AppFacturacion25.Domain.Domain;
using AppFacturacion25.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppFacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _service;

        public FacturasController(FacturaService service)
        {
            _service = service;
        }

        [HttpGet("{nro}")]
        public ActionResult<Factura> GetById(int nro)
        {
            var f = _service.Obtener(nro);
            if (f == null) return NotFound();
            return Ok(f);
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] Factura factura)
        {
            var nro = _service.Crear(factura);
            return CreatedAtAction(nameof(GetById), new { nro = nro }, factura);
        }
    }
}
