using CapaDato;
using CapaNegocio;
using CapaEntidad;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{

    [Authorize]
    public class PagoController : Controller
    {
        private CN_Pago cNPago = new CN_Pago();
        private CN_Venta cnVenta = new CN_Venta();
        // GET: Pago
        [ValidarPermisos(NombrePermiso = "Gestionar Pagos")]
        public ActionResult Pago()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarPagos()
        {
            List<Pago> lista = cNPago.Listar();

            var listaMapeada = lista.Select(p => new
            {
                p.id_pago,
                p.nombre,
                p.fecha,
                p.hora,
                p.monto,
                p.id_venta,
                p.nombreTP,
            }).ToList();

            return Json(new { data = listaMapeada }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerVentaSimple(int idVenta)
        {
            try
            {
                List<VentaSimple> listaVentas = cnVenta.Listar(); // Asumo que el CN_Venta.Listar() que existe se llama ListarSimples()

                VentaSimple venta = listaVentas
                    .Where(v => v.id_venta == idVenta)
                    .FirstOrDefault();

                if (venta == null)
                {
                    return Json(new { resultado = false, mensaje = $"Venta ID {idVenta} no encontrada." }, JsonRequestBehavior.AllowGet);
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

        [ValidarPermisos(NombrePermiso = "Gestionar Tipos de Pago")]
        public ActionResult TipoPago()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTiposPago()
        {
            List<TipoPago> lista = new CN_TipoPago().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoPago(TipoPago objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_tipo_pago == 0)
            {
                resultado = new CN_TipoPago().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_TipoPago().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoPago(int id)
        {
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoPago().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}