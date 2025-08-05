using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Gasto
    {
        /*CREATE TABLE gasto (
    id_gasto INT NOT NULL AUTO_INCREMENT,
    descripcion VARCHAR(250) NOT NULL,
    monto DECIMAL(30,3) NOT NULL,
    fecha DATE NOT NULL,
    tipo_gasto_id_tipo_gasto INT NOT NULL,
    vehiculo_id_vehiculo INT NOT NULL,
    PRIMARY KEY (id_gasto)
);*/
        public int id_gasto { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public DateTime fecha { get; set; }
        public int tipo_gasto_id_tipo_gasto { get; set; }
        public int vehiculo_id_vehiculo { get; set; }
    }
}
