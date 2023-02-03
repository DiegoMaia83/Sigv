using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sigv.Domain
{
    [Table("laudos_opcionais_apontamentos")]
    public class LaudoOpcionalApontamento
    {
        [Key]
        public int ApontamentoId { get; set; }
        public int LaudoId { get; set; }
        public int OpcionalId { get; set; }
        public string UsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
