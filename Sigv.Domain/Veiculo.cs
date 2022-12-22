using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigv.Domain
{
    [Table("veiculos")]
    public class Veiculo
    {
        [Key]
        public int VeiculoId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Renavam { get; set; }
        public int CombustivelId { get; set; }
        public int CondicaoId { get; set; }
        public int StatusId { get; set; }
        public int EspecieId { get; set; }
        public DateTime DataEntrada { get; set; }
        public int UsuCriacaoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Observacoes { get; set; }

        [ForeignKey("CombustivelId")]
        public virtual VeiculoCombustivel Combustivel { get; set; }

        [ForeignKey("CondicaoId")]
        public virtual VeiculoEspecie Condicao { get; set; }

        [ForeignKey("EspecieId")]
        public virtual VeiculoEspecie Especie { get; set; }

        [ForeignKey("StatusId")]
        public virtual VeiculoStatus Status { get; set; }
    }

    [Table("veiculos_combustivel")]
    public class VeiculoCombustivel
    {
        [Key]
        public int CombustivelId { get; set; }
        public string Nome { get; set; }
    }

    [Table("veiculos_condicao")]
    public class VeiculoCondicao
    {
        [Key]
        public int CondicaoId { get; set; }
        public string Nome { get; set; }
    }

    [Table("veiculos_especie")]
    public class VeiculoEspecie
    {
        [Key]
        public int EspecieId { get; set; }
        public string Nome { get; set; }
    }

    [Table("veiculos_status")]
    public class VeiculoStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string Nome { get; set; }
    }
}
