using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace CapaDato
{
    public class Conexion
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public Conexion()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        public static string cn = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();


        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                    Console.WriteLine("Conexión a MySQL abierta exitosamente.");
                }
                return _connection;
            }
            catch (MySqlException ex)
            {
                // Registra el error detallado (puedes usar un logger real aquí)
                Console.WriteLine($"Error al abrir la conexión MySQL: {ex.Message}");
                // Lanza una nueva excepción para que la capa superior la maneje
                throw new Exception("No se pudo conectar a la base de datos MySQL. Revisa tu conexión y que XAMPP esté corriendo.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error inesperado al abrir la conexión: {ex.Message}");
                throw; // Re-lanza cualquier otra excepción
            }
        }
        // Método para cerrar la conexión
        public void CerrarConexion()
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    // Esto es solo para depuración; puedes quitarlo en producción
                    Console.WriteLine("Conexión a MySQL cerrada.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al cerrar la conexión MySQL: {ex.Message}");
                // Considera si necesitas relanzar o solo loggear este error
            }
            finally
            {
                // Es buena práctica asegurarse de que el objeto de conexión se libere
                if (_connection != null)
                {
                    _connection.Dispose();
                }
            }
        }

        // Opcional: un método para obtener la conexión directamente sin abrirla/cerrarla inmediatamente
        // Esto es útil si vas a pasar la conexión a un repositorio o método DAO
        public MySqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
