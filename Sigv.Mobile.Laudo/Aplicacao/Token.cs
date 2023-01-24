using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }

        //Verifica se o login não expirou
        /*
        public static bool isOk()
        {

            //return new AuthService().RetornarTokenState();
            return TokenCookie.Token != "";
        }
        */
    }
}
