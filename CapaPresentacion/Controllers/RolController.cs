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
            return View();
        }
    }
}