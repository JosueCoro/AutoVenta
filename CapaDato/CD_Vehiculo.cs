using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
        

namespace CapaDato
{
    public class CD_Vehiculo
    {
        public List<Vehiculo> Listar()
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_VEHICULO", oConexion);
                    cmd.Parameters.AddWithValue("@Operacion", "SELECT");
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Vehiculo()
                            {
                                id_vehiculo = Convert.ToInt32(dr["id_vehiculo"]),
                                modelo = dr["modelo"].ToString(),
                                año = dr["año"].ToString(),
                                placa = dr["placa"].ToString(),
                                color = dr["color"].ToString(),
                                estado = dr["estado"].ToString(),
                                fecha_ingreso = dr["fecha_ingreso"].ToString(),
                                precio_compra = Convert.ToDecimal(dr["precio_compra"]),
                                precio_venta = Convert.ToDecimal(dr["precio_venta"]),
                                imagen = dr["imagen"].ToString(),

                                oMarca = new Marca()
                                {
                                    id_marca = Convert.ToInt32(dr["id_marca"]),
                                    nombre = dr["NombreMarca"].ToString()
                                },
                                oTipoVehiculo = new TipoVehiculo()
                                {
                                    id_tp_vehiculo = Convert.ToInt32(dr["id_tp_vehiculo"]),
                                    descripcion = dr["DescripcionTipoVehiculo"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception )
            {
                lista = new List<Vehiculo>();
            }
            return lista;
        }

        public int Registrar(Vehiculo obj, int idUsuario, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_VEHICULO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "INSERT");
                    cmd.Parameters.AddWithValue("@Modelo", obj.modelo);
                    cmd.Parameters.AddWithValue("@Anio", obj.año);
                    cmd.Parameters.AddWithValue("@Placa", obj.placa);
                    cmd.Parameters.AddWithValue("@Color", obj.color);
                    cmd.Parameters.AddWithValue("@Estado", obj.estado);
                    cmd.Parameters.AddWithValue("@FechaIngreso", obj.fecha_ingreso);
                    cmd.Parameters.AddWithValue("@PrecioCompra", obj.precio_compra);
                    cmd.Parameters.AddWithValue("@IdMarca", obj.oMarca.id_marca);
                    cmd.Parameters.AddWithValue("@Imagen", obj.imagen);
                    cmd.Parameters.AddWithValue("@IdTpVehiculo", obj.oTipoVehiculo.id_tp_vehiculo);
                    cmd.Parameters.AddWithValue("@PrecioVenta", obj.precio_venta);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuario);

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

        public bool Editar(Vehiculo obj, int idUsuario, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_VEHICULO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "UPDATE");
                    cmd.Parameters.AddWithValue("@IdVehiculo", obj.id_vehiculo);
                    cmd.Parameters.AddWithValue("@Modelo", obj.modelo);
                    cmd.Parameters.AddWithValue("@Anio", obj.año);
                    cmd.Parameters.AddWithValue("@Placa", obj.placa);
                    cmd.Parameters.AddWithValue("@Color", obj.color);
                    cmd.Parameters.AddWithValue("@Estado", obj.estado);
                    cmd.Parameters.AddWithValue("@FechaIngreso", obj.fecha_ingreso);
                    cmd.Parameters.AddWithValue("@PrecioCompra", obj.precio_compra);
                    cmd.Parameters.AddWithValue("@IdMarca", obj.oMarca.id_marca);
                    cmd.Parameters.AddWithValue("@Imagen", obj.imagen);
                    cmd.Parameters.AddWithValue("@IdTpVehiculo", obj.oTipoVehiculo.id_tp_vehiculo);
                    cmd.Parameters.AddWithValue("@PrecioVenta", obj.precio_venta);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuario);

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
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, int idUsuario, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_VEHICULO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "DELETE");
                    cmd.Parameters.AddWithValue("@IdVehiculo", id);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuario);

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
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
        public bool ActualizarRutaImagen(Vehiculo obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.sp_ActualizarRutaImagen", oConexion);

                    cmd.Parameters.AddWithValue("@IdVehiculo", obj.id_vehiculo);
                    cmd.Parameters.AddWithValue("@RutaImagen", obj.imagen);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = "Error DB al actualizar ruta: " + ex.Message;
            }
            return resultado;
        }
        public Vehiculo ObtenerVehiculo(int idVehiculo)
        {
            Vehiculo objVehiculo = null;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.ObtenerVehiculo", oConexion);
                    cmd.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            objVehiculo = new Vehiculo()
                            {
                                id_vehiculo = Convert.ToInt32(dr["id_vehiculo"]),
                                placa = dr["placa"].ToString(),
                                modelo = dr["modelo"].ToString(),
                                imagen = dr["imagen"].ToString(),

                                oMarca = new Marca()
                                {
                                    
                                    nombre = dr["NombreMarca"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            catch (Exception )
            {
                objVehiculo = null;
            }

            return objVehiculo;
        }

        public ReporteCostoVehiculoDTO ReporteGastoVehiculo(int idVehiculo, out string mensaje)
        {
            ReporteCostoVehiculoDTO reporte = new ReporteCostoVehiculoDTO
            {
                ListaGastos = new List<GastoVehiculoReporte>(),
                Resumen = new ResumenCostoVehiculo()
            };
            mensaje = string.Empty;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("comercial.ReporteGastoVehiculo", oConexion);
                    cmd.Parameters.AddWithValue("IdVehiculo", idVehiculo);
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // 1. Primer conjunto de resultados: Detalle de Gastos
                        while (dr.Read())
                        {
                            reporte.ListaGastos.Add(new GastoVehiculoReporte()
                            {
                                descripcion = dr["descripcion"].ToString(),
                                monto = Convert.ToDecimal(dr["monto"]),
                                fecha = dr["fecha"].ToString(), // Ajustar formato si es necesario
                                TipoGasto = dr["TipoGasto"].ToString()
                            });
                        }

                        // Pasar al segundo conjunto de resultados: Resumen Financiero
                        if (dr.NextResult() && dr.Read())
                        {
                            reporte.Resumen = new ResumenCostoVehiculo()
                            {
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                                TotalGastos = Convert.ToDecimal(dr["TotalGastos"]),
                                CostoTotal = Convert.ToDecimal(dr["CostoTotal"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"])
                            };
                        }
                    }

                    int resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    if (resultado == 0)
                    {
                        return null; // El vehículo no se encontró o hubo un error
                    }

                    // OBTENER DATOS ADICIONALES DEL VEHÍCULO (Placa, Modelo, Marca, ImagenRuta)
                    Vehiculo v = new CD_Vehiculo().ObtenerVehiculo(idVehiculo);
                    if (v != null)
                    {
                        reporte.Placa = v.placa;
                        reporte.Modelo = v.modelo;
                        reporte.Marca = v.oMarca.nombre;
                        reporte.ImagenRuta = v.imagen;
                    }
                }
                catch (Exception ex)
                {
                    reporte = null;
                    mensaje = "Error al obtener el reporte: " + ex.Message;
                }
            }
            return reporte;
        }
    }
}
