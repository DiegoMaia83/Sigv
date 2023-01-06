using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("permissoes")]
    public class Permissao
    {
        [Key]
        public int PermissaoId { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<PermissaoGrupo> Grupo { get; set; }
    }
}
