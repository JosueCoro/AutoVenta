using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        // GET: Login (para mostrar la vista de login)
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidarLogin(string correo, string contrasena)
        {
            string mensaje = string.Empty;
            Usuario_Activo oUsuario = new CN_Usuario().ValidarLogin(correo, contrasena, out mensaje);

            if (oUsuario != null)
            {
                // Si el login es exitoso, puedes establecer la autenticación de formularios

                // Almacenar datos del usuario en la sesión
                Session["Usuario"] = oUsuario;
                Session["NombreUsuario"] = oUsuario.nombre + " " + oUsuario.apellido;
                Session["Email"] = oUsuario.correo;
                Session["RolUsuario"] = oUsuario.nombre_rol;

                FormsAuthentication.SetAuthCookie(oUsuario.correo, false); 

                return Json(new { resultado = true, mensaje = mensaje, redirectUrl = Url.Action("Index", "Home") });
            }
            else
            {
                return Json(new { resultado = false, mensaje = mensaje });
            }
        }

        // NUEVA ACCIÓN: Para cerrar la sesión
        [HttpPost]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Usuario"); // Redirige a la página de login
        }

    }
}