using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class VentaTransaccion
    {
        public Venta oVenta { get; set; }
        public decimal MontoPago { get; set; }
        public int IdTipoPago { get; set; }
    }
}
