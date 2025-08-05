using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Marca()
        {
            return View();
        }

        // Acción para listar todas las marcas
        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> lista = new CN_Marca().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Acción para registrar o editar una marca
        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (objeto.id_marca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Acción para eliminar una marca
        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool resultado = new CN_Marca().Eliminar(id, out string mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}