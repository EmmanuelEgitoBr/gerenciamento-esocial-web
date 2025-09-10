using Gerenciamento.Informacoes.ESocial.Api.Models.Auth;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;

namespace Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<List<UserResponseModel>>> GetAllUsersAsync();
        Task<ApiResponse<List<string>>> GetAllRolesAsync();
        Task<ApiResponse<LoginResponseModel>> LoginAsync(LoginModel model);
        Task<ResponseModel> RegistrarUsuarioAsync(RegisterModel model);
        Task<ApiResponse<TokenModel>> RefreshTokenAsync(TokenModel tokenModel);
        Task<ResponseModel> RevokeAsync(string username);
        Task<ApiResponse<UserResponseModel>> RecoveryUserDataAsync(string? userId, string? email);
        Task LogoutAsync();
        Task<ResponseModel> CreateRoleAsync(string roleName);
        Task<ResponseModel> AddUserToRoleAsync(string userIdentifier, string roleName);
    }
}
