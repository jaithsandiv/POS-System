using DevExpress.Printing.Utils.DocumentStoring;
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
    internal class DAL_SalesTerminal
    {
        public DAL_SalesTerminal()
        {
        }

        public DataTable GetCategories()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Category;
            dt.Clear();

            string query = @"
                SELECT 
                    category_id,
                    category_name,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM Category
                WHERE status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["category_id"] = row["category_id"]?.ToString();
                r["category_name"] = row["category_name"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        public DataTable GetBrands()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Brand;
            dt.Clear();
            string query = @"
                SELECT 
                    brand_id,
                    brand_name,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM Brand
                WHERE status = 'A'";
            DataTable result = Connection.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["brand_id"] = row["brand_id"]?.ToString();
                r["brand_name"] = row["brand_name"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();
                dt.Rows.Add(r);
            }
            return dt;
        }

        public DataTable GetProducts()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Product;
            dt.Clear();

            string query = @"
                SELECT 
                    p.product_id,
                    p.product_name,
                    p.product_code,
                    p.barcode,
                    p.product_type,
                    p.category_id,
                    p.brand_id,
                    p.unit_id,
                    p.purchase_cost,
                    p.selling_price,
                    p.stock_quantity,
                    p.expiry_date,
                    p.manufacture_date,
                    p.description,
                    p.image,
                    p.status,
                    p.created_by,
                    CONVERT(varchar, p.created_date, 23) AS created_date,
                    p.updated_by,
                    CONVERT(varchar, p.updated_date, 23) AS updated_date,
                    pp.promotion_type,
                    pp.discount_value
                FROM Product p
                LEFT JOIN ProductPromotion pp
                    ON p.product_id = pp.product_id
                    AND pp.status = 'A'
                    AND GETDATE() BETWEEN (SELECT start_date FROM Promotion WHERE promotion_id = pp.promotion_id)
                                      AND (SELECT end_date FROM Promotion WHERE promotion_id = pp.promotion_id)
                WHERE p.status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["product_id"] = row["product_id"]?.ToString();
                r["product_name"] = row["product_name"]?.ToString();
                r["product_code"] = row["product_code"]?.ToString();
                r["barcode"] = row["barcode"]?.ToString();
                r["product_type"] = row["product_type"]?.ToString();
                r["category_id"] = row["category_id"]?.ToString();
                r["brand_id"] = row["brand_id"]?.ToString();
                r["unit_id"] = row["unit_id"]?.ToString();
                r["purchase_cost"] = row["purchase_cost"]?.ToString();
                r["selling_price"] = row["selling_price"]?.ToString();
                r["stock_quantity"] = row["stock_quantity"]?.ToString();
                r["expiry_date"] = row["expiry_date"]?.ToString();
                r["manufacture_date"] = row["manufacture_date"]?.ToString();
                r["description"] = row["description"]?.ToString();
                r["image"] = row["image"] as byte[];
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();
                r["promotion_type"] = row["promotion_type"]?.ToString();
                r["discount_value"] = row["discount_value"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }

        public DataTable GetStaffPin()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.StaffPin;
            dt.Clear();

            string query = @"
                SELECT 
                    pin_code
                FROM [User]
                WHERE pin_code IS NOT NULL
                  AND status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["pin_code"] = row["pin_code"]?.ToString();
                dt.Rows.Add(r);
            }

            return dt;
        }

        public DataTable GetCustomers()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Customer;
            dt.Clear();

            string query = @"
                SELECT 
                    c.customer_id,
                    c.full_name,
                    c.email,
                    c.phone,
                    c.address,
                    c.status,
                    c.created_by,
                    CONVERT(varchar, c.created_date, 23) AS created_date,
                    c.updated_by,
                    CONVERT(varchar, c.updated_date, 23) AS updated_date,
                    cg.group_id,
                    cg.group_name,
                    cg.discount_percent
                FROM Customer c
                LEFT JOIN CustomerGroup cg
                    ON c.group_id = cg.group_id
                WHERE c.status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["customer_id"] = row["customer_id"]?.ToString();
                r["full_name"] = row["full_name"]?.ToString();
                r["email"] = row["email"]?.ToString();
                r["phone"] = row["phone"]?.ToString();
                r["address"] = row["address"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();
                r["group_id"] = row["group_id"]?.ToString();
                r["group_name"] = row["group_name"]?.ToString();
                r["discount_percent"] = row["discount_percent"]?.ToString();
                dt.Rows.Add(r);
            }

            return dt;
        }
        public DataTable GetTables()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Table;
            dt.Clear();
            string query = @"
                SELECT 
                    table_id,
                    table_name,
                    capacity,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM [Table]
                WHERE status = 'A'";
            DataTable result = Connection.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["table_id"] = row["table_id"]?.ToString();
                r["table_name"] = row["table_name"]?.ToString();
                r["capacity"] = row["capacity"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();
                dt.Rows.Add(r);
            }
            return dt;
        }

        public bool IsKotEnabled()
        {
            string query = @"
                SELECT 
                    setting_value
                FROM SystemSetting
                WHERE setting_key = 'kot_enabled'
                  AND status = 'A'";

            DataTable result = Connection.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                string settingValue = result.Rows[0]["setting_value"]?.ToString();
                return string.Equals(settingValue, "true", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
