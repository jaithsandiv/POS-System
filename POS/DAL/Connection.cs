using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal static class Connection
    {
        private static readonly string _connectionString;

        static Connection()
        {
            // Retrieve the connection string from App.config
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["POSConnectionString"].ConnectionString;
        }

        public static SqlConnection GetConnection()
        {   
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        return result;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}