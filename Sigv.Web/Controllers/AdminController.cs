using Sigv.Domain;
using Sigv.Web.App;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using static Sigv.Web.App.ConfigProvider;

namespace Sigv.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Permissoes()
        {
            return View();
        }

        [HttpGet]
        [Filtro(Roles = "5")]
        public ActionResult ListarPermissoes()
        {
            try
            {
                var listaPermissoes = new List<Permissao>();

                using (var srv = new HttpService<List<Permissao>>())
                {
                    listaPermissoes = srv.ReturnService("api/permissao/listar-permissoes");
                }

                return View("_ListarPermissoes", listaPermissoes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Filtro(Roles = "6")]
        public ActionResult InserirPermissaoGrupo(PermissaoGrupo permissao)
        {
            try
            {
                using (var srv = new HttpService<PermissaoGrupo>())
                {
                    var result = srv.ExecuteService(permissao, "api/permissao/inserir-permissao-grupo");

                    if (result != null)
                    {
                        return Json(new MensagemRetorno { Id = result.Id, Sucesso = true, Mensagem = "Operação efetuada com sucesso!" });
                    }
                    else
                    {
                        return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
                    }                    
                }                
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpPost]
        [Filtro(Roles = "7")]
        public ActionResult RemoverPermissaoGrupo(PermissaoGrupo permissao)
        {
            try
            {
                using (var srv = new HttpService<PermissaoGrupo>())
                {
                    var result = srv.ExecuteService(permissao, "api/permissao/remover-permissao-grupo");

                    if (result != null)
                    {
                        return Json(new MensagemRetorno { Id = result.Id, Sucesso = true, Mensagem = "Operação efetuada com sucesso!" });
                    }
                    else
                    {
                        return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }
    }
}