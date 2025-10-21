using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Asesor
    {
        /*CREATE TABLE administracion.asesor 
            (
             id_asesor INTEGER NOT NULL IDENTITY(1,1), 
             nombre VARCHAR (150) NOT NULL , 
             apellidos VARCHAR (150) NOT NULL , 
             telefono NVARCHAR (15) NOT NULL , 
             ci VARCHAR (50) NOT NULL , 
             direccion VARCHAR (255) NOT NULL 
            )
        GO*/

        public int id_asesor {  get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string ci {  get; set; }
        public string direccion { get; set; }
    }
}
