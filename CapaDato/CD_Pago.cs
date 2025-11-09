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
    public class CD_Pago
    {
        public List<Pago> Listar()
        {
            List<Pago> lista = new List<Pago>();
            // Usamos try-catch para manejar errores de conexión/SQL
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_PAGO", oConexion);
                    cmd.Parameters.AddWithValue("@Operacion", "SELECT");
                    // Los parámetros de salida son necesarios aunque no se usen en el SELECT
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pago()
                            {
                                id_pago = Convert.ToInt32(dr["id_pago"]),
                                nombre = dr["nombre"].ToString(),
                                fecha = Convert.ToDateTime(dr["fecha"]).ToShortDateString(),
                                hora = dr["hora"].ToString(),
                                monto = Convert.ToDecimal(dr["monto"]),
                                id_venta = Convert.ToInt32(dr["id_venta"]),
                                nombreTP = dr["NombreTipoPago"].ToString(),
                                
                            });
                        }
                    }
                }
            }
            catch { lista = new List<Pago>(); }
            return lista;
        }
        public int Registrar(Pago objPago, int idUsuarioAuditoria, out string Mensaje)
        {
            int idPagoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.RegistrarPago", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdVenta", objPago.id_venta);
                    cmd.Parameters.AddWithValue("@MontoPago", objPago.monto);
                    cmd.Parameters.AddWithValue("@IdTipoPago", objPago.id_tipo_pago);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdPagoGenerado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idPagoGenerado = Convert.ToInt32(cmd.Parameters["@IdPagoGenerado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idPagoGenerado = 0;
                Mensaje = "Error en la Capa de Datos al registrar pago: " + ex.Message;
            }
            return idPagoGenerado;
        }
    }
}
