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
                                Asignado = Convert.ToInt32(dr["Asignado"]) == 1
                            });
                        }
                    }
                }
            }
            catch (Exception )
            {
                lista = new List<Permiso>();
            }
            return lista;
        }

        public bool GuardarPermisos(int idRol, List<Permiso> permisosAsignados, int idUsuarioAuditoria, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
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

                    cmd.Parameters.AddWithValue("@Operacion", "GUARDAR");
                    cmd.Parameters.AddWithValue("@IdRol", idRol);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    SqlParameter paramTVP = new SqlParameter("@TVP_Permisos", SqlDbType.Structured);
                    paramTVP.TypeName = "administracion.ListaPermisos"; 
                    paramTVP.Value = dtPermisos;
                    cmd.Parameters.Add(paramTVP);


                    SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                    pMensaje.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pMensaje);

                    SqlParameter pResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                    pResultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pResultado);

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

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
