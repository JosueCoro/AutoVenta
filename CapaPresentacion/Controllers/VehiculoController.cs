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

        public JsonResult ListarVehiculos()
        {
            List<Vehiculo> lista = new CN_Vehiculo().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        private const string RUTA_BASE_IMAGENES = "C:\\imagenes\\"; 
        private const string NOMBRE_IMAGEN_DEFAULT = "default.png";


        [HttpPost]
        public JsonResult GuardarVehiculo(string objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string Mensaje = string.Empty;

            Vehiculo objVehiculo = new JavaScriptSerializer().Deserialize<Vehiculo>(objeto);

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;
            bool hayNuevoArchivo = archivoImagen != null && archivoImagen.ContentLength > 0;

            string nombreImagenExistente = objVehiculo.imagen;

            if (objVehiculo.id_vehiculo == 0)
            {
                objVehiculo.fecha_ingreso = DateTime.Now.ToString("yyyy-MM-dd");
                objVehiculo.imagen = NOMBRE_IMAGEN_DEFAULT;

                int idGenerado = new CN_Vehiculo().Registrar(objVehiculo, idUsuario, out Mensaje);
                resultado = idGenerado;
                objVehiculo.id_vehiculo = idGenerado;
            }
            else
            {
                resultado = new CN_Vehiculo().Editar(objVehiculo, idUsuario, out Mensaje);
            }

            if ((Convert.ToInt32(resultado) > 0 || (resultado is bool && (bool)resultado)) && hayNuevoArchivo)
            {
                string extension = Path.GetExtension(archivoImagen.FileName).ToLower();
                string nombreArchivo = objVehiculo.id_vehiculo.ToString() + extension;

                string rutaFisicaCompleta = Path.Combine(RUTA_BASE_IMAGENES, nombreArchivo);

                try
                {
                    if (!Directory.Exists(RUTA_BASE_IMAGENES))
                        Directory.CreateDirectory(RUTA_BASE_IMAGENES);

                    archivoImagen.SaveAs(rutaFisicaCompleta);

                    objVehiculo.imagen = nombreArchivo;

                    bool actualizacionExitosa = new CN_Vehiculo().ActualizarRutaImagen(objVehiculo, out string msg);

                    if (!actualizacionExitosa)
                        Mensaje += $". Advertencia: La imagen se guardó en disco ({nombreArchivo}), pero no se actualizó la referencia en BD.";
                    else
                        Mensaje = (resultado is bool && (bool)resultado) ? Mensaje : "Vehículo registrado con imagen."; 
                }
                catch (Exception ex)
                {
                    Mensaje += $". Error crítico al guardar la imagen en disco: {ex.Message}";
                }
            }
            else if (hayNuevoArchivo && Convert.ToInt32(resultado) == 0)
            {
                Mensaje += ". La imagen no fue subida porque la operación de Vehículo falló.";
            }


            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous] 
        public FileResult ObtenerImagen(string nombreArchivo)
        {
            if (string.IsNullOrWhiteSpace(nombreArchivo) || nombreArchivo.Contains(".."))
            {
                nombreArchivo = NOMBRE_IMAGEN_DEFAULT;
            }

            string rutaFisicaCompleta = Path.Combine(RUTA_BASE_IMAGENES, nombreArchivo);
            string mimeType = "image/png"; 

            if (System.IO.File.Exists(rutaFisicaCompleta))
            {
                mimeType = MimeMapping.GetMimeMapping(rutaFisicaCompleta);
            }
            else
            {
                rutaFisicaCompleta = Server.MapPath($"~/Content/imagenes/{NOMBRE_IMAGEN_DEFAULT}");
                mimeType = MimeMapping.GetMimeMapping(rutaFisicaCompleta);
            }

            return File(rutaFisicaCompleta, mimeType);
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
            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;

            bool resultado = new CN_TipoVehiculo().Eliminar(id, idUsuario, out string mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}