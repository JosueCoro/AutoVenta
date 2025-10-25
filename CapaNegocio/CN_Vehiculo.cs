using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Vehiculo
    {
        private CD_Vehiculo cdvehiculo = new CD_Vehiculo();

        public List<Vehiculo> Listar()
        {
            return cdvehiculo.Listar();
        }

        public int Registrar(Vehiculo obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.placa)) Mensaje = "La placa es obligatoria.";
            else if (string.IsNullOrEmpty(obj.modelo)) Mensaje = "El modelo es obligatorio.";
            else if (string.IsNullOrEmpty(obj.año)) Mensaje = "El año es obligatorio.";
            else if (obj.precio_compra <= 0) Mensaje = "El precio de compra debe ser mayor a cero.";
            else if (obj.precio_venta <= 0) Mensaje = "El precio de venta debe ser mayor a cero.";

            if (!string.IsNullOrEmpty(Mensaje)) return 0;

            return cdvehiculo.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(Vehiculo obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.id_vehiculo == 0) Mensaje = "El ID del vehículo no es válido.";
            else if (string.IsNullOrEmpty(obj.placa)) Mensaje = "La placa es obligatoria.";
            else if (string.IsNullOrEmpty(obj.modelo)) Mensaje = "El modelo es obligatorio.";
            else if (string.IsNullOrEmpty(obj.año)) Mensaje = "El año es obligatorio.";
            else if (obj.precio_compra <= 0) Mensaje = "El precio de compra debe ser mayor a cero.";
            else if (obj.precio_venta <= 0) Mensaje = "El precio de venta debe ser mayor a cero.";

            if (!string.IsNullOrEmpty(Mensaje)) return false;

            return cdvehiculo.Editar(obj, idUsuario, out Mensaje);
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (id == 0)
            {
                Mensaje = "El ID del vehículo no es válido.";
                return false;
            }

            return cdvehiculo.Eliminar(id, idUsuario, out Mensaje);
        }
    }
}
