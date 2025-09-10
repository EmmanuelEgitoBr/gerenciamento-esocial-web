using Gerenciamento.Informacoes.ESocial.Api.Models.Auth;
using Gerenciamento.Informacoes.ESocial.Api.Resources;
using Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Api.Utils;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthService(ILogger<AuthService> logger,
        ITokenService tokenService,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        IUserService userService)
    {
        _logger = logger;
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<ApiResponse<List<UserResponseModel>>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userManager.Users.ToListAsync();
            var userModels = new List<UserResponseModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserResponseModel model = new()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles,
                    CreatedAt = user.CreatedAt
                };
                userModels.Add(model);
            }

            return new ApiResponse<List<UserResponseModel>>
            {
                Success = true,
                Result = userModels
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<UserResponseModel>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<List<string>>> GetAllRolesAsync()
    {
        try
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleNames = roles.Select(r => r.Name!).ToList();
            return new ApiResponse<List<string>>
            {
                Success = true,
                Result = roleNames
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<string>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<ApiResponse<LoginResponseModel>> LoginAsync(LoginModel model)
    {
        var user = await GetUserByInputType(model.InputValue!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                                out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            var responseModel = new LoginResponseModel
            {
                UserId = user.Id,
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
                Message = ResponseModelConstants.UserAlreadyExists
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
                Status = ResponseModelConstants.Error,
                Message = ResponseModelConstants.UserNotCraeted
            };
        }

        return new ResponseModel
        {
            IsSuccess = true,
            Status = ResponseModelConstants.Success,
            Message = ResponseModelConstants.UserCreated
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
            ErrorMessage = ResponseModelConstants.InvalidToken
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
                ErrorMessage = ResponseModelConstants.InvalidToken
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
            Message = ResponseModelConstants.InvalidCredentials
        };

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return new ResponseModel
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResponse<UserResponseModel>> RecoveryUserDataAsync(string? userId, string? email)
    {
        // Busca roles do Identity
        var user = await _userManager.FindByIdAsync(userId!);
        var roles = await _userManager.GetRolesAsync(user!);

        UserResponseModel model = new()
        {
            UserId = userId,
            Email = email,
            Roles = roles
        };

        return new ApiResponse<UserResponseModel>
        {
            Success = true,
            Result = model
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ResponseModel> CreateRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, ResponseModelConstants.RoleCreated);
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseModelConstants.Success,
                    Message = ResponseModelConstants.RoleCreated
                };
            }
            else
            {
                _logger.LogInformation(2, ResponseModelConstants.Error);
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = ResponseModelConstants.Error,
                    Message = ResponseModelConstants.RoleNotCreated
                };
            }
        }
        return new ResponseModel
        {
            IsSuccess = false,
            Status = ResponseModelConstants.Error,
            Message = ResponseModelConstants.RoleAlreadyExists
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
                _logger.LogInformation(1, ResponseModelConstants.RoleAdded(roleName, user.UserName!));
                return new ResponseModel
                {
                    IsSuccess = true,
                    Status = ResponseModelConstants.Success,
                    Message = ResponseModelConstants.RoleAdded(roleName, user.UserName!)
                };
            }
            else
            {
                _logger.LogInformation(1, ResponseModelConstants.ErrorAddingRole(roleName, user.UserName!));
                return new ResponseModel
                {
                    IsSuccess = false,
                    Status = ResponseModelConstants.Error,
                    Message = ResponseModelConstants.ErrorAddingRole(roleName, user.UserName!)
                };
            }
        }
        return new ResponseModel
        {
            IsSuccess = false,
            Status = ResponseModelConstants.Error,
            Message = ResponseModelConstants.UserNotFound
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
