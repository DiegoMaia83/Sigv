using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("permissoes_grupos")]
    public class PermissaoGrupo
    {
        [Key]
        public int Id { get; set; }
        public int PermissaoId { get; set; }
        public int GrupoId { get; set; }
    }
}
