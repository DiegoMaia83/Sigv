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
    public class StatsController : ApiController
    {
        private readonly StatsAplicacao _statsAplicacao = new StatsAplicacao();
        private readonly AcessoAplicacao _acessosAplicacao = new AcessoAplicacao();

        [Authorize]
        [HttpGet]
        [Route("api/stats/retornar-status-veiculos")]
        public IEnumerable<StatsVeiculo> RetornarStatusVeiculos()
        {
            try
            {
                return _statsAplicacao.RetornarStatusVeiculos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/stats/retornar-especies-veiculos")]
        public IEnumerable<StatsVeiculo> RetornarEspeciesVeiculos()
        {
            try
            {
                return _statsAplicacao.RetornarEspeciesVeiculos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/stats/retornar-entradas-periodo")]
        public IEnumerable<StatsPeriodo> RetornarEntradasPeriod(int ano)
        {
            try
            {
                return _statsAplicacao.RetornarEntradasPeriodo(ano);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/acesso/salvar")]
        public Acesso SalvarAcesso(Acesso acesso)
        {
            try
            {
                return _acessosAplicacao.Salvar(acesso);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao processar a rotina!", ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/acesso/listar")]
        public IEnumerable<Acesso> ListarAcessos()
        {
            try
            {
                return _acessosAplicacao.Listar();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao processar a rotina!", ex);
            }
        }
    }
}