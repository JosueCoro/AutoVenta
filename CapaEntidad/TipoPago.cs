using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class TipoPago
    {
        /*CREATE TABLE comercial.tipo_pago 
            (
             id_tipo_pago INTEGER NOT NULL IDENTITY(1,1), 
             nombre VARCHAR (150) NOT NULL 
            )
        GO*/
        public int id_tipo_pago { get; set; }
        public string nombre { get; set; }
    }
}
