using POS.DAL.DataSource;
using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_Registration
    {
        public bool RegisterComplete(
            DAL_DS_Initialize.BusinessRow businessRow,
            DAL_DS_Initialize.StoreRow storeRow,
            DAL_DS_Initialize.UserRow userRow,
            out string errorMessage)
        {
            errorMessage = string.Empty;
            SqlConnection connection = null;
            SqlTransaction transaction = null;

            try
            {
                connection = Connection.GetConnection();
                transaction = connection.BeginTransaction();

                // Insert Business with automatic 3-day trial
                string businessQuery = @"
                    INSERT INTO Business (business_name, logo, trial_start_date, trial_end_date, is_licensed, status, created_by, created_date)
                    OUTPUT INSERTED.business_id
                    VALUES (@business_name, @logo, GETDATE(), DATEADD(DAY, 3, GETDATE()), 0, 'A', NULL, GETDATE())";

                SqlParameter[] businessParams = {
                    new SqlParameter("@business_name", businessRow.business_name),
                    new SqlParameter("@logo", businessRow.IslogoNull() ? (object)DBNull.Value : businessRow.logo)
                };

                int businessId;
                using (SqlCommand cmd = new SqlCommand(businessQuery, connection, transaction))
                {
                    cmd.Parameters.AddRange(businessParams);
                    businessId = (int)cmd.ExecuteScalar();
                }

                // Insert Store
                string storeQuery = @"
                    INSERT INTO Store (store_name, phone, email, address, city, state, country, postal_code, status, created_by, created_date)
                    OUTPUT INSERTED.store_id
                    VALUES (@store_name, @phone, @email, @address, @city, @state, @country, @postal_code, 'A', NULL, GETDATE())";

                SqlParameter[] storeParams = {
                    new SqlParameter("@store_name", storeRow.store_name),
                    new SqlParameter("@phone", string.IsNullOrWhiteSpace(storeRow.phone) ? (object)DBNull.Value : storeRow.phone),
                    new SqlParameter("@email", string.IsNullOrWhiteSpace(storeRow.email) ? (object)DBNull.Value : storeRow.email),
                    new SqlParameter("@address", string.IsNullOrWhiteSpace(storeRow.address) ? (object)DBNull.Value : storeRow.address),
                    new SqlParameter("@city", string.IsNullOrWhiteSpace(storeRow.city) ? (object)DBNull.Value : storeRow.city),
                    new SqlParameter("@state", string.IsNullOrWhiteSpace(storeRow.state) ? (object)DBNull.Value : storeRow.state),
                    new SqlParameter("@country", string.IsNullOrWhiteSpace(storeRow.country) ? (object)DBNull.Value : storeRow.country),
                    new SqlParameter("@postal_code", string.IsNullOrWhiteSpace(storeRow.postal_code) ? (object)DBNull.Value : storeRow.postal_code)
                };

                int storeId;
                using (SqlCommand cmd = new SqlCommand(storeQuery, connection, transaction))
                {
                    cmd.Parameters.AddRange(storeParams);
                    storeId = (int)cmd.ExecuteScalar();
                }

                // Insert User
                string userQuery = @"
                    INSERT INTO [User] (store_id, role_id, full_name, username, email, phone, password_hash, pin_code, is_super_admin, status, created_by, created_date)
                    VALUES (@store_id, @role_id, @full_name, @username, @email, @phone, @password_hash, @pin_code, 0, 'A', NULL, GETDATE())";

                SqlParameter[] userParams = {
                    new SqlParameter("@store_id", storeId),
                    new SqlParameter("@role_id", 1), // Default role_id = 1
                    new SqlParameter("@full_name", userRow.full_name),
                    new SqlParameter("@username", userRow.username),
                    new SqlParameter("@email", userRow.email),
                    new SqlParameter("@phone", string.IsNullOrWhiteSpace(userRow.phone) ? (object)DBNull.Value : userRow.phone),
                    new SqlParameter("@password_hash", userRow.password_hash),
                    new SqlParameter("@pin_code", string.IsNullOrWhiteSpace(userRow.pin_code) ? (object)DBNull.Value : userRow.pin_code)
                };

                using (SqlCommand cmd = new SqlCommand(userQuery, connection, transaction))
                {
                    cmd.Parameters.AddRange(userParams);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch { }
                }

                errorMessage = ex.Message;
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
