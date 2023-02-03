using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sigv.Domain
{
    [Table("laudos_opcionais")]
    public class LaudoOpcional
    {
        [Key]
        public int OpcionalId { get; set; }
        public string Descricao { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
