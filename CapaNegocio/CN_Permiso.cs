using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_RolPermiso cdPermiso = new CD_RolPermiso();
        public List<Permiso> ListarPermisosPorRol(int idRol)
        {
            
            if (idRol <= 0)
            {
                return new List<Permiso>();
            }

            return cdPermiso.ListarPermisosPorRol(idRol);
        }
        public bool GuardarPermisos(int idRol, List<Permiso> permisosAsignados, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idRol <= 0)
            {
                Mensaje = "Debe especificar un Rol válido.";
                return false;
            }

            if (idUsuarioAuditoria <= 0)
            {
                Mensaje = "Usuario de auditoría no válido.";
                return false;
            }


            return cdPermiso.GuardarPermisos(idRol, permisosAsignados, idUsuarioAuditoria, out Mensaje);
        }
    }
}
