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
    internal class DAL_Store
    {
        public DataTable GetStores()
        {
            DAL_DS_Initialize ds = new DAL_DS_Initialize();
            DataTable dt = ds.Store;
            dt.Clear();

            string query = @"
                SELECT 
                    store_id, 
                    store_name, 
                    phone, 
                    email, 
                    address, 
                    city, 
                    state, 
                    country, 
                    postal_code, 
                    status
                FROM Store
                WHERE status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["store_id"] = row["store_id"]?.ToString();
                r["store_name"] = row["store_name"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["city"] = row["city"]?.ToString();
                r["state"] = row["state"]?.ToString();
                r["country"] = row["country"]?.ToString();
                r["postal_code"] = row["postal_code"]?.ToString();
                r["status"] = row["status"]?.ToString();
                dt.Rows.Add(r);
            }

            return dt;
        }

        public int InsertStore(string name, string phone, string email, string address, string city, string state, string country, string postalCode, int createdBy)
        {
            string query = @"
                INSERT INTO Store (store_name, phone, email, address, city, state, country, postal_code, created_by, status)
                VALUES (@store_name, @phone, @email, @address, @city, @state, @country, @postal_code, @created_by, 'A');
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@store_name", name),
                new SqlParameter("@phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@email", email ?? (object)DBNull.Value),
                new SqlParameter("@address", address ?? (object)DBNull.Value),
                new SqlParameter("@city", city ?? (object)DBNull.Value),
                new SqlParameter("@state", state ?? (object)DBNull.Value),
                new SqlParameter("@country", country ?? (object)DBNull.Value),
                new SqlParameter("@postal_code", postalCode ?? (object)DBNull.Value),
                new SqlParameter("@created_by", createdBy)
            };

            object result = Connection.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public bool UpdateStore(int storeId, string name, string phone, string email, string address, string city, string state, string country, string postalCode, int updatedBy)
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
                new SqlParameter("@store_name", name),
                new SqlParameter("@phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@email", email ?? (object)DBNull.Value),
                new SqlParameter("@address", address ?? (object)DBNull.Value),
                new SqlParameter("@city", city ?? (object)DBNull.Value),
                new SqlParameter("@state", state ?? (object)DBNull.Value),
                new SqlParameter("@country", country ?? (object)DBNull.Value),
                new SqlParameter("@postal_code", postalCode ?? (object)DBNull.Value),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteStore(int storeId, int updatedBy)
        {
            string query = @"
                UPDATE Store
                SET status = 'I',
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE store_id = @store_id";

            SqlParameter[] parameters = {
                new SqlParameter("@store_id", storeId),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable GetStoreById(int storeId)
        {
            string query = @"
                SELECT 
                    store_id, 
                    store_name, 
                    phone, 
                    email, 
                    address, 
                    city, 
                    state, 
                    country, 
                    postal_code, 
                    status
                FROM Store
                WHERE store_id = @store_id";

            SqlParameter[] parameters = {
                new SqlParameter("@store_id", storeId)
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        public DataTable SearchStores(string keyword)
        {
            DAL_DS_Initialize ds = new DAL_DS_Initialize();
            DataTable dt = ds.Store;
            dt.Clear();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetStores();
            }

            string query = @"
                SELECT 
                    store_id, 
                    store_name, 
                    phone, 
                    email, 
                    address, 
                    city, 
                    state, 
                    country, 
                    postal_code, 
                    status
                FROM Store
                WHERE status = 'A' AND (store_name LIKE @keyword OR phone LIKE @keyword OR email LIKE @keyword OR city LIKE @keyword)";

            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["store_id"] = row["store_id"]?.ToString();
                r["store_name"] = row["store_name"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["city"] = row["city"]?.ToString();
                r["state"] = row["state"]?.ToString();
                r["country"] = row["country"]?.ToString();
                r["postal_code"] = row["postal_code"]?.ToString();
                r["status"] = row["status"]?.ToString();
                dt.Rows.Add(r);
            }

            return dt;
        }
    }
}
