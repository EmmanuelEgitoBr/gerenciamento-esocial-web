using System.ComponentModel.DataAnnotations;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Arquivo
    {
        [Key]
        public int ArquivoId { get; set; }
        public int? TrabalhadorId { get; set; }
        public string? Tipo { get; set; }
        public string? NomeArquivo { get; set; }
        
    }
}
