using POS.DAL.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_User
    {
        /// <summary>
        /// Gets all active users with their store and role information
        /// </summary>
        public DataTable GetUsers()
        {
            string query = @"
                SELECT 
                    u.user_id,
                    u.store_id,
                    s.store_name,
                    u.role_id,
                    r.role_name,
                    u.full_name,
                    u.username,
                    u.email,
                    u.phone,
                    u.is_super_admin,
                    u.status
                FROM [User] u
                LEFT JOIN Store s ON u.store_id = s.store_id
                LEFT JOIN Role r ON u.role_id = r.role_id
                WHERE u.status = 'A'
                ORDER BY u.user_id ASC";

            return Connection.ExecuteQuery(query);
        }

        /// <summary>
        /// Gets a specific user by ID
        /// </summary>
        public DataTable GetUserById(int userId)
        {
            string query = @"
                SELECT 
                    u.user_id,
                    u.store_id,
                    s.store_name,
                    u.role_id,
                    r.role_name,
                    u.full_name,
                    u.username,
                    u.email,
                    u.phone,
                    u.pin_code,
                    u.is_super_admin,
                    u.status
                FROM [User] u
                LEFT JOIN Store s ON u.store_id = s.store_id
                LEFT JOIN Role r ON u.role_id = r.role_id
                WHERE u.user_id = @userId AND u.status = 'A'";

            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Soft deletes a user (sets status to 'I')
        /// </summary>
        public bool DeleteUser(int userId, int updatedBy)
        {
            string query = @"
                UPDATE [User]
                SET status = 'I', updated_by = @updatedBy, updated_date = GETDATE()
                WHERE user_id = @userId";

            SqlParameter[] parameters = {
                new SqlParameter("@userId", userId),
                new SqlParameter("@updatedBy", updatedBy)
            };

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}
