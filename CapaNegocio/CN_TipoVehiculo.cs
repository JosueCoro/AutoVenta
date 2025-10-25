using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_TipoVehiculo
    {
        private CD_TipoVehiculo cdtipovehiculo = new CD_TipoVehiculo();

        public List<TipoVehiculo> Listar()
        {
            return cdtipovehiculo.Listar();
        }

        public int Registrar(TipoVehiculo obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.descripcion))
            {
                Mensaje = "La descripción no puede ser vacía.";
                return 0;
            }

            return cdtipovehiculo.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(TipoVehiculo obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_tp_vehiculo == 0)
            {
                Mensaje = "El ID del tipo de vehículo no es válido.";
                return false;
            }

            if (string.IsNullOrEmpty(obj.descripcion))
            {
                Mensaje = "La descripción no puede ser vacía.";
                return false;
            }

            return cdtipovehiculo.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del tipo de vehículo no es válido.";
                return false;
            }

            return cdtipovehiculo.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
