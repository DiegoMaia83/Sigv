using Sigv.Dal.Database;
using Sigv.Dal.Repositorio;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Application
{
    public class VeiculoApp
    {
        private readonly LaudoApp _laudoApp = new LaudoApp();

        public Veiculo Retornar(int veiculoId)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    return veiculos.GetAll().Where(x => x.VeiculoId == veiculoId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Veiculo> Pesquisar(string filtro = "", string param = "", int condicaoId = 0, int statusId = 0, int especieId = 0, string dataEntradaInicial = "", string dataEntradaFinal = "")
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    var listaVeiculos = veiculos.GetAll().ToList();

                    if (!String.IsNullOrEmpty(filtro) && !String.IsNullOrEmpty(param))
                    {
                        if (filtro == "veiculoId")
                        {
                            listaVeiculos = listaVeiculos.Where(x => x.VeiculoId == Convert.ToInt32(param)).ToList();
                        }
                        else if (filtro == "placa")
                        {
                            listaVeiculos = listaVeiculos.Where(x => x.Placa.Replace("-","").Trim().ToLower().Contains(param.Replace("-", "").Trim().ToLower())).ToList();
                        }
                        else if (filtro == "chassi")
                        {
                            listaVeiculos = listaVeiculos.Where(x => x.Chassi.Trim().ToLower().Contains(param.Trim().ToLower())).ToList();
                        }
                    }

                    if (condicaoId > 0)
                        listaVeiculos = listaVeiculos.Where(x => x.CondicaoId == condicaoId).ToList();

                    if (especieId > 0)
                        listaVeiculos = listaVeiculos.Where(x => x.EspecieId == especieId).ToList();

                    if (statusId > 0)
                        listaVeiculos = listaVeiculos.Where(x => x.StatusId == statusId).ToList();

                    if (!String.IsNullOrEmpty(dataEntradaInicial))
                        listaVeiculos = listaVeiculos.Where(x => x.DataEntrada >= Convert.ToDateTime(dataEntradaInicial)).ToList();

                    if (!String.IsNullOrEmpty(dataEntradaFinal))
                        listaVeiculos = listaVeiculos.Where(x => x.DataEntrada >= Convert.ToDateTime(dataEntradaFinal)).ToList();


                    return listaVeiculos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Veiculo> ListarVeiculosPorStatusLaudo(int statusId = 0)
        {
            try
            {
                using (var conn = new ConexaoMySql())
                {
                    var sql = new StringBuilder();
                    sql.Append(" SELECT t1.*, ");
                    sql.Append(" t2.LaudoId, t2.StatusId as 'LaudoStatusId' ");
                    sql.Append(" FROM sigv.veiculos t1 ");
                    sql.Append(" Left Join sigv.laudos t2 on t2.VeiculoId = t1.VeiculoId ");

                    if (statusId == 0)
                    {
                        sql.Append(" where t2.LaudoId is null ");
                    }
                    else if (statusId == 1)
                    {
                        sql.Append(" where t2.StatusId = 1 ");
                    }
                    else if (statusId == 2)
                    {
                        sql.Append(" where t2.StatusId = 2 ");
                        sql.Append(" and t2.DataFechamento >= DATE_SUB(NOW(), INTERVAL 30 DAY) ");
                    }

                    var listaVeiculos = new List<Veiculo>();

                    var reader = conn.RetornaComando(sql.ToString());

                    while (reader.Read()) 
                    { 
                        var veiculo = new Veiculo();
                        veiculo.VeiculoId = reader.GetInt32(reader.GetOrdinal("VeiculoId"));
                        veiculo.Marca = reader[reader.GetOrdinal("Marca")].ToString();
                        veiculo.Modelo = reader[reader.GetOrdinal("Modelo")].ToString();
                        veiculo.AnoFabricacao = reader[reader.GetOrdinal("AnoFabricacao")] != DBNull.Value ? reader.GetInt32("AnoFabricacao") : 0;
                        veiculo.AnoModelo = reader[reader.GetOrdinal("AnoModelo")] != DBNull.Value ? reader.GetInt32("AnoModelo") : 0;
                        veiculo.Placa = reader[reader.GetOrdinal("Placa")].ToString();

                        veiculo.LaudoId = reader[reader.GetOrdinal("LaudoId")] != DBNull.Value ? reader.GetInt32("LaudoId") : 0;
                        veiculo.LaudoStatusId = reader[reader.GetOrdinal("LaudoStatusId")] != DBNull.Value ? reader.GetInt32("LaudoStatusId") : 0;

                        listaVeiculos.Add(veiculo);
                    }

                    return listaVeiculos;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Veiculo Salvar(Veiculo veiculo)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    veiculos.Adicionar(veiculo);
                    veiculos.SalvarTodos();

                    return veiculo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Veiculo Alterar(Veiculo veiculo)
        {
            try
            {
                using (var veiculos = new VeiculoRepositorio())
                {
                    veiculos.Atualizar(veiculo);
                    veiculos.SalvarTodos();

                    return veiculo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Ocorrências //

        public IEnumerable<VeiculoOcorrencia> ListarOcorrencias(int veiculoId)
        {
            try
            {
                using (var ocorrencias = new VeiculoOcorrenciaRepositorio())
                {
                    return ocorrencias.GetAll().Where(x => x.VeiculoId == veiculoId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VeiculoOcorrencia SalvarOcorrencia(VeiculoOcorrencia ocorrencia)
        {
            try
            {
                using (var ocorrencias = new VeiculoOcorrenciaRepositorio())
                {
                    ocorrencias.Adicionar(ocorrencia);
                    ocorrencias.SalvarTodos();

                    return ocorrencia;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Fotos //

        public IEnumerable<VeiculoFoto> ListarFotos(int veiculoId)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    return fotos.GetAll().Where(x => x.VeiculoId == veiculoId && x.Excluida == false).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoFoto> ListarFotos(int veiculoId, string tipo)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    return fotos.GetAll().Where(x => x.VeiculoId == veiculoId && x.Tipo == tipo && x.Excluida == false).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VeiculoFoto SalvarFoto(VeiculoFoto foto)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    fotos.Adicionar(foto);
                    fotos.SalvarTodos();

                    return foto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VeiculoFoto RemoverFoto(VeiculoFoto foto)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    var fotoDb = fotos.GetAll().Where(x => x.FotoId == foto.FotoId).FirstOrDefault();
                    fotoDb.Excluida = true;
                    fotoDb.UsuExclusao = foto.UsuExclusao;
                    fotoDb.DataExclusao= DateTime.Now;

                    fotos.Atualizar(fotoDb);
                    fotos.SalvarTodos();

                    return fotoDb;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int RetornarUltimaInserida(int veiculoId, string tipo)
        {
            try
            {
                using (var fotos = new VeiculoFotoRepositorio())
                {
                    return fotos.GetAll()
                        .Where(x => x.VeiculoId == veiculoId && x.Tipo == tipo)
                        .OrderByDescending(x => x.NumeroFoto)
                        .Select(x => x.NumeroFoto)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CopiarParaDiretorio(VeiculoFoto arq)
        {
            try
            {
                var ultimaInserida = RetornarUltimaInserida(arq.VeiculoId, arq.Tipo);
                var numFoto = ultimaInserida + 1;
                var extensao = Path.GetExtension(arq.SourcePath);
                var arquivo = arq.Tipo + arq.VeiculoId.ToString("000000") + "_" + numFoto.ToString("00") + extensao;

                var newFile = Path.Combine(arq.TargetPath, arquivo);

                if (!Directory.Exists(arq.TargetPath))
                    Directory.CreateDirectory(arq.TargetPath);


                if (!File.Exists(newFile))
                    File.Move(arq.SourcePath, newFile);


                var foto = new VeiculoFoto
                {
                    VeiculoId = arq.VeiculoId,
                    NumeroFoto = numFoto,
                    Tipo = arq.Tipo,
                    Extensao = extensao,
                    UsuCriacao = arq.UsuCriacao,
                    DataCriacao = DateTime.Now
                };

                var fotoInserida = SalvarFoto(foto);

                return fotoInserida.FotoId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IEnumerable<VeiculoCombustivel> ListarCombustiveis()
        {
            try
            {
                using (var combustiveis = new VeiculoCombustivelRepositorio())
                {
                    return combustiveis.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoCondicao> ListarCondicoes()
        {
            try
            {
                using (var condicoes = new VeiculoCondicaoRepositorio())
                {
                    return condicoes.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoEspecie> ListarEspecies()
        {
            try
            {
                using (var especies = new VeiculoEspecieRepositorio())
                {
                    return especies.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoStatus> ListarStatus()
        {
            try
            {
                using (var status = new VeiculoStatusRepositorio())
                {
                    return status.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VeiculoCor> ListarCores()
        {
            try
            {
                using (var cores = new VeiculoCorRepositorio())
                {
                    return cores.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
