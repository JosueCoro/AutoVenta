using CapaDato;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        [ValidarPermisos(NombrePermiso = "Gestionar Vehiculo")]
        public ActionResult Vehiculo()
        {
            ViewBag.ListaEstados = new List<SelectListItem>() {
                new SelectListItem() { Value = "En Venta", Text = "En Venta" },
                new SelectListItem() { Value = "Vendido", Text = "Vendido" },
                new SelectListItem() { Value = "Reservado", Text = "Reservado" },
                new SelectListItem() { Value = "En Mantenimiento", Text = "En Mantenimiento" }
            };
            return View();
        }
        [HttpGet]
        public JsonResult ListarVehiculo()
        {
            List<Vehiculo> lista = new CN_Vehiculo().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarVehiculo(string objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string Mensaje = string.Empty;

            Vehiculo objVehiculo = new JavaScriptSerializer().Deserialize<Vehiculo>(objeto);
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;
            bool hayNuevoArchivo = archivoImagen != null && archivoImagen.ContentLength > 0;

            if (objVehiculo.id_vehiculo == 0)
            {
                objVehiculo.fecha_ingreso = DateTime.Now.ToString("yyyy-MM-dd");
                objVehiculo.imagen = "~/imagenes/default.png"; 

                int idGenerado = new CN_Vehiculo().Registrar(objVehiculo, idUsuario, out Mensaje);
                resultado = idGenerado;
                objVehiculo.id_vehiculo = idGenerado;
            }
            else
            {
                resultado = new CN_Vehiculo().Editar(objVehiculo, idUsuario, out Mensaje);
            }

            if (Convert.ToInt32(resultado) > 0 && hayNuevoArchivo)
            {
                string extension = Path.GetExtension(archivoImagen.FileName);
                string nombreArchivo = objVehiculo.id_vehiculo.ToString() + extension;

                string carpetaImagenes = "~/imagenes/"; 
                string rutaRelativa = carpetaImagenes + nombreArchivo;
                string rutaFisica = Server.MapPath(rutaRelativa);

                try
                {
                    string rutaDirectorio = Server.MapPath(carpetaImagenes);
                    if (!Directory.Exists(rutaDirectorio))
                        Directory.CreateDirectory(rutaDirectorio);

                    archivoImagen.SaveAs(rutaFisica);

                    objVehiculo.imagen = rutaRelativa;

                    bool actualizacionExitosa = new CN_Vehiculo().ActualizarRutaImagen(objVehiculo, out string msg);

                    if (!actualizacionExitosa)
                    {
                        Mensaje += ". Advertencia: Falló al actualizar la ruta de la imagen en DB. RUTA NO ALMACENADA.";
                    }
                }
                catch (Exception ex)
                {
                    Mensaje += $". Error al guardar el archivo físico: {ex.Message}";
                }
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarVehiculo(int id)
        {
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_Vehiculo().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [ValidarPermisos(NombrePermiso = "Gestionar Tipos de Vehiculo")]
        public ActionResult TipoVehiculo()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarTiposVehiculo()
        {
            List<TipoVehiculo> lista = new CN_TipoVehiculo().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoVehiculo(TipoVehiculo objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            if (objeto.id_tp_vehiculo == 0)
            {
                resultado = new CN_TipoVehiculo().Registrar(objeto, idUsuario, out Mensaje);
            }
            else
            {
                resultado = new CN_TipoVehiculo().Editar(objeto, idUsuario, out Mensaje);
            }

            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTipoVehiculo(int id)
        {
            // OBTENER ID DEL USUARIO DE LA SESIÓN 
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoVehiculo().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}