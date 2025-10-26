using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleGasto
    {
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public int id_tipo_gasto { get; set; }
    }
}
