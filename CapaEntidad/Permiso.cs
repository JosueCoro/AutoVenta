using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Permiso
    {
        /*CREATE TABLE administracion.permiso 
            (
             id_permiso INTEGER NOT NULL IDENTITY(1,1), 
             accion VARCHAR (150) NOT NULL , 
             estado BIT NOT NULL 
            )
        GO*/
        public int id_permiso {  get; set; }
        public string accion { get; set; }
        public bool estado { get; set; }
    }
}
