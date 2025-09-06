using Gerenciamento.Informacoes.ESocial.Api.Models.Auth;
using Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Api.Utils;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Gerenciamento.Informacoes.ESocial.Api.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthService(ILogger<AuthService> logger,
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IUserService userService)
    {
        _logger = logger;
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<ApiResponse<LoginResponseModel>> LoginAsync(LoginModel model)
    {
        var user = await GetUserByInputType(model.InputValue!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
            var oi = token.ValidTo;
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                                out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            var responseModel = new LoginResponseModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

            return new ApiResponse<LoginResponseModel> 
            { 
                Success = true,
                Result = responseModel
            };
        }

        return new ApiResponse<LoginResponseModel>
        {
            Success = false,
            ErrorMessage = HttpStatusCode.Unauthorized.ToString()
        };
    }

    public async Task<ResponseModel> RegistrarUsuarioAsync(RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName!);

        if (userExists != null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Usuário já existe!"
            };
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            Cpf = model.Cpf
        };
        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Status = "Error",
                Message = "Falha na criação de usuário!"
            };
        }

        return new ResponseModel
        {
            IsSuccess = true,
            Status = "Success",
            Message = "Usuário criado com sucesso!"
        };
    }

    public async Task<ApiResponse<TokenModel>> RefreshTokenAsync(TokenModel tokenModel)
    {
        if (tokenModel is null) return new ApiResponse<TokenModel>
        {
            Success = false,
            ErrorMessage = "Invalid client request"
        };

        string? accessToken = tokenModel.AccessToken
            ?? throw new ArgumentNullException(nameof(tokenModel));

        string? refreshToken = tokenModel.RefreshToken
            ?? throw new ArgumentNullException(nameof(tokenModel));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

        if (principal != null) return new ApiResponse<TokenModel>
        {
            Success = false,
            ErrorMessage = "Invalid access/refresh token"
        };
        
        string userName = principal!.Identity!.Name!;

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null
            || user.RefreshToken != refreshToken
            || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return new ApiResponse<TokenModel>
            {
                Success = false,
                ErrorMessage = "Invalid access/refresh token"
            };
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        var newTokenModel = new TokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
        };

        return new ApiResponse<TokenModel>
        {
            Success = true,
            Result = newTokenModel
        };
    }

    public async Task<ResponseModel> RevokeAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return new ResponseModel
        {
            IsSuccess = false,
            Message = "Invalid user name"
        };

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return new ResponseModel
        {
            IsSuccess = true
        };
    }

    public async Task<ResponseModel> CreateRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles added!");
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = "Success",
                    Message = $"Role {roleName} added successfully"
                };
            }
            else
            {
                _logger.LogInformation(2, "Error");
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = "Error",
                    Message = $"Error to add the role {roleName}"
                };
            }
        }
        return new ResponseModel
        {
            IsSuccess = false,
            Status = "Error",
            Message = $"The role {roleName} already exists"
        };
    }

    public async Task<ResponseModel> AddUserToRoleAsync(string userIdentifier, string roleName)
    {
        var user = await GetUserByInputType(userIdentifier);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"Role {roleName} added to user {user.UserName} successfully");
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = "Success",
                    Message = $"Role {roleName} added to user {user.UserName} successfully"
                };
            }
            else
            {
                _logger.LogInformation(1, $"Unable to add the role {roleName} to user {user.UserName}");
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = "Error",
                    Message = $"Unable to add the role {roleName} to user {user.UserName}"
                };
            }
        }
        return new ResponseModel
        {
            IsSuccess = false,
            Status = "Error",
            Message = $"Unable to find the user"
        };
    }

#region Métodos privados

    private async Task<ApplicationUser> GetUserByInputType(string input)
    {
        if (InputValidationUtils.IsValidEmail(input))
        {
            return await _userManager.FindByEmailAsync(input);

        }
        else if (InputValidationUtils.IsValidCpf(input))
        {
            return await _userService.FindByCpfAsync(input);
        }

        return await _userManager.FindByNameAsync(input);
    }

#endregion
}
