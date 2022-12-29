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
        private readonly ComumApp _comumAplicacao = new ComumApp();

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
                        var log = new Log
                        {
                            CodReferencia = result.Id,
                            Processo = "Permissao",
                            UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                            Ip = Request.ServerVariables["REMOTE_ADDR"],
                            DataLog = DateTime.Now,
                            Descricao = $"Inseriu a permissão Id [{result.Id}]"
                        };

                        using (var conn = new HttpService<Log>())
                        {
                            conn.ExecuteService(log, "api/log/salvar");
                        };

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
                        var log = new Log
                        {
                            CodReferencia = result.Id,
                            Processo = "Permissao",
                            UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                            Ip = Request.ServerVariables["REMOTE_ADDR"],
                            DataLog = DateTime.Now,
                            Descricao = $"Removeu a permissão Id [{result.Id}]"
                        };

                        using (var conn = new HttpService<Log>())
                        {
                            conn.ExecuteService(log, "api/log/salvar");
                        };

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

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        [Filtro(Roles = "2")]
        public ActionResult ListarUsuarios()
        {
            try
            {
                var listaUsuarios = new List<Usuario>();

                using (var srv = new HttpService<List<Usuario>>())
                {
                    listaUsuarios = srv.ReturnService("api/usuario/listar");
                }

                return View("_ListarUsuarios", listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Filtro(Roles = "2")]
        public ActionResult PesquisarUsuarios(string nome = "", string email = "", int grupoId = 0)
        {
            try
            {
                var listaUsuarios = new List<Usuario>();

                using (var srv = new HttpService<List<Usuario>>())
                {
                    listaUsuarios = srv.ReturnService("api/usuario/pesquisar?nome=" + nome + "&email=" + email + "&grupoId=" + grupoId);
                }

                return View("_ListarUsuarios", listaUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Usuario(int id = 0)
        {
            try
            {
                var usuario = new Usuario();

                if (id > 0)
                {
                    using (var srv = new HttpService<Usuario>())
                    {
                        usuario = srv.ReturnService("api/usuario/retornar?usuarioId=" + id);
                    }
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Filtro(Roles = "3")]
        public ActionResult SalvarUsuario(Usuario usuario)
        {
            try
            {
                using (var srv = new HttpService<Usuario>())
                {
                    if (usuario.UsuarioId > 0)
                    {
                        var result = srv.ExecuteService(usuario, "api/usuario/alterar");

                        if (result.UsuarioId > 0)
                        {
                            var log = new Log
                            {
                                CodReferencia = result.UsuarioId,
                                Processo = "Usuario",
                                UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                                Ip = Request.ServerVariables["REMOTE_ADDR"],
                                DataLog = DateTime.Now,
                                Descricao = "Alterou o cadastro do usuário"
                            };

                            using (var conn = new HttpService<Log>())
                            {
                                conn.ExecuteService(log, "api/log/salvar");
                            };

                            return Json(new MensagemRetorno { Id = result.UsuarioId, Sucesso = true, Mensagem = "Operação efetuada com sucesso!" });
                        }
                        else
                        {
                            return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });

                        }
                    }
                    else
                    {
                        // Verifica se o Login informado já existe
                        using (var srv2 = new HttpService<bool>())
                        {
                            var verificaLoginExistente = srv2.ReturnService("api/usuario/verifica-login-existente?login=" + usuario.Login);

                            if (verificaLoginExistente)
                                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Já existe um usuário cadastrado com esse Login" });
                        }

                        var result = srv.ExecuteService(usuario, "api/usuario/salvar");

                        if (result.UsuarioId > 0)
                        {
                            var log = new Log
                            {
                                CodReferencia = result.UsuarioId,
                                Processo = "Usuario",
                                UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                                Ip = Request.ServerVariables["REMOTE_ADDR"],
                                DataLog = DateTime.Now,
                                Descricao = "Inseriu o cadastro do usuário"
                            };

                            using (var conn = new HttpService<Log>())
                            {
                                conn.ExecuteService(log, "api/log/salvar");
                            };

                            return Json(new MensagemRetorno { Id = result.UsuarioId, Sucesso = true, Mensagem = "Operação efetuada com sucesso!" });
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


        [HttpPost]
        [Filtro(Roles = "3")]
        public ActionResult EnviarSenha(int usuarioId)
        {
            try
            {
                var usuario = new Usuario();

                using (var srv = new HttpService<Usuario>())
                {
                    usuario = srv.ReturnService("api/usuario/retornar?usuarioId=" + usuarioId);

                    if (usuario != null)
                    {
                        var password = _comumAplicacao.GetRandomPassword();
                        usuario.Password = _comumAplicacao.HashMD5(password);

                        var usuarioAlterado = srv.ExecuteService(usuario, "api/usuario/alterar-senha");

                        Mail.EnviarSenha(usuarioAlterado, password, Request.ServerVariables["REMOTE_ADDR"]);

                        return Json(new MensagemRetorno { Id = usuarioAlterado.UsuarioId, Sucesso = true, Mensagem = "Email enviado com sucesso!" });
                    }
                    else
                    {
                        return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Usuário não localizado!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Usuário não localizado!", Erro = ex.Message });
            }
        }
    }
}