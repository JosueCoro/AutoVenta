using CapaDato;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    [Authorize]
    public class BitacoraController : Controller
    {
        private CN_Bitacora cnBitacora = new CN_Bitacora();
        private CN_Usuario cnUsuario = new CN_Usuario();
        // GET: Bitacora
        [ValidarPermisos(NombrePermiso = "Visualizar Bitacora")]
        public ActionResult Bitacora()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> lista = cnUsuario.Listar()
                .Select(u => new Usuario
                {
                    id_usuario = u.id_usuario,
                    nombre = u.nombre,
                    apellido = u.apellido
                }).ToList();

            lista.Insert(0, new Usuario { id_usuario = 0, nombre = "TODOS", apellido = "LOS USUARIOS" });

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarUsuarios2()
        {
            try
            {
                List<Usuario> lista = cnUsuario.Listar();

                var listaMapeada = lista.Select(v => new
                {
                    v.id_usuario,
                    v.nombre,
                    v.apellido,
                    v.ci,
                    v.correo,
                    //mostrar el nombre del rol
                    nombre_rol = v.oRol != null ? v.oRol.nombre : string.Empty




                }).ToList();
                return Json(new { data = listaMapeada }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<object>(), error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult ConsultarBitacora(string fechaInicio, string fechaFin, int idUsuario)
        {
            DateTime inicio = DateTime.Parse(fechaInicio);
            DateTime fin = DateTime.Parse(fechaFin);

            List<Bitacora> lista = cnBitacora.Consultar(inicio, fin, idUsuario);

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}