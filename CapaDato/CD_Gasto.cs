using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDato
{
    public class CD_Gasto
    {
        public List<Gasto> Listar()
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

                                // Mapeo simple de Vehículo (solo modelo y año para display)
                                id_vehiculo = esGastoVehiculo ? (int?)Convert.ToInt32(dr["id_vehiculo"]) : null,
                                oVehiculo = esGastoVehiculo ? new Vehiculo()
                                {
                                    modelo = dr["ModeloVehiculo"].ToString(),
                                    año = dr["AnioVehiculo"].ToString(),
                                    placa = dr["Placa"].ToString(),
                                } : null,

                                // Mapeo simple de Venta (solo ID)
                                id_venta = dr["id_venta"] != DBNull.Value ? (int?)Convert.ToInt32(dr["id_venta"]) : null,
                                oVenta = null // Venta no necesita sub-objeto si solo usamos id_venta
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                // Dejamos lista vacía, pero si el problema persiste, es la conexión o el SP.
                lista = new List<Gasto>();
                throw new Exception("Fallo en CD_Gasto.Listar: " + mensajeError);
            }
            return lista;
        }

        public int RegistrarMultiples(int idAsociacion, string tipoAsociacion, List<DetalleGasto> listaDetalle, int idUsuarioAuditoria, out string Mensaje)
        {
            int gastosInsertados = 0;
            Mensaje = string.Empty;

            try
            {
                // Paso 1: Convertir la lista C# a un DataTable para el parámetro de tipo tabla SQL
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

                    // Paso 2: Configurar parámetros de asociación mutua
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
                    // Si la CN validó correctamente, esto siempre será 'VEHICULO' o 'VENTA'.

                    // Paso 3: Configurar el parámetro de tipo tabla READONLY
                    SqlParameter paramDetalle = new SqlParameter();
                    paramDetalle.ParameterName = "@DetalleGastos";
                    paramDetalle.SqlDbType = SqlDbType.Structured; // Clave para tipos de tabla
                    paramDetalle.TypeName = "comercial.DetalleGastoTipo";
                    paramDetalle.Value = dtDetalle;
                    cmd.Parameters.Add(paramDetalle);

                    cmd.Parameters.AddWithValue("@IdUsuarioAuditoria", idUsuarioAuditoria);

                    // Paso 4: Configurar Output y ejecución
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

        // CÓDIGO DE Eliminar (Sin cambios)
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