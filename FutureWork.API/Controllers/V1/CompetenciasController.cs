using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using FutureWork.API.Data;
using FutureWork.API.Models;

namespace FutureWork.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CompetenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompetenciasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var competencias = await _context.Competencias.ToListAsync();
            return Ok(competencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var competencia = await _context.Competencias.FindAsync(id);
            if (competencia == null) return NotFound();

            return Ok(competencia);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Competencia competencia)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Competencias.Add(competencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = competencia.Id }, competencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Competencia competencia)
        {
            if (id != competencia.Id)
                return BadRequest("ID da URL não corresponde ao ID do objeto.");

            var existe = await _context.Competencias.AnyAsync(c => c.Id == id);
            if (!existe) return NotFound();

            _context.Entry(competencia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var competencia = await _context.Competencias.FindAsync(id);
            if (competencia == null) return NotFound();

            _context.Competencias.Remove(competencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
