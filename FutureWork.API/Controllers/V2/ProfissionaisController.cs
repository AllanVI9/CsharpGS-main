using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using FutureWork.API.Data;
using FutureWork.API.Models;

namespace FutureWork.API.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class ProfissionaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfissionaisController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profissionais = await _context.Profissionais.ToListAsync();
            return Ok(profissionais);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null) return NotFound();

            return Ok(profissional);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Profissional profissional)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Profissionais.Add(profissional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = profissional.Id }, profissional);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Profissional profissional)
        {
            if (id != profissional.Id)
                return BadRequest("O ID na URL não corresponde ao ID do objeto enviado.");

            var existe = await _context.Profissionais.AnyAsync(p => p.Id == id);
            if (!existe)
                return NotFound();

            _context.Entry(profissional).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null) return NotFound();

            _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
