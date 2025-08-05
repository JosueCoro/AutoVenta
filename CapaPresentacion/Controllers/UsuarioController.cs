using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class UsuarioController : Controller
    {

        // GET: Usuario
        public ActionResult Usuario()
        {
            return View();
        }

        // Acción para listar los roles, que se usa en la vista de creación de usuarios
        [HttpGet]
        public JsonResult ListarRoles()
        {
            List<Rol> lista = new CN_Rol().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Acción para listar todos los usuarios
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> lista = new CN_Usuario().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        // Acción para registrar o editar un usuario
        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (objeto.id_usuario == 0)
            {
                resultado = new CN_Usuario().Registrar(objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Usuario().Editar(objeto, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        // Acción para eliminar lógicamente un usuario
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool resultado = new CN_Usuario().Eliminar(id, out string mensaje);
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}