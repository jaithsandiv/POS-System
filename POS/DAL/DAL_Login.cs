using POS.DAL.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_Login
    {
        public DataTable GetUserByUsername(string username)
        {
            DAL_DS_Initialize ds = new DAL_DS_Initialize();
            DataTable dt = ds.User;
            dt.Clear();

            string query = @"
                SELECT 
                    user_id,
                    store_id,
                    role_id,
                    full_name,
                    username,
                    email,
                    phone,
                    password_hash,
                    pin_code,
                    is_super_admin,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM [User]
                WHERE username = @username AND status = 'A'";

            SqlParameter[] parameters = { new SqlParameter("@username", username) };
            DataTable result = Connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["user_id"] = row["user_id"]?.ToString();
                r["store_id"] = row["store_id"]?.ToString();
                r["role_id"] = row["role_id"]?.ToString();
                r["full_name"] = row["full_name"]?.ToString();
                r["username"] = row["username"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["password_hash"] = row["password_hash"]?.ToString();
                r["pin_code"] = row["pin_code"]?.ToString();
                r["is_super_admin"] = row["is_super_admin"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }
    }
}
