using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_Registration
    {
        public int InsertBusiness(string businessName, string phone, string email, string address, 
            string city, string country, string taxNumber)
        {
            string query = @"INSERT INTO Business (business_name, phone, email, address, city, country, tax_number, status, created_date, updated_date)
                           OUTPUT INSERTED.business_id
                           VALUES (@business_name, @phone, @email, @address, @city, @country, @tax_number, 'A', GETDATE(), GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@business_name", string.IsNullOrWhiteSpace(businessName) ? (object)DBNull.Value : businessName),
                new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                new SqlParameter("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                new SqlParameter("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                new SqlParameter("@city", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city),
                new SqlParameter("@country", string.IsNullOrWhiteSpace(country) ? "Sri Lanka" : country),
                new SqlParameter("@tax_number", string.IsNullOrWhiteSpace(taxNumber) ? (object)DBNull.Value : taxNumber)
            };

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public int InsertStore(string storeName, string managerName, string phone, string email, 
            string address, string city, string state, string country, string postalCode)
        {
            string query = @"INSERT INTO Store (store_name, manager_name, phone, email, address, city, state, country, postal_code, status, created_date, updated_date)
                           OUTPUT INSERTED.store_id
                           VALUES (@store_name, @manager_name, @phone, @email, @address, @city, @state, @country, @postal_code, 'A', GETDATE(), GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@store_name", storeName),
                new SqlParameter("@manager_name", string.IsNullOrWhiteSpace(managerName) ? (object)DBNull.Value : managerName),
                new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                new SqlParameter("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                new SqlParameter("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                new SqlParameter("@city", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city),
                new SqlParameter("@state", string.IsNullOrWhiteSpace(state) ? (object)DBNull.Value : state),
                new SqlParameter("@country", string.IsNullOrWhiteSpace(country) ? (object)DBNull.Value : country),
                new SqlParameter("@postal_code", string.IsNullOrWhiteSpace(postalCode) ? (object)DBNull.Value : postalCode)
            };

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public int InsertUser(int storeId, string fullName, string username, string email, string phone, 
            string passwordHash, string pinCode)
        {
            string query = @"INSERT INTO [User] (store_id, role_id, full_name, username, email, phone, password_hash, pin_code, is_super_admin, status, created_date, updated_date)
                           OUTPUT INSERTED.user_id
                           VALUES (@store_id, 2, @full_name, @username, @email, @phone, @password_hash, @pin_code, 1, 'A', GETDATE(), GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@store_id", storeId),
                new SqlParameter("@full_name", fullName),
                new SqlParameter("@username", username),
                new SqlParameter("@email", email),
                new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                new SqlParameter("@password_hash", passwordHash),
                new SqlParameter("@pin_code", string.IsNullOrWhiteSpace(pinCode) ? (object)DBNull.Value : pinCode)
            };

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public bool CheckUsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE username = @username";
            SqlParameter[] parameters = { new SqlParameter("@username", username) };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            return result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0][0]) > 0;
        }

        public bool CheckEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE email = @email";
            SqlParameter[] parameters = { new SqlParameter("@email", email) };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            return result.Rows.Count > 0 && Convert.ToInt32(result.Rows[0][0]) > 0;
        }
    }
}
