namespace Sigv.Web.App
{
    public class UsuarioLogado
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }

        //Verifica se o login não expirou
        public static bool isOk()
        {
            return SessionCookie.Logado.Login != null;
        }

    }
}