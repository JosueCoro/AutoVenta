using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteCostoVehiculoDTO
    {
        public List<GastoVehiculoReporte> ListaGastos { get; set; }
        public ResumenCostoVehiculo Resumen { get; set; }

        // Datos adicionales del vehículo para el encabezado
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string ImagenRuta { get; set; }
    }
}
