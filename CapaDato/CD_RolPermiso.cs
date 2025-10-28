using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato
{
    public class CD_RolPermiso
    {
        public List<Permiso> ListarPermisosPorRol(int idRol)
        {
            List<Permiso> lista = new List<Permiso>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CRUD_ROLES_PERMISOS", oConexion);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Operacion", "SELECT_ASIGNADOS");
                    cmd.Parameters.AddWithValue("@IdRol", idRol);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                id_permiso = Convert.ToInt32(dr["id_permiso"]),
                                accion = dr["accion"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                // Mapeamos la columna 'Asignado' devuelta por el SP (1 o 0)
                                Asignado = Convert.ToInt32(dr["Asignado"]) == 1
                            });
                        }
                    }
                }
            }
            catch (Exception )
            {
                lista = new List<Permiso>();
                // Aquí podrías loggear el error (ex.Message)
            }
            return lista;
        }


        // ----------------------------------------------------
        // GUARDAR: Asignar permisos masivamente a un rol
        // ----------------------------------------------------
        public bool GuardarPermisos(int idRol, List<Permiso> permisosAsignados, int idUsuarioAuditoria, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                // 1. Convertir la lista List<Permiso> a un DataTable para enviarlo como TVP al SP
                DataTable dtPermisos = new DataTable();
                dtPermisos.Columns.Add("id_permiso", typeof(int));

                foreach (Permiso p in permisosAsignados)
                {
                    dtPermisos.Rows.Add(p.id_permiso);
                }

                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CRUD_ROLES_PERMISOS", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 2. Asignar Parámetros de Entrada
                    cmd.Parameters.AddWithValue("@Operacion", "GUARDAR");
                    cmd.Parameters.AddWithValue("@IdRol", idRol);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    // Asignar el Table-Valued Parameter (TVP)
                    SqlParameter paramTVP = new SqlParameter("@TVP_Permisos", SqlDbType.Structured);
                    paramTVP.TypeName = "administracion.ListaPermisos"; // Nombre del tipo de tabla en la DB
                    paramTVP.Value = dtPermisos;
                    cmd.Parameters.Add(paramTVP);


                    // 3. Asignar Parámetros de Salida
                    SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    pMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMensaje);

                    SqlParameter pResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                    pResultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pResultado);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    // 4. Leer Resultados
                    respuesta = Convert.ToBoolean(pResultado.Value);
                    Mensaje = pMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error en CapaDatos: " + ex.Message;
            }

            return respuesta;
        }
    }
}
