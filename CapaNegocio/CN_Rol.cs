using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDato;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Rol
    {
        private CD_Rol cdrol = new CD_Rol();

        public List<Rol> Listar()
        {
            return cdrol.Listar();
        }

        public int Registrar(Rol obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del rol es obligatorio.";
                return 0;
            }
            if (obj.estado == null)
            {
                Mensaje = "El estado del rol (Activo/Inactivo) es obligatorio.";
                return 0;
            }

            return cdrol.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(Rol obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_rol == 0)
            {
                Mensaje = "El ID del rol no es válido.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre del rol es obligatorio.";
                return false;
            }
            if (obj.estado == null)
            {
                Mensaje = "El estado del rol (Activo/Inactivo) es obligatorio.";
                return false;
            }

            return cdrol.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del rol no es válido.";
                return false;
            }

            return cdrol.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
