using Sigv.Domain;
using Sigv.Web.App;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Sigv.Web.App.ConfigProvider;

namespace Sigv.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        [Filtro(Roles = "19")]
        public JsonResult RetornarStatusVeiculos()
        {
            try
            {
                var lista = new List<StatsVeiculo>();

                using (var srv = new HttpService<List<StatsVeiculo>>())
                {
                    lista = srv.ReturnService("api/stats/retornar-status-veiculos");
                }

                return Json(lista.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpGet]
        [Filtro(Roles = "19")]
        public JsonResult RetornarEspeciesVeiculos()
        {
            try
            {
                var lista = new List<StatsVeiculo>();

                using (var srv = new HttpService<List<StatsVeiculo>>())
                {
                    lista = srv.ReturnService("api/stats/retornar-especies-veiculos");
                }

                return Json(lista.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpGet]
        [Filtro(Roles = "19")]
        public JsonResult RetornarEntradasPeriodo(int ano)
        {
            try
            {
                var lista = new List<StatsPeriodo>();

                using (var srv = new HttpService<List<StatsPeriodo>>())
                {
                    lista = srv.ReturnService("api/stats/retornar-entradas-periodo?ano=" + ano);
                }

                return Json(lista.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpGet]
        [Filtro(Roles = "19")]
        public ActionResult ListarAcessos(int limit)
        {
            try
            {
                var lista = new List<Acesso>();

                using (var srv = new HttpService<List<Acesso>>())
                {
                    lista = srv.ReturnService("api/acesso/listar?limit=" + limit);
                }

                return View("_ListarAcessos", lista);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" , Erro = ex.Message });
            }
        }

        [HttpGet]
        [Filtro(Roles = "19")]
        public ActionResult ListarLogs(int limit)
        {
            try
            {
                var lista = new List<Log>();

                using (var srv = new HttpService<List<Log>>())
                {
                    lista = srv.ReturnService("api/log/listar-logs?limit=" + limit);
                }

                return View("_ListarLogs", lista);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }
    }
}