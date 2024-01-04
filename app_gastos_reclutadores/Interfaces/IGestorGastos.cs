using app_gastos_reclutadores.dtos;
using app_gastos_reclutadores.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_gastos_reclutadores.Interfaces
{
    public interface IGestorDeGastos
    {
        void RegistrarGasto(GastoDto gastoDto);
        void AnadirCategoria(string nombreCategoria);
        void EditarCategoria(int id, string nuevoNombre);
        void EliminarCategoria(int id);
        void GenerarInforme(TipoInforme tipoInforme);
        void VerHistorialGastos();
    }
}
