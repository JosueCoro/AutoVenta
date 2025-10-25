using CapaDato;
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
        // GET: Pago
        [ValidarPermisos(NombrePermiso = "Gestionar Pagos")]
        public ActionResult Pago()
        {
            return View();
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

            // OBTENER ID DEL USUARIO DE LA SESIÓN 
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
            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoPago().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}