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
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    oConexion.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ListarUsuarios", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                                nombre = dr["nombre"].ToString(),
                                apellido = dr["apellido"].ToString(),
                                ci = dr["ci"].ToString(),
                                correo = dr["correo"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                rol_id_rol = Convert.ToInt32(dr["rol_id_rol"]),
                                nombre_rol = dr["nombre_rol"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Usuario>();
            }
            return lista;
        }

        // Método para registrar un nuevo usuario
        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("p_apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("p_ci", obj.ci);
                    cmd.Parameters.AddWithValue("p_correo", obj.correo);
                    cmd.Parameters.AddWithValue("p_contraseña", obj.contraseña);
                    cmd.Parameters.AddWithValue("p_rol_id_rol", obj.rol_id_rol);
                    cmd.Parameters.Add("p_id_usuario", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_Mensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["p_id_usuario"].Value);
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

        // Método para editar un usuario
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EditarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("p_id_usuario", obj.id_usuario);
                    cmd.Parameters.AddWithValue("p_nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("p_apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("p_ci", obj.ci);
                    cmd.Parameters.AddWithValue("p_correo", obj.correo);
                    cmd.Parameters.AddWithValue("p_contraseña", obj.contraseña ?? "");
                    cmd.Parameters.AddWithValue("p_estado", obj.estado);
                    cmd.Parameters.AddWithValue("p_rol_id_rol", obj.rol_id_rol);
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

        // Método para eliminar lógicamente un usuario
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection oConexion = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("p_id_usuario", id);
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
    }
}
