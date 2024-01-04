using app_gastos_reclutadores.dtos;
using app_gastos_reclutadores.Modelos;
using System;
using System.Collections.Generic;

namespace app_gastos_reclutadores.Interfaces
{
    public interface IGastosRepository
    {
        void Add(Gasto gasto);
        void SaveChanges();
        IEnumerable<Gasto> GetGastosPorFecha(DateTime fecha);
        IEnumerable<Gasto> GetGastosPorMes(int año, int mes);
        IEnumerable<Gasto> GetGastosPorAño(int año);
        IEnumerable<GastoHistorialDto> GetHistorialGastos();
        bool Any(Func<Gasto, bool> predicate);


    }
}
