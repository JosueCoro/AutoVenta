using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacion.Controllers
{
   [Authorize]
    public class MarcaController : Controller
    {

        // GET: Marca
        [ValidarPermisos(NombrePermiso = "Gestionar Marcas")]
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

            // OBTENER ID DEL USUARIO DE LA SESIÓN (REQUISITO DE AUDITORÍA)
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_marca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Acción para eliminar una marca
        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN (REQUISITO DE AUDITORÍA)
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Marca().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}