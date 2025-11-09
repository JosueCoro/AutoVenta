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
    public class CD_Dashboard
    {
        public Dictionary<string, object> ObtenerResumenDashboard()
        {
            var resultados = new Dictionary<string, object>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.ObtenerResumenDashboard", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            var kpi = new ReporteDashboard
                            {
                                TotalStockDisponible = Convert.ToInt32(dr["TotalStockDisponible"]),
                                TotalVentasMes = Convert.ToDecimal(dr["TotalVentasMes"]),
                                TotalClientes = Convert.ToInt32(dr["TotalClientes"]),
                                TotalComisionesPendientes = Convert.ToDecimal(dr["TotalComisionesPendientes"])
                            };
                            resultados.Add("KPIs", kpi);
                        }

                        if (dr.NextResult())
                        {
                            var listaMensual = new List<ReporteDashboard>();
                            while (dr.Read())
                            {
                                listaMensual.Add(new ReporteDashboard
                                {
                                    Periodo = dr["Periodo"].ToString(),
                                    Año = Convert.ToInt32(dr["Anio"]),
                                    MesNumero = Convert.ToInt32(dr["MesNumero"]),
                                    IngresoTotal = Convert.ToDecimal(dr["IngresoTotal"])
                                });
                            }
                            resultados.Add("ResumenMensual", listaMensual);
                        }

                        if (dr.NextResult())
                        {
                            var listaInventario = new List<ReporteDashboard>();
                            while (dr.Read())
                            {
                                listaInventario.Add(new ReporteDashboard
                                {
                                    EstadoInventario = dr["EstadoInventario"].ToString(),
                                    Conteo = Convert.ToInt32(dr["Conteo"])
                                });
                            }
                            resultados.Add("EstadoInventario", listaInventario);
                        }
                    }
                }
            }
            catch {
                resultados = new Dictionary<string, object>();
            }
            return resultados;
        }
    }
}
