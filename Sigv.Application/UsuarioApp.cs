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
                using(var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll()
                        .Where(x => x.Login == login && x.Password == senha && x.Bloqueado == false && x.DataExpira > DateTime.Now)
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
                    usuarios.Atualizar(usuario);
                    usuarios.SalvarTodos();

                    return usuario;
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
