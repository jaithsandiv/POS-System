using System;
using System.Data;
using POS.DAL;

namespace POS.BLL
{
    internal class BLL_User
    {
        private readonly DAL_User _dalUser = new DAL_User();

        /// <summary>
        /// Gets all active users
        /// </summary>
        public DataTable GetUsers()
        {
            return _dalUser.GetUsers();
        }

        /// <summary>
        /// Gets a specific user by ID
        /// </summary>
        public DataTable GetUserById(int userId)
        {
            return _dalUser.GetUserById(userId);
        }

        /// <summary>
        /// Soft deletes a user
        /// </summary>
        public bool DeleteUser(int userId, int updatedBy)
        {
            return _dalUser.DeleteUser(userId, updatedBy);
        }
    }
}
