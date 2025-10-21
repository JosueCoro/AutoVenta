using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Bitacora
    {
        /*CREATE TABLE administracion.bitacora 
            (
             id_bitacora INTEGER NOT NULL IDENTITY(1,1), 
             accion NVARCHAR (255) NOT NULL , 
             fecha DATE NOT NULL , 
             hora TIME NOT NULL , 
             id_usuario INTEGER NOT NULL 
            )
        GO*/

        public int id_bitacora {  get; set; }
        public string accion {  get; set; }

        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }

        public int id_usuario { get; set; }

        public Usuario _usuario { get; set; }
    }
}
