using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using CapaEntidad;

namespace CapaDato
{
    public class CD_TipoGasto
    {
        public List<TipoGasto> Listar()
        {
            List<TipoGasto> lista = new List<TipoGasto>();
            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    oConexion.Open();
                    string query = "SELECT id_tipo_gasto, nombre FROM tipo_gasto;";

                    MySqlCommand cmd = new MySqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TipoGasto()
                            {
                                id_tipo_gasto = Convert.ToInt32(dr["id_tipo_gasto"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<TipoGasto>();
            }
            return lista;
        }

        // Método para registrar un nuevo tipo de gasto
        public int Registrar(TipoGasto obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarTipoGasto", oConexion);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.Add("p_id_tipo_gasto", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_Mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["p_id_tipo_gasto"].Value);
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

        // Método para editar un tipo de gasto
        public bool Editar(TipoGasto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EditarTipoGasto", oConexion);
                    cmd.Parameters.AddWithValue("p_id_tipo_gasto", obj.id_tipo_gasto);
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

        // Método para eliminar un tipo de gasto
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarTipoGasto", oConexion);
                    cmd.Parameters.AddWithValue("p_id_tipo_gasto", id);
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

        public bool TipoGastoAsociadoAGasto(int id_tipo_gasto)
        {
            bool existe = false;
            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    oConexion.Open();
                    string query = "SELECT COUNT(*) FROM gasto WHERE tipo_gasto_id_tipo_gasto = @id_tipo_gasto;";
                    MySqlCommand cmd = new MySqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@id_tipo_gasto", id_tipo_gasto);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    existe = count > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al verificar la asociación del tipo de gasto: " + ex.Message);
            }
            return existe;
        }
    }
}
