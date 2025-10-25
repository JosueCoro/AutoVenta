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
    public class AsesorController : Controller
    {
        // GET: Asesor
        [ValidarPermisos(NombrePermiso = "Gestionar Asesores")]
        public ActionResult Asesor()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarAsesores()
        {
            List<Asesor> lista = new CN_Asesor().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAsesor(Asesor objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            // OBTENER ID DEL USUARIO DE LA SESIÓN (AUDITORÍA)
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_asesor == 0)
            {
                resultado = new CN_Asesor().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_Asesor().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarAsesor(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN (AUDITORÍA)
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Asesor().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}