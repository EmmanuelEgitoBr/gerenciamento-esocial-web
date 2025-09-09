using Gerenciamento.Informacoes.ESocial.Api.Models.Auth;
using Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    #region Authentication

    /// <summary>
    /// Endpoint para o Login do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponseModel>>> Login([FromBody] LoginModel model)
    {
        var result = await _authService.LoginAsync(model);

        if (!result.Success) return Unauthorized();

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para registro de um novo funcionário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<ResponseModel>> Register([FromBody] RegisterModel model)
    {
        var result = await _authService.RegistrarUsuarioAsync(model);

        if(!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para geração do refresh token
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<TokenModel>>> RefreshToken(TokenModel tokenModel)
    {
        var result = await _authService.RefreshTokenAsync(tokenModel);

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para revogar token
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("revoke/{username}")]
    [Authorize(Policy = "ExclusiveOnly")]
    public async Task<ActionResult<ResponseModel>> Revoke(string username)
    {
        var result = await _authService.RevokeAsync(username);

        if (!result.IsSuccess) return BadRequest(result);

        return NoContent();
    }
    #endregion

    #region Authorization Policies

    /// <summary>
    /// Endpoint para criação de um tipo de usuário do sistema
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpPost("create-role")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResponseModel>> CreateRole([FromBody] string roleName)
    {
        var result = await _authService.CreateRoleAsync(roleName);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// Endpoint para associar um tipo de usuário a um usuário específico
    /// </summary>
    /// <param name="userIdentifier"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpPost("add-user-to-role")]
    [Authorize]
    public async Task<ActionResult<ResponseModel>> AddUserToRole([FromBody]string userIdentifier, [FromQuery]string roleName)
    {
        var result = await _authService.AddUserToRoleAsync(userIdentifier, roleName);

        if (!result.IsSuccess) return BadRequest(result);

        return Ok(result);
    }

    #endregion
}
