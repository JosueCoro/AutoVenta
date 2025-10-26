using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Asesor
    {
        private CD_Asesor cdasesor = new CD_Asesor();

        public List<Asesor> Listar()
        {
            return cdasesor.Listar();
        }

        public int Registrar(Asesor obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.apellidos))
            {
                Mensaje = "Los apellidos son obligatorios.";
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
            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El número de telefono es obligatoria.";
                return 0;
            }
            if (!obj.telefono.All(char.IsDigit))
            {
                Mensaje = "El número de teléfono debe contener solo números.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.direccion))
            {
                Mensaje = "La Direccion es obligatoria.";
                return 0;
            }


            return cdasesor.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(Asesor obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_asesor == 0)
            {
                Mensaje = "El ID del asesor no es válido.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.nombre))
            {
                Mensaje = "El nombre es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.apellidos))
            {
                Mensaje = "Los apellidos son obligatorios.";
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
            if (string.IsNullOrEmpty(obj.telefono))
            {
                Mensaje = "El numero de telefono es obligatoria.";
                return false;
            }
            if (!obj.telefono.All(char.IsDigit))
            {
                Mensaje = "El número de teléfono debe contener solo números.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.direccion))
            {
                Mensaje = "La Direccion es obligatoria.";
                return false;
            }

            return cdasesor.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del asesor no es válido.";
                return false;
            }

            return cdasesor.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
