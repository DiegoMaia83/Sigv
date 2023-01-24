using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao
{
    public class MensagemRetorno
    {
        public int Id { get; set; }
        public bool Sucesso { get; set; }
        public string Erro { get; set; }
        public string Mensagem { get; set; }
    }
}
