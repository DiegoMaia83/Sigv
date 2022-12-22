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
        public string Mensagem { get; set; }
        public string Erro { get; set; }
    }
}