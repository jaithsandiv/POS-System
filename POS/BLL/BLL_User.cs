using System;
using System.Data;
using POS.DAL;

namespace POS.BLL
{
    internal class BLL_User
    {
        private static readonly DAL_User _dalUser = new();

        /// <summary>
        /// Gets all active users
        /// </summary>
        public static DataTable GetUsers()
        {
            return _dalUser.GetUsers();
        }

        /// <summary>
        /// Searches users by keyword
        /// </summary>
        public static DataTable SearchUsers(string searchKeyword)
        {
            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                // If search is empty, return all users
                return GetUsers();
            }

            return _dalUser.SearchUsers(searchKeyword.Trim());
        }

        /// <summary>
        /// Gets a specific user by ID
        /// </summary>
        public static DataTable GetUserById(int userId)
        {
            return _dalUser.GetUserById(userId);
        }

        /// <summary>
        /// Soft deletes a user
        /// </summary>
        public static bool DeleteUser(int userId, int updatedBy)
        {
            return _dalUser.DeleteUser(userId, updatedBy);
        }

        /// <summary>
        /// Inserts a new user
        /// </summary>
        public static int InsertUser(int storeId, int? roleId, string fullName, string username, string email,
                                    string phone, string password, string pinCode, bool isSuperAdmin, int createdBy)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");
            if (storeId <= 0)
                throw new ArgumentException("Store is required.");

            // Hash the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return _dalUser.InsertUser(storeId, roleId, fullName, username, email, phone, 
                                      passwordHash, pinCode, isSuperAdmin, createdBy);
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        public static bool UpdateUser(int userId, int storeId, int? roleId, string fullName, string username,
                                     string email, string phone, string pinCode, int updatedBy)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");
            if (storeId <= 0)
                throw new ArgumentException("Store is required.");

            return _dalUser.UpdateUser(userId, storeId, roleId, fullName, username, email, phone, pinCode, updatedBy);
        }

        /// <summary>
        /// Updates user password
        /// </summary>
        public static bool UpdateUserPassword(int userId, string password, int updatedBy)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");

            // Hash the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return _dalUser.UpdateUserPassword(userId, passwordHash, updatedBy);
        }

        /// <summary>
        /// Verifies if the current password is correct
        /// </summary>
        public static bool VerifyCurrentPassword(int userId, string currentPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                return false;

            string passwordHash = _dalUser.GetUserPasswordHash(userId);
            
            if (string.IsNullOrEmpty(passwordHash))
                return false;

            return BCrypt.Net.BCrypt.Verify(currentPassword, passwordHash);
        }

        /// <summary>
        /// Updates user PIN code
        /// </summary>
        public static bool UpdateUserPin(int userId, string pinCode, int updatedBy)
        {
            // Validate PIN format (4 digits)
            if (!string.IsNullOrWhiteSpace(pinCode))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(pinCode, @"^\d{4}$"))
                    throw new ArgumentException("PIN must be exactly 4 digits.");
            }

            return _dalUser.UpdateUserPin(userId, pinCode, updatedBy);
        }

        /// <summary>
        /// Verifies if the entered PIN matches any super admin user's PIN
        /// </summary>
        public static bool VerifySuperAdminPin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
                return false;

            DataTable superAdmins = _dalUser.GetSuperAdminUsers();
            
            foreach (DataRow admin in superAdmins.Rows)
            {
                string adminPin = admin["pin_code"]?.ToString();
                if (!string.IsNullOrEmpty(adminPin) && adminPin == pin)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
