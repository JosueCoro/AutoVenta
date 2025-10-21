using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Comision
    {
        /*CREATE TABLE comercial.comision 
            (
             id_comision INTEGER NOT NULL IDENTITY(1,1), 
             monto DECIMAL (18,2) NOT NULL , 
             fecha_pago DATETIME2 , 
             fecha_generacion DATETIME2 NOT NULL , 
             estado VARCHAR (50) NOT NULL , 
             observaciones NVARCHAR (255) NOT NULL , 
             id_venta INTEGER NOT NULL , 
             id_asesor INTEGER NOT NULL 
            )
        GO*/

        public int id_comision {  get; set; }
        public float monto { get; set; }
        public DateTime fecha_pago { get; set; }
        public DateTime fecha_generacion { get; set; }
        public string estado { get; set; }
        public string observaciones { get; set; }
        public int id_venta { get; set; }
        public int id_asesor { get; set; }
        public Venta _venta { get; set; }
        public Asesor _asesor { get; set; }
    }
}
