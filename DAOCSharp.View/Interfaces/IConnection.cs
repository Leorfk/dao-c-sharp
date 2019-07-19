using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAOCSharp.View.Interfaces
{
    public interface IConnection
    {
        SqlConnection Abrir();
        SqlConnection Buscar();
        void Fechar();
    }
}
