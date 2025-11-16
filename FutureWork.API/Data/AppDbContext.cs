using Microsoft.EntityFrameworkCore;
using FutureWork.API.Models;

namespace FutureWork.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Competencia> Competencias { get; set; }
        public DbSet<Recomendacao> Recomendacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura a chave composta de Recomendacao
            modelBuilder.Entity<Recomendacao>()
                .HasKey(r => new { r.ProfissionalId, r.VagaId });
        }
    }
}
