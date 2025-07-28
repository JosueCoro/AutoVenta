using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Vehiculo()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult TipoGasto()
        {
            return View();
        }
        public ActionResult Gasto()
        {
            return View();
        }
    }
}