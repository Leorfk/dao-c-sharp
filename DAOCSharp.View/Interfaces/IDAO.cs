using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAOCSharp.View.Interfaces
{
    public interface IDAO<T> : IDisposable
        where T : class, new()
    {
        T Inserir(T model);
        void Atualizar(T model);
        bool Remover(T model);
        T BuscarPorID(params Object[] keys);
        Collection<T> ListarTudo();
    }
}
