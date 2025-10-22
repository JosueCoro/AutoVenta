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
        public ActionResult Pago()
        {
            return View();
        }
    }
}