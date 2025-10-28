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
    public class PermisoController : Controller
    {
        private CN_Rol cnRol = new CN_Rol();
        private CN_Permiso cnPermiso = new CN_Permiso();
        // GET: Permiso
        [ValidarPermisos(NombrePermiso = "Gestionar Roles y Permisos")]
        public ActionResult Permiso()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerPermisos(int idRol)
        {
            if (idRol <= 0)
            {
                return Json(new { lista = new List<Permiso>(), error = "ID de rol inválido" }, JsonRequestBehavior.AllowGet);
            }

            // Llama a la CN, que a su vez llama al SP 'SELECT_ASIGNADOS'
            List<Permiso> listaPermisos = cnPermiso.ListarPermisosPorRol(idRol);

            return Json(new { lista = listaPermisos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarPermisos(int idRol, List<int> permisos)
        {
            string Mensaje = string.Empty;
            bool Resultado = false;

            int idUsuarioAuditoria = 0;
            if (Session["Usuario"] is Usuario_Activo usuarioActivo)
            {
                idUsuarioAuditoria = usuarioActivo.id_usuario;
            }

            if (idUsuarioAuditoria == 0)
            {
                Mensaje = "No se pudo identificar al usuario de auditoría.";
                return Json(new { resultado = false, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }

            List<Permiso> permisosAsignados = permisos
                .Where(id => id > 0)
                .Select(id => new Permiso { id_permiso = id })
                .ToList();

            Resultado = cnPermiso.GuardarPermisos(idRol, permisosAsignados, idUsuarioAuditoria, out Mensaje);

            return Json(new { resultado = Resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}