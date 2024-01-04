using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_gastos_reclutadores.dtos
{
    public class GastoHistorialDto
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public decimal Monto { get; set; }
    }

}
