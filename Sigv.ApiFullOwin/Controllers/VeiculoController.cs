using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sigv.ApiFullOwin.Controllers
{
    public class VeiculoController : ApiController
    {
        private readonly VeiculoApp _veiculoApp = new VeiculoApp();

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/retornar")]
        public Veiculo Retornar(int veiculoId)
        {
            try
            {
                return _veiculoApp.Retornar(veiculoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar")]
        public IEnumerable<Veiculo> Listar()
        {
            try
            {
                return _veiculoApp.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/pesquisar")]
        public IEnumerable<Veiculo> Pesquisar(string filtro = "", string param = "", int condicaoId = 0, int statusId = 0, int especieId = 0, string dataEntradaInicial = "", string dataEntradaFinal = "")
        {
            try
            {
                return _veiculoApp.Pesquisar(filtro, param, condicaoId, statusId, especieId, dataEntradaInicial, dataEntradaFinal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo/salvar")]
        public Veiculo Salvar(Veiculo veiculo)
        {
            try
            {
                return _veiculoApp.Salvar(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo/alterar")]
        public Veiculo Alterar(Veiculo veiculo)
        {
            try
            {
                return _veiculoApp.Alterar(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Ocorrências //

        [Authorize]
        [HttpGet]
        [Route("api/veiculo-ocorrencia/listar")]
        public IEnumerable<VeiculoOcorrencia> ListarOcorrencias(int veiculoId)
        {
            try
            {
                return _veiculoApp.ListarOcorrencias(veiculoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo-ocorrencia/salvar")]
        public VeiculoOcorrencia SalvarOcorrencia(VeiculoOcorrencia ocorrencia)
        {
            try
            {
                return _veiculoApp.SalvarOcorrencia(ocorrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Fotos //

        [Authorize]
        [HttpGet]
        [Route("api/veiculo-foto/listar")]
        public IEnumerable<VeiculoFoto> ListarFotos(int veiculoId)
        {
            try
            {
                return _veiculoApp.ListarFotos(veiculoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo-foto/listar-por-tipo")]
        public IEnumerable<VeiculoFoto> ListarFotos(int veiculoId, string tipo)
        {
            try
            {
                return _veiculoApp.ListarFotos(veiculoId, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo-foto/salvar")]
        public VeiculoFoto SalvarFoto(VeiculoFoto foto)
        {
            try
            {
                return _veiculoApp.SalvarFoto(foto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo-foto/remover")]
        public VeiculoFoto RemoverFoto(VeiculoFoto foto)
        {
            try
            {
                return _veiculoApp.RemoverFoto(foto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo-foto/copiar-para-diretorio")]
        public int CopiarParaDiretorio(VeiculoFoto arq)
        {
            try
            {
                return _veiculoApp.CopiarParaDiretorio(arq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       


        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-combustiveis")]
        public IEnumerable<VeiculoCombustivel> ListarCombustiveis()
        {
            try
            {
                return _veiculoApp.ListarCombustiveis();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-condicoes")]
        public IEnumerable<VeiculoCondicao> ListarCondicoes()
        {
            try
            {
                return _veiculoApp.ListarCondicoes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-especies")]
        public IEnumerable<VeiculoEspecie> ListarEspecies()
        {
            try
            {
                return _veiculoApp.ListarEspecies();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-status")]
        public IEnumerable<VeiculoStatus> ListarStatus()
        {
            try
            {
                return _veiculoApp.ListarStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}