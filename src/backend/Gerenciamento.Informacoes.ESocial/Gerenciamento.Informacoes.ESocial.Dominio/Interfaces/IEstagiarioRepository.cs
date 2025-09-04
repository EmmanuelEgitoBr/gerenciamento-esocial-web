using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Base;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;

public interface IEstagiarioRepository : IBaseRepository<Estagiario>
{
    Task<Estagiario> GetEstagiarioByTrabalhadorIdAsync(int trabalhadorId);
}
