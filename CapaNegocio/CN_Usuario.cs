using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario cdusuario = new CD_Usuario();
        private CN_Recursos recursos = new CN_Recursos(); 

        public List<Usuario> Listar()
        {
            return cdusuario.Listar();
        }


        public int Registrar(Usuario obj, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.apellido))
            {
                Mensaje = "El apellido es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.ci))
            {
                Mensaje = "La Cédula de Identidad (CI) es obligatoria.";
                return 0;
            }
            if (!obj.ci.All(char.IsDigit))
            {
                Mensaje = "La Cédula de Identidad (CI) debe contener solo números.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.correo))
            {
                Mensaje = "El correo es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.contraseña))
            {
                Mensaje = "La contraseña es obligatoria para el nuevo usuario.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El teléfono es obligatorio.";
                return 0;
            }
            if (!obj.telefono.All(char.IsDigit))
            {
                Mensaje = "El teléfono debe contener solo números.";
                return 0;
            }
            if (obj.oRol == null || obj.oRol.id_rol == 0)
            {
                Mensaje = "Debe seleccionar un rol.";
                return 0;
            }

            obj.contraseña = recursos.EncriptarSHA256(obj.contraseña);

            return cdusuario.Registrar(obj, idUsuarioAuditoria, out Mensaje);
        }

        public bool Editar(Usuario obj, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_usuario == 0)
            {
                Mensaje = "El ID del usuario no es válido.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.apellido))
            {
                Mensaje = "El apellido es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.ci))
            {
                Mensaje = "La Cédula de Identidad (CI) es obligatoria.";
                return false;
            }
            if (!obj.ci.All(char.IsDigit))
            {
                Mensaje = "La Cédula de Identidad (CI) debe contener solo números.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.correo))
            {
                Mensaje = "El correo es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El teléfono es obligatorio.";
                return false;
            }
            if (!obj.telefono.All(char.IsDigit))
            {
                Mensaje = "El teléfono debe contener solo números.";
                return false;
            }
            if (obj.oRol == null || obj.oRol.id_rol == 0 )
            {
                Mensaje = "Debe seleccionar un rol.";
                return false;
            }

            if (!string.IsNullOrEmpty(obj.contraseña))
            {
                obj.contraseña = recursos.EncriptarSHA256(obj.contraseña);
            }

            return cdusuario.Editar(obj, idUsuarioAuditoria, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuarioAuditoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0) Mensaje = "El ID del usuario no es válido.";
            if (id == idUsuarioAuditoria) Mensaje = "No puedes eliminar o inactivar tu propia cuenta.";

            if (!string.IsNullOrEmpty(Mensaje)) return false;

            return cdusuario.Eliminar(id, idUsuarioAuditoria, out Mensaje);
        }
    }
}
