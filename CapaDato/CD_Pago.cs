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

                    // Parámetros de ENTRADA
                    cmd.Parameters.AddWithValue("@IdVenta", objPago.id_venta);
                    cmd.Parameters.AddWithValue("@MontoPago", objPago.monto);
                    cmd.Parameters.AddWithValue("@IdTipoPago", objPago.id_tipo_pago);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    // Parámetros de SALIDA
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdPagoGenerado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    // Capturar valores de salida
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
