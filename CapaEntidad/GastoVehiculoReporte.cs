using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class GastoVehiculoReporte
    {
        //gasto del vehoculo
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public string fecha { get; set; }
        public string TipoGasto { get; set; }
    }
}
