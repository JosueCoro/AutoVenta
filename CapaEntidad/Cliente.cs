using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        /*CREATE TABLE comercial.cliente 
            (
             id_cliente INTEGER NOT NULL IDENTITY(1,1), 
             nombre_completo VARCHAR (150) NOT NULL , 
             ci_nit VARCHAR (150) NOT NULL , 
             telefono VARCHAR (20) NOT NULL , 
             direccion VARCHAR (150) 
            )
        GO
        */
        public int id_cliente { get; set; }
        public string nombre_completo { get; set; }
        public string ci_nit { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }
}
