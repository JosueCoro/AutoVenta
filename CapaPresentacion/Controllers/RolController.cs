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
    public class RolController : Controller
    {
        // GET: Rol
        [ValidarPermisos(NombrePermiso = "Gestionar Roles y Permisos")]
        public ActionResult Rol()
        {
            ViewBag.ListaEstados = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "Activo" },
                new SelectListItem() { Value = "0", Text = "Inactivo" }
            };
            return View();
        }


        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<Rol> lista = new CN_Rol().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarRol(Rol objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_rol == 0)
            {
                resultado = new CN_Rol().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_Rol().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarRol(int id)
        {
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Rol().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}