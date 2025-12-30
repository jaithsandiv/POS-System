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

        /// <summary>
        /// Inserts a new role
        /// </summary>
        public int InsertRole(string roleName, string description, int createdBy)
        {
            string query = @"
                INSERT INTO Role (role_name, description, status, created_by, created_date)
                VALUES (@roleName, @description, 'A', @createdBy, GETDATE());
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = [
                new("@roleName", roleName),
                new("@description", description ?? (object)DBNull.Value),
                new("@createdBy", createdBy)
            ];

            object result = Connection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Updates an existing role
        /// </summary>
        public bool UpdateRole(int roleId, string roleName, string description, int updatedBy)
        {
            string query = @"
                UPDATE Role
                SET role_name = @roleName,
                    description = @description,
                    updated_by = @updatedBy,
                    updated_date = GETDATE()
                WHERE role_id = @roleId";

            SqlParameter[] parameters = [
                new("@roleId", roleId),
                new("@roleName", roleName),
                new("@description", description ?? (object)DBNull.Value),
                new("@updatedBy", updatedBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Soft deletes a role (sets status to 'I')
        /// </summary>
        public bool DeleteRole(int roleId, int updatedBy)
        {
            string query = @"
                UPDATE Role
                SET status = 'I', updated_by = @updatedBy, updated_date = GETDATE()
                WHERE role_id = @roleId";

            SqlParameter[] parameters = [
                new("@roleId", roleId),
                new("@updatedBy", updatedBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Gets all permissions for a specific role
        /// </summary>
        public DataTable GetRolePermissions(int roleId)
        {
            string query = @"
                SELECT 
                    role_id,
                    permission_code,
                    status
                FROM RolePermission
                WHERE role_id = @roleId AND status = 'A'";

            SqlParameter[] parameters = [new SqlParameter("@roleId", roleId)];
            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Inserts a permission for a role
        /// </summary>
        public bool InsertRolePermission(int roleId, string permissionCode, int createdBy)
        {
            string query = @"
                IF NOT EXISTS (SELECT 1 FROM RolePermission WHERE role_id = @roleId AND permission_code = @permissionCode)
                BEGIN
                    INSERT INTO RolePermission (role_id, permission_code, status, created_by, created_date)
                    VALUES (@roleId, @permissionCode, 'A', @createdBy, GETDATE());
                END";

            SqlParameter[] parameters = [
                new("@roleId", roleId),
                new("@permissionCode", permissionCode),
                new("@createdBy", createdBy)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes all permissions for a role (used before re-inserting during updates)
        /// </summary>
        public bool DeleteAllRolePermissions(int roleId)
        {
            string query = @"
                DELETE FROM RolePermission
                WHERE role_id = @roleId";

            SqlParameter[] parameters = [new SqlParameter("@roleId", roleId)];
            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return true; // Returns true even if no rows deleted (role might have no permissions)
        }

        /// <summary>
        /// Deletes a specific permission for a role
        /// </summary>
        public bool DeleteRolePermission(int roleId, string permissionCode)
        {
            string query = @"
                DELETE FROM RolePermission
                WHERE role_id = @roleId AND permission_code = @permissionCode";

            SqlParameter[] parameters = [
                new("@roleId", roleId),
                new("@permissionCode", permissionCode)
            ];

            int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
    }
}
