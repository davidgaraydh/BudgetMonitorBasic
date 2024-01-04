using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_gastos_reclutadores.dtos
{
    public class GastoDto
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaID { get; set; }
    }

}
