using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;


namespace CapaDato
{
    public class CD_Login
    {
        public Usuario_Activo ValidarUsuario(string correo, string contrasenaHasheada)
        {
            Usuario_Activo usuario = null;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
            {
                SqlCommand cmd = new SqlCommand("administracion.sp_login", oConexion);
                cmd.Parameters.AddWithValue("Correo", correo);
                cmd.Parameters.AddWithValue("Contraseña", contrasenaHasheada);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    // --- PRIMER RESULT SET: DATOS DEL USUARIO ---
                    if (dr.Read())
                    {
                        // Si hay filas, el login fue exitoso. Mapeamos el usuario.
                        usuario = new Usuario_Activo()
                        {
                            id_usuario = Convert.ToInt32(dr["id_usuario"]),
                            nombre = dr["nombre"].ToString(),
                            apellido = dr["apellido"].ToString(),
                            ci = dr["ci"].ToString(),
                            correo = dr["correo"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            estado = Convert.ToBoolean(dr["EstadoUsuario"]),
                            id_rol = Convert.ToInt32(dr["id_rol"]),
                            nombre_rol = dr["NombreRol"].ToString(),
                            EstadoRol = Convert.ToBoolean(dr["EstadoRol"]),
                            ListaPermisos = new List<Permiso>()
                        };
                    }

                    // Si el usuario fue encontrado, consumimos el SEGUNDO RESULT SET (Permisos)
                    if (usuario != null)
                    {
                        // Mueve el lector al segundo Result Set (reader.NextResult())
                        if (dr.NextResult())
                        {
                            // --- SEGUNDO RESULT SET: IDs DE PERMISO ---
                            while (dr.Read())
                            {
                                usuario.ListaPermisos.Add(new Permiso()
                                {
                                    id_permiso = Convert.ToInt32(dr["id_permiso"]),
                                    accion = dr["accion"].ToString()
                                });
                            }
                        }
                    }

                    dr.Close();

                }
                catch
                {
                    usuario = null;
                }
            }
            return usuario;
        }
    }
}
