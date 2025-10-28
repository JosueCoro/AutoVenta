using CapaEntidad;
using System.Data.SqlClient;
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
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CRUD_USUARIO", oConexion);
                    cmd.Parameters.AddWithValue("@Operacion", "SELECT");
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                                nombre = dr["nombre"].ToString(),
                                apellido = dr["apellido"].ToString(),
                                ci = dr["ci"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                correo = dr["correo"].ToString(),
                                contraseña = dr["contraseña"].ToString(), 
                                estado = Convert.ToBoolean(dr["estado"]),
                                id_rol = Convert.ToInt32(dr["id_rol"]),
                                oRol = new Rol()
                                {
                                    id_rol = Convert.ToInt32(dr["id_rol"]),
                                    nombre = dr["NombreRol"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch { lista = new List<Usuario>(); }
            return lista;
        }

        public int Registrar(Usuario obj, int idUsuarioAuditoria, out string Mensaje)
        {
            int idGenerado = 0; Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CRUD_USUARIO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "INSERT");
                    cmd.Parameters.AddWithValue("@Nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("@Ci", obj.ci);
                    cmd.Parameters.AddWithValue("@Telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@Correo", obj.correo);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.contraseña);
                    cmd.Parameters.AddWithValue("@Estado", obj.estado);
                    cmd.Parameters.AddWithValue("@IdRol", obj.oRol.id_rol);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            { 
                idGenerado = 0; 
                Mensaje = ex.Message; 
            }
            return idGenerado;
        }

        public bool Editar(Usuario obj, int idUsuarioAuditoria, out string Mensaje)
        {
            bool resultado = false; Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CRUD_USUARIO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "UPDATE");
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.id_usuario);
                    cmd.Parameters.AddWithValue("@Nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.apellido);
                    cmd.Parameters.AddWithValue("@Ci", obj.ci);
                    cmd.Parameters.AddWithValue("@Telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@Correo", obj.correo);
                    cmd.Parameters.AddWithValue("@Contrasena", obj.contraseña);
                    cmd.Parameters.AddWithValue("@Estado", obj.estado);
                    cmd.Parameters.AddWithValue("@IdRol", obj.oRol.id_rol);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value) == 1;
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            { resultado = false; Mensaje = ex.Message; }
            return resultado;
        }

        public bool Eliminar(int id, int idUsuarioAuditoria, out string Mensaje)
        {
            bool resultado = false; Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CRUD_USUARIO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "DELETE");
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value) == 1;
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            { resultado = false; Mensaje = ex.Message; }
            return resultado;
        }
    }
}
