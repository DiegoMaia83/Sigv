using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Sigv.Domain
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int GrupoId { get; set; }
        public DateTime DataExpira { get; set; }
        public bool Bloqueado { get; set; }

        [ForeignKey("GrupoId")]
        public virtual UsuarioGrupo Grupo { get; set; }
    }
}
