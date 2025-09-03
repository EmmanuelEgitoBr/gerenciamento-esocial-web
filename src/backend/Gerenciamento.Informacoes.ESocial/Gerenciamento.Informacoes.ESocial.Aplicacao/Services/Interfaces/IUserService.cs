using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> FindByCpfAsync(string cpf);
}
