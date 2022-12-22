using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("veiculos_ocorrencia")]
    public class VeiculoOcorrencia
    {
        [Key]
        public int OcorrenciaId { get; set; }
        public int VeiculoId { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataOcorrencia { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}
