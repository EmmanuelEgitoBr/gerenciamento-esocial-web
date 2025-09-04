namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

public class Arquivo
{
    public int ArquivoId { get; set; }
    public int? TrabalhadorId { get; set; }
    public string? Tipo { get; set; }
    public string? NomeArquivo { get; set; }
    public long Tamanho { get; set; }
    public byte[] Dados { get; set; } = Array.Empty<byte>();
    public DateTime DataUpload { get; set; } = DateTime.Now;
}
