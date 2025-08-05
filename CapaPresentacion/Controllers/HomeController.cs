using CapaDato;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // --- Nuevo método para probar la conexión ---
        public ActionResult TestConnection()
        {
            string message = "";
            Conexion conexion = new Conexion();

            try
            {
                MySqlConnection conn = conexion.AbrirConexion();
                message = "¡Conexión a MySQL exitosa!";
                // Puedes realizar una pequeña consulta de prueba aquí si lo deseas
                // Por ejemplo:
                // using (MySqlCommand cmd = new MySqlCommand("SELECT 1", conn))
                // {
                //     cmd.ExecuteScalar();
                // }
            }
            catch (Exception ex)
            {
                message = $"Error de conexión a MySQL: {ex.Message}";
                // Log the full exception for debugging
                System.Diagnostics.Debug.WriteLine($"Error de conexión: {ex}");
            }
            finally
            {
                conexion.CerrarConexion();
            }

            // Pasa el mensaje a la vista
            ViewBag.ConnectionStatus = message;
            return View(); // Puedes crear una vista específica para esto o usar Index
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