using System.ComponentModel;
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

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public string CheckBoxName
        {
            get { return "checkBox_" + AvariaId.ToString(); }
        }

    }
}
