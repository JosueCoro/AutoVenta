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
    }
}