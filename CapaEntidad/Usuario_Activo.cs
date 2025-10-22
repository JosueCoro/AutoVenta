using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario_Activo
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string ci { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public bool estado { get; set; }
        public string telefono { get; set; }
        public int id_rol { get; set; }
        public Rol _rol { get; set; }
        public string nombre_rol { get; set; }
        public bool EstadoRol { get; set; }

        public List<Permiso> ListaPermisos { get; set; }
        public List<int> ListaIdPermisos { get; set; }
    }
}
