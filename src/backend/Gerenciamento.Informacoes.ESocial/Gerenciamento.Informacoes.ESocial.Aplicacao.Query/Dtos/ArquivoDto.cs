namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;

public class ArquivoDto
{
    public int ArquivoId { get; set; }
    public int? TrabalhadorId { get; set; }
    public string? Tipo { get; set; }
    public string? NomeArquivo { get; set; }
    public long Tamanho { get; set; }
    public byte[] Dados { get; set; }
    public DateTime DataUpload { get; set; }
}
