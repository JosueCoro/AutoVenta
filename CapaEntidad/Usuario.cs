using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        /*CREATE TABLE administracion.usuario 
            (
             id_usuario INTEGER NOT NULL IDENTITY(1,1), 
             nombre VARCHAR (150) NOT NULL , 
             apellido VARCHAR (150) NOT NULL , 
             ci VARCHAR (50) NOT NULL , 
             correo VARCHAR (150) NOT NULL , 
             contraseña VARCHAR (150) NOT NULL , 
             estado BIT NOT NULL , 
             id_rol INTEGER NOT NULL , 
             telefono NVARCHAR (15) NOT NULL 
            )
        GO*/
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string ci { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string telefono { get; set; }
        public bool estado { get; set; }
        public int id_rol { get; set; }
        public Rol _rol { get; set; }
        public string nombre_rol { get; set; }

        
    }
}
