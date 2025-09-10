using Gerenciamento.Informacoes.ESocial.Api.Models.Auth;
using Gerenciamento.Informacoes.ESocial.Api.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

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

    /// <summary>
    /// Endpoint para retornar todos os usuários do sistema
    /// </summary>
    /// <returns></returns>
    [HttpGet("users")]
    public async Task<ActionResult<ApiResponse<List<UserResponseModel>>>> RetornarTodosUsuarios()
    {
        var result = await _authService.GetAllUsersAsync();
        if (!result.Success) return BadRequest(result);
        return Ok(result);
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

        Response.Cookies.Append("access_token", result.Result!.Token!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = result.Result.Expiration
        });

        Response.Cookies.Append("user_id", result.Result.UserId!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = result.Result.Expiration
        });

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

    /// <summary>
    /// Recupera dados do usuário a partir do token
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (userId == null) return Unauthorized();

        var result = await _authService.RecoveryUserDataAsync(userId, email);

        return Ok(result);
    }

    /// <summary>
    /// Endpoint responsável pelo logout do sistema
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        var result = await _authService.RevokeAsync(User.FindFirst(ClaimTypes.Name)?.Value!);
        Response.Cookies.Delete("access_token");
        return Ok(new { message = "Logoff realizado com sucesso" });
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
