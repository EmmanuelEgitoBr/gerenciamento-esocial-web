﻿using Gerenciamento.Informacoes.ESocial.Api.Models.Auth;
using Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Api.Utils;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(ILogger<AuthController> logger,
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

    #region Authentication

    /// <summary>
    /// Endpoint para o Login do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
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

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                                out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    /// <summary>
    /// Endpoint para registro de um novo funcionário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName!);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                new ResponseModel
                                {
                                    Status = "Error",
                                    Message = "Usuário já existe!"
                                });
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
            return StatusCode(StatusCodes.Status500InternalServerError,
                                new ResponseModel
                                {
                                    Status = "Error",
                                    Message = "Falha na criação de usuário!"
                                });
        }

        return Ok(new ResponseModel
        {
            Status = "Success",
            Message = "Usuário criado com sucesso!"
        });
    }

    /// <summary>
    /// Endpoint para geração do refresh token
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("Refresh-Token")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {
        if (tokenModel is null) { return BadRequest("Invalid client request"); }

        string? accessToken = tokenModel.AccessToken
            ?? throw new ArgumentNullException(nameof(tokenModel));

        string? refreshToken = tokenModel.RefreshToken
            ?? throw new ArgumentNullException(nameof(tokenModel));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

        if (principal != null) { return BadRequest("Invalid access/refresh token"); }

        string userName = principal!.Identity!.Name!;

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null
            || user.RefreshToken != refreshToken
            || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access/refresh token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken,
        });
    }

    /// <summary>
    /// Endpoint para revogar token
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("Revoke/{username}")]
    [Authorize(Policy = "ExclusiveOnly")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) { return BadRequest("Invalid user name"); }

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
    #endregion

    #region Authorization Policies

    /// <summary>
    /// Endpoint para criação de um tipo de usuário do sistema
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpPost("CreateRole")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles added!");
                return StatusCode(StatusCodes.Status200OK,
                                    new ResponseModel
                                    {
                                        Status = "Success",
                                        Message = $"Role {roleName} added successfully"
                                    });
            }
            else
            {
                _logger.LogInformation(2, "Error");
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new ResponseModel
                                    {
                                        Status = "Error",
                                        Message = $"Error to add the role {roleName}"
                                    });
            }
        }

        return StatusCode(StatusCodes.Status400BadRequest,
                                    new ResponseModel
                                    {
                                        Status = "Error",
                                        Message = $"The role {roleName} already exists"
                                    });
    }

    /// <summary>
    /// Endpoint para associar um tipo de usuário a um usuário específico
    /// </summary>
    /// <param name="userIdentifier"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpPost("AddUserToRole")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> AddUserToRole(string userIdentifier, string roleName)
    {
        var user = await GetUserByInputType(userIdentifier);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"Role {roleName} added to user {user.UserName} successfully");
                return StatusCode(StatusCodes.Status200OK,
                                    new ResponseModel
                                    {
                                        Status = "Success",
                                        Message = $"Role {roleName} added to user {user.UserName} successfully"
                                    });
            }
            else
            {
                _logger.LogInformation(1, $"Unable to add the role {roleName} to user {user.UserName}");
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new ResponseModel
                                    {
                                        Status = "Error",
                                        Message = $"Unable to add the role {roleName} to user {user.UserName}"
                                    });
            }
        }
        return BadRequest("Unable to find the user");
    }

    #endregion

    #region Private Methods

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
