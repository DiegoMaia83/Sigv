using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos_apontamentos_condicao")]
    public class LaudoApontamentoCondicao
    {
        [Key]
        public int CondicaoId { get; set; }
        public string Descricao { get; set; }
    }
}
