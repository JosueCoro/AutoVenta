using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using CapaEntidad;

namespace CapaDato
{
    public class CD_Cliente
    {

        //listar Cliente
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            
            try
            {
                using (MySqlConnection cn = new MySqlConnection(Conexion.cn))
                {
                    string query = "SELECT id_cliente, nombre_completo, ci_nit, telefono, direccion FROM cliente";
                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    using (MySqlDataReader dr=cmd.ExecuteReader() ) {
                        while (dr.Read()) { 
                            lista.Add(
                                new Cliente()
                                {
                                    id_cliente = Convert.ToInt32(dr["id_cliente"]),
                                    nombre_completo = dr["nombre_completo"].ToString(),
                                    ci_nit = dr["ci_nit"].ToString(),
                                    telefono = dr["telefono"].ToString(),
                                    direccion = dr["direccion"].ToString()
                                }
                                );
                        }

                    }
                }
                
            }
            catch
            {
                lista = new List<Cliente>();
            }
            return lista;
        }
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection cn = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("RegistrarCliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("pNombreCompleto", obj.nombre_completo);
                    cmd.Parameters.AddWithValue("pCiNit", obj.ci_nit);
                    cmd.Parameters.AddWithValue("pTelefono", obj.telefono);
                    cmd.Parameters.AddWithValue("pDireccion", obj.direccion);

                    cmd.Parameters.Add("pMensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("pResultado", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["pResultado"].Value);
                    Mensaje = cmd.Parameters["pMensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (MySqlConnection cn = new MySqlConnection(Conexion.cn))
                {
                    MySqlCommand cmd = new MySqlCommand("EditarCliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("pIdCliente", obj.id_cliente);
                    cmd.Parameters.AddWithValue("pNombreCompleto", obj.nombre_completo);
                    cmd.Parameters.AddWithValue("pCiNit", obj.ci_nit);
                    cmd.Parameters.AddWithValue("pTelefono", obj.telefono);
                    cmd.Parameters.AddWithValue("pDireccion", obj.direccion);

                    cmd.Parameters.Add("pMensaje", MySqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("pResultado", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);
                    Mensaje = cmd.Parameters["pMensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }


    }
}
