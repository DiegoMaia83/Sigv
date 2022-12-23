using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("usuarios_grupos")]
    public class UsuarioGrupo
    {
        [Key]
        public int GrupoId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<PermissaoGrupo> PermissaoGrupo { get; set; }
    }
}
