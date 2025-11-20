using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDato
{
    public class CD_Reporte
    {
        // Método para el Reporte de Ganancia de una Venta
        public ReporteVentaGanancia ObtenerReporteVenta(int idVenta, out string Mensaje)
        {
            ReporteVentaGanancia reporte = null;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.ReporteGananciaVenta", oConexion);
                    cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // 1. LECTURA DEL PRIMER RESULT SET (RESUMEN)
                        if (dr.Read())
                        {
                            reporte = new ReporteVentaGanancia()
                            {
                                id_venta = Convert.ToInt32(dr["id_venta"]),
                                fecha = Convert.ToDateTime(dr["fecha"]).ToShortDateString(),
                                IngresoTotalVenta = Convert.ToDecimal(dr["IngresoTotalVenta"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                NombreVendedor = dr["NombreVendedor"].ToString(),
                                VehiculosVendidos = dr["VehiculosVendidos"].ToString(),
                                ComisionAsesor = Convert.ToDecimal(dr["ComisionAsesor"]),
                                GastosTotalesVehiculos = Convert.ToDecimal(dr["GastosTotalesVehiculos"]),
                                GananciaNeta = Convert.ToDecimal(dr["GananciaNeta"]),
                                DetalleGastos = new List<ReporteGastoDetalle>() // Inicializar lista
                            };
                        }

                        // 2. AVANZAR AL SEGUNDO RESULT SET (DETALLE DE GASTOS)
                        if (reporte != null && dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                reporte.DetalleGastos.Add(new ReporteGastoDetalle()
                                {
                                    descripcion = dr["descripcion"].ToString(),
                                    monto = Convert.ToDecimal(dr["monto"]),
                                    TipoGasto = dr["TipoGasto"].ToString(),
                                    PlacaVehiculo = dr["PlacaVehiculo"].ToString(),
                                    ModeloVehiculo = dr["ModeloVehiculo"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                reporte = null;
                Mensaje = ex.Message;
            }
            return reporte;
        }

        // Método para el Reporte de Rentabilidad de un Vehículo
        public ReporteVehiculoRentabilidad ObtenerReporteVehiculo(int idVehiculo, out string Mensaje)
        {
            ReporteVehiculoRentabilidad reporte = null;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.ReporteRentabilidadVehiculo", oConexion);
                    cmd.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // 1. LECTURA DEL PRIMER RESULT SET (RESUMEN)
                        if (dr.Read())
                        {
                            reporte = new ReporteVehiculoRentabilidad()
                            {
                                id_vehiculo = Convert.ToInt32(dr["id_vehiculo"]),
                                placa = dr["placa"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                estado = dr["estado"].ToString(),
                                precio_compra = Convert.ToDecimal(dr["precio_compra"]),
                                precio_venta = dr["precio_venta"] is DBNull ? (decimal?)null : Convert.ToDecimal(dr["precio_venta"]),
                                TotalGastosVehiculo = Convert.ToDecimal(dr["TotalGastosVehiculo"]),
                                GananciaNetaExacta = Convert.ToDecimal(dr["GananciaNetaExacta"]),
                                IdVentaAsociada = dr["IdVentaAsociada"] is DBNull ? (int?)null : Convert.ToInt32(dr["IdVentaAsociada"]),
                                FechaVenta = dr["FechaVenta"] is DBNull ? (DateTime?)null : Convert.ToDateTime(dr["FechaVenta"]),
                                ClienteAsociado = dr["ClienteAsociado"].ToString(),
                                DetalleGastos = new List<ReporteGastoDetalle>()
                            };
                        }

                        // 2. AVANZAR AL SEGUNDO RESULT SET (DETALLE DE GASTOS)
                        if (reporte != null && dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                reporte.DetalleGastos.Add(new ReporteGastoDetalle()
                                {
                                    descripcion = dr["descripcion"].ToString(),
                                    monto = Convert.ToDecimal(dr["monto"]),
                                    TipoGasto = dr["TipoGasto"].ToString(),
                                    fecha = Convert.ToDateTime(dr["fecha"]) // Solo la fecha es importante aquí
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                reporte = null;
                Mensaje = ex.Message;
            }
            return reporte;
        }
    }
}