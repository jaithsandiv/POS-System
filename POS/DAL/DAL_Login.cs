using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_Login
    {
        public DataTable GetUserByUsername(string username)
        {
            string query = "SELECT * FROM [User] WHERE username = @username AND status = 'A'";
            SqlParameter[] parameters = { new SqlParameter("@username", username) };

            return Connection.ExecuteQuery(query, parameters);
        }
    }
}
