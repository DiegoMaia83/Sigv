﻿using Sigv.Dal.Repositorio;
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
        public Laudo Retornar(int laudoId)
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

        public IEnumerable<Laudo> Listar()
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

        public IEnumerable<Laudo> Listar(int statusId)
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

        public Laudo InserirLaudo(Laudo laudo)
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

        public Laudo FinalizarLaudo(Laudo laudo)
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

        public Laudo ReabrirLaudo(Laudo laudo)
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

    }
}
