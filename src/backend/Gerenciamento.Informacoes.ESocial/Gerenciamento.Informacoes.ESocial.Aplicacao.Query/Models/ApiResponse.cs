namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(bool success, T? result, string? errorMessage)
    {
        Success = success;
        Result = result;
        ErrorMessage = errorMessage;
    }
}
