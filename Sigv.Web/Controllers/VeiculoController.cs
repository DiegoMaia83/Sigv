using Sigv.Domain;
using Sigv.Web.App;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static Sigv.Web.App.ConfigProvider;

namespace Sigv.Web.Controllers
{
    public class VeiculoController : Controller
    {
        [Filtro(Roles = "22")]
        public ActionResult Home()
        {
            return View();
        }

        [Filtro(Roles = "22")]
        public ActionResult Index(int id = 0)
        {
            try
            {
                ViewBag.Id = id;

                return View();     
            }
            catch (Exception ex)
            {
                throw ex;
            }
                   
        }

        // ----- Veículo Dados ----- //

        [HttpGet]
        [Filtro(Roles = "8")]
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
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpGet]
        [Filtro(Roles = "9")]
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
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpPost]
        [Filtro(Roles = "10")]
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


        // ----- Veículo Logs ----- //

        [HttpGet]
        [Filtro(Roles = "12")]
        public ActionResult RetornarVeiculoLogs(string processo, int codReferencia)
        {
            try
            {
                var listaLogs = new List<Log>();

                using (var srv = new HttpService<List<Log>>())
                {
                    listaLogs = srv.ReturnService("api/log/listar-logs-processo?processo=" + processo + "&codreferencia=" + codReferencia);

                }

                return View("_RetornarVeiculo_Logs", listaLogs);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }


        // ----- Veículo Ocorrências ----- //

        [HttpGet]
        [Filtro(Roles = "13")]
        public ActionResult RetornarVeiculoOcorrencias(int veiculoId)
        {
            try
            {
                ViewBag.VeiculoId = veiculoId;

                return View("_RetornarVeiculo_Ocorrencias");
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpGet]
        [Filtro(Roles = "13")]
        public ActionResult ListarOcorrencias(int veiculoId)
        {
            try
            {
                var listaOcorrencias = new List<VeiculoOcorrencia>();

                using (var srv = new HttpService<List<VeiculoOcorrencia>>())
                {
                    listaOcorrencias = srv.ReturnService("api/veiculo-ocorrencia/listar?veiculoId=" + veiculoId);
                }

                return View("_ListarOcorrencias", listaOcorrencias);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [HttpPost]
        [Filtro(Roles = "14")]
        public ActionResult SalvarOcorrencia(VeiculoOcorrencia ocorrencia)
        {
            try
            {
                ocorrencia.UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId);

                using (var srv = new HttpService<VeiculoOcorrencia>())
                {
                    var result = srv.ExecuteService(ocorrencia, "api/veiculo-ocorrencia/salvar");

                    if (result.OcorrenciaId > 0)
                    {
                        var log = new Log
                        {
                            CodReferencia = result.VeiculoId,
                            Processo = "Veiculo",
                            UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                            Ip = Request.ServerVariables["REMOTE_ADDR"],
                            DataLog = DateTime.Now,
                            Descricao = "Inseriu uma ocorrência ao veículo"
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
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }


        // ----- Veículo Fotos ----- //

        [HttpGet]
        [Authorize]
        [Filtro(Roles = "15")]
        public ActionResult RetornarVeiculoFotos(int veiculoId)
        {
            try
            {
                ViewBag.VeiculoId = veiculoId;

                return View("_RetornarVeiculo_Fotos");
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [Authorize]
        [Filtro(Roles = "15")]
        [HttpGet]
        public ActionResult ListarFotos(int id, string tipo)
        {
            try
            {
                var lista = new List<VeiculoFoto>();

                using (var srv = new HttpService<List<VeiculoFoto>>())
                {
                    lista = srv.ReturnService("api/veiculo-foto/listar-por-tipo?VeiculoId=" + id + "&Tipo=" + tipo);
                }

                ViewBag.Tipo = tipo;

                return View("_ListarFotos", lista);
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
            }
        }

        [Authorize]
        [Filtro(Roles = "16")]
        [HttpPost]
        public JsonResult SalvarFotos(int veiculoId, string tipo)
        {
            try
            {
                int arquivosSalvos = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase arquivo = Request.Files[i];

                    //Suas validações ......

                    //Salva o arquivo
                    if (arquivo.ContentLength > 0)
                    {
                        var uploadPath = Server.MapPath("~/Content/Uploads");
                        string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                        string diretorioDestino = Server.MapPath("~/Content/Imagens/" + veiculoId.ToString("000000"));

                        arquivo.SaveAs(caminhoArquivo);
                        arquivosSalvos++;

                        var fotoVeiculo = new VeiculoFoto
                        {
                            VeiculoId = veiculoId,
                            Tipo = tipo,
                            SourcePath = caminhoArquivo,
                            TargetPath = diretorioDestino,
                            UsuCriacao = SessionCookie.Logado.Login
                        };

                        using (var srv = new HttpService<int>())
                        {
                            var fotoId = srv.ExecuteService(fotoVeiculo, "api/veiculo-foto/copiar-para-diretorio");

                            var log = new Log()
                            {
                                CodReferencia = veiculoId,
                                Processo = "Veiculo",
                                UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                                Ip = Request.ServerVariables["REMOTE_ADDR"],
                                DataLog = DateTime.Now,
                                Descricao = "Inseriu o arquivo id [ " + fotoId + " ]"
                            };

                            using (var conn = new HttpService<Log>())
                            {
                                conn.ExecuteService(log, "api/log/salvar");
                            };
                        }

                    }
                }

                return Json(new MensagemRetorno { Id = veiculoId, Sucesso = true, Mensagem = arquivosSalvos + " arquivos salvos com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" , Erro = ex.Message });
            }

        }

        [Authorize]
        [Filtro(Roles = "17")]
        [HttpPost]
        public JsonResult ExcluirFoto(VeiculoFoto foto)
        {
            try
            {
                if (foto.FotoId > 0)
                {
                    foto.UsuExclusao = SessionCookie.Logado.Login;

                    using (var srv = new HttpService<VeiculoFoto>())
                    {
                        var result = srv.ExecuteService(foto, "api/veiculo-foto/remover");

                        if (result.FotoId > 0)
                        {
                            var log = new Log()
                            {
                                CodReferencia = 2,
                                Processo = "Veiculo",
                                UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                                Ip = Request.ServerVariables["REMOTE_ADDR"],
                                DataLog = DateTime.Now,
                                Descricao = "Removeu o arquivo id [ " + foto.FotoId + " ]"
                            };

                            using (var conn = new HttpService<Log>())
                            {
                                conn.ExecuteService(log, "api/log/salvar");
                            };
                        }

                        return Json(new MensagemRetorno { Id = result.VeiculoId, Sucesso = true, Mensagem = "Operação realizada com sucesso!" });

                    }

                }
                else
                {
                    return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Arquivo não localizado!" });
                }
            }
            catch (Exception ex) 
            {
                return Json(new MensagemRetorno { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!", Erro = ex.Message });
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

        [HttpGet]
        public JsonResult ListarCores()
        {
            try
            {
                var listaCores = new List<VeiculoCor>();

                using (var srv = new HttpService<List<VeiculoCor>>())
                {
                    listaCores = srv.ReturnService("api/veiculo/listar-cores");
                }

                return Json(listaCores, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}