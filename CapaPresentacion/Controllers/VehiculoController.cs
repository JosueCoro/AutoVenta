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
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        [ValidarPermisos(NombrePermiso = "Gestionar Vehiculo")]
        public ActionResult Vehiculo()
        {
            ViewBag.ListaEstados = new List<SelectListItem>() {
                new SelectListItem() { Value = "En Venta", Text = "En Venta" },
                new SelectListItem() { Value = "Vendido", Text = "Vendido" },
                new SelectListItem() { Value = "Reservado", Text = "Reservado" },
                new SelectListItem() { Value = "En Mantenimiento", Text = "En Mantenimiento" } // Mantenemos el estado de mantenimiento
            };
            return View();
        }
        [HttpGet]
        public JsonResult ListarVehiculo()
        {
            List<Vehiculo> lista = new CN_Vehiculo().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarVehiculo(Vehiculo objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_vehiculo == 0)
            {
                resultado = new CN_Vehiculo().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_Vehiculo().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarVehiculo(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Vehiculo().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [ValidarPermisos(NombrePermiso = "Gestionar Tipos de Vehiculo")]
        public ActionResult TipoVehiculo()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarTiposVehiculo()
        {
            List<TipoVehiculo> lista = new CN_TipoVehiculo().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoVehiculo(TipoVehiculo objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_tp_vehiculo == 0)
            {
                resultado = new CN_TipoVehiculo().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_TipoVehiculo().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoVehiculo(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoVehiculo().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}