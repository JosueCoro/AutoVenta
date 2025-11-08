using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class VentaSimple
    {
        public int id_venta { get; set; }
        public string fecha { get; set; }
        public decimal total { get; set; }
        public string NombreCliente { get; set; }
        public string CiNitCliente { get; set; }
        public string VehiculosVendidos { get; set; }
    }
}
