using System;
using POS.DAL;

namespace POS.BLL
{
    internal class BLL_Registration
    {
        private readonly DAL_Registration _dalRegistration = new DAL_Registration();

        public bool RegisterComplete(
            // Business data
            string businessName, string businessPhone, string businessEmail, string businessAddress,
            string businessCity, string businessCountry, string businessTaxNo,
            // Store data
            string storeName, string managerName, string storePhone, string storeEmail,
            string storeAddress, string storeCity, string storeState, string storeCountry, string storePostalCode,
            // User data
            string fullName, string username, string email, string phone, string password, string pinCode,
            out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Check if username already exists
                if (_dalRegistration.CheckUsernameExists(username))
                {
                    errorMessage = "Username already exists.";
                    return false;
                }

                // Check if email already exists
                if (_dalRegistration.CheckEmailExists(email))
                {
                    errorMessage = "Email already exists.";
                    return false;
                }

                // Hash the password
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                // Insert Business
                int businessId = _dalRegistration.InsertBusiness(
                    businessName, businessPhone, businessEmail, businessAddress,
                    businessCity, businessCountry, businessTaxNo);

                if (businessId == 0)
                {
                    errorMessage = "Failed to create business record.";
                    return false;
                }

                // Insert Store
                int storeId = _dalRegistration.InsertStore(
                    storeName, managerName, storePhone, storeEmail,
                    storeAddress, storeCity, storeState, storeCountry, storePostalCode);

                if (storeId == 0)
                {
                    errorMessage = "Failed to create store record.";
                    return false;
                }

                // Insert User
                int userId = _dalRegistration.InsertUser(
                    storeId, fullName, username, email, phone, passwordHash, pinCode);

                if (userId == 0)
                {
                    errorMessage = "Failed to create user record.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Registration failed: {ex.Message}";
                return false;
            }
        }
    }
}
