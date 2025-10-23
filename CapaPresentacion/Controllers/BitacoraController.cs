using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class BitacoraController : Controller
    {
        // GET: Bitacora
        [ValidarPermisos(NombrePermiso = "Visualizar Bitacora")]
        public ActionResult Bitacora()
        {
            return View();
        }
    }
}