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
        /// Searches suppliers by keyword in multiple fields
        /// </summary>
        public DataTable SearchSuppliers(string searchKeyword)
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
                    AND (
                        supplier_name LIKE @searchKeyword 
                        OR company_name LIKE @searchKeyword
                        OR email LIKE @searchKeyword
                        OR phone LIKE @searchKeyword
                        OR address LIKE @searchKeyword
                    )
                ORDER BY supplier_name";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@searchKeyword", "%" + searchKeyword + "%")
            };

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

        /// <summary>
        /// Gets all active Customers from the database with their group information
        /// </summary>
        public DataTable GetCustomers()
        {
            DataTable dt = new DataTable("Customer");
            
            // Define columns
            dt.Columns.Add("customer_id", typeof(string));
            dt.Columns.Add("group_id", typeof(string));
            dt.Columns.Add("full_name", typeof(string));
            dt.Columns.Add("company_name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("address", typeof(string));
            dt.Columns.Add("city", typeof(string));
            dt.Columns.Add("state", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("postal_code", typeof(string));
            dt.Columns.Add("credit_limit", typeof(string));
            dt.Columns.Add("credit_balance", typeof(string));
            dt.Columns.Add("group_name", typeof(string));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("created_by", typeof(string));
            dt.Columns.Add("created_date", typeof(string));
            dt.Columns.Add("updated_by", typeof(string));
            dt.Columns.Add("updated_date", typeof(string));

            string query = @"
                SELECT 
                    c.customer_id,
                    c.group_id,
                    c.full_name,
                    c.company_name,
                    c.email,
                    c.phone,
                    c.address,
                    c.city,
                    c.state,
                    c.country,
                    c.postal_code,
                    c.credit_limit,
                    c.credit_balance,
                    cg.group_name,
                    c.status,
                    c.created_by,
                    CONVERT(varchar, c.created_date, 23) AS created_date,
                    c.updated_by,
                    CONVERT(varchar, c.updated_date, 23) AS updated_date
                FROM Customer c
                LEFT JOIN CustomerGroup cg ON c.group_id = cg.group_id AND cg.status = 'A'
                WHERE c.status = 'A'
                ORDER BY c.full_name";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["customer_id"] = row["customer_id"]?.ToString();
                r["group_id"] = row["group_id"]?.ToString();
                r["full_name"] = row["full_name"]?.ToString();
                r["company_name"] = row["company_name"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["city"] = row["city"]?.ToString();
                r["state"] = row["state"]?.ToString();
                r["country"] = row["country"]?.ToString();
                r["postal_code"] = row["postal_code"]?.ToString();
                r["credit_limit"] = row["credit_limit"]?.ToString();
                r["credit_balance"] = row["credit_balance"]?.ToString();
                r["group_name"] = row["group_name"]?.ToString();
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
        /// Gets a specific Customer by ID
        /// </summary>
        public DataTable GetCustomerById(int customerId)
        {
            DataTable dt = new DataTable("Customer");
            
            // Define columns
            dt.Columns.Add("customer_id", typeof(string));
            dt.Columns.Add("group_id", typeof(string));
            dt.Columns.Add("full_name", typeof(string));
            dt.Columns.Add("company_name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("address", typeof(string));
            dt.Columns.Add("city", typeof(string));
            dt.Columns.Add("state", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("postal_code", typeof(string));
            dt.Columns.Add("credit_limit", typeof(string));
            dt.Columns.Add("credit_balance", typeof(string));
            dt.Columns.Add("group_name", typeof(string));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("created_by", typeof(string));
            dt.Columns.Add("created_date", typeof(string));
            dt.Columns.Add("updated_by", typeof(string));
            dt.Columns.Add("updated_date", typeof(string));

            string query = @"
                SELECT 
                    c.customer_id,
                    c.group_id,
                    c.full_name,
                    c.company_name,
                    c.email,
                    c.phone,
                    c.address,
                    c.city,
                    c.state,
                    c.country,
                    c.postal_code,
                    c.credit_limit,
                    c.credit_balance,
                    cg.group_name,
                    c.status,
                    c.created_by,
                    CONVERT(varchar, c.created_date, 23) AS created_date,
                    c.updated_by,
                    CONVERT(varchar, c.updated_date, 23) AS updated_date
                FROM Customer c
                LEFT JOIN CustomerGroup cg ON c.group_id = cg.group_id AND cg.status = 'A'
                WHERE c.customer_id = @customer_id AND c.status = 'A'";

            var parameters = new SqlParameter[] { new SqlParameter("@customer_id", customerId) };
            DataTable result = Connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["customer_id"] = row["customer_id"]?.ToString();
                r["group_id"] = row["group_id"]?.ToString();
                r["full_name"] = row["full_name"]?.ToString();
                r["company_name"] = row["company_name"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["city"] = row["city"]?.ToString();
                r["state"] = row["state"]?.ToString();
                r["country"] = row["country"]?.ToString();
                r["postal_code"] = row["postal_code"]?.ToString();
                r["credit_limit"] = row["credit_limit"]?.ToString();
                r["credit_balance"] = row["credit_balance"]?.ToString();
                r["group_name"] = row["group_name"]?.ToString();
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
        /// Inserts a new Customer into the database
        /// </summary>
        public int InsertCustomer(int? groupId, string fullName, string companyName, string email, 
                                   string phone, string address, string city, string state, 
                                   string country, string postalCode, int createdBy)
        {
            try
            {
                string query = @"
                    INSERT INTO Customer (
                        group_id, full_name, company_name, email, phone, address, 
                        city, state, country, postal_code, credit_limit, credit_balance,
                        status, created_by, created_date
                    )
                    VALUES (
                        @group_id, @full_name, @company_name, @email, @phone, @address,
                        @city, @state, @country, @postal_code, 0, 0,
                        'A', @created_by, GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@group_id", groupId.HasValue ? (object)groupId.Value : DBNull.Value),
                    new SqlParameter("@full_name", fullName),
                    new SqlParameter("@company_name", string.IsNullOrWhiteSpace(companyName) ? (object)DBNull.Value : companyName),
                    new SqlParameter("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                    new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                    new SqlParameter("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                    new SqlParameter("@city", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city),
                    new SqlParameter("@state", string.IsNullOrWhiteSpace(state) ? (object)DBNull.Value : state),
                    new SqlParameter("@country", string.IsNullOrWhiteSpace(country) ? (object)DBNull.Value : country),
                    new SqlParameter("@postal_code", string.IsNullOrWhiteSpace(postalCode) ? (object)DBNull.Value : postalCode),
                    new SqlParameter("@created_by", createdBy)
                };

                DataTable result = Connection.ExecuteQuery(query, parameters);
                if (result.Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Rows[0][0]);
                }

                throw new Exception("Failed to insert Customer.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting Customer: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing Customer
        /// </summary>
        public bool UpdateCustomer(int customerId, int? groupId, string fullName, string companyName, 
                                    string email, string phone, string address, string city, 
                                    string state, string country, string postalCode, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE Customer
                    SET 
                        group_id = @group_id,
                        full_name = @full_name,
                        company_name = @company_name,
                        email = @email,
                        phone = @phone,
                        address = @address,
                        city = @city,
                        state = @state,
                        country = @country,
                        postal_code = @postal_code,
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE customer_id = @customer_id AND status = 'A'";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@customer_id", customerId),
                    new SqlParameter("@group_id", groupId.HasValue ? (object)groupId.Value : DBNull.Value),
                    new SqlParameter("@full_name", fullName),
                    new SqlParameter("@company_name", string.IsNullOrWhiteSpace(companyName) ? (object)DBNull.Value : companyName),
                    new SqlParameter("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email),
                    new SqlParameter("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone),
                    new SqlParameter("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address),
                    new SqlParameter("@city", string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city),
                    new SqlParameter("@state", string.IsNullOrWhiteSpace(state) ? (object)DBNull.Value : state),
                    new SqlParameter("@country", string.IsNullOrWhiteSpace(country) ? (object)DBNull.Value : country),
                    new SqlParameter("@postal_code", string.IsNullOrWhiteSpace(postalCode) ? (object)DBNull.Value : postalCode),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Customer: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Soft deletes a Customer (sets status to 'I')
        /// </summary>
        public bool DeleteCustomer(int customerId, int updatedBy)
        {
            try
            {
                string query = @"
                    UPDATE Customer
                    SET 
                        status = 'I',
                        updated_by = @updated_by,
                        updated_date = GETDATE()
                    WHERE customer_id = @customer_id";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@customer_id", customerId),
                    new SqlParameter("@updated_by", updatedBy)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Customer: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets customer group sales report - total sales grouped by customer group
        /// </summary>
        public DataTable GetCustomerGroupSalesReport()
        {
            try
            {
                string query = @"
                    SELECT 
                        ISNULL(cg.group_name, 'No Group') AS customer_group,
                        SUM(s.grand_total) AS total_sales
                    FROM Sale s
                    LEFT JOIN Customer c ON s.customer_id = c.customer_id
                    LEFT JOIN CustomerGroup cg ON c.group_id = cg.group_id
                    WHERE s.status = 'A' 
                      AND s.sale_type = 'SALE'
                    GROUP BY cg.group_name
                    ORDER BY total_sales DESC";

                return Connection.ExecuteQuery(query) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving customer group sales report: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches customer group sales report by keyword
        /// </summary>
        public DataTable SearchCustomerGroupSalesReport(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetCustomerGroupSalesReport();
                }

                string query = @"
                    SELECT 
                        customer_group,
                        total_sales
                    FROM (
                        SELECT 
                            ISNULL(cg.group_name, 'No Group') AS customer_group,
                            SUM(s.grand_total) AS total_sales
                        FROM Sale s
                        LEFT JOIN Customer c ON s.customer_id = c.customer_id
                        LEFT JOIN CustomerGroup cg ON c.group_id = cg.group_id
                        WHERE s.status = 'A' 
                          AND s.sale_type = 'SALE'
                        GROUP BY cg.group_name
                    ) AS GroupedSales
                    WHERE customer_group LIKE @keyword
                       OR CONVERT(VARCHAR, total_sales) LIKE @keyword
                    ORDER BY total_sales DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@keyword", "%" + keyword + "%")
                };

                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching customer group sales report: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets supplier/customer report - all customers with their purchase and sales data
        /// </summary>
        public DataTable GetSupplierCustomerReport()
        {
            try
            {
                string query = @"
                    WITH CustomerSales AS (
                        SELECT 
                            c.customer_id,
                            ISNULL(c.full_name, 'Walk-In Customer') AS customer_name,
                            ISNULL(SUM(CASE WHEN s.sale_type = 'SALE' THEN s.grand_total ELSE 0 END), 0) AS total_sale,
                            ISNULL(SUM(CASE WHEN s.sale_type = 'SALE_RETURN' THEN s.grand_total ELSE 0 END), 0) AS total_sell_return,
                            ISNULL(c.credit_balance, 0) AS opening_balance
                        FROM Customer c
                        LEFT JOIN Sale s ON c.customer_id = s.customer_id AND s.status = 'A'
                        WHERE c.status = 'A'
                        GROUP BY c.customer_id, c.full_name, c.credit_balance
                    )
                    SELECT 
                        customer_name,
                        CAST(0 AS DECIMAL(18,2)) AS total_purchase,
                        CAST(0 AS DECIMAL(18,2)) AS total_purchase_return,
                        total_sale,
                        total_sell_return,
                        opening_balance,
                        (opening_balance + total_sale - total_sell_return) AS due
                    FROM CustomerSales
                    ORDER BY customer_name";

                return Connection.ExecuteQuery(query) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving supplier/customer report: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches supplier/customer report by keyword
        /// </summary>
        public DataTable SearchSupplierCustomerReport(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetSupplierCustomerReport();
                }

                string query = @"
                    WITH CustomerSales AS (
                        SELECT 
                            c.customer_id,
                            ISNULL(c.full_name, 'Walk-In Customer') AS customer_name,
                            ISNULL(SUM(CASE WHEN s.sale_type = 'SALE' THEN s.grand_total ELSE 0 END), 0) AS total_sale,
                            ISNULL(SUM(CASE WHEN s.sale_type = 'SALE_RETURN' THEN s.grand_total ELSE 0 END), 0) AS total_sell_return,
                            ISNULL(c.credit_balance, 0) AS opening_balance
                        FROM Customer c
                        LEFT JOIN Sale s ON c.customer_id = s.customer_id AND s.status = 'A'
                        WHERE c.status = 'A'
                        GROUP BY c.customer_id, c.full_name, c.credit_balance
                    )
                    SELECT 
                        customer_name,
                        CAST(0 AS DECIMAL(18,2)) AS total_purchase,
                        CAST(0 AS DECIMAL(18,2)) AS total_purchase_return,
                        total_sale,
                        total_sell_return,
                        opening_balance,
                        (opening_balance + total_sale - total_sell_return) AS due
                    FROM CustomerSales
                    WHERE customer_name LIKE @keyword
                       OR CONVERT(VARCHAR, total_sale) LIKE @keyword
                       OR CONVERT(VARCHAR, total_sell_return) LIKE @keyword
                       OR CONVERT(VARCHAR, opening_balance) LIKE @keyword
                       OR CONVERT(VARCHAR, opening_balance + total_sale - total_sell_return) LIKE @keyword
                    ORDER BY customer_name";

                SqlParameter[] parameters = {
                    new SqlParameter("@keyword", "%" + keyword + "%")
                };

                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching supplier/customer report: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets customer transactions (sales, payments, returns) within a date range
        /// </summary>
        public DataTable GetCustomerTransactions(int customerId, DateTime startDate, DateTime endDate, int? storeId = null)
        {
            string query = "usp_GetCustomerTransactions";
            
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CustomerId", customerId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            
            if (storeId.HasValue)
                parameters.Add(new SqlParameter("@StoreId", storeId.Value));
            else
                parameters.Add(new SqlParameter("@StoreId", DBNull.Value));

            return Connection.ExecuteStoredProcedure(query, parameters.ToArray());
        }

        /// <summary>
        /// Gets customer account summary (opening balance, totals, current balance)
        /// </summary>
        public DataTable GetCustomerAccountSummary(int customerId, DateTime startDate, DateTime endDate, int? storeId = null)
        {
            string query = "usp_GetCustomerAccountSummary";
            
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CustomerId", customerId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            
            if (storeId.HasValue)
                parameters.Add(new SqlParameter("@StoreId", storeId.Value));
            else
                parameters.Add(new SqlParameter("@StoreId", DBNull.Value));

            return Connection.ExecuteStoredProcedure(query, parameters.ToArray());
        }

        /// <summary>
        /// Gets supplier transactions (placeholder)
        /// </summary>
        public DataTable GetSupplierTransactions(int supplierId, DateTime startDate, DateTime endDate, int? storeId = null)
        {
            string query = "usp_GetSupplierTransactions";
            
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SupplierId", supplierId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            
            if (storeId.HasValue)
                parameters.Add(new SqlParameter("@StoreId", storeId.Value));
            else
                parameters.Add(new SqlParameter("@StoreId", DBNull.Value));

            return Connection.ExecuteStoredProcedure(query, parameters.ToArray());
        }
    }
}
