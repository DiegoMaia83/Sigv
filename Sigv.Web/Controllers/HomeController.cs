using Sigv.Domain;
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
                throw ex;
            }
        }

        [HttpGet]
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
                throw ex;
            }
        }

        [HttpGet]
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
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ListarAcessos()
        {
            try
            {
                var lista = new List<Acesso>();

                using (var srv = new HttpService<List<Acesso>>())
                {
                    lista = srv.ReturnService("api/acesso/listar");
                }

                return View("_ListarAcessos", lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}