using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos_status")]
    public class LaudoStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string Descricao { get; set; }
    }
}
