using Sigv.Domain;
using System.Configuration;

namespace Sigv.Web.App
{
    public class Mail
    {
        public static void EnviarSenha(Usuario usuario, string senha, string ip)
        {
            var body = "<html>" +
                "<body>" +
                "Prezado " + usuario.Nome + "<br/><br/>" +
                "Segue os seus dados de acesso ao sistema<br/>" +
                "Login: " + usuario.Login + "<br/>" +
                "Senha: " + senha + "<br/><br/>" +
                "</body>" +
                "</html>";

            string[] anexos = { };

            Mailer.Send(usuario.Email, usuario.Email, "Dados de acesso", body, "", new string[0]);
        }
    }
}