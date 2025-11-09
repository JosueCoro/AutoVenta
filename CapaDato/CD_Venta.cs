using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDato
{
    public class CD_Venta
    {
        public List<VentaSimple> ListarSimples()
        {
            List<VentaSimple> lista = new List<VentaSimple>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.ListarVentasParaBusqueda", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new VentaSimple()
                            {
                                id_venta = Convert.ToInt32(dr["id_venta"]),
                                fecha = Convert.ToDateTime(dr["fecha"]).ToShortDateString(),
                                total = Convert.ToDecimal(dr["total"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                CiNitCliente = dr["CiNitCliente"].ToString(),
                                VehiculosVendidos = dr["VehiculosVendidos"].ToString()
                            });
                        }
                    }
                }
            }
            catch { lista = new List<VentaSimple>(); }
            return lista;
        }

        public List<VentaSimple> ReporteVenta(string fechaInicio, string fechaFin)
        {
            List<VentaSimple> lista = new List<VentaSimple>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.ReporteVenta", oConexion);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new VentaSimple()
                            {
                                id_venta = Convert.ToInt32(dr["id_venta"]), 
                                fecha = dr["fecha"].ToString(),
                                total = Convert.ToDecimal(dr["total"]),
                                comision = Convert.ToDecimal(dr["comision"]), 
                                NombreUsuario = dr["NombreUsuario"].ToString(), 
                                NombreCliente = dr["NombreCliente"].ToString(),
                                CiNitCliente = dr["CiNitCliente"].ToString(),
                                VehiculosVendidos = dr["VehiculosVendidos"].ToString() 
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<VentaSimple>();
            }
            return lista;
        }

        public int Registrar(Venta objVenta, out string Mensaje)
        {
            int idVentaGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle.Columns.Add("id_vehiculo", typeof(int));
                dtDetalle.Columns.Add("precio_venta", typeof(decimal)); 

                foreach (DetalleVenta detalle in objVenta.oDetalleVenta)
                {
                    dtDetalle.Rows.Add(new object[] {
                        detalle.id_vehiculo,
                        detalle.precio_venta
                    });
                }

                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.RegistrarVenta", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Fecha", objVenta.fecha);
                    cmd.Parameters.AddWithValue("@Observaciones", objVenta.observaciones);
                    cmd.Parameters.AddWithValue("@IdCliente", objVenta.id_cliente);
                    cmd.Parameters.AddWithValue("@IdUsuario", objVenta.id_usuario);
                    cmd.Parameters.AddWithValue("@MontoComision", objVenta.comision); 
                    cmd.Parameters.AddWithValue("@IdAsesor", objVenta.id_asesor);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", objVenta.id_usuario); 

                    SqlParameter paramDetalle = new SqlParameter();
                    paramDetalle.ParameterName = "@DetalleVenta";
                    paramDetalle.Value = dtDetalle;
                    paramDetalle.SqlDbType = SqlDbType.Structured;
                    paramDetalle.TypeName = "comercial.DetalleVenta";
                    cmd.Parameters.Add(paramDetalle);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdVentaGenerado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idVentaGenerado = Convert.ToInt32(cmd.Parameters["@IdVentaGenerado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idVentaGenerado = 0;
                Mensaje = "Error en la Capa de Datos: " + ex.Message;
            }
            return idVentaGenerado;
        }
    }
}