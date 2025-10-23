using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{

    [Authorize]
    public class PagoController : Controller
    {
        // GET: Pago
        [ValidarPermisos(NombrePermiso = "Gestionar Pagos")]
        public ActionResult Pago()
        {
            return View();
        }
        [ValidarPermisos(NombrePermiso = "Gestionar Tipos de Pago")]
        public ActionResult TipoPago()
        {
            return View();
        }
    }
}