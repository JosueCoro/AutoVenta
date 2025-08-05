using CapaEntidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato
{
    public class CD_Marca
    {
        // Método para listar todas las marcas
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    oConexion.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ListarMarcas", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                id_marca = Convert.ToInt32(dr["id_marca"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Marca>();
            }
            return lista;
        }

        // Método para registrar una nueva marca
        public int Registrar(Marca obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarMarca", oConexion);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.Add("p_id_marca", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_Mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["p_id_marca"].Value);
                    Mensaje = cmd.Parameters["p_Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Mensaje = ex.Message;
            }
            return idGenerado;
        }

        // Método para editar una marca
        public bool Editar(Marca obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EditarMarca", oConexion);
                    cmd.Parameters.AddWithValue("p_id_marca", obj.id_marca);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.Add("p_Mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["p_Mensaje"].Value.ToString();
                    resultado = Mensaje == "";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        // Método para eliminar una marca
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarMarca", oConexion);
                    cmd.Parameters.AddWithValue("p_id_marca", id);
                    cmd.Parameters.Add("p_Mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Mensaje = cmd.Parameters["p_Mensaje"].Value.ToString();
                    resultado = Mensaje == "";
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
        public bool MarcaAsociadaAVehiculo(int id_marca)
        {
            bool existe = false;
            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    oConexion.Open();
                    string query = "SELECT COUNT(*) FROM vehiculo WHERE marca_id_marca = @id_marca;";
                    MySqlCommand cmd = new MySqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@id_marca", id_marca);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    existe = count > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al verificar la asociación de la marca: " + ex.Message);
            }
            return existe;
        }
    }
}
