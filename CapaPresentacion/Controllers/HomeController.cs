using CapaDato;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class HomeController : Controller
        
    {
        private CD_Dashboard objCdDashboard = new CD_Dashboard();
        public ActionResult Index()
        {
            Dictionary<string, object> resumenData = objCdDashboard.ObtenerResumenDashboard();

            ViewBag.KPIs = resumenData.ContainsKey("KPIs") ? resumenData["KPIs"] : null;
            ViewBag.ResumenMensual = resumenData.ContainsKey("ResumenMensual") ? resumenData["ResumenMensual"] : null;
            ViewBag.EstadoInventario = resumenData.ContainsKey("EstadoInventario") ? resumenData["EstadoInventario"] : null;


            return View();
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}