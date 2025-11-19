using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        private CN_Venta cnVenta = new CN_Venta();
        [ValidarPermisos(NombrePermiso = "Visualizar Reportes")]
        public ActionResult ReporteVentas()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ReporteVenta(string fechaInicio, string fechaFin)
        {

            try
            {
                List<VentaSimple> lista = cnVenta.ReporteVenta(fechaInicio, fechaFin);

                return Json(new { data = lista, resultado = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), resultado = false, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidarPermisos(NombrePermiso = "Visualizar Ganancias por Ventas")]
        public ActionResult RepoeGananciaVenta()
        {
            return View();
        }

        [ValidarPermisos(NombrePermiso = "Visualizar Ganancias por Vehiculo")]
        public ActionResult RepoeGananciaVehiculo()
        {
            return View();
        }
    }
}