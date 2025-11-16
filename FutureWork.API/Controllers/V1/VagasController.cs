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
    public class VagasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VagasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vagas = await _context.Vagas.ToListAsync();
            return Ok(vagas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null) return NotFound();

            return Ok(vaga);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vaga vaga)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = vaga.Id }, vaga);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Vaga vaga)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != vaga.Id)
                return BadRequest("O ID da URL não corresponde ao ID do objeto enviado.");

            var existe = await _context.Vagas.AnyAsync(v => v.Id == id);
            if (!existe)
                return NotFound();

            _context.Entry(vaga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Erro de concorrência ao atualizar a vaga.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
                return NotFound();

            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
