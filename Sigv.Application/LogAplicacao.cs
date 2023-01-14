using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Application
{
    public class LogAplicacao
    {
        public IEnumerable<Log> Listar()
        {
            try
            {
                using (var logs = new LogRepositorio())
                {
                    return logs.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Log> Listar(int limit)
        {
            try
            {
                using (var logs = new LogRepositorio())
                {
                    return logs.GetAll()
                        .OrderByDescending(x => x.LogId)
                        .Take(limit)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Log> Listar(string processo, int codReferencia)
        {
            try
            {
                using (var logs = new LogRepositorio())
                {
                    return logs.GetAll()
                        .Where(x => x.Processo == processo && x.CodReferencia == codReferencia)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Log Salvar(Log log)
        {
            try
            {
                using (var logs = new LogRepositorio())
                {
                    logs.Adicionar(log);
                    logs.SalvarTodos();

                    return log;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
