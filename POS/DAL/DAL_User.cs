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

            SqlParameter[] parameters = [new SqlParameter("@userId", userId)];
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

            SqlParameter[] parameters = [
                new("@userId", userId),
                new("@updatedBy", updatedBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Searches users by keyword in multiple fields
        /// </summary>
        public DataTable SearchUsers(string searchKeyword)
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
                    AND (
                        u.full_name LIKE @searchKeyword 
                        OR u.username LIKE @searchKeyword
                        OR u.email LIKE @searchKeyword
                        OR u.phone LIKE @searchKeyword
                        OR s.store_name LIKE @searchKeyword
                        OR r.role_name LIKE @searchKeyword
                    )
                ORDER BY u.user_id ASC";

            SqlParameter[] parameters = [
                new("@searchKeyword", "%" + searchKeyword + "%")
            ];

            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Inserts a new user
        /// </summary>
        public int InsertUser(int storeId, int? roleId, string fullName, string username, string email, 
                             string phone, string passwordHash, string pinCode, bool isSuperAdmin, int createdBy)
        {
            string query = @"
                INSERT INTO [User] (store_id, role_id, full_name, username, email, phone, password_hash, pin_code, is_super_admin, status, created_by)
                VALUES (@storeId, @roleId, @fullName, @username, @email, @phone, @passwordHash, @pinCode, @isSuperAdmin, 'A', @createdBy);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = [
                new("@storeId", storeId),
                new("@roleId", roleId.HasValue ? (object)roleId.Value : DBNull.Value),
                new("@fullName", fullName),
                new("@username", username),
                new("@email", email),
                new("@phone", phone ?? (object)DBNull.Value),
                new("@passwordHash", passwordHash),
                new("@pinCode", pinCode ?? (object)DBNull.Value),
                new("@isSuperAdmin", isSuperAdmin),
                new("@createdBy", createdBy)
            ];

            object result = Connection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        public bool UpdateUser(int userId, int storeId, int? roleId, string fullName, string username, 
                              string email, string phone, string pinCode, int updatedBy)
        {
            string query = @"
                UPDATE [User]
                SET store_id = @storeId,
                    role_id = @roleId,
                    full_name = @fullName,
                    username = @username,
                    email = @email,
                    phone = @phone,
                    pin_code = @pinCode,
                    updated_by = @updatedBy,
                    updated_date = GETDATE()
                WHERE user_id = @userId";

            SqlParameter[] parameters = [
                new("@userId", userId),
                new("@storeId", storeId),
                new("@roleId", roleId.HasValue ? (object)roleId.Value : DBNull.Value),
                new("@fullName", fullName),
                new("@username", username),
                new("@email", email),
                new("@phone", phone ?? (object)DBNull.Value),
                new("@pinCode", pinCode ?? (object)DBNull.Value),
                new("@updatedBy", updatedBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Updates user password
        /// </summary>
        public bool UpdateUserPassword(int userId, string passwordHash, int updatedBy)
        {
            string query = @"
                UPDATE [User]
                SET password_hash = @passwordHash,
                    updated_by = @updatedBy,
                    updated_date = GETDATE()
                WHERE user_id = @userId";

            SqlParameter[] parameters = [
                new("@userId", userId),
                new("@passwordHash", passwordHash),
                new("@updatedBy", updatedBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets user password hash for verification
        /// </summary>
        public string GetUserPasswordHash(int userId)
        {
            string query = @"
                SELECT password_hash
                FROM [User]
                WHERE user_id = @userId AND status = 'A'";

            SqlParameter[] parameters = [new SqlParameter("@userId", userId)];
            DataTable result = Connection.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["password_hash"]?.ToString();
            }

            return null;
        }

        /// <summary>
        /// Updates user PIN code
        /// </summary>
        public bool UpdateUserPin(int userId, string pinCode, int updatedBy)
        {
            string query = @"
                UPDATE [User]
                SET pin_code = @pinCode,
                    updated_by = @updatedBy,
                    updated_date = GETDATE()
                WHERE user_id = @userId";

            SqlParameter[] parameters = [
                new("@userId", userId),
                new("@pinCode", pinCode ?? (object)DBNull.Value),
                new("@updatedBy", updatedBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}
