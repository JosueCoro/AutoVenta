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

        private const string RUTA_BASE_IMAGENES = "C:\\imagenes\\"; // <--- ¡CAMBIE ESTA RUTA!
        private const string NOMBRE_IMAGEN_DEFAULT = "default.png";

        // CapaPresentacion/Controllers/VehiculoController.cs

        [HttpPost]
        public JsonResult GuardarVehiculo(string objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string Mensaje = string.Empty;

            Vehiculo objVehiculo = new JavaScriptSerializer().Deserialize<Vehiculo>(objeto);

            int idUsuario = ((Usuario_Activo)Session["Usuario"]).id_usuario;
            bool hayNuevoArchivo = archivoImagen != null && archivoImagen.ContentLength > 0;

            // Almacenar el nombre de la imagen que ya está en DB o la que se usará por defecto.
            string nombreImagenExistente = objVehiculo.imagen;

            // 1. REGISTRO (INSERT)
            if (objVehiculo.id_vehiculo == 0)
            {
                objVehiculo.fecha_ingreso = DateTime.Now.ToString("yyyy-MM-dd");
                // Usamos el nombre del archivo por defecto
                objVehiculo.imagen = NOMBRE_IMAGEN_DEFAULT;

                int idGenerado = new CN_Vehiculo().Registrar(objVehiculo, idUsuario, out Mensaje);
                resultado = idGenerado;
                objVehiculo.id_vehiculo = idGenerado;
            }
            // 2. EDICIÓN (UPDATE)
            else
            {
                // Si no hay archivo nuevo, objVehiculo.imagen ya contiene el nombre del archivo antiguo.
                // Si hay archivo nuevo, usaremos ese ID para renombrarlo y actualizar la DB más abajo.
                resultado = new CN_Vehiculo().Editar(objVehiculo, idUsuario, out Mensaje);
            }

            // 3. PROCESAMIENTO Y GUARDA DE IMAGEN
            // Si la operación de DB fue exitosa (resultado > 0 para INSERT, resultado = true para UPDATE)
            if ((Convert.ToInt32(resultado) > 0 || (resultado is bool && (bool)resultado)) && hayNuevoArchivo)
            {
                // Generar el nombre del archivo basado en el ID del vehículo y la extensión original.
                string extension = Path.GetExtension(archivoImagen.FileName).ToLower();
                string nombreArchivo = objVehiculo.id_vehiculo.ToString() + extension;

                // Ruta física de destino
                string rutaFisicaCompleta = Path.Combine(RUTA_BASE_IMAGENES, nombreArchivo);

                try
                {
                    // Crear carpeta si no existe
                    if (!Directory.Exists(RUTA_BASE_IMAGENES))
                        Directory.CreateDirectory(RUTA_BASE_IMAGENES);

                    // Guardar la imagen en el disco físico externo
                    archivoImagen.SaveAs(rutaFisicaCompleta);

                    // Guardamos SOLO el nombre del archivo en el objeto para la DB
                    objVehiculo.imagen = nombreArchivo;

                    // Actualizar la ruta/nombre en BD
                    bool actualizacionExitosa = new CN_Vehiculo().ActualizarRutaImagen(objVehiculo, out string msg);

                    if (!actualizacionExitosa)
                        Mensaje += $". Advertencia: La imagen se guardó en disco ({nombreArchivo}), pero no se actualizó la referencia en BD.";
                    else
                        Mensaje = (resultado is bool && (bool)resultado) ? Mensaje : "Vehículo registrado con imagen."; // Mensaje más claro si insertó y subió imagen
                }
                catch (Exception ex)
                {
                    Mensaje += $". Error crítico al guardar la imagen en disco: {ex.Message}";
                }
            }
            else if (hayNuevoArchivo && Convert.ToInt32(resultado) == 0)
            {
                // Si hay archivo, pero la operación de DB falló (ej: placa duplicada).
                Mensaje += ". La imagen no fue subida porque la operación de Vehículo falló.";
            }


            return Json(new { resultado = resultado, mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
        // ... (Dentro de la clase VehiculoController) ...

        [HttpGet]
        [AllowAnonymous] // Permite el acceso directo a la imagen sin login (necesario para el <img>)
        public FileResult ObtenerImagen(string nombreArchivo)
        {
            // 1. Validar nombre de archivo para evitar Path Traversal
            if (string.IsNullOrWhiteSpace(nombreArchivo) || nombreArchivo.Contains(".."))
            {
                nombreArchivo = NOMBRE_IMAGEN_DEFAULT;
            }

            string rutaFisicaCompleta = Path.Combine(RUTA_BASE_IMAGENES, nombreArchivo);
            string mimeType = "image/png"; // Default MIME type

            // 2. Verificar existencia del archivo físico
            if (System.IO.File.Exists(rutaFisicaCompleta))
            {
                // Determinar el tipo MIME basado en la extensión
                mimeType = MimeMapping.GetMimeMapping(rutaFisicaCompleta);
            }
            else
            {
                // Si no existe, servir la imagen por defecto
                rutaFisicaCompleta = Server.MapPath($"~/Content/imagenes/{NOMBRE_IMAGEN_DEFAULT}");
                mimeType = MimeMapping.GetMimeMapping(rutaFisicaCompleta);
            }

            // 3. Devolver el archivo
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