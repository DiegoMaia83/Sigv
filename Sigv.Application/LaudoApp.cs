using Sigv.Dal.Database;
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

        public string RetornarResumoAvarias(int laudoId)
        {
            try
            {
                using (var conn = new ConexaoMySql())
                {
                    var sql = new StringBuilder();
                    sql.Append(" SELECT GROUP_CONCAT(t2.Descricao SEPARATOR ', ') as 'Avarias' ");
                    sql.Append(" FROM sigv.laudos_avarias_apontamentos t1 ");
                    sql.Append(" Left Join sigv.laudos_avarias t2 on t2.AvariaId = t1.AvariaId ");
                    sql.AppendFormat(" where t1.LaudoId = {0} ", laudoId);

                    var reader = conn.RetornaComando(sql.ToString());

                    string avarias = "";

                    if (reader.Read())
                    {
                        avarias = reader[reader.GetOrdinal("Avarias")].ToString();
                    }

                    return avarias;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IEnumerable<LaudoOpcional> ListarOpcionais()
        {
            try
            {
                using (var opcionais = new LaudoOpcionalRepositorio())
                {
                    return opcionais.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LaudoOpcionalApontamento> ListarOpcionaisApontamentos(int laudoId)
        {
            try
            {
                using (var opcionais = new LaudoOpcionalApontamentoRepositorio())
                {
                    return opcionais.GetAll()
                        .Where(x => x.LaudoId == laudoId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoOpcionalApontamento InserirOpcionalApontamento(LaudoOpcionalApontamento opcional)
        {
            try
            {
                using (var opcionais = new LaudoOpcionalApontamentoRepositorio())
                {
                    opcionais.Adicionar(opcional);
                    opcionais.SalvarTodos();

                    return opcional;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LaudoOpcionalApontamento RemoverOpcionalApontamento(LaudoOpcionalApontamento opcional)
        {
            try
            {
                using (var opcionais = new LaudoOpcionalApontamentoRepositorio())
                {
                    var opcionalDb = opcionais.GetAll()
                        .Where(x => x.LaudoId == opcional.LaudoId && x.OpcionalId == opcional.OpcionalId)
                        .FirstOrDefault();

                    opcionais.Excluir(opcionalDb);
                    opcionais.SalvarTodos();

                    return opcional;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RetornarResumoOpcionais(int laudoId)
        {
            try
            {
                using (var conn = new ConexaoMySql())
                {
                    var sql = new StringBuilder();
                    sql.Append(" SELECT GROUP_CONCAT(t2.Descricao SEPARATOR ', ') as 'Opcionais' ");
                    sql.Append(" FROM sigv.laudos_opcionais_apontamentos t1 ");
                    sql.Append(" Left Join sigv.laudos_opcionais t2 on t2.OpcionalId = t1.OpcionalId ");
                    sql.AppendFormat(" where t1.LaudoId = {0} ", laudoId);

                    var reader = conn.RetornaComando(sql.ToString());

                    string opcionais = "";

                    if (reader.Read())
                    {
                        opcionais = reader[reader.GetOrdinal("Opcionais")].ToString();
                    }

                    return opcionais;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool AlterarSyncStatusFoto(VeiculoFoto foto)
        {
            try
            {
                using (var conn = new ConexaoMySql())
                {
                    var sql = new StringBuilder();
                    sql.Append(" UPDATE sigv.veiculos_foto ");
                    sql.Append(" SET  ");
                    sql.AppendFormat(" SyncStatus = '{0}' ", foto.SyncStatus);
                    sql.AppendFormat(" WHERE FotoId = '{0}' ", foto.FotoId);

                    conn.ExecutaComando(sql.ToString());

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
