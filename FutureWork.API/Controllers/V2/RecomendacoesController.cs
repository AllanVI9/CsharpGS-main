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
    public class RecomendacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecomendacoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recomendacoes = await _context.Recomendacoes
                .Include(r => r.Profissional)
                .Include(r => r.Vaga)
                .ToListAsync();

            return Ok(recomendacoes);
        }

        [HttpGet("{profissionalId:int}/{vagaId:int}")]
        public async Task<IActionResult> GetByIds(int profissionalId, int vagaId)
        {
            var recomendacao = await _context.Recomendacoes
                .Include(r => r.Profissional)
                .Include(r => r.Vaga)
                .FirstOrDefaultAsync(r =>
                    r.ProfissionalId == profissionalId && r.VagaId == vagaId);

            if (recomendacao == null)
                return NotFound();

            return Ok(recomendacao);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Recomendacao recomendacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profissionalExiste = await _context.Profissionais.AnyAsync(p => p.Id == recomendacao.ProfissionalId);
            var vagaExiste = await _context.Vagas.AnyAsync(v => v.Id == recomendacao.VagaId);

            if (!profissionalExiste)
                return BadRequest($"Profissional com ID {recomendacao.ProfissionalId} não existe.");

            if (!vagaExiste)
                return BadRequest($"Vaga com ID {recomendacao.VagaId} não existe.");

            _context.Recomendacoes.Add(recomendacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIds),
                new { profissionalId = recomendacao.ProfissionalId, vagaId = recomendacao.VagaId },
                recomendacao);
        }

        [HttpPut("{profissionalId:int}/{vagaId:int}")]
        public async Task<IActionResult> Put(int profissionalId, int vagaId, [FromBody] Recomendacao recomendacao)
        {
            if (profissionalId != recomendacao.ProfissionalId || vagaId != recomendacao.VagaId)
                return BadRequest("IDs na URL não correspondem ao corpo da requisição.");

            var existente = await _context.Recomendacoes
                .AnyAsync(r => r.ProfissionalId == profissionalId && r.VagaId == vagaId);

            if (!existente)
                return NotFound();

            _context.Entry(recomendacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{profissionalId:int}/{vagaId:int}")]
        public async Task<IActionResult> Delete(int profissionalId, int vagaId)
        {
            var recomendacao = await _context.Recomendacoes
                .FindAsync(profissionalId, vagaId);

            if (recomendacao == null)
                return NotFound();

            _context.Recomendacoes.Remove(recomendacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
