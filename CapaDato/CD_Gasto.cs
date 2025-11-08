using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDato
{
    public class CD_Gasto
    {
        /*public List<Gasto> Listar()
        {
            List<Gasto> lista = new List<Gasto>();
            string mensajeError = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_GASTO", oConexion);
                    cmd.Parameters.AddWithValue("@Operacion", "SELECT");
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bool esGastoVehiculo = dr["id_vehiculo"] != DBNull.Value;

                            lista.Add(new Gasto()
                            {
                                id_gasto = Convert.ToInt32(dr["id_gasto"]),
                                descripcion = dr["descripcion"].ToString(),
                                monto = Convert.ToDecimal(dr["monto"]),
                                fecha = Convert.ToDateTime(dr["fecha"]).ToShortDateString(),

                                id_tipo_gasto = Convert.ToInt32(dr["id_tipo_gasto"]),
                                oTipoGasto = new TipoGasto()
                                {
                                    nombre = dr["NombreTipoGasto"].ToString()
                                },

                                id_vehiculo = esGastoVehiculo ? (int?)Convert.ToInt32(dr["id_vehiculo"]) : null,
                                oVehiculo = esGastoVehiculo ? new Vehiculo()
                                {
                                    modelo = dr["ModeloVehiculo"].ToString(),
                                    año = dr["AnioVehiculo"].ToString(),
                                    placa = dr["Placa"].ToString(),
                                } : null,

                                id_venta = dr["id_venta"] != DBNull.Value ? (int?)Convert.ToInt32(dr["id_venta"]) : null,
                                oVenta = null 
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                lista = new List<Gasto>();
                throw new Exception("Fallo en CD_Gasto.Listar: " + mensajeError);
            }
            return lista;
        }*/
        public List<Gasto> ListarGeneral()
        {
            return ListarGastosBase("SELECT");
        }

        public List<Gasto> ListarGastosVehiculos()
        {
            return ListarGastosBase("SELECT_GVH");
        }

        public List<Gasto> ListarGastosVentas()
        {
            return ListarGastosBase("SELECT_GVS");
        }

        private List<Gasto> ListarGastosBase(string operacion)
        {
            List<Gasto> lista = new List<Gasto>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_GASTO", oConexion);
                    cmd.Parameters.AddWithValue("@Operacion", operacion);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Determinar si la columna de Vehículo tiene valor (no es nula)
                            bool esGastoVehiculo = dr["id_vehiculo"] != DBNull.Value;
                            // Determinar si la columna de Venta tiene valor (no es nula)
                            bool esGastoVenta = dr["id_venta"] != DBNull.Value;

                            Gasto nuevoGasto = new Gasto()
                            {
                                id_gasto = Convert.ToInt32(dr["id_gasto"]),
                                descripcion = dr["descripcion"].ToString(),
                                monto = Convert.ToDecimal(dr["monto"]),
                                fecha = Convert.ToDateTime(dr["fecha"]).ToShortDateString(),

                                id_tipo_gasto = Convert.ToInt32(dr["id_tipo_gasto"]),
                                oTipoGasto = new TipoGasto()
                                {
                                    nombre = dr["NombreTipoGasto"].ToString()
                                },

                                // *** DATOS DE VEHÍCULO ***
                                id_vehiculo = esGastoVehiculo ? (int?)Convert.ToInt32(dr["id_vehiculo"]) : null,
                                oVehiculo = esGastoVehiculo ? new Vehiculo()
                                {
                                    modelo = dr["ModeloVehiculo"].ToString(),
                                    año = dr["AnioVehiculo"].ToString(),
                                    placa = dr["Placa"].ToString(),
                                    // Asumiendo que esta es la entidad Vehiculo completa
                                } : null,

                                // *** DATOS DE VENTA Y CLIENTE ***
                                id_venta = esGastoVenta ? (int?)Convert.ToInt32(dr["id_venta"]) : null,
                                oVenta = esGastoVenta ? new Venta()
                                {
                                    id_venta = Convert.ToInt32(dr["id_venta"]),
                                    oCliente = new Cliente() // Cliente es una sub-entidad de Venta
                                    {
                                        nombre_completo = dr["NombreCliente"].ToString(),
                                        ci_nit = dr["CiNitCliente"].ToString()
                                    }
                                } : null,

                                // Asignar InfoAsociacion para fácil visualización
                                InfoAsociacion = esGastoVehiculo ? "Vehículo - " + dr["Placa"].ToString() :
                                                 esGastoVenta ? "Venta - Cliente: " + dr["NombreCliente"].ToString() : "General"
                            };

                            lista.Add(nuevoGasto);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de errores simplificado
                lista = new List<Gasto>();
                // Idealmente, se registra el error o se lanza una excepción más controlada
            }
            return lista;
        }

        public int RegistrarMultiples(int idAsociacion, string tipoAsociacion, List<DetalleGasto> listaDetalle, int idUsuarioAuditoria, out string Mensaje)
        {
            int gastosInsertados = 0;
            Mensaje = string.Empty;

            try
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle.Columns.Add("descripcion", typeof(string));
                dtDetalle.Columns.Add("monto", typeof(decimal));
                dtDetalle.Columns.Add("id_tipo_gasto", typeof(int));

                foreach (DetalleGasto dg in listaDetalle)
                {
                    dtDetalle.Rows.Add(dg.descripcion, dg.monto, dg.id_tipo_gasto);
                }

                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.RegistrarGastos", oConexion);

                    if (tipoAsociacion == "VEHICULO")
                    {
                        cmd.Parameters.AddWithValue("@IdVehiculo", idAsociacion);
                        cmd.Parameters.AddWithValue("@IdVenta", DBNull.Value);
                    }
                    else if (tipoAsociacion == "VENTA")
                    {
                        cmd.Parameters.AddWithValue("@IdVehiculo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdVenta", idAsociacion);
                    }

                    SqlParameter paramDetalle = new SqlParameter();
                    paramDetalle.ParameterName = "@DetalleGastos";
                    paramDetalle.SqlDbType = SqlDbType.Structured; 
                    paramDetalle.TypeName = "comercial.DetalleGastoTipo";
                    paramDetalle.Value = dtDetalle;
                    cmd.Parameters.Add(paramDetalle);

                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    gastosInsertados = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                gastosInsertados = 0;
                Mensaje = "Error en la capa de datos al registrar gastos: " + ex.Message;
            }
            return gastosInsertados;
        }

        public bool Eliminar(int idGasto, int idUsuarioAuditoria, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("comercial.CRUD_GASTO", oConexion);

                    cmd.Parameters.AddWithValue("@Operacion", "DELETE");
                    cmd.Parameters.AddWithValue("@IdGasto", idGasto);
                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

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
                Mensaje = "Error DB al eliminar el gasto: " + ex.Message;
            }
            return resultado;
        }
    }
}