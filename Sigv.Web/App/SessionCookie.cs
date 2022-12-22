using System;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Sigv.Web.App
{
    public class SessionCookie
    {
        private static readonly string chaveCrypt = "OwB.(Q+3=>Vx'"; //Chave 128 bits

        public static UsuarioLogado Logado
        {
            get
            {
                return GetCookie();
            }
        }

        public static UsuarioLogado GetCookie()
        {
            var usuarioLogado = new UsuarioLogado();

            try
            {

                if (HttpContext.Current.Request.Cookies["c-sigv"] != null)
                {
                    usuarioLogado.UsuarioId = Decriptar(HttpContext.Current.Request.Cookies["c-sigv"]["id"], chaveCrypt);
                    usuarioLogado.Nome = Decriptar(HttpContext.Current.Request.Cookies["c-sigv"]["n"], chaveCrypt);
                    usuarioLogado.Login = Decriptar(HttpContext.Current.Request.Cookies["c-sigv"]["l"], chaveCrypt);

                }
            }
            catch
            {
                //Não faz nada, só vai retornar um objeto vazio
            }

            return usuarioLogado;

        }

        public static void SetCookie(UsuarioLogado value)
        {

            HttpContext.Current.Response.Cookies["c-sigv"]["id"] = Encriptar(value.UsuarioId, chaveCrypt);
            HttpContext.Current.Response.Cookies["c-sigv"]["n"] = Encriptar(value.Nome, chaveCrypt);
            HttpContext.Current.Response.Cookies["c-sigv"]["l"] = Encriptar(value.Login, chaveCrypt);
        }


        public static void Logoff()
        {

            try
            {
                HttpContext.Current.Response.Cookies["c-sigv"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Request.Cookies["c-sigv"].Expires = DateTime.Now.AddDays(-1);
            }
            catch { }

        }

        private static string Encriptar(string texto, string chave)
        {
            byte[] stream = Encoding.UTF8.GetBytes(texto);
            byte[] encodedValue = MachineKey.Protect(stream, chave);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        private static string Decriptar(string texto, string chave)
        {
            byte[] stream = HttpServerUtility.UrlTokenDecode(texto);
            byte[] decodedValue = MachineKey.Unprotect(stream, chave);
            return Encoding.UTF8.GetString(decodedValue);
        }

    }
}