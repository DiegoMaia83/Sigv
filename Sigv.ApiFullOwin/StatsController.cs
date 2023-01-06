using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sigv.ApiFullOwin
{
    public class StatsController : ApiController
    {
        private readonly StatsAplicacao _statsAplicacao = new StatsAplicacao();

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
    }
}