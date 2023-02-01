using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos_avarias")]
    public class LaudoAvaria
    {
        [Key]
        public int ItemId { get; set; }
        public string Descricao { get; set; }
    }
}
