using System;
using CapaDato;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte cdreporte = new CD_Reporte();

        public ReporteVentaGanancia ObtenerReporteVenta(int idVenta, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idVenta == 0)
            {
                Mensaje = "El ID de la venta es inválido.";
                return null;
            }

            return cdreporte.ObtenerReporteVenta(idVenta, out Mensaje);
        }

        public ReporteVehiculoRentabilidad ObtenerReporteVehiculo(int idVehiculo, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idVehiculo == 0)
            {
                Mensaje = "El ID del vehículo es inválido.";
                return null;
            }

            return cdreporte.ObtenerReporteVehiculo(idVehiculo, out Mensaje);
        }
    }
}