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
    internal class DAL_Contacts
    {
        public DAL_Contacts()
        {
        }

        /// <summary>
        /// Gets all active CustomerGroups from the database
        /// </summary>
        public DataTable GetCustomerGroups()
        {
            DAL_DS_Contacts ds = new DAL_DS_Contacts();
            DataTable dt = ds.CustomerGroup;
            dt.Clear();

            string query = @"
                SELECT 
                    group_id,
                    group_name,
                    discount_percent,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM CustomerGroup
                WHERE status = 'A'
                ORDER BY group_name";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["group_id"] = row["group_id"]?.ToString();
                r["group_name"] = row["group_name"]?.ToString();
                r["discount_percent"] = row["discount_percent"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        /// <summary>
        /// Gets all active Suppliers from the database
        /// </summary>
        public DataTable GetSuppliers()
        {
            DAL_DS_Contacts ds = new DAL_DS_Contacts();
            DataTable dt = ds.Supplier;
            dt.Clear();

            string query = @"
                SELECT 
                    supplier_id,
                    supplier_name,
                    company_name,
                    email,
                    phone,
                    address,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM Supplier
                WHERE status = 'A'
                ORDER BY supplier_name";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["supplier_id"] = row["supplier_id"]?.ToString();
                r["supplier_name"] = row["supplier_name"]?.ToString();
                r["company_name"] = row["company_name"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        /// <summary>
        /// Gets a specific CustomerGroup by ID
        /// </summary>
        public DataTable GetCustomerGroupById(int groupId)
        {
            DAL_DS_Contacts ds = new DAL_DS_Contacts();
            DataTable dt = ds.CustomerGroup;
            dt.Clear();

            string query = @"
                SELECT 
                    group_id,
                    group_name,
                    discount_percent,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM CustomerGroup
                WHERE group_id = @group_id AND status = 'A'";

            var parameters = new SqlParameter[] { new SqlParameter("@group_id", groupId) };
            DataTable result = Connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["group_id"] = row["group_id"]?.ToString();
                r["group_name"] = row["group_name"]?.ToString();
                r["discount_percent"] = row["discount_percent"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        /// <summary>
        /// Gets a specific Supplier by ID
        /// </summary>
        public DataTable GetSupplierById(int supplierId)
        {
            DAL_DS_Contacts ds = new DAL_DS_Contacts();
            DataTable dt = ds.Supplier;
            dt.Clear();

            string query = @"
                SELECT 
                    supplier_id,
                    supplier_name,
                    company_name,
                    email,
                    phone,
                    address,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM Supplier
                WHERE supplier_id = @supplier_id AND status = 'A'";

            var parameters = new SqlParameter[] { new SqlParameter("@supplier_id", supplierId) };
            DataTable result = Connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["supplier_id"] = row["supplier_id"]?.ToString();
                r["supplier_name"] = row["supplier_name"]?.ToString();
                r["company_name"] = row["company_name"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        /// <summary>
        /// Inserts a new CustomerGroup into the database
        /// </summary>
        public int InsertCustomerGroup(string groupName, decimal discountPercent, int createdBy)
        {
            try
            {
                string query = @"
                    INSERT INTO CustomerGroup (
                        group_name, discount_percent, status, created_by, created_date
                    )
                    VALUES (
                        @group_name, @discount_percent, 'A', @created_by, GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@group_name", groupName),
                    new SqlParameter("@discount_percent", discountPercent),
                    new SqlParameter("@created_by", createdBy)
                };

                DataTable result = Connection.ExecuteQuery(query, parameters);
                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0][0]);
                }

                throw new Exception("Failed to insert CustomerGroup.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting CustomerGroup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Inserts a new Supplier into the database
        /// </summary>
        public int InsertSupplier(string supplierName, string companyName, string email, 
                                   string phone, string address, int createdBy)
        {
            try
            {
                string query = @"
                    INSERT INTO Supplier (
                        supplier_name, company_name, email, phone, address, 
                        status, created_by, created_date
                    )
                    VALUES (
                        @supplier_name, @company_name, @email, @phone, @address,
                        'A', @created_by, GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@supplier_name", supplierName),
                    new SqlParameter("@company_name", string.IsNullOrWhiteSpace(companyName) ? (object)DBNull.Value : companyName),
                    new SqlParameter("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                    new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                    new SqlParameter("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                    new SqlParameter("@created_by", createdBy)
                };

                DataTable result = Connection.ExecuteQuery(query, parameters);
                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0][0]);
                }

                throw new Exception("Failed to insert Supplier.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting Supplier: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing CustomerGroup
        /// </summary>
        public bool UpdateCustomerGroup(int groupId, string groupName, decimal discountPercent, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE CustomerGroup
                    SET 
                        group_name = @group_name,
                        discount_percent = @discount_percent,
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE group_id = @group_id AND status = 'A'";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@group_id", groupId),
                    new SqlParameter("@group_name", groupName),
                    new SqlParameter("@discount_percent", discountPercent),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating CustomerGroup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing Supplier
        /// </summary>
        public bool UpdateSupplier(int supplierId, string supplierName, string companyName, 
                                    string email, string phone, string address, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE Supplier
                    SET 
                        supplier_name = @supplier_name,
                        company_name = @company_name,
                        email = @email,
                        phone = @phone,
                        address = @address,
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE supplier_id = @supplier_id AND status = 'A'";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@supplier_id", supplierId),
                    new SqlParameter("@supplier_name", supplierName),
                    new SqlParameter("@company_name", string.IsNullOrWhiteSpace(companyName) ? (object)DBNull.Value : companyName),
                    new SqlParameter("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                    new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                    new SqlParameter("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Supplier: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Soft deletes a CustomerGroup (sets status to 'I')
        /// </summary>
        public bool DeleteCustomerGroup(int groupId, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE CustomerGroup
                    SET 
                        status = 'I',
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE group_id = @group_id";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@group_id", groupId),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting CustomerGroup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Soft deletes a Supplier (sets status to 'I')
        /// </summary>
        public bool DeleteSupplier(int supplierId, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE Supplier
                    SET 
                        status = 'I',
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE supplier_id = @supplier_id";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@supplier_id", supplierId),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Supplier: {ex.Message}", ex);
            }
        }
    }
}
