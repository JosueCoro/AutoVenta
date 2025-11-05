using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization; 

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private CN_Venta cnVenta = new CN_Venta();
        private CN_Vehiculo cnVehiculo = new CN_Vehiculo();
        private CN_Asesor cnAsesor = new CN_Asesor();
        private CN_Pago cnPago = new CN_Pago();

        // GET: Venta
        [ValidarPermisos(NombrePermiso = "Realizar Venta")]
        public ActionResult Venta()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarVehiculosDisponibles()
        {
            List<Vehiculo> lista = cnVehiculo.Listar().Where(v => v.estado == "En Venta").ToList();

            
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarAsesores()
        {
            List<Asesor> lista = cnAsesor.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult FinalizarVentaCompleta(string TransaccionJson) 
        {
            string Mensaje = string.Empty;
            int idGenerado = 0; 

            try
            {
                VentaTransaccion objTransaccion = new JavaScriptSerializer().Deserialize<VentaTransaccion>(TransaccionJson);

                int idUsuarioSesion = ((Usuario_Activo)Session["Usuario"]).id_usuario;
                objTransaccion.oVenta.id_usuario = idUsuarioSesion;

                idGenerado = cnVenta.RegistrarVentaYPago(objTransaccion.oVenta,
                                                        objTransaccion.MontoPago,
                                                        objTransaccion.IdTipoPago,
                                                        idUsuarioSesion,
                                                        out Mensaje);
            }
            catch (Exception ex)
            {
                Mensaje = "Error en el servidor al procesar la transacción: " + ex.Message;
                idGenerado = 0;
            }

            return Json(new { resultado = idGenerado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}