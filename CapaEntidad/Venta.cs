using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        /*CREATE TABLE comercial.venta 
            (
             id_venta INTEGER NOT NULL IDENTITY(1,1), 
             fecha DATE NOT NULL , 
             total DECIMAL (30,3) NOT NULL , 
             observaciones VARCHAR (250) , 
             id_cliente INTEGER NOT NULL , 
             id_usuario INTEGER NOT NULL , 
             comision DECIMAL (18,2) NOT NULL 
            )
        GO*/
        public int id_venta {  get; set; }
        public DateTime fecha { get; set; }
        public float total { get; set; }
        public float comision { get; set; }
        public string observaciones { get; set; }
        public int id_cliente { get; set; }
        public int id_usuario { get; set; }
        public Usuario _usuario { get; set; }
        public Cliente _cliente { get; set; }

    }
}
