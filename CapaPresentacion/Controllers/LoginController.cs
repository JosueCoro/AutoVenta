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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidarLogin(string correo, string contrasena)
        {
            Usuario_Activo oUsuario = new Usuario_Activo();
            string mensaje = string.Empty;

            // Instancia de la Capa de Negocio
            oUsuario = new CN_Login().ValidarUsuario(correo, contrasena);

            if (oUsuario != null)
            {
                // Login Exitoso
                // Almacenamos el objeto de usuario completo en la sesión.
                Session["Usuario"] = oUsuario;
                Session["NombreUsuario"] = oUsuario.nombre + " " + oUsuario.apellido;
                Session["Email"] = oUsuario.correo;
                Session["RolUsuario"] = oUsuario.nombre_rol;

                FormsAuthentication.SetAuthCookie(oUsuario.correo, false);

                return Json(new
                {
                    resultado = true,
                    mensaje = "Acceso concedido.",
                    redirectUrl = Url.Action("Index", "Home") 
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                mensaje = "Correo o contraseña incorrectos, o el usuario/rol no está activo.";

                return Json(new
                {
                    resultado = false,
                    mensaje = mensaje
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }


        public ActionResult AccesoDenegado()
        {
            return View();
        }
    }
}