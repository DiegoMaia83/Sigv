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
    public class PermissaoController : ApiController
    {
        private readonly PermissaoApp _permissaoApp = new PermissaoApp();

        [Authorize]
        [HttpGet]
        [Route("api/permissao/listar-permissoes")]
        public List<Permissao> ListarPermissoes()
        {
            try
            {
                return _permissaoApp.ListarPermissoes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/permissao/inserir-permissao-grupo")]
        public PermissaoGrupo InserirPermissaoGrupo(PermissaoGrupo permissao)
        {
            try
            {
                return _permissaoApp.InserirPermissaoGrupo(permissao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/permissao/remover-permissao-grupo")]
        public PermissaoGrupo RemoverPermissaoGrupo(PermissaoGrupo permissao)
        {
            try
            {
                return _permissaoApp.RemoverPermissaoGrupo(permissao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/permissao/listar-permissoes-usuario")]
        public string[] ListarPermissoesUsuario(string username)
        {
            try
            {
                return _permissaoApp.ListarPermissoesUsuario(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}