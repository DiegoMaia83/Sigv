using Sigv.Application.Util;
using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigv.Application
{
    public class UsuarioApp
    {
        //  Usuários  //
        public Usuario Retornar(string login, string senha)
        {
            try
            {
                var hashPassword = MD5HashEncryDecry.MD5Hash(senha);

                using (var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll()
                        .Where(x => x.Login == login && x.Password == hashPassword && x.Bloqueado == false && x.DataExpira > DateTime.Now)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Retornar(int usuarioId)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll()
                        .Where(x => x.UsuarioId == usuarioId)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VerificaLoginExistente(string login)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll().Where(x => x.Login == login).FirstOrDefault() != null;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> Listar()
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> Pesquisar(string nome = "", string email = "", int grupoId = 0)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    var listaUsuarios = usuarios.GetAll();

                    if (!String.IsNullOrEmpty(nome))
                        listaUsuarios = listaUsuarios.Where(x => x.Nome.ToLower().Contains(nome.ToLower()));

                    if (!String.IsNullOrEmpty(email))
                        listaUsuarios = listaUsuarios.Where(x => x.Email.ToLower().Contains(email.ToLower()));

                    if (grupoId > 0)
                        listaUsuarios = listaUsuarios.Where(x => x.GrupoId == grupoId);

                    return listaUsuarios.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Salvar(Usuario usuario)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    usuarios.Adicionar(usuario);
                    usuarios.SalvarTodos();

                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Alterar(Usuario usuario)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    var usuarioDb = usuarios.GetAll().Where(x => x.UsuarioId == usuario.UsuarioId).FirstOrDefault();                    
                    usuarioDb.Nome = usuario.Nome;
                    usuarioDb.Email = usuario.Email;
                    usuarioDb.Bloqueado = usuario.Bloqueado;
                    usuarioDb.GrupoId = usuario.GrupoId;

                    usuarios.Atualizar(usuarioDb);
                    usuarios.SalvarTodos();

                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario AlterarSenha(Usuario usuario)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    var usuarioDb = usuarios.GetAll().Where(x => x.UsuarioId == usuario.UsuarioId).FirstOrDefault();
                    usuarioDb.Password = usuario.Password;
                    usuarioDb.DataExpira = DateTime.Now;

                    usuarios.Atualizar(usuarioDb);
                    usuarios.SalvarTodos();

                    return usuarioDb;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //  Grupos Usuários  //

        public List<UsuarioGrupo> ListarGrupos()
        {
            try
            {
                using(var grupos = new UsuarioGrupoRepositorio())
                {
                    return grupos.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
