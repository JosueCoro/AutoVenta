using CapaDato;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class GastoController : Controller
    {
        private CN_Gasto cnGasto = new CN_Gasto();
        private CN_Vehiculo cnVehiculo = new CN_Vehiculo();

        private CN_TipoGasto cnTipoGasto = new CN_TipoGasto();
        private CN_Venta cnVenta = new CN_Venta();

        // GET: Gasto
        [ValidarPermisos(NombrePermiso = "Gestionar Gasto")]
        public ActionResult Gasto()
        {
            return View();
        }
        // GET: Gasto
        [ValidarPermisos(NombrePermiso = "Gestionar Gasto")]
        public ActionResult GastoVenta()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarGastos()
        {
            try
            {
                List<Gasto> lista = cnGasto.Listar();

                if (lista == null) lista = new List<Gasto>();

                var datosParaVista = lista.Select(g => new
                {
                    g.id_gasto,
                    g.fecha,
                    g.descripcion,
                    g.monto,
                    oTipoGasto = new { nombre = g.oTipoGasto != null ? g.oTipoGasto.nombre : "N/A" },
                    g.id_vehiculo,
                    g.id_venta,
                    
                    oVehiculo = g.oVehiculo 

                }).ToList();

                return Json(new { data = datosParaVista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ListarGastosVentas()
        {
            try
            {
                List<Gasto> lista = cnGasto.ListarVentas();

                if (lista == null) lista = new List<Gasto>();

                var datosParaVista = lista.Select(g => new
                {
                    g.id_gasto,
                    g.fecha,
                    g.descripcion,
                    g.monto,
                    oTipoGasto = new { nombre = g.oTipoGasto != null ? g.oTipoGasto.nombre : "N/A" },
                    g.id_vehiculo,
                    g.id_venta,

                    oVehiculo = g.oVehiculo

                }).ToList();

                return Json(new { data = datosParaVista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public JsonResult RegistrarGastos(int idVehiculo, int idVenta, string detalleGastosJson)
        {
            string Mensaje = string.Empty;
            int resultado = 0;

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            List<DetalleGasto> listaDetalle;
            try
            {
                listaDetalle = new JavaScriptSerializer().Deserialize<List<DetalleGasto>>(detalleGastosJson);
            }
            catch (Exception)
            {
                Mensaje = "Error en el formato del detalle de gastos.";
                return Json(new { resultado = 0, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
            }
           

            resultado = cnGasto.RegistrarMultiples(idVehiculo, idVenta, listaDetalle, idUsuario, out Mensaje);

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarGasto(int id)
        {
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;
            bool resultado = cnGasto.Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVehiculosSimples()
        {
            var lista = cnVehiculo.Listar().Select(v => new {
                id_vehiculo = v.id_vehiculo,
                info = $"{v.placa} - {v.modelo} ({v.estado})"
            }).ToList();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult ListarVentasSimples()
        {
            try
            {
                List<VentaSimple> lista = cnVenta.Listar();

                var listaMapeada = lista.Select(v => new
                {
                    v.id_venta,
                    v.fecha,
                    v.total,

                    v.NombreCliente,
                    v.CiNitCliente,
                    v.VehiculosVendidos 

                }).ToList();

                return Json(new { data = listaMapeada }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ListarTiposGasto()
        {
            List<TipoGasto> lista = cnTipoGasto.Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }



        [ValidarPermisos(NombrePermiso = "Gestionar Tipos de Gasto")]
        public ActionResult TipoGasto()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarTipoGasto(TipoGasto objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_tipo_gasto == 0)
            {
                resultado = new CN_TipoGasto().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_TipoGasto().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoGasto(int id)
        {
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoGasto().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}