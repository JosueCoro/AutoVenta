using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDato;
using MySql.Data.MySqlClient;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Cliente()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            lista = new CN_Cliente().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCliente(Cliente objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (objeto.id_cliente == 0)
            {
                resultado = new CN_Cliente().Registrar(objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Cliente().Editar(objeto, out Mensaje);
            }
            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }


    }
}