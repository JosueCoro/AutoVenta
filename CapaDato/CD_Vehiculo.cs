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
            catch (Exception ex)
            {
                lista = new List<Vehiculo>();
                // Manejo de excepción
            }
            return lista;
        }

        // Método para registrar un nuevo Vehículo
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

        // Método para editar un Vehículo
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

        // Método para eliminar un Vehículo
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
    }
}
