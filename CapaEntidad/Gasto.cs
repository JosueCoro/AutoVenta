using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Gasto
    {
        /*CREATE TABLE comercial.gasto 
            (
             id_gasto INTEGER NOT NULL IDENTITY(1,1), 
             descripcion VARCHAR (250) NOT NULL , 
             monto DECIMAL (30,3) NOT NULL , 
             fecha DATE NOT NULL , 
             id_tipo_gasto INTEGER NOT NULL , 
             id_vehiculo INTEGER , 
             id_venta INTEGER 
            )
        GO;*/
        public int id_gasto { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public string fecha { get; set; }
        public int id_tipo_gasto { get; set; }
        public TipoGasto oTipoGasto { get; set; }
        public int? id_vehiculo { get; set; }
        public Vehiculo oVehiculo { get; set; }
        public int? id_venta { get; set; }
        public Venta oVenta { get; set; }
        public string InfoAsociacion { get; set; }
    }
}
