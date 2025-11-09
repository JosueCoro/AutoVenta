using CapaEntidad;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaDato
{
    public class CD_Comision
    {
        public List<Comision> Listar()
        {
            List<Comision> lista = new List<Comision>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_COMISION", oConexion);
                    cmd.Parameters.AddWithValue("@Operacion", "SELECT");
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", 1);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Comision()
                            {
                                id_comision = Convert.ToInt32(dr["id_comision"]),
                                monto = Convert.ToDecimal(dr["monto"]),
                                fecha_pago = dr["fecha_pago"] is DBNull ? null : Convert.ToDateTime(dr["fecha_pago"]).ToString("yyyy-MM-dd HH:mm"),
                                fecha_generacion = Convert.ToDateTime(dr["fecha_generacion"]).ToString("yyyy-MM-dd HH:mm"),
                                estado = dr["estado"].ToString(),
                                observaciones = dr["observaciones"].ToString(),
                                id_venta = Convert.ToInt32(dr["id_venta"]),
                                id_asesor = Convert.ToInt32(dr["id_asesor"]),

                                NombreAsesor = dr["NombreAsesor"].ToString(),
                                CiAsesor = dr["CiAsesor"].ToString()
                            });
                        }
                    }
                }
            }
            catch { lista = new List<Comision>(); }
            return lista;
        }

        public bool Pagar(int idComision, int idUsuarioAuditoria, out string Mensaje)
        {
            bool resultado = false; Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_COMISION", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Operacion", "UPDATE");
                    cmd.Parameters.AddWithValue("@IdComision", idComision);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

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