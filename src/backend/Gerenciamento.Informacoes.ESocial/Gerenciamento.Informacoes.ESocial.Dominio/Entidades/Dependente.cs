namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

public class Dependente
{   
    public int DependenteId { get; set; }
    public int TrabalhadorId { get; set; }
    public int TipoDependente { get; set; }
    public string? NomeDependente { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? CpfDependente { get; set; }
    public bool EhDepTrabalhadorIrrf { get; set; }
    public bool EhDepIncapaFisMentTrab { get; set; }
    public bool EhDependentePensao { get; set; }
    public string? Responsavel { get; set; }
    public string? TelefoneResponsavel { get; set; }

    public Trabalhador? Trabalhador { get; set; }
}