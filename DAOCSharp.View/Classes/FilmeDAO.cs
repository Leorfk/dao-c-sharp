using DAOCSharp.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace DAOCSharp.View.Classes
{
    public class FilmeDAO:IDAO<Filme>, IDisposable
    {
        private IConnection _connection;

        public FilmeDAO(IConnection Connection)
        {
            this._connection = Connection;
        }

        public void Atualizar(Filme model)
        {
            using (SqlCommand comando = _connection.Buscar().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "update filme set nome=@nome, preco=@preco, ano=@ano where id=@id";

                comando.Parameters.Add("@nome", SqlDbType.Text).Value = model.Nome;
                comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = model.Preco;
                comando.Parameters.Add("@ano", SqlDbType.Text).Value = model.Ano;
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;
                comando.ExecuteNonQuery();
            }
        }

        public Filme BuscarPorID(params object[] keys)
        {
            Filme filme = null;
            using (SqlCommand comando = _connection.Buscar().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select id, noem, preco, ano from filme where id=@id";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = keys[0];

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        filme = new Filme();
                        reader.Read();
                        filme.Id = reader.GetInt32(0);
                        filme.Nome = reader.GetString(1);
                        filme.Preco = reader.GetDouble(2);
                        filme.Ano = reader.GetString(3);
                    }
                }
            }
            return filme;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Filme Inserir(Filme model)
        {
            using (SqlCommand comando = _connection.Buscar().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "" +
                    "insert into filme (nome, preco, ano) values " +
                    "(@nome, @preco, @ano);Select @Identity";
                comando.Parameters.Add("@nome", SqlDbType.Text).Value = model.Nome;
                comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = model.Preco;
                comando.Parameters.Add("@ano", SqlDbType.Text).Value = model.Ano;
            }
            return model;
        }

        public Collection<Filme> ListarTudo()
        {
            Collection<Filme> colecao = new Collection<Filme>();

            using (SqlCommand comando = _connection.Buscar().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select id, nome, preco, ano from filme order by nome";

                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        Filme filme = new Filme
                        {
                            Id = int.Parse(row["id"].ToString()),
                            Nome = row["nome"].ToString(),
                            Preco = Convert.ToDouble(row["preco"].ToString()),
                            Ano = row["ano"].ToString()
                        };
                        colecao.Add(filme);
                    }
                }
            }
            return colecao;
        }

        public bool Remover(Filme model)
        {
            bool retornar = false;
            using(SqlCommand comando = _connection.Buscar().CreateCommand())
            {
                comando.CommandType = CommandType.Text;
                comando.CommandText = "delete from filme where id=@id";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = model.Id;

                if (comando.ExecuteNonQuery() > 0)
                {
                    retornar = true;
                }
            }
            return retornar;
        }
    }
}
