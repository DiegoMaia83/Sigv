﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("laudos_apontamentos")]
    public class LaudoApontamento
    {
        [Key]
        public int ApontamentoId { get; set; }
        public int LaudoId { get; set; }
        public int CondicaoId { get; set; }
        public int UsuarioIdCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
