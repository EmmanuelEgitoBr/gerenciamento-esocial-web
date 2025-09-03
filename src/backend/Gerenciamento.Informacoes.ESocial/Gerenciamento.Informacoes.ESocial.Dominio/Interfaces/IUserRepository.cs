using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByCpfAsync(string cpf);
}
