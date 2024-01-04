using app_gastos_reclutadores.Modelos;
using System.Collections.Generic;

namespace app_gastos_reclutadores.Interfaces
{
    public interface ICategoriasRepository
    {
        void Add(Categoria categoria);
        Categoria FindById(int id);
        void Remove(Categoria categoria);
        void SaveChanges();
    }
}
