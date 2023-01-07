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
    public class LogController : ApiController
    {
        private readonly LogAplicacao _logAplicacao = new LogAplicacao();
        [Authorize]
        [HttpGet]
        [Route("api/log/listar-logs")]
        public IEnumerable<Log> Listar()
        {
            try
            {
                return _logAplicacao.Listar();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao processar a rotina!", ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/log/listar-logs-processo")]
        public IEnumerable<Log> Listar(string processo, int codReferencia)
        {
            try
            {
                return _logAplicacao.Listar(processo, codReferencia);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao processar a rotina!", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/log/salvar")]
        public Log Salvar(Log log)
        {
            try
            {
                return _logAplicacao.Salvar(log);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao processar a rotina!", ex);
            }
        }
    }
}