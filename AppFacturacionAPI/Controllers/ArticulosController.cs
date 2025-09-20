using Microsoft.AspNetCore.Mvc;
using AppFacturacion25.Domain.Interfaces;
using AppFacturacionAPI.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ArticulosController : ControllerBase
{
    private readonly IRepository<Articulo> _repo;

    public ArticulosController(IRepository<Articulo> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articulos = await _repo.GetAllAsync();
        return Ok(articulos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var art = await _repo.GetByIdAsync(id);
        if (art == null) return NotFound();
        return Ok(art);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Articulo articulo)
    {
        await _repo.AddAsync(articulo);
        return CreatedAtAction(nameof(GetById), new { id = articulo.IdArticulo }, articulo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Articulo articulo)
    {
        if (id != articulo.IdArticulo) return BadRequest();
        await _repo.UpdateAsync(articulo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}