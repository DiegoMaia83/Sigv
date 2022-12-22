using Sigv.Application.Util;
using Sigv.Domain;
using System;

namespace Sigv.Application
{
    public class SegurancaApp
    {
        public Usuario EfetuarLogin(string login, string senha)
        {
            try
            {
                //Desencripta a senha
                var password = AESEncrytDecry.DecryptStringAES(senha);
                //var password = senha;

                return new UsuarioApp().Retornar(login, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
