using Gerenciamento.Informacoes.ESocial.Dominio.Enums;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Utils
{
    public static class EmailBuilder
    {
        public static string ConstruirCorpoEmail(string nomeTrabalhador, 
            string situacaoCadastro, 
            string pendencias)
        {
            string strBody = "";
            strBody = strBody + "<html>";
            strBody = strBody + "<head>";
            strBody = strBody + "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>";
            strBody = strBody + "<title>Untitled Document</title>";
            strBody = strBody + "</head>";
            strBody = strBody + "<body>";

            strBody = strBody + "<table style='font-family: verdana; font-size: 11px; color: #000000;' width='100%'>";
            strBody = strBody + "<tr align=center><td colspan=2><img src='cid:Imagem1' /></td></tr>";
            strBody = strBody + "<tr align=center><td colspan=2></td></tr>";
            strBody = strBody + "<tr style='=font-weight:bold;' align=center><td colspan=2>EMPRESA EXEMPLAR LTDA</td></tr>";
            strBody = strBody + "<tr style='=font-weight:bold;' align=center><td colspan=2>SETOR DE TECNOLOGIA</td></tr>";

            strBody = strBody + "<tr><td font-weight:bold'><p><p></td></tr> ";
            strBody = strBody + "</table> ";
            strBody = strBody + "<br><br>";
            strBody = strBody + $"Olá {nomeTrabalhador}!";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Viemos informar a situação do seu cadastro.";
            strBody = strBody + "<br><br>";
            strBody = strBody + "<br><br>";
            strBody = strBody + situacaoCadastro;
            strBody = strBody + "<br><br>";
            strBody = strBody + pendencias;
            strBody = strBody + "<br><br>";
            strBody = strBody + $"Data: {DateTime.Now.Date}";
            strBody = strBody + "<br><br>";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Para conferir seus dados cadatrados, entre no sistema Censo Esocial.";
            strBody = strBody + "<br><br>";
            strBody = strBody + "Esta é uma  mensagem automática enviada pelo sistema. Não precisa responder.";
            strBody = strBody + "</body>";
            strBody = strBody + "</html>";

            return strBody;
        }
    }
}
