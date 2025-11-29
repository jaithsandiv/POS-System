using System;
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
    }
}
