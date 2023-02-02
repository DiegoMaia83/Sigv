using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Application
{
    public class LaudoApp
    {
        public LaudoVeiculo Retornar(int laudoId)
        {
            try
            {
                using (var laudos = new LaudoRepositorio())
                {
                    return laudos.GetAll().Where(x => x.LaudoId== laudoId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LaudoVeiculo> Listar()
        {
            try
            {
                using (var laudos = new LaudoRepositorio())
                {
                    return laudos.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LaudoVeiculo> Listar(int statusId)
        {
            try
            {
                using (var laudos = new LaudoRepositorio())
                {
                    return laudos.GetAll().Where(x => x.StatusId == statusId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoVeiculo InserirLaudo(LaudoVeiculo laudo)
        {
            try
            {
                using (var laudos = new LaudoRepositorio())
                {
                    laudos.Adicionar(laudo);
                    laudos.SalvarTodos();

                    return laudo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoVeiculo FinalizarLaudo(LaudoVeiculo laudo)
        {
            try
            {
                using (var laudos = new LaudoRepositorio())
                {
                    var laudoDb = laudos.GetAll().Where(x => x.LaudoId == laudo.LaudoId).FirstOrDefault();
                    laudoDb.StatusId = 2;
                    laudoDb.DataFechamento = laudo.DataFechamento;
                    laudoDb.UsuarioFechamento = laudo.UsuarioFechamento;

                    laudos.Atualizar(laudoDb);
                    laudos.SalvarTodos();

                    return laudoDb;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoVeiculo ReabrirLaudo(LaudoVeiculo laudo)
        {
            try
            {
                using (var laudos = new LaudoRepositorio())
                {
                    var laudoDb = laudos.GetAll().Where(x => x.LaudoId == laudo.LaudoId).FirstOrDefault();
                    laudoDb.StatusId = 1;
                    laudoDb.DataReabre = laudo.DataReabre;
                    laudoDb.UsuarioReabre = laudo.UsuarioReabre;

                    laudos.Atualizar(laudoDb);
                    laudos.SalvarTodos();

                    return laudoDb;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IEnumerable<LaudoAvaria> ListarAvarias()
        {
            try
            {
                using (var avarias = new LaudoAvariaRepositorio())
                {
                    return avarias.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LaudoAvariaApontamento> ListarAvariasApomtamentos(int laudoId)
        {
            try
            {
                using (var apontamentos = new LaudoAvariaApontamentoRepositorio())
                {
                    return apontamentos.GetAll()
                        .Where(x => x.LaudoId == laudoId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoAvariaApontamento InserirAvariaApontamento(LaudoAvariaApontamento apontamento)
        {
            try
            {
                using (var apontamentos = new LaudoAvariaApontamentoRepositorio())
                {
                    apontamentos.Adicionar(apontamento);
                    apontamentos.SalvarTodos();

                    return apontamento;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoAvariaApontamento RemoverAvariaApontamento(LaudoAvariaApontamento apontamento)
        {
            try
            {
                using (var apontamentos = new LaudoAvariaApontamentoRepositorio())
                {
                    var apontamentoDb = apontamentos.GetAll()
                        .Where(x => x.LaudoId == apontamento.LaudoId && x.AvariaId == apontamento.AvariaId)
                        .FirstOrDefault();
                        
                    apontamentos.Excluir(apontamentoDb);
                    apontamentos.SalvarTodos();

                    return apontamento;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
