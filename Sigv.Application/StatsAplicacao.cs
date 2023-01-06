using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Application
{
    public class StatsAplicacao
    {
        public IEnumerable<StatsVeiculo> RetornarStatusVeiculos()
        {
            try
            {
                var lista = new List<StatsVeiculo>();

                using (var status = new VeiculoStatusRepositorio())
                {
                    var listaStatus = status.GetAll().ToList();

                    foreach (var item in listaStatus)
                    {
                        var stats = new StatsVeiculo
                        {
                            Status = item.Nome,
                            Statusid = item.StatusId,
                            Quantidade = RetornarQuantidadeVeiculos(item.StatusId, "status")
                        };

                        lista.Add(stats);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<StatsVeiculo> RetornarEspeciesVeiculos()
        {
            try
            {
                var lista = new List<StatsVeiculo>();

                using (var status = new VeiculoEspecieRepositorio())
                {
                    var listaEspecies = status.GetAll().ToList();

                    foreach (var item in listaEspecies)
                    {
                        var stats = new StatsVeiculo
                        {
                            Especie = item.Nome,
                            EspecieId = item.EspecieId,
                            Quantidade = RetornarQuantidadeVeiculos(item.EspecieId, "especie")
                        };

                        lista.Add(stats);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int RetornarQuantidadeVeiculos(int valor, string tipoConsulta)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    if (tipoConsulta == "status")
                    {
                        return veiculos.GetAll().Where(x => x.StatusId == valor).Count();
                    }
                    else if (tipoConsulta == "especie")
                    {
                        return veiculos.GetAll().Where(x => x.EspecieId == valor).Count();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
