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


        // ----------------------------------------------------------------------
        // MÉTODO AJAX DE REGISTRO TRANSACCIONAL
        // ----------------------------------------------------------------------

        [HttpPost]
        public JsonResult RegistrarVenta(string VentaJson)
        {
            string Mensaje = string.Empty;
            int idGenerado = 0;

            try
            {
                // Deserializa el objeto Venta que llega como una cadena JSON
                Venta objVenta = new JavaScriptSerializer().Deserialize<Venta>(VentaJson);

                // Obtener el ID del usuario que realiza la operación (Auditoría y FK en Venta)
                // Asumo que la estructura Usuario_Activo existe en la sesión
                int idUsuarioSesion = ((Usuario_Activo)Session["Usuario"]).id_usuario;
                objVenta.id_usuario = idUsuarioSesion; // Establece el usuario que registra la venta

                // Llamar a la Capa de Negocio para realizar la transacción completa
                idGenerado = cnVenta.Registrar(objVenta, out Mensaje);
            }
            catch (Exception ex)
            {
                // Capturar cualquier error inesperado (serialización, null reference, etc.)
                Mensaje = "Error interno del servidor al procesar la solicitud: " + ex.Message;
                idGenerado = 0;
            }

            // Devolver el resultado de la operación
            return Json(new { resultado = idGenerado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}