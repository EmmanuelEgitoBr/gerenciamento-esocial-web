using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

public class Trabalhador
{
    public int TrabalhadorId { get; set; }
    public int Tipo { get; set; }
    public string? Nome { get; set; }
    public int SexoId { get; set; }
    public int RacaCorId { get; set; }
    public int EstadoCivilId { get; set; }
    public int GrauInstrucaoId { get; set; }
    public bool IsPrimeiroEmprego { get; set; }
    public string? CodigoNomeTravTrans { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? MunicipioNascimento { get; set; }
    public string? UfNascimento { get; set; }
    public string? PaisNascimento { get; set; }
    public string? Nacionalidade { get; set; }
    public string? NomeMae { get; set; }
    public string? NomePai { get; set; }
    public Documento? DocumentosPessoais { get; set; }
    public Endereco? EnderecoResidencial { get; set; }
    public Deficiencia? DadosDeficiencia { get; set; }
    public bool RecebeBeneficioPrevidencia { get; set; }
    public Contato? Contato { get; set; }
    public bool IsVerificado { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public DateTime? DataUltimaAtualizacao { get; set; }
    public  ICollection<Dependente>? Dependentes { get; set; }
    public ICollection<Estagiario>? Estagiarios { get; set; }
    public ICollection<Cedido>? Cedidos { get; set; }
}
