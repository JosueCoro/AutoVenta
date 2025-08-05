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
        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre_completo))
            {
                Mensaje = "El nombre del cliente no puede ser vacío.";
                return 0;
            }

            if (string.IsNullOrEmpty(obj.ci_nit))
            {
                Mensaje = "El CI/NIT del cliente no puede ser vacío.";
                return 0;
            }

            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El teléfono del cliente no puede ser vacío.";
                return 0;
            }

            if (string.IsNullOrEmpty(obj.direccion))
            {
                Mensaje = "La dirección del cliente no puede ser vacío.";
                return 0;
            }
            return cdcliente.Registrar(obj, out Mensaje);
        }

        
        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_cliente == 0)
            {
                Mensaje = "El ID del cliente no es válido.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.nombre_completo))
            {
                Mensaje = "El nombre del cliente no puede ser vacío.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.ci_nit))
            {
                Mensaje = "El CI/NIT del cliente no puede ser vacío.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El teléfono del cliente no puede ser vacío.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.direccion))
            {
                Mensaje = "La dirección del cliente no puede ser vacío.";
                return false;
            }

            return cdcliente.Editar(obj, out Mensaje);
        }
    }
}
