using System;
using System.Collections.Generic;

namespace CapaEntidad
{
    public class ReporteVentaGanancia
    {
        // Datos de la Venta
        public int id_venta { get; set; }
        public string fecha { get; set; }
        public decimal IngresoTotalVenta { get; set; }
        public string NombreCliente { get; set; }
        public string NombreVendedor { get; set; }
        public string VehiculosVendidos { get; set; } // El string concatenado

        // Cálculos de Rentabilidad
        public decimal ComisionAsesor { get; set; }
        public decimal GastosTotalesVehiculos { get; set; }
        public decimal GananciaNeta { get; set; }

        // Para el desglose de gastos (Se usarán en la capa de datos)
        public List<ReporteGastoDetalle> DetalleGastos { get; set; }
    }

    public class ReporteGastoDetalle
    {
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public string TipoGasto { get; set; }
        public string PlacaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public DateTime? fecha { get; set; } // Se usa en el reporte de vehículo
    }
}