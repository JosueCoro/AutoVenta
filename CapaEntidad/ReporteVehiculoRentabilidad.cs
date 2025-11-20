using System;
using System.Collections.Generic;

namespace CapaEntidad
{
    public class ReporteVehiculoRentabilidad
    {
        // Datos del Vehículo
        public int id_vehiculo { get; set; }
        public string placa { get; set; }
        public string modelo { get; set; }
        public string estado { get; set; }

        // Datos Financieros
        public decimal precio_compra { get; set; }
        public decimal? precio_venta { get; set; } // Puede ser NULL si no se vendió
        public decimal TotalGastosVehiculo { get; set; }

        // Cálculo de Rentabilidad
        public decimal GananciaNetaExacta { get; set; }

        // Datos de la Venta Asociada (si aplica)
        public int? IdVentaAsociada { get; set; }
        public DateTime? FechaVenta { get; set; }
        public string ClienteAsociado { get; set; }

        // Para el desglose de gastos (Se usarán en la capa de datos)
        public List<ReporteGastoDetalle> DetalleGastos { get; set; }
    }
}