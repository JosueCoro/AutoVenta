using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Gasto
    {
        private CD_Gasto cD_Gasto = new CD_Gasto();

        public List<Gasto> Listar()
        {
            return cD_Gasto.ListarGastosVehiculos();
        }
        public List<Gasto> ListarVentas()
        {
            return cD_Gasto.ListarGastosVentas();
        }


        public int RegistrarMultiples(int idVehiculo, int idVenta, List<DetalleGasto> listaDetalle, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;
            int idAsociacion = 0;
            string tipoAsociacion = string.Empty;

            

            if (idVehiculo > 0 && idVenta > 0)
            {
                Mensaje = "Un registro de gastos debe estar asociado a un Vehículo O a una Venta, no a ambos.";
                return 0;
            }

            if (idVehiculo == 0 && idVenta == 0)
            {
                Mensaje = "Debe seleccionar un Vehículo o una Venta para asociar los gastos.";
                return 0;
            }

            if (idVehiculo > 0)
            {
                idAsociacion = idVehiculo;
                tipoAsociacion = "VEHICULO";
            }
            else 
            {
                idAsociacion = idVenta;
                tipoAsociacion = "VENTA";
            }

            if (listaDetalle == null || listaDetalle.Count == 0)
            {
                Mensaje = "Debe añadir al menos un ítem al detalle de gastos.";
                return 0;
            }

            for (int i = 0; i < listaDetalle.Count; i++)
            {
                var detalle = listaDetalle[i];
                string itemNum = $"Item #{i + 1}: ";

                if (string.IsNullOrWhiteSpace(detalle.descripcion))
                {
                    Mensaje = itemNum + "La descripción del gasto es obligatoria.";
                    return 0;
                }
                if (detalle.monto <= 0)
                {
                    Mensaje = itemNum + "El monto del gasto debe ser mayor a cero.";
                    return 0;
                }
                if (detalle.id_tipo_gasto <= 0)
                {
                    Mensaje = itemNum + "Debe seleccionar el Tipo de Gasto para este ítem.";
                    return 0;
                }
            }

            return cD_Gasto.RegistrarMultiples(idAsociacion, tipoAsociacion, listaDetalle, idUsuario, out Mensaje);
        }
        public bool Eliminar(int idGasto, int idUsuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idGasto <= 0)
            {
                Mensaje = "El ID del gasto a eliminar no es válido.";
                return false;
            }

            // En este punto podemos agregar lógica para evitar la eliminación,
            // por ejemplo: "No se pueden eliminar gastos con más de 90 días de antigüedad".

            return cD_Gasto.Eliminar(idGasto, idUsuario, out Mensaje);
        }
    }
}
