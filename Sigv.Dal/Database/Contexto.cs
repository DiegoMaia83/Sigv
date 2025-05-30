﻿using Sigv.Domain;
using System.Data.Entity;

namespace Sigv.Dal.Database
{
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class Contexto : DbContext
    {
        public Contexto() : base("name=MySql")
        {
            //Arquivo ConnectionStrings.config            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioGrupo> UsuariosGrupos { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<PermissaoGrupo> PermissoesGrupos { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Acesso> Acessos { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<VeiculoCombustivel> VeiculosCombustiveis { get; set; }
        public DbSet<VeiculoCondicao> VeiculosCondicoes { get; set; }
        public DbSet<VeiculoEspecie> VeiculosEspecies { get; set; }
        public DbSet<VeiculoStatus> VeiculosStatus { get; set; }
        public DbSet<VeiculoCor> VeiculosCores { get; set; }
        public DbSet<VeiculoOcorrencia> VeiculosOcorrencias { get; set; }
        public DbSet<VeiculoFoto> VeiculosFotos { get; set; }

        public DbSet<LaudoVeiculo> Laudos { get; set; }
        public DbSet<LaudoStatus> LaudosStatus { get; set; }
        public DbSet<LaudoAvaria> LaudosAvarias { get; set; }
        public DbSet<LaudoAvariaApontamento> LaudosAvariasApontamentos { get; set; }
        public DbSet<LaudoOpcional> LaudosOpcionais { get; set; }
        public DbSet<LaudoOpcionalApontamento> LaudosOpcionaisApontamentos { get; set; }
    }
}
