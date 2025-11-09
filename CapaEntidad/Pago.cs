using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pago
    {
        /*CREATE TABLE comercial.pago 
            (
             id_pago INTEGER NOT NULL IDENTITY(1,1), 
             nombre VARCHAR (150) NOT NULL , 
             fecha DATE NOT NULL , 
             hora TIME NOT NULL , 
             id_tipo_pago INTEGER NOT NULL , 
             id_venta INTEGER NOT NULL 
            )
        GO*/
        public int id_pago {  get; set; }
        public string nombre { get; set; }
        public String fecha { get; set; }
        public String hora { get; set; }
        public decimal monto { get; set; }
        public int id_tipo_pago { get; set; }
        public TipoPago oTipopago{ get; set; }
        public string nombreTP { get; set; }
        public int id_venta { get; set; }
        public Venta oVenta { get; set; }
    }
}
