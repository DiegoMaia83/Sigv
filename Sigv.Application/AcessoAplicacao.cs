using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigv.Application
{
    public class AcessoAplicacao
    {
        public IEnumerable<Acesso> Listar(int limit)
        {
            try
            {
                using (var acessos = new AcessoRepositorio())
                {
                    return acessos.GetAll()
                        .OrderByDescending(x => x.AcessoId)
                        .Take(limit)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Acesso Salvar(Acesso acesso)
        {
            try
            {
                using (var acessos = new AcessoRepositorio())
                {
                    acessos.Adicionar(acesso);
                    acessos.SalvarTodos();

                    return acesso;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
