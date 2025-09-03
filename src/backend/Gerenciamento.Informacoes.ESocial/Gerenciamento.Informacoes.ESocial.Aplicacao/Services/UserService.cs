using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApplicationUser> FindByCpfAsync(string cpf)
    {
        var user = await _userRepository.GetUserByCpfAsync(cpf);

        return user;
    }
}
