using POS.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL
{
    internal class DAL_SystemSettings
    {
        public DAL_SystemSettings()
        {
        }

        public DataTable GetSystemSettings()
        {
            DAL_DS_SystemSettings ds = new DAL_DS_SystemSettings();
            DataTable dt = ds.SystemSetting;
            dt.Clear();

            string query = @"
                SELECT 
                    setting_key,
                    setting_value,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM SystemSetting
                WHERE status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["setting_key"] = row["setting_key"]?.ToString();
                r["setting_value"] = row["setting_value"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        public bool UpdateSystemSetting(string key, string value, int updatedBy)
        {
            try
            {
                string query = @"
                    MERGE SystemSetting AS target
                    USING (SELECT @setting_key AS setting_key) AS source
                    ON (target.setting_key = source.setting_key)
                    WHEN MATCHED THEN
                        UPDATE SET 
                            setting_value = @setting_value,
                            updated_by = @updated_by,
                            updated_date = GETDATE()
                    WHEN NOT MATCHED THEN
                        INSERT (setting_key, setting_value, status, created_by, created_date, updated_by, updated_date)
                        VALUES (@setting_key, @setting_value, 'A', @updated_by, GETDATE(), @updated_by, GETDATE());";

                SqlParameter[] parameters = {
                    new SqlParameter("@setting_key", key),
                    new SqlParameter("@setting_value", value ?? (object)DBNull.Value),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating system setting '{key}': {ex.Message}", ex);
            }
        }

        public bool UpdateBusinessSettings(string businessName, byte[] logo, int updatedBy)
        {
            try
            {
                // We assume there is only one active business row, usually with ID 1
                // If not exists, we might need to insert, but usually it exists from initialization.
                // We'll update the first active record.

                string query = @"
                    UPDATE Business
                    SET business_name = @business_name,
                        logo = @logo,
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE status = 'A'"; 
                    // Ideally WHERE business_id = 1, but status='A' is safer if id changed.
                    // Assuming single tenant.

                SqlParameter[] parameters = {
                    new SqlParameter("@business_name", businessName),
                    new SqlParameter("@logo", logo ?? (object)DBNull.Value),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating business settings: {ex.Message}", ex);
            }
        }

        public bool UpdateStoreSettings(int storeId, string storeName, string phone, string email, string address, string city, string state, string country, string postalCode, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE Store
                    SET store_name = @store_name,
                        phone = @phone,
                        email = @email,
                        address = @address,
                        city = @city,
                        state = @state,
                        country = @country,
                        postal_code = @postal_code,
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE store_id = @store_id";

                SqlParameter[] parameters = {
                    new SqlParameter("@store_id", storeId),
                    new SqlParameter("@store_name", storeName),
                    new SqlParameter("@phone", phone ?? (object)DBNull.Value),
                    new SqlParameter("@email", email ?? (object)DBNull.Value),
                    new SqlParameter("@address", address ?? (object)DBNull.Value),
                    new SqlParameter("@city", city ?? (object)DBNull.Value),
                    new SqlParameter("@state", state ?? (object)DBNull.Value),
                    new SqlParameter("@country", country ?? (object)DBNull.Value),
                    new SqlParameter("@postal_code", postalCode ?? (object)DBNull.Value),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating store settings: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates license and trial period settings in the Business table
        /// </summary>
        public bool UpdateLicenseSettings(DateTime? trialStartDate, DateTime? trialEndDate, bool isLicensed, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE Business
                    SET trial_start_date = @trialStartDate,
                        trial_end_date = @trialEndDate,
                        is_licensed = @isLicensed,
                        updated_by = @updatedBy,
                        updated_date = GETDATE()
                    WHERE status = 'A'";

                SqlParameter[] parameters = {
                    new SqlParameter("@trialStartDate", trialStartDate ?? (object)DBNull.Value),
                    new SqlParameter("@trialEndDate", trialEndDate ?? (object)DBNull.Value),
                    new SqlParameter("@isLicensed", isLicensed),
                    new SqlParameter("@updatedBy", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating license settings: {ex.Message}", ex);
            }
        }
    }
}
