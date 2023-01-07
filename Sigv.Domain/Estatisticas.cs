using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Domain
{
    public class Estatisticas
    {        
    }

    public class StatsVeiculo
    {
        public string Status { get; set; }
        public int Statusid { get; set; }
        public string Especie { get; set; }
        public int EspecieId { get; set; }
        public int Quantidade { get; set; }
    }

    public class StatsPeriodo
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string MesExtenso { get; set; }
        public int Quantidade { get; set; }
    }
}
