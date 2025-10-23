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
        // GET: Permiso
        [ValidarPermisos(NombrePermiso = "Gestionar Roles y Permisos")]
        public ActionResult Permiso()
        {
            return View();
        }
    }
}