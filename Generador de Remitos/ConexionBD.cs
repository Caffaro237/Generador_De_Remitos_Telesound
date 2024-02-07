using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Ternium.Modelos.Core.Security;

namespace Generador_de_Remitos
{
    public class ConexionBD
    {

        private string connectionString;

        public ConexionBD(string db)
        {
            connectionString = GetConnectionString(db);
        }


        public static string GetConnectionString(string dbKey)
        {
            try
            {
                string connectionString;
                string password;

                if (ConfigurationManager.ConnectionStrings[dbKey].ToString() == null)
                {
                    throw new Exception("No se encontro el valor para ConnectionStrings");
                }

                connectionString = ConfigurationManager.ConnectionStrings[dbKey].ToString();

                password = SecurityManager.Decrypt(Utils.PasswordBD);

                connectionString = connectionString + "; User ID = " + Utils.UsuarioBD + "; Password = " + password;

                return connectionString;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public DataTable DataSelect(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = GetCommand(query, conn, CommandType.Text);
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                    adapter.Dispose();
                    return dt;
                }
                catch (SqlException sqlException)
                {
                    throw sqlException;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public DataTable DataTableFromView(string nombreView)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM " + nombreView;
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = GetCommand(query, conn, CommandType.Text);

                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                    adapter.Dispose();
                    return dt;
                }
                catch (SqlException sqlException)
                {
                    throw sqlException;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public DataTable DataTableFromSP(string procedimientoAlmacenado, params SqlParameter[] parametros)
        {
            DataTable table;

            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand command = GetCommand(procedimientoAlmacenado, connection, CommandType.StoredProcedure);
                    SqlParameter[] parameterArray = parametros;
                    int index = 0;

                    while (true)
                    {
                        if (index >= parameterArray.Length)
                        {
                            connection.Open();
                            DataTable tableAux = new DataTable();
                            tableAux.Load(command.ExecuteReader());
                            connection.Close();
                            table = tableAux;
                            break;
                        }

                        SqlParameter parameter = parameterArray[index];
                        command.Parameters.Add(parameter);
                        index++;
                    }
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar procedimiento almacenado ", ex);
            }

            return table;
        }

        public void EjecutarStoredProcedure(string procedimientoAlmacenado, params SqlParameter[] parametros)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    SqlCommand command = GetCommand(procedimientoAlmacenado, connection, CommandType.StoredProcedure);
                    SqlParameter[] parameterArray = parametros;
                    int index = 0;

                    while (true)
                    {
                        if (index >= parameterArray.Length)
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            break;
                        }

                        SqlParameter parameter = parameterArray[index];
                        command.Parameters.Add(parameter);
                        index++;
                    }
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar procedimiento almacenado ", ex);
            }
        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static SqlCommand GetCommand(string query, SqlConnection conn, CommandType commandType)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.Connection = conn;
            command.CommandType = commandType;
            command.CommandTimeout = Utils.ConnectionTimeout;
            return command;
        }
    }
}
