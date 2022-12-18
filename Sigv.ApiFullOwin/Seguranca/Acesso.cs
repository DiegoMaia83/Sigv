using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigv.ApiFullOwin.Seguranca
{
    public class Acesso
    {
        //Retorna o usuário para fazer o login no provider
        public static Usuario Login(string Login, string Senha)
        {
            return new SegurancaApp().EfetuarLogin(Login, Senha);
            //As Claims são geradas no Provider de Token *GrantResourceOwnerCredentials*
        }
    }
}