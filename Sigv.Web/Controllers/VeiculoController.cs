using Sigv.Domain;
using Sigv.Web.App;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Sigv.Web.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: Veiculo
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Index(int id = 0)
        {
            ViewBag.Id = id;

            return View();            
        }

        [HttpGet]
        public ActionResult RetornarVeiculoDados(int id)
        {
            try
            {
                var veiculo = new Veiculo();

                if (id > 0)
                {
                    using (var srv = new HttpService<Veiculo>())
                    {
                        veiculo = srv.ReturnService("api/veiculo/retornar?veiculoId=" + id);
                    }
                }
                
                return View("_RetornarVeiculo_Dados", veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult RetornarVeiculoLogs()
        {
            try
            {
                return View("_RetornarVeiculo_Logs");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult RetornarVeiculoOcorrencias()
        {
            try
            {
                return View("_RetornarVeiculo_Ocorrencias");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult RetornarVeiculoFotos()
        {
            try
            {
                return View("_RetornarVeiculo_Fotos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult ListarVeiculos()
        {
            try
            {
                var listaVeiculos = new List<Veiculo>();

                using (var srv = new HttpService<List<Veiculo>>())
                {
                    listaVeiculos = srv.ReturnService("api/veiculo/listar");
                }

                return View("_ListarVeiculos", listaVeiculos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult PesquisarVeiculos(string filtro = "", string param = "", int condicaoId = 0, int statusId = 0, int especieId = 0, string dataEntradaInicial = "", string dataEntradaFinal = "")
        {
            try
            {
                var parametros = new StringBuilder();
                parametros.AppendFormat("?filtro={0}", filtro);
                parametros.AppendFormat("&param={0}", param);
                parametros.AppendFormat("&condicaoId={0}", condicaoId);
                parametros.AppendFormat("&statusId={0}", statusId);
                parametros.AppendFormat("&especieId={0}", especieId);
                parametros.AppendFormat("&dataEntradaInicial={0}", dataEntradaInicial);
                parametros.AppendFormat("&dataEntradaFinal={0}", dataEntradaFinal);

                var listaVeiculos = new List<Veiculo>();

                using (var srv = new HttpService<List<Veiculo>>())
                {
                    listaVeiculos = srv.ReturnService("api/veiculo/pesquisar" + parametros);
                }

                return View("_ListarVeiculos", listaVeiculos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SalvarVeiculo(Veiculo veiculo)
        {
            try
            {
                using (var srv = new HttpService<Veiculo>())
                {
                    if (veiculo.VeiculoId > 0)
                    {
                        var result = srv.ExecuteService(veiculo, "api/veiculo/alterar");

                        if (result.VeiculoId > 0)
                        {
                            var log = new Log
                            {
                                CodReferencia = result.VeiculoId,
                                Processo = "Veiculo",
                                UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                                Ip = Request.ServerVariables["REMOTE_ADDR"],
                                DataLog = DateTime.Now,
                                Descricao = "Alterou o cadastro do veículo"
                            };

                            using (var conn = new HttpService<Log>())
                            {
                                conn.ExecuteService(log, "api/log/salvar");
                            };

                            return Json(new MensagemRetorno { Id = result.VeiculoId, Sucesso = true, Mensagem = "Operação efetuada com sucesso!" });
                        }
                        else
                        {
                            return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });

                        }
                    }
                    else
                    {

                        veiculo.UsuCriacaoId = Convert.ToInt32(SessionCookie.Logado.UsuarioId);
                        veiculo.DataCriacao = DateTime.Now;
                        veiculo.StatusId = 1;

                        var result = srv.ExecuteService(veiculo, "api/veiculo/salvar");

                        if (result.VeiculoId > 0)
                        {
                            var log = new Log
                            {
                                CodReferencia = result.VeiculoId,
                                Processo = "Veiculo",
                                UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                                Ip = Request.ServerVariables["REMOTE_ADDR"],
                                DataLog = DateTime.Now,
                                Descricao = "Inseriu o cadastro do veículo"
                            };

                            using (var conn = new HttpService<Log>())
                            {
                                conn.ExecuteService(log, "api/log/salvar");
                            };

                            return Json(new MensagemRetorno { Id = result.VeiculoId, Sucesso = true, Mensagem = "Operação efetuada com sucesso!" });
                        }
                        else
                        {
                            return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }


        [HttpGet]
        public JsonResult ListarCondicoes()
        {
            try
            {
                var listaCombustiveis = new List<VeiculoCondicao>();

                using (var srv = new HttpService<List<VeiculoCondicao>>())
                {
                    listaCombustiveis = srv.ReturnService("api/veiculo/listar-condicoes");
                }

                return Json(listaCombustiveis, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult ListarEspecies()
        {
            try
            {
                var listaEspecies = new List<VeiculoEspecie>();

                using (var srv = new HttpService<List<VeiculoEspecie>>())
                {
                    listaEspecies = srv.ReturnService("api/veiculo/listar-especies");
                }

                return Json(listaEspecies, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult ListarStatus()
        {
            try
            {
                var listaStatus = new List<VeiculoStatus>();

                using (var srv = new HttpService<List<VeiculoStatus>>())
                {
                    listaStatus = srv.ReturnService("api/veiculo/listar-status");
                }

                return Json(listaStatus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult ListarCombustiveis()
        {
            try
            {
                var listaCombustiveis = new List<VeiculoCombustivel>();

                using (var srv = new HttpService<List<VeiculoCombustivel>>())
                {
                    listaCombustiveis = srv.ReturnService("api/veiculo/listar-combustiveis");
                }

                return Json(listaCombustiveis, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}