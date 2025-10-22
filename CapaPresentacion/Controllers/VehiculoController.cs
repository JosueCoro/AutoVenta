using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        public ActionResult Vehiculo()
        {
            return View();
        }
    }
}