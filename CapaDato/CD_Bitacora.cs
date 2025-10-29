using CapaDato;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Bitacora
    {
        public List<Bitacora> Consultar(DateTime fechaInicio, DateTime fechaFin, int idUsuario)
        {
            List<Bitacora> lista = new List<Bitacora>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("administracion.CONSULTAR_BITACORA", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Bitacora()
                            {
                                id_bitacora = Convert.ToInt32(dr["id_bitacora"]),
                                accion = dr["accion"].ToString(),
                                fecha = Convert.ToDateTime(dr["fecha"]),
                                hora = TimeSpan.Parse(dr["hora"].ToString()),
                                id_usuario = Convert.ToInt32(dr["id_usuario"]),
                                oUsuario = new Usuario() 
                                {
                                    nombre = dr["NombreUsuario"].ToString(),
                                    apellido = dr["ApellidoUsuario"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Bitacora>();
            }
            return lista;
        }
    }
}