using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos")]
    public class Laudo
    {
        [Key]
        public int LaudoId { get; set; }
        public int VeiculoId { get; set; }
        public int StatusId { get; set; }
        public DateTime DataAbertura { get; set; }
        public string UsuarioAbertura { get; set; }
        public DateTime DataFechamento { get; set; }
        public string UsuarioFechamento { get; set; }
        public DateTime DataReabre { get; set; }
        public string UsuarioReabre { get; set; }
    }
}
