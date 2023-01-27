using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos_itens")]
    public class LaudoItem
    {
        [Key]
        public int ItemId { get; set; }
        public string Descricao { get; set; }
    }
}
