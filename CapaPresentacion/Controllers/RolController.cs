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
        public ActionResult Rol()
        {
            return View();
        }
    }
}