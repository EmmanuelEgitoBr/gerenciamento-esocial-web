using Gerenciamento.Informacoes.ESocial.Dominio.Enums;

namespace Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;

public class Endereco
{
    public int Id { get; set; }
    public TipoLogradouro? TipoLogradouro { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? CEP { get; set; }
    public int MunicipioId { get; set; }
    public string? Uf { get; set; }
}
