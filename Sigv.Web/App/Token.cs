using Sigv.Web.Services;

namespace Sigv.Web.App
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }

        //Verifica se o login não expirou
        public static bool isOk()
        {

            return new AuthService().RetornarTokenState();
            // return TokenCookie.Token != "";
        }
    }
}