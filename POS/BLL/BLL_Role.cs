using System;
using System.Collections.Generic;
using System.Data;
using POS.DAL;

namespace POS.BLL
{
    internal class BLL_Role
    {
        private static readonly DAL_Role _dalRole = new();

        /// <summary>
        /// Gets all active roles
        /// </summary>
        public static DataTable GetRoles()
        {
            return _dalRole.GetRoles();
        }

        /// <summary>
        /// Gets a specific role by ID
        /// </summary>
        public static DataTable GetRoleById(int roleId)
        {
            return _dalRole.GetRoleById(roleId);
        }

        /// <summary>
        /// Inserts a new role with permissions
        /// </summary>
        public static int InsertRole(string roleName, string description, List<string> permissionCodes, int createdBy)
        {
            // Insert the role
            int roleId = _dalRole.InsertRole(roleName, description, createdBy);

            if (roleId > 0 && permissionCodes != null && permissionCodes.Count > 0)
            {
                // Insert permissions
                foreach (string permissionCode in permissionCodes)
                {
                    _dalRole.InsertRolePermission(roleId, permissionCode, createdBy);
                }
            }

            return roleId;
        }

        /// <summary>
        /// Updates an existing role with permissions
        /// </summary>
        public static bool UpdateRole(int roleId, string roleName, string description, List<string> permissionCodes, int updatedBy)
        {
            // Update the role
            bool roleUpdated = _dalRole.UpdateRole(roleId, roleName, description, updatedBy);

            if (roleUpdated)
            {
                // Delete all existing permissions
                _dalRole.DeleteAllRolePermissions(roleId);

                // Insert new permissions
                if (permissionCodes != null && permissionCodes.Count > 0)
                {
                    foreach (string permissionCode in permissionCodes)
                    {
                        _dalRole.InsertRolePermission(roleId, permissionCode, updatedBy);
                    }
                }
            }

            return roleUpdated;
        }

        /// <summary>
        /// Deletes a role (soft delete)
        /// </summary>
        public static bool DeleteRole(int roleId, int updatedBy)
        {
            return _dalRole.DeleteRole(roleId, updatedBy);
        }

        /// <summary>
        /// Gets all permissions for a specific role
        /// </summary>
        public static DataTable GetRolePermissions(int roleId)
        {
            return _dalRole.GetRolePermissions(roleId);
        }
    }
}
