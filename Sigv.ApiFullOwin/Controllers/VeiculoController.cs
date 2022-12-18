﻿using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sigv.ApiFullOwin.Controllers
{
    public class VeiculoController : ApiController
    {
        private readonly VeiculoApp _veiculoApp = new VeiculoApp();

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/retornar")]
        public Veiculo Retornar(int veiculoId)
        {
            try
            {
                return _veiculoApp.Retornar(veiculoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar")]
        public IEnumerable<Veiculo> Listar()
        {
            try
            {
                return _veiculoApp.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo/salvar")]
        public Veiculo Salvar(Veiculo veiculo)
        {
            try
            {
                return _veiculoApp.Salvar(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/veiculo/alterar")]
        public Veiculo Alterar(Veiculo veiculo)
        {
            try
            {
                return _veiculoApp.Alterar(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-combustiveis")]
        public IEnumerable<VeiculoCombustivel> ListarCombustiveis()
        {
            try
            {
                return _veiculoApp.ListarCombustiveis();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-condicoes")]
        public IEnumerable<VeiculoCondicao> ListarCondicoes()
        {
            try
            {
                return _veiculoApp.ListarCondicoes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-especies")]
        public IEnumerable<VeiculoEspecie> ListarEspecies()
        {
            try
            {
                return _veiculoApp.ListarEspecies();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/veiculo/listar-status")]
        public IEnumerable<VeiculoStatus> ListarStatus()
        {
            try
            {
                return _veiculoApp.ListarStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}