using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace Sigv.Application
{
    public class PermissaoApp
    {
        public List<Permissao> ListarPermissoes()
        {
            try
            {
                using (var permissoes = new PermissaoRepositorio())
                {
                    return permissoes.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PermissaoGrupo InserirPermissaoGrupo(PermissaoGrupo permissao)
        {
            try
            {
                using (var permissoes = new PermissaoGrupoRepositorio())
                {
                    permissoes.Adicionar(permissao);
                    permissoes.SalvarTodos();

                    return permissao;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PermissaoGrupo RemoverPermissaoGrupo(PermissaoGrupo permissao)
        {
            try
            {
                using (var permissoes = new PermissaoGrupoRepositorio())
                {
                    var permissaoDb = permissoes.GetAll().Where(x => x.Id == permissao.Id).FirstOrDefault();

                    permissoes.Excluir(permissaoDb);
                    permissoes.SalvarTodos();

                    return permissao;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Retorna um array com as permissoes do usuário para utilizar nos Roles
        public string[] ListarPermissoesUsuario(string username)
        {
            try
            {
                using (var permissoes = new UsuarioRepositorio())
                {
                    var listapermissoes = permissoes.GetAll()
                        .Where(x => x.Login == username)
                        .FirstOrDefault()
                        .Grupo.PermissaoGrupo.Select(x => x.PermissaoId.ToString()).ToList();

                    return listapermissoes.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool VerificaPermissao(int permissaoId)
        {
            ClaimsPrincipal currentPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            //Identifica a lista de de Claims gravadas ao gerar o token
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            var grupoId = Convert.ToInt32(claims?.FirstOrDefault(x => x.Type.Equals("GrupoId", StringComparison.OrdinalIgnoreCase))?.Value);

            try
            {
                using (var permissoes = new PermissaoGrupoRepositorio())
                {
                    var ret = permissoes.GetAll()
                        .Where(x => x.GrupoId == grupoId && x.PermissaoId == permissaoId).FirstOrDefault() != null;

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
