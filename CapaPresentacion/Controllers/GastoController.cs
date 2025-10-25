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
    public class GastoController : Controller
    {
        // GET: Gasto
        [ValidarPermisos(NombrePermiso = "Gestionar Gasto")]
        public ActionResult Gasto()
        {
            return View();
        }
        [ValidarPermisos(NombrePermiso = "Gestionar Tipos de Gasto")]
        public ActionResult TipoGasto()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTiposGasto()
        {
            List<TipoGasto> lista = new CN_TipoGasto().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoGasto(TipoGasto objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_tipo_gasto == 0)
            {
                resultado = new CN_TipoGasto().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_TipoGasto().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoGasto(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoGasto().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}