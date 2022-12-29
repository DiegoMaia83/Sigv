using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigv.Web.App
{
    public class MensagemRetorno
    {
        public int Id { get; set; }
        public bool Sucesso { get; set; }
        public string Erro { get; set; }

        private string mensagem;
        public string Mensagem
        {
            get
            {
                var msg = this.mensagem;
                msg = Convert.ToInt32(SessionCookie.Logado.GrupoId) == 5 && !String.IsNullOrEmpty(Erro) ? msg += " Exception Message: " + Erro : msg;
                return msg;
            }
            set
            {
                this.mensagem = value;
            }
        }
    }
}