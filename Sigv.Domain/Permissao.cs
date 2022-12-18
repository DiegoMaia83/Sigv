using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("permissoes")]
    public class Permissao
    {
        [Key]
        public int PermissaoId { get; set; }
        public string Descricao { get; set; }
    }
}
