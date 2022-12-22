using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Application
{
    public class VeiculoApp
    {
        public Veiculo Retornar(int veiculoId)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    return veiculos.GetAll().Where(x => x.VeiculoId == veiculoId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Veiculo> Listar()
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    return veiculos.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Veiculo Salvar(Veiculo veiculo)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    veiculos.Adicionar(veiculo);
                    veiculos.SalvarTodos();

                    return veiculo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Veiculo Alterar(Veiculo veiculo)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    veiculos.Atualizar(veiculo);
                    veiculos.SalvarTodos();

                    return veiculo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Ocorrências //

        public IEnumerable<VeiculoOcorrencia> ListarOcorrencias()
        {
            try
            {
                using (var ocorrencias = new VeiculoOcorrenciaRepositorio())
                {
                    return ocorrencias.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SalvarOcorrencia(VeiculoOcorrencia ocorrencia)
        {
            try
            {
                using (var ocorrencias = new VeiculoOcorrenciaRepositorio())
                {
                    ocorrencias.Adicionar(ocorrencia);
                    ocorrencias.SalvarTodos();

                    return ocorrencia.OcorrenciaId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public IEnumerable<VeiculoCombustivel> ListarCombustiveis()
        {
            try
            {
                using (var combustiveis = new VeiculoCombustivelRepositorio())
                {
                    return combustiveis.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoCondicao> ListarCondicoes()
        {
            try
            {
                using (var condicoes = new VeiculoCondicaoRepositorio())
                {
                    return condicoes.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoEspecie> ListarEspecies()
        {
            try
            {
                using (var especies = new VeiculoEspecieRepositorio())
                {
                    return especies.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoStatus> ListarStatus()
        {
            try
            {
                using (var status = new VeiculoStatusRepositorio())
                {
                    return status.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
