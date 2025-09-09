namespace Gerenciamento.Informacoes.ESocial.Api.Resources
{
    public static class ResponseModelConstants
    {
        public const string Success = "Sucesso";
        public const string Error = "Erro";
        public const string UserCreated = "Usuário criado com sucesso";
        public const string UserNotCraeted = "Não foi possível criar o usuário";
        public const string UserAlreadyExists = "Usuário já existe";
        public const string UserNotFound = "Usuário não encontrado";
        public const string InvalidCredentials = "Credenciais inválidas";
        public const string InvalidToken = "Toke inválido";

        public const string RoleCreated = "Função criada com sucesso";
        public const string RoleNotCreated = "Não foi possível criar a função";
        public const string RoleAlreadyExists = "Função já existe";
        public static string RoleAdded(string roleName, string userName)
        {
            return $"Função '{roleName}' adicionada ao usuário '{userName}' com sucesso.";
        }
        public static string ErrorAddingRole(string roleName, string userName)
        {
            return $"Não foi possível adicionar a função '{roleName}' ao usuário '{userName}'.";
        }
    }
}
