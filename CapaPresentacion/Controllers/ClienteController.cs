using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDato;
using MySql.Data.MySqlClient;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        [ValidarPermisos(NombrePermiso = "Gestionar Cliente")]
        public ActionResult Cliente()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Cliente> lista = new CN_Cliente().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCliente(Cliente objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            // OBTENER ID DEL USUARIO DE LA SESIÓN (AUDITORÍA)
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_cliente == 0)
            {
                resultado = new CN_Cliente().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_Cliente().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCliente(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN (AUDITORÍA)
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Cliente().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


    }
}