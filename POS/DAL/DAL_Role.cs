using POS.DAL.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_Role
    {
        /// <summary>
        /// Gets all active roles
        /// </summary>
        public DataTable GetRoles()
        {
            string query = @"
                SELECT 
                    role_id,
                    role_name,
                    description,
                    status
                FROM Role
                WHERE status = 'A'
                ORDER BY role_name ASC";

            return Connection.ExecuteQuery(query);
        }

        /// <summary>
        /// Gets a specific role by ID
        /// </summary>
        public DataTable GetRoleById(int roleId)
        {
            string query = @"
                SELECT 
                    role_id,
                    role_name,
                    description,
                    status
                FROM Role
                WHERE role_id = @roleId AND status = 'A'";

            SqlParameter[] parameters = [new SqlParameter("@roleId", roleId)];
            return Connection.ExecuteQuery(query, parameters);
        }
    }
}
