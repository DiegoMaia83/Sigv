﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Domain
{
    [Table("veiculos_foto")]
    public class VeiculoFoto
    {
        [Key]
        public int FotoId { get; set; }
        public int VeiculoId { get; set; }
        public int NumeroFoto { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public string UsuCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Excluida { get; set; }
        public string UsuExclusao { get; set; }
        public DateTime? DataExclusao { get; set; }

        [NotMapped]
        public string SourcePath { get; set; }
        [NotMapped]
        public string TargetPath { get; set; }
    }
}
