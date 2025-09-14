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

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrEmpty(obj.apellido) || string.IsNullOrEmpty(obj.correo) || string.IsNullOrEmpty(obj.contraseña))
            {
                Mensaje = "Todos los campos obligatorios deben ser completados.";
                return 0;
            }

            // Se encripta la contraseña antes de guardarla
            obj.contraseña = recursos.ConvertirSha256(obj.contraseña);

            return cdusuario.Registrar(obj, out Mensaje);
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_usuario == 0)
            {
                Mensaje = "El ID del usuario no es válido.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrEmpty(obj.apellido) || string.IsNullOrEmpty(obj.correo))
            {
                Mensaje = "Los campos de nombre, apellido y correo no pueden ser vacíos.";
                return false;
            }

            // Solo se encripta la nueva contraseña si se proporciona un valor
            if (!string.IsNullOrEmpty(obj.contraseña))
            {
                obj.contraseña = recursos.ConvertirSha256(obj.contraseña);
            }

            return cdusuario.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del usuario no es válido.";
                return false;
            }

            return cdusuario.Eliminar(id, out Mensaje);
        }


        // ... (otros métodos Listar, Registrar, Editar, Eliminar existentes) ...

        // Nuevo método para validar el login
        public Usuario_Activo ValidarLogin(string correo, string contrasena, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones básicas
            if (string.IsNullOrEmpty(correo))
            {
                Mensaje = "El correo es obligatorio.";
                return null;
            }
            if (string.IsNullOrEmpty(contrasena))
            {
                Mensaje = "La contraseña es obligatoria.";
                return null;
            }

            // Hashear la contraseña antes de enviarla a la capa de datos
            string contrasenaHasheada = recursos.ConvertirSha256(contrasena);

            // Llamar al método de la capa de datos
            Usuario_Activo usuarioEncontrado = cdusuario.ValidarLogin(correo, contrasenaHasheada);

            if (usuarioEncontrado == null)
            {
                Mensaje = "Credenciales incorrectas o usuario inactivo.";
            }
            else
            {
                Mensaje = "Login exitoso.";
            }

            return usuarioEncontrado;
        }
    }
}
