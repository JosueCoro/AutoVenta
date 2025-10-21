using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_venta
    {
        /*CREATE TABLE comercial.detalle_venta 
            (
             id_detalle_venta INTEGER NOT NULL IDENTITY(1,1), 
             precio_venta DECIMAL (30,3) NOT NULL , 
             id_vehiculo INTEGER NOT NULL , 
             id_venta INTEGER NOT NULL 
            )
        GO*/
        public int id_detalle_venta {  get; set; }
        public float precion_venta { get; set; }
        public int id_vehiculo { get; set; }
        public Vehiculo _vehiculo { get; set; }
        public int id_venta { get; set; }
        public Venta _venta { get; set; }
    }
}
