using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class ComisionController : Controller
    {
        // GET: Comision
        [ValidarPermisos(NombrePermiso = "Generar Comisiones")]
        public ActionResult Comision()
        {
            return View();
        }
    }
}