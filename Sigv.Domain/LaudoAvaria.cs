using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos_avarias")]
    public class LaudoAvaria
    {
        [Key]
        public int AvariaId { get; set; }
        public string Descricao { get; set; }
    }
}
