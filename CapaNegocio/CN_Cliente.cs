using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDato;
using System.Data;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente cdcliente = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return cdcliente.Listar();
        }

        public int Registrar(Cliente obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre_completo))
            {
                Mensaje = "El nombre completo es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.ci_nit))
            {
                Mensaje = "El CI/NIT es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El teléfono es obligatorio.";
                return 0;
            }

            return cdcliente.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(Cliente obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_cliente == 0)
            {
                Mensaje = "El ID del cliente no es válido.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.nombre_completo))
            {
                Mensaje = "El nombre completo es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.ci_nit))
            {
                Mensaje = "El CI/NIT es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El teléfono es obligatorio.";
                return false;
            }

            return cdcliente.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del cliente no es válido.";
                return false;
            }

            // La verificación de dependencia con Venta está en el SP
            return cdcliente.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
