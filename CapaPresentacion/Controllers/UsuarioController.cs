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
    public class UsuarioController : Controller
    {

        // GET: Usuario
        [ValidarPermisos(NombrePermiso = "Gestionar Usuarios")]
        public ActionResult Usuario()
        {
            ViewBag.ListaRoles = new CN_Rol().Listar();

            ViewBag.ListaEstados = new List<SelectListItem>() {
                new SelectListItem() { Value = "1", Text = "Activo" },
                new SelectListItem() { Value = "0", Text = "Inactivo" }
            };

            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> lista = new CN_Usuario().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            int idUsuarioAuditoria = ((Usuario_Activo)Session["Usuario"]).id_usuario;


            if (objeto.id_usuario == 0)
            {
                resultado = new CN_Usuario().Registrar(objeto, idUsuarioAuditoria, out Mensaje);
            }
            else
            {
                resultado = new CN_Usuario().Editar(objeto, idUsuarioAuditoria, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            int idUsuarioAuditoria = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Usuario().Eliminar(id, idUsuarioAuditoria, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }





    }
}