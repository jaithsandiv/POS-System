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
                    UPDATE SystemSetting
                    SET setting_value = @setting_value,
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE setting_key = @setting_key";

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
    }
}
