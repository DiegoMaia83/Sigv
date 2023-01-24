using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao
{
    public class UserPreferences
    {
        public static UsuarioLogado Logado
        {
            get
            {
                return GetPreferences();
            }
        }

        public static UsuarioLogado GetPreferences()
        {
            var usuarioLogado = new UsuarioLogado();

            try
            {
                usuarioLogado.UsuarioId = Preferences.Get("UsuarioId", "");
                usuarioLogado.Nome = Preferences.Get("Nome", "");
                usuarioLogado.Login = Preferences.Get("Login", "");
                usuarioLogado.GrupoId = Preferences.Get("GrupoId", "");
                usuarioLogado.Ip = Preferences.Get("Ip", "");
            }
            catch
            {
                //Não faz nada, só vai retornar um objeto vazio
            }

            return usuarioLogado;

        }

        public static void SetPreferences(UsuarioLogado value)
        {
            Preferences.Set("UsuarioId", value.UsuarioId);
            Preferences.Set("Nome", value.Nome);
            Preferences.Set("Login", value.Login);
            Preferences.Set("GrupoId", value.GrupoId);
            Preferences.Set("Ip", GetDeviceIp());
        }

        private static string GetDeviceIp()
        {
            // Verificar implementação na produção. Está retornando 127.0.0.1
            return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault().ToString();
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
