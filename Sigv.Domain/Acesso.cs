using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("Acessos")]
    public class Acesso
    {
        public int AcessoId { get; set; }
        public int UsuarioId { get; set; }
        public string Ip { get; set; }
        public DateTime Data { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}
