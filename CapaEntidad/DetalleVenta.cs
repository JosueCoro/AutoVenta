using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int id_detalle_venta { get; set; } 
        public int id_vehiculo { get; set; }
        public decimal precio_venta { get; set; } 
    }
}