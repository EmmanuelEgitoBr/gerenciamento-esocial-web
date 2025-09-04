namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Resources.Constants;

public static class SituacaoCadastro
{
    public const string Criado = "O cadastro foi concluído, mas ainda não foi verificado" +
    ". Aguarde a equipe realizar a verificação e entraremos em contato novamente por email para informar o status do se cadastro";
    public const string Pendente = "O cadastro foi verificado, mas foi constatada(s) pendências. " +
        "Por favor, resolver a(s) pendência(s) listadas a seguir. " +
        "Após resolvê-las, entraremos em contato para te atualizarmos a situação do cadastro.";
    public const string Concluido = "Parabéns! O cadastro foi verificado com sucesso. Obrigado pelo cadastro de suas informações.";
}
