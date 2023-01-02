using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("logs")]
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public int CodReferencia { get; set; }
        public string Processo { get; set; }
        public int UsuarioId { get; set; }
        public string Ip { get; set; }
        public DateTime DataLog { get; set; }
        public string Descricao { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}
