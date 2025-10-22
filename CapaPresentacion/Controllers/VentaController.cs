using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        // GET: Venta
        public ActionResult Venta()
        {
            return View();
        }
    }
}