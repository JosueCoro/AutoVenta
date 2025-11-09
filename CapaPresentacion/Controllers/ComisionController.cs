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
    public class ComisionController : Controller
    {
        private CN_Comision cnComision = new CN_Comision();
        private CN_Venta cnVenta = new CN_Venta();
        // GET: Comision
        [ValidarPermisos(NombrePermiso = "Generar Comisiones")]
        public ActionResult Comision()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarComisiones()
        {
            List<Comision> lista = cnComision.Listar();

            var listaMapeada = lista.Select(c => new
            {
                c.id_comision,
                c.monto,
                c.fecha_pago,
                c.fecha_generacion,
                c.estado,
                c.observaciones,
                c.id_venta,
                c.NombreAsesor,
                c.CiAsesor
            }).ToList();

            return Json(new { data = listaMapeada }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerVentaSimple(int idVenta)
        {
            try
            {
                List<VentaSimple> listaVentas = cnVenta.Listar();

                VentaSimple venta = listaVentas
                    .Where(v => v.id_venta == idVenta)
                    .FirstOrDefault(); 

                if (venta == null)
                {
                    return Json(new { resultado = false, mensaje = $"Venta ID {idVenta} no encontrada en el sistema." }, JsonRequestBehavior.AllowGet);
                }

                var data = new
                {
                    venta.id_venta,
                    venta.fecha,
                    venta.total,
                    venta.NombreCliente,
                    venta.CiNitCliente,
                    venta.VehiculosVendidos
                };

                return Json(new { resultado = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { resultado = false, mensaje = "Error al obtener el detalle de la venta: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult PagarComision(int idComision)
        {

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = cnComision.Pagar(idComision, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}