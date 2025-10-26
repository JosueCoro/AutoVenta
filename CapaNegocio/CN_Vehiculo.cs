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


            if (string.IsNullOrEmpty(obj.modelo))
            {
                Mensaje = "El nombre del modelo es obligatorio.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.año))
            {
                Mensaje = "El año de fabricacion del vehiculo es obligatorio.";
                return 0;
            }
            if (!obj.año.All(char.IsDigit))
            {
                Mensaje = "El año de fabricacion del vehiculo debe contener solo números.";
                return 0;
            }
            if (string.IsNullOrEmpty(obj.placa))
            {
                Mensaje = "El numero de placa es obligatorio.";
                return 0;
            }
            if (obj.precio_compra <= 0)
            {
                Mensaje = "El precio de compra debe ser mayor a cero.";
                return 0;
            }
            if (obj.precio_venta <= 0)
            {
                Mensaje = "El precio de venta debe ser mayor a cero.";
                return 0;
            }
            if (obj.oMarca == null || obj.oMarca.id_marca == 0)
            {
                Mensaje = "Debe seleccionar una marca.";
                return 0;
            }

            // Su validación final de TipoVehiculo es la correcta.
            if (obj.oTipoVehiculo == null || obj.oTipoVehiculo.id_tp_vehiculo == 0)
            {
                Mensaje = "Debe seleccionar un tipo de vehículo.";
                return 0;
            }

            return cdvehiculo.Registrar(obj, idUsuario, out Mensaje);
        }

        public bool Editar(Vehiculo obj, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.modelo))
            {
                Mensaje = "El nombre del modelo es obligatorio.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.año))
            {
                Mensaje = "El año de fabricacion del vehiculo es obligatorio.";
                return false;
            }
            if (!obj.año.All(char.IsDigit))
            {
                Mensaje = "El año de fabricacion del vehiculo debe contener solo números.";
                return false;
            }
            if (string.IsNullOrEmpty(obj.placa))
            {
                Mensaje = "El numero de placa es obligatorio.";
                return false;
            }
            if (obj.precio_compra <= 0)
            {
                Mensaje = "El precio de compra debe ser mayor a cero.";
                return false;
            }
            if (obj.precio_venta <= 0)
            {
                Mensaje = "El precio de venta debe ser mayor a cero.";
                return false;
            }
            if (obj.oMarca == null || obj.oMarca.id_marca == 0)
            {
                Mensaje = "Debe seleccionar una marca.";
                return false;
            }

            if (obj.oTipoVehiculo == null || obj.oTipoVehiculo.id_tp_vehiculo == 0)
            {
                Mensaje = "Debe seleccionar un tipo de vehículo.";
                return false;
            }

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
        public bool ActualizarRutaImagen(Vehiculo obj, out string Mensaje)
        {
            return cdvehiculo.ActualizarRutaImagen(obj, out Mensaje);
        }
    }
}
