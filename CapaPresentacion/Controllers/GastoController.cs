using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class GastoController : Controller
    {
        // GET: Gasto
        public ActionResult Gasto()
        {
            return View();
        }

        // Acción para listar los tipos de gasto
        [HttpGet]
        public JsonResult Listar()
        {
            List<TipoGasto> lista = new CN_TipoGasto().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Acción para registrar o editar un tipo de gasto
        [HttpPost]
        public JsonResult GuardarGasto(TipoGasto objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (objeto.id_tipo_gasto == 0)
            {
                // Es un nuevo registro
                resultado = new CN_TipoGasto().Registrar(objeto, out Mensaje);
            }
            else
            {
                // Es una edición
                resultado = new CN_TipoGasto().Editar(objeto, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Acción para eliminar un tipo de gasto
        [HttpPost]
        public JsonResult EliminarGasto(int id)
        {
            bool resultado = new CN_TipoGasto().Eliminar(id, out string mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}