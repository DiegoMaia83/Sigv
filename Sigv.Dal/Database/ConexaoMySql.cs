using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Sigv.Dal.Database
{
    public class ConexaoMySql : IDisposable
    {
        private readonly MySqlConnection conn;

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Mysql"].ToString();

        public ConexaoMySql()
        {
            conn = new MySqlConnection(_connectionString);

            if (conn.State == ConnectionState.Closed) conn.Open();
        }

        //Executa um comando
        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new MySqlCommand(strQuery, conn);

            cmdComando.ExecuteNonQuery();
        }

        //Retorna um Reader
        public MySqlDataReader RetornaComando(string strQuery)
        {
            var cmdComando = new MySqlCommand(strQuery, conn);
            return cmdComando.ExecuteReader();
        }

        //Método que retorna uam consulta em um data tabe
        public DataTable RetornarDataAdapter(string strQuery)
        {
            var cmdComando = new MySqlCommand(strQuery, conn);
            var adapter = new MySqlDataAdapter(cmdComando);
            var datatable = new DataTable();
            adapter.Fill(datatable);
            return datatable;
        }

        public void Dispose()
        {
            //Fecha a conexão
            GC.SuppressFinalize(this);
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }
}
