using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Sigv.ApiFullOwin.Controllers
{   
    public class UsuarioController : ApiController
    {
        private readonly PermissaoApp _permissaoApp = new PermissaoApp();
        private readonly UsuarioApp _usuarioApp = new UsuarioApp();

        //  Usuários  //

        [Authorize]
        [HttpGet]
        [Route("api/usuario/retornar")]
        public Usuario Retornar(int usuarioId)
        {
            try
            {
                return _usuarioApp.Retornar(usuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/usuario/listar")]
        public List<Usuario> Listar()
        {
            //if (!PermissaoApp.VerificaPermissao(2))
            //    throw new Exception("Permissão negada na API");

            try
            {
                return _usuarioApp.Listar().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/usuario/salvar")]
        public Usuario Salvar(Usuario usuario)
        {
            try
            {
                return _usuarioApp.Salvar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/usuario/alterar")]
        public Usuario Alterar(Usuario usuario)
        {
            try
            {
                return _usuarioApp.Alterar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //  Grupos Usuários  //

        [Authorize]
        [HttpGet]
        [Route("api/usuario/listar-grupos")]
        public List<UsuarioGrupo> ListarGrupos()
        {
            try
            {
                return _usuarioApp.ListarGrupos().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}