using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutureWork.API.Models
{
    public class Recomendacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;

        // Chaves estrangeiras
        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; } = null!;

        public int VagaId { get; set; }
        public Vaga Vaga { get; set; } = null!;
    }
}
