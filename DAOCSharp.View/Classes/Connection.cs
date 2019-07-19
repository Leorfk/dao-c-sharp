using DAOCSharp.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DAOCSharp.View.Classes
{
    public class Connection : IConnection, IDisposable
    {
        private SqlConnection _connection;

        public Connection()
        {
            _connection = new SqlConnection("" +
                "Data Source=CASA2; " +
                "Initial Catalog=cinema; " +
                "Integrated Security=True");
        }
        public SqlConnection Abrir()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public SqlConnection Buscar()
        {
            return this.Abrir();
        }

        public void Fechar()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            this.Fechar();
            GC.SuppressFinalize(this);
        }
    }
}
