using CapaDato;
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
        private CN_Reporte cnreporte = new CN_Reporte();
        private CN_Venta cnventa = new CN_Venta(); 
        private CN_Vehiculo cnvehiculo = new CN_Vehiculo();

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
        [HttpGet]
        public JsonResult ListarVentasSimples()
        {
            try
            {
                List<VentaSimple> lista = cnVenta.Listar();

                var listaMapeada = lista.Select(v => new
                {
                    v.id_venta,
                    v.fecha,
                    v.total,

                    v.NombreCliente,
                    v.CiNitCliente,
                    v.VehiculosVendidos

                }).ToList();

                return Json(new { data = listaMapeada }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ObtenerReporteVentaGanancia(int idVenta)
        {
            string mensaje;
            ReporteVentaGanancia reporte = cnreporte.ObtenerReporteVenta(idVenta, out mensaje);

            bool resultado = reporte != null;

            return Json(new
            {
                resultado = resultado,
                reporte = reporte,
                mensaje = mensaje
            }, JsonRequestBehavior.AllowGet);
        }

        [ValidarPermisos(NombrePermiso = "Visualizar Ganancias por Vehiculo")]
        public ActionResult RepoeGananciaVehiculo()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarVehiculosSimples()
        {
            try
            {
                List<Vehiculo> lista = cnvehiculo.Listar();

                var listaMapeada = lista.Select(v => new
                {
                    v.id_vehiculo,
                    v.placa,
                    v.modelo,
                    v.color,
                    v.año,

                    v.precio_venta


                }).ToList();
                return Json(new { data = listaMapeada }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), error = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost]
        public JsonResult ObtenerReporteVehiculoRentabilidad(int idVehiculo)
        {
            string mensaje;
            ReporteVehiculoRentabilidad reporte = cnreporte.ObtenerReporteVehiculo(idVehiculo, out mensaje);

            bool resultado = reporte != null;

            return Json(new
            {
                resultado = resultado,
                reporte = reporte,
                mensaje = mensaje
            }, JsonRequestBehavior.AllowGet);
        }
    }
}