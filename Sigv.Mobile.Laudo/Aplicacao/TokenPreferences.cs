using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao
{
    public class TokenPreferences
    {
        public static string Token
        {
            get
            {
                return Preferences.Get("Token", "");
            }
            set
            {
                Preferences.Set("Token", value);
            }
        }

        public static void Logoff()
        {

            try
            {
                Preferences.Clear();
            }
            catch
            {

            }

        }
    }
}
