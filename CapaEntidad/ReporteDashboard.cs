using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteDashboard
    {
        //principal
        public int TotalStockDisponible { get; set; }
        public decimal TotalVentasMes { get; set; }
        public int TotalClientes { get; set; }
        public decimal TotalComisionesPendientes { get; set; }

        //Reporte mensual
        public string Periodo { get; set; } 
        public int Año { get; set; }
        public int MesNumero { get; set; }
        public decimal IngresoTotal { get; set; }

        //estado inventario
        public string EstadoInventario { get; set; }
        public int Conteo { get; set; }
    }
}
