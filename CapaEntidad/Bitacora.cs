using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaEntidad
{
    public class Bitacora
    {
        public int id_bitacora { get; set; }
        public string accion { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; } 

        public int id_usuario { get; set; }
        public Usuario oUsuario { get; set; }

    }
}