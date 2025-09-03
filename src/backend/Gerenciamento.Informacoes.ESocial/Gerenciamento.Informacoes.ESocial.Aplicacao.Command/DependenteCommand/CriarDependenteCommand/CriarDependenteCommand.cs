using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.CriarDependenteCommand;

public class CriarDependenteCommand : IRequest<ApiResponse<int>>
{
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
}
