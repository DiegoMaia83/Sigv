using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao
{
    public class UsuarioLogado
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string GrupoId { get; set; }
        public string Ip { get; set; }

        //Verifica se o login não expirou
        /*
        public static bool isOk()
        {
            return SessionCookie.Logado.Login != null;
        }
        */

    }
}
