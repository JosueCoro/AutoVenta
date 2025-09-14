using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario_Activo
    {
        public int id_usuario_activo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string ci { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public bool estado { get; set; }
        public int rol_id_rol { get; set; }
        public Rol _rol { get; set; }
        public string nombre_rol { get; set; }
    }
}
