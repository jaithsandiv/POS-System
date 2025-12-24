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
                    supplier_id,
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
                r["supplier_id"] = row["supplier_id"]?.ToString();
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
                    u.code as unit_name,
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
                LEFT JOIN Unit u ON p.unit_id = u.unit_id
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
                r["unit_name"] = row["unit_name"]?.ToString();
                r["purchase_cost"] = row["purchase_cost"]?.ToString();
                r["selling_price"] = row["selling_price"]?.ToString();
                r["stock_quantity"] = row["stock_quantity"]?.ToString();
                r["expiry_date"] = row["expiry_date"]?.ToString();
                r["manufacture_date"] = row["manufacture_date"]?.ToString();
                r["description"] = row["description"]?.ToString();
                r["image"] = row["image"] is DBNull ? DBNull.Value : row["image"];
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
            string query = @"
                SELECT 
                    c.customer_id,
                    c.full_name,
                    c.email,
                    c.phone,
                    c.address,
                    c.credit_limit,
                    c.credit_balance,
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
                    AND cg.status = 'A'
                WHERE c.status = 'A'";

            return Connection.ExecuteQuery(query);
        }
        public DataTable GetTables()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Table;
            dt.Clear();
            string query = @"
                SELECT 
                    table_id,
                    table_number,
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
                r["table_number"] = row["table_number"]?.ToString();
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

        public DataTable GetAvailableTables()
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
            DataTable dt = ds.Table;
            dt.Clear();
            string query = @"
                SELECT 
                    table_id,
                    table_number,
                    capacity,
                    status,
                    created_by,
                    CONVERT(varchar, created_date, 23) AS created_date,
                    updated_by,
                    CONVERT(varchar, updated_date, 23) AS updated_date
                FROM [Table]
                WHERE status = 'A'
                AND table_number NOT IN (
                    SELECT table_number 
                    FROM Sale 
                    WHERE sale_type = 'DRAFT' 
                    AND status = 'A' 
                    AND table_number IS NOT NULL
                )";
            DataTable result = Connection.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["table_id"] = row["table_id"]?.ToString();
                r["table_number"] = row["table_number"]?.ToString();
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

        public int SaveSale(int storeId, int billerId, int? customerId, string saleType, 
                            string discountType, decimal discountValue, decimal totalAmount, 
                            int totalItems, decimal grandTotal, string notes, DataTable saleItems,
                            decimal totalPaid = 0, decimal changeDue = 0,
                            string invoiceNumber = null, string quotationNumber = null, 
                            string orderType = null, string tableNumber = null,
                            int saleId = 0)
        {
            try
            {
                if (saleId > 0)
                {
                    // UPDATE Logic
                    string updateQuery = @"
                        UPDATE Sale SET
                            store_id = @store_id,
                            sale_type = @sale_type,
                            customer_id = @customer_id,
                            biller_id = @biller_id,
                            total_items = @total_items,
                            total_amount = @total_amount,
                            discount_type = @discount_type,
                            discount_value = @discount_value,
                            grand_total = @grand_total,
                            total_paid = @total_paid,
                            change_due = @change_due,
                            notes = @notes,
                            updated_by = @created_by,
                            updated_date = GETDATE()
                        WHERE sale_id = @sale_id";

                     // Build parameters list
                    var paramList = new List<SqlParameter>
                    {
                        new SqlParameter("@sale_id", saleId),
                        new SqlParameter("@store_id", storeId),
                        new SqlParameter("@sale_type", saleType),
                        new SqlParameter("@customer_id", customerId ?? (object)DBNull.Value),
                        new SqlParameter("@biller_id", billerId),
                        new SqlParameter("@total_items", totalItems),
                        new SqlParameter("@total_amount", totalAmount),
                        new SqlParameter("@discount_type", discountType),
                        new SqlParameter("@discount_value", discountValue),
                        new SqlParameter("@grand_total", grandTotal),
                        new SqlParameter("@total_paid", totalPaid),
                        new SqlParameter("@change_due", changeDue),
                        new SqlParameter("@notes", notes ?? (object)DBNull.Value),
                        new SqlParameter("@created_by", billerId)
                    };

                    // For optional fields, we only update if they are provided, OR we can overwrite.
                    // To be safe and simple, let's update them if provided or null out if not?
                    // Usually invoice_number is generated once.
                    if (!string.IsNullOrEmpty(invoiceNumber))
                    {
                        updateQuery += "; UPDATE Sale SET invoice_number = @invoice_number WHERE sale_id = @sale_id";
                        paramList.Add(new SqlParameter("@invoice_number", invoiceNumber));
                    }
                    if (!string.IsNullOrEmpty(quotationNumber))
                    {
                        updateQuery += "; UPDATE Sale SET quotation_number = @quotation_number WHERE sale_id = @sale_id";
                        paramList.Add(new SqlParameter("@quotation_number", quotationNumber));
                    }

                    // OrderType and TableNumber are updatable
                    updateQuery += "; UPDATE Sale SET order_type = @order_type, table_number = @table_number WHERE sale_id = @sale_id";
                    paramList.Add(new SqlParameter("@order_type", orderType ?? (object)DBNull.Value));
                    paramList.Add(new SqlParameter("@table_number", tableNumber ?? (object)DBNull.Value));

                    Connection.ExecuteNonQuery(updateQuery, paramList.ToArray());

                    // Delete existing SaleItems to replace with new ones
                    string deleteItemsQuery = "DELETE FROM SaleItem WHERE sale_id = @sale_id";
                    Connection.ExecuteNonQuery(deleteItemsQuery, new SqlParameter[] { new SqlParameter("@sale_id", saleId) });
                }
                else
                {
                    // INSERT Logic
                    // Build the INSERT query with only the required fields
                    string saleQuery = @"
                        INSERT INTO Sale (
                            store_id, sale_type, customer_id, biller_id, total_items, total_amount,
                            discount_type, discount_value, grand_total, total_paid, change_due,
                            payment_status, sale_status, notes, status, created_by, created_date";

                    // Add optional fields to query based on sale type
                    if (!string.IsNullOrEmpty(invoiceNumber))
                        saleQuery += ", invoice_number";
                    if (!string.IsNullOrEmpty(quotationNumber))
                        saleQuery += ", quotation_number";
                    if (!string.IsNullOrEmpty(orderType))
                        saleQuery += ", order_type";
                    if (!string.IsNullOrEmpty(tableNumber))
                        saleQuery += ", table_number";

                    saleQuery += @"
                        )
                        VALUES (
                            @store_id, @sale_type, @customer_id, @biller_id, @total_items, @total_amount,
                            @discount_type, @discount_value, @grand_total, @total_paid, @change_due,
                            @payment_status, @sale_status, @notes, @status, @created_by, GETDATE()";

                    // Add optional values to query based on sale type
                    if (!string.IsNullOrEmpty(invoiceNumber))
                        saleQuery += ", @invoice_number";
                    if (!string.IsNullOrEmpty(quotationNumber))
                        saleQuery += ", @quotation_number";
                    if (!string.IsNullOrEmpty(orderType))
                        saleQuery += ", @order_type";
                    if (!string.IsNullOrEmpty(tableNumber))
                        saleQuery += ", @table_number";

                    saleQuery += @"
                        );
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    // Build parameters list
                    var paramList = new List<SqlParameter>
                    {
                        new SqlParameter("@store_id", storeId),
                        new SqlParameter("@sale_type", saleType),
                        new SqlParameter("@customer_id", customerId ?? (object)DBNull.Value),
                        new SqlParameter("@biller_id", billerId),
                        new SqlParameter("@total_items", totalItems),
                        new SqlParameter("@total_amount", totalAmount),
                        new SqlParameter("@discount_type", discountType),
                        new SqlParameter("@discount_value", discountValue),
                        new SqlParameter("@grand_total", grandTotal),
                        new SqlParameter("@total_paid", totalPaid),
                        new SqlParameter("@change_due", changeDue),
                        new SqlParameter("@payment_status", "PENDING"),
                        new SqlParameter("@sale_status", "COMPLETED"),
                        new SqlParameter("@notes", notes ?? (object)DBNull.Value),
                        new SqlParameter("@status", "A"),
                        new SqlParameter("@created_by", billerId)
                    };

                    // Add optional parameters based on sale type
                    if (!string.IsNullOrEmpty(invoiceNumber))
                        paramList.Add(new SqlParameter("@invoice_number", invoiceNumber));
                    if (!string.IsNullOrEmpty(quotationNumber))
                        paramList.Add(new SqlParameter("@quotation_number", quotationNumber));
                    if (!string.IsNullOrEmpty(orderType))
                        paramList.Add(new SqlParameter("@order_type", orderType));
                    if (!string.IsNullOrEmpty(tableNumber))
                        paramList.Add(new SqlParameter("@table_number", tableNumber));

                    var saleParams = paramList.ToArray();

                    DataTable result = Connection.ExecuteQuery(saleQuery, saleParams);
                    if (result.Rows.Count == 0)
                        throw new Exception($"Failed to create {saleType} sale record.");

                    saleId = Convert.ToInt32(result.Rows[0][0]);
                }

                // Insert SaleItems (New or Re-insert)
                if (saleItems != null && saleItems.Rows.Count > 0)
                {
                    foreach (DataRow item in saleItems.Rows)
                    {
                        string itemQuery = @"
                            INSERT INTO SaleItem (
                                sale_id, product_id, product_name, product_code, unit, unit_price,
                                quantity, discount_type, discount_value, subtotal, status, created_by, created_date
                            )
                            VALUES (
                                @sale_id, @product_id, @product_name, @product_code, @unit, @unit_price,
                                @quantity, @discount_type, @discount_value, @subtotal, @status, @created_by, GETDATE()
                            );";

                        var itemParams = new SqlParameter[]
                        {
                            new SqlParameter("@sale_id", saleId),
                            new SqlParameter("@product_id", item["product_id"]),
                            new SqlParameter("@product_name", item["product_name"] ?? (object)DBNull.Value),
                            new SqlParameter("@product_code", item["product_code"] ?? (object)DBNull.Value),
                            new SqlParameter("@unit", item["unit"] ?? (object)DBNull.Value),
                            new SqlParameter("@unit_price", decimal.Parse(item["unit_price"]?.ToString() ?? "0")),
                            new SqlParameter("@quantity", decimal.Parse(item["quantity"]?.ToString() ?? "0")),
                            new SqlParameter("@discount_type", item["discount_type"]?.ToString() ?? "PERCENTAGE"),
                            new SqlParameter("@discount_value", decimal.Parse(item["discount_value"]?.ToString() ?? "0")),
                            new SqlParameter("@subtotal", decimal.Parse(item["subtotal"]?.ToString() ?? "0")),
                            new SqlParameter("@status", "A"),
                            new SqlParameter("@created_by", billerId)
                        };

                        Connection.ExecuteNonQuery(itemQuery, itemParams);
                    }
                }

                return saleId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving {saleType} sale: {ex.Message}", ex);
            }
        }

        public int SaveDraft(int storeId, int billerId, int? customerId, string discountType,
                             decimal discountValue, decimal totalAmount, int totalItems, decimal grandTotal,
                             string orderType, string tableNumber, string notes, DataTable saleItems)
        {
            return SaveSale(storeId, billerId, customerId, "DRAFT", discountType, discountValue, 
                totalAmount, totalItems, grandTotal, notes, saleItems, 0m, 0m, null, null, orderType, tableNumber);
        }

        public int SaveQuotation(int storeId, int billerId, int? customerId, string quotationNumber,
                                 string discountType, decimal discountValue, decimal totalAmount,
                                 int totalItems, decimal grandTotal, string notes, DataTable saleItems)
        {
            return SaveSale(storeId, billerId, customerId, "QUOTATION", discountType, discountValue, 
                totalAmount, totalItems, grandTotal, notes, saleItems, 0m, 0m, null, quotationNumber);
        }

        public int SaveCreditSale(int storeId, int billerId, int? customerId, 
                                  string discountType, decimal discountValue, decimal totalAmount,
                                  int totalItems, decimal grandTotal, string notes, DataTable saleItems)
        {
            return SaveSale(storeId, billerId, customerId, "CREDIT_SALE", discountType, discountValue, 
                totalAmount, totalItems, grandTotal, notes, saleItems, 0m, 0m);
        }

        public DataTable GetDrafts(int storeId)
        {
            try
            {
                string query = @"
                    SELECT 
                        sale_id,
                        sale_type,
                        customer_id,
                        biller_id,
                        total_items,
                        total_amount,
                        discount_type,
                        discount_value,
                        grand_total,
                        order_type,
                        table_number,
                        notes,
                        status,
                        created_by,
                        CONVERT(varchar, created_date, 23) AS created_date,
                        updated_by,
                        CONVERT(varchar, updated_date, 23) AS updated_date
                    FROM Sale
                    WHERE store_id = @store_id 
                      AND sale_type = 'DRAFT'
                      AND status = 'A'
                    ORDER BY created_date DESC";

                var parameters = new SqlParameter[] { new SqlParameter("@store_id", storeId) };
                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving drafts: {ex.Message}", ex);
            }
        }
        public DataTable GetQuotations(int storeId)
        {
            try
            {
                string query = @"
                    SELECT 
                        sale_id,
                        quotation_number,
                        sale_type,
                        customer_id,
                        biller_id,
                        total_items,
                        total_amount,
                        discount_type,
                        discount_value,
                        grand_total,
                        notes,
                        status,
                        created_by,
                        CONVERT(varchar, created_date, 23) AS created_date,
                        updated_by,
                        CONVERT(varchar, updated_date, 23) AS updated_date
                    FROM Sale
                    WHERE store_id = @store_id 
                      AND sale_type = 'QUOTATION'
                      AND status = 'A'
                    ORDER BY created_date DESC";

                var parameters = new SqlParameter[] { new SqlParameter("@store_id", storeId) };
                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving quotations: {ex.Message}", ex);
            }
        }

        public DataTable GetSaleItems(int saleId)
        {
            try
            {
                string query = @"
                    SELECT 
                        sale_item_id,
                        sale_id,
                        product_id,
                        product_name,
                        product_code,
                        unit,
                        unit_price,
                        quantity,
                        discount_type,
                        discount_value,
                        subtotal,
                        status,
                        created_by,
                        CONVERT(varchar, created_date, 23) AS created_date,
                        updated_by,
                        CONVERT(varchar, updated_date, 23) AS updated_date
                    FROM SaleItem
                    WHERE sale_id = @sale_id AND status = 'A'";

                var parameters = new SqlParameter[] { new SqlParameter("@sale_id", saleId) };
                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sale items: {ex.Message}", ex);
            }
        }

        public string GetNextQuotationNumber()
        {
            try
            {
                string query = "SELECT NEXT VALUE FOR QuotationNumberSequence AS quotation_seq;";
                DataTable result = Connection.ExecuteQuery(query);

                if (result.Rows.Count > 0)
                {
                    int sequenceValue = Convert.ToInt32(result.Rows[0]["quotation_seq"]);
                    return $"QT-{DateTime.Now:yyyy}-{sequenceValue:D6}";
                }

                throw new Exception("Failed to generate quotation number from sequence.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating quotation number: {ex.Message}", ex);
            }
        }

        public string GetNextInvoiceNumber()
        {
            try
            {
                string query = "SELECT NEXT VALUE FOR InvoiceNumberSequence AS invoice_seq;";
                DataTable result = Connection.ExecuteQuery(query);

                if (result.Rows.Count > 0)
                {
                    int sequenceValue = Convert.ToInt32(result.Rows[0]["invoice_seq"]);
                    return $"INV-{DateTime.Now:yyyy}-{sequenceValue:D6}";
                }

                throw new Exception("Failed to generate invoice number from sequence.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating invoice number: {ex.Message}", ex);
            }
        }

        public void SavePayments(int saleId, DataTable payments, int createdBy)
        {
            try
            {
                if (payments == null || payments.Rows.Count == 0)
                    return;

                foreach (DataRow payment in payments.Rows)
                {
                    string paymentMethod = payment["payment_method"]?.ToString();
                    if (string.IsNullOrWhiteSpace(paymentMethod))
                        continue;

                    decimal amount = 0;
                    if (payment["amount"] != DBNull.Value)
                        decimal.TryParse(payment["amount"].ToString(), out amount);

                    if (amount <= 0)
                        continue;

                    string query = @"
                        INSERT INTO Payment (
                            sale_id, payment_method, amount, 
                            card_last_four_digits, card_holder_name, 
                            card_transaction_number, card_type, 
                            bank_reference_number, 
                            status, created_by, created_date
                        )
                        VALUES (
                            @sale_id, @payment_method, @amount,
                            @card_last_four_digits, @card_holder_name,
                            @card_transaction_number, @card_type,
                            @bank_reference_number,
                            @status, @created_by, GETDATE()
                        );";

                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@sale_id", saleId),
                        new SqlParameter("@payment_method", paymentMethod),
                        new SqlParameter("@amount", amount),
                        new SqlParameter("@card_last_four_digits", payment["card_last_four_digits"] ?? (object)DBNull.Value),
                        new SqlParameter("@card_holder_name", payment["card_holder_name"] ?? (object)DBNull.Value),
                        new SqlParameter("@card_transaction_number", payment["card_transaction_number"] ?? (object)DBNull.Value),
                        new SqlParameter("@card_type", payment["card_type"] ?? (object)DBNull.Value),
                        new SqlParameter("@bank_reference_number", payment["bank_reference_number"] ?? (object)DBNull.Value),
                        new SqlParameter("@status", "A"),
                        new SqlParameter("@created_by", createdBy)
                    };

                    Connection.ExecuteNonQuery(query, parameters);

                    // Update customer credit balance if payment is CREDIT
                    if (paymentMethod == "CREDIT")
                    {
                        // Update the customer's credit balance in the database
                        string updateCreditQuery = @"
                            UPDATE Customer
                            SET credit_balance = ISNULL(credit_balance, 0) + @amount,
                                updated_by = @updated_by,
                                updated_date = GETDATE()
                            WHERE customer_id = (SELECT customer_id FROM Sale WHERE sale_id = @sale_id)";

                        var creditParams = new SqlParameter[]
                        {
                            new SqlParameter("@amount", amount),
                            new SqlParameter("@updated_by", createdBy),
                            new SqlParameter("@sale_id", saleId)
                        };

                        Connection.ExecuteNonQuery(updateCreditQuery, creditParams);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving payments: {ex.Message}", ex);
            }
        }

        public DataTable GetSale(int saleId)
        {
            try
            {
                string query = @"
                    SELECT * FROM Sale WHERE sale_id = @sale_id";
                var parameters = new SqlParameter[] { new SqlParameter("@sale_id", saleId) };
                return Connection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sale: {ex.Message}", ex);
            }
        }

        public DataTable GetSalePayments(int saleId)
        {
            try
            {
                string query = @"
                    SELECT * FROM Payment WHERE sale_id = @sale_id AND status = 'A'";
                var parameters = new SqlParameter[] { new SqlParameter("@sale_id", saleId) };
                return Connection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sale payments: {ex.Message}", ex);
            }
        }

        public DataTable GetSales(string saleType)
        {
            string query = @"
                SELECT 
                    s.sale_id,
                    s.invoice_number,
                    c.full_name as customer_name,
                    s.created_date as sale_date,
                    s.grand_total,
                    s.payment_status,
                    s.sale_status,
                    u.full_name as biller_name
                FROM Sale s
                LEFT JOIN Customer c ON s.customer_id = c.customer_id
                LEFT JOIN [User] u ON s.biller_id = u.user_id
                WHERE s.status = 'A' AND s.sale_type = '" + saleType + @"'
                ORDER BY s.sale_id DESC";

            return Connection.ExecuteQuery(query);
        }

        public DataTable SearchSales(string saleType, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetSales(saleType);
            }

            string query = @"
                SELECT 
                    s.sale_id,
                    s.invoice_number,
                    c.full_name as customer_name,
                    s.created_date as sale_date,
                    s.grand_total,
                    s.payment_status,
                    s.sale_status,
                    u.full_name as biller_name
                FROM Sale s
                LEFT JOIN Customer c ON s.customer_id = c.customer_id
                LEFT JOIN [User] u ON s.biller_id = u.user_id
                WHERE s.status = 'A' AND s.sale_type = @saleType
                AND (s.invoice_number LIKE @keyword OR c.full_name LIKE @keyword OR u.full_name LIKE @keyword)
                ORDER BY s.sale_id DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@saleType", saleType),
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Gets table sales report - total sales grouped by table number
        /// </summary>
        public DataTable GetTableSalesReport()
        {
            try
            {
                string query = @"
                    SELECT 
                        ISNULL(s.table_number, 'No Table') AS table_number,
                        COUNT(s.sale_id) AS total_orders,
                        SUM(s.total_items) AS total_items,
                        SUM(s.total_amount) AS total_amount,
                        SUM(s.grand_total) AS grand_total,
                        MIN(s.created_date) AS first_order_date,
                        MAX(s.created_date) AS last_order_date
                    FROM Sale s
                    WHERE s.status = 'A' 
                      AND s.sale_type = 'SALE'
                      AND s.table_number IS NOT NULL
                    GROUP BY s.table_number
                    ORDER BY grand_total DESC";

                return Connection.ExecuteQuery(query) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving table sales report: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches table sales report by keyword
        /// </summary>
        public DataTable SearchTableSalesReport(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetTableSalesReport();
                }

                string query = @"
                    SELECT 
                        ISNULL(s.table_number, 'No Table') AS table_number,
                        COUNT(s.sale_id) AS total_orders,
                        SUM(s.total_items) AS total_items,
                        SUM(s.total_amount) AS total_amount,
                        SUM(s.grand_total) AS grand_total,
                        MIN(s.created_date) AS first_order_date,
                        MAX(s.created_date) AS last_order_date
                    FROM Sale s
                    WHERE s.status = 'A' 
                      AND s.sale_type = 'SALE'
                      AND s.table_number IS NOT NULL
                      AND s.table_number LIKE @keyword
                    GROUP BY s.table_number
                    ORDER BY grand_total DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@keyword", "%" + keyword + "%")
                };

                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching table sales report: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets sale payments report - all sales with payment details grouped by customer
        /// </summary>
        public DataTable GetSalePayments()
        {
            try
            {
                string query = @"
                    SELECT 
                        s.sale_id,
                        s.invoice_number AS reference_number,
                        s.created_date AS paid_on,
                        s.grand_total AS amount,
                        ISNULL(c.full_name, 'Walk-In Customer') AS customer,
                        ISNULL(cg.group_name, 'None') AS customer_group,
                        STUFF((
                            SELECT DISTINCT ', ' + p.payment_method
                            FROM Payment p
                            WHERE p.sale_id = s.sale_id AND p.status = 'A'
                            FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS payment_method
                    FROM Sale s
                    LEFT JOIN Customer c ON s.customer_id = c.customer_id
                    LEFT JOIN CustomerGroup cg ON c.group_id = cg.group_id
                    WHERE s.status = 'A' 
                      AND s.sale_type = 'SALE'
                    ORDER BY s.created_date DESC";

                return Connection.ExecuteQuery(query) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sale payments: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches sale payments report by keyword
        /// </summary>
        public DataTable SearchSalePayments(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetSalePayments();
                }

                string query = @"
                    SELECT 
                        s.sale_id,
                        s.invoice_number AS reference_number,
                        s.created_date AS paid_on,
                        s.grand_total AS amount,
                        ISNULL(c.full_name, 'Walk-In Customer') AS customer,
                        ISNULL(cg.group_name, 'None') AS customer_group,
                        STUFF((
                            SELECT DISTINCT ', ' + p.payment_method
                            FROM Payment p
                            WHERE p.sale_id = s.sale_id AND p.status = 'A'
                            FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS payment_method
                    FROM Sale s
                    LEFT JOIN Customer c ON s.customer_id = c.customer_id
                    LEFT JOIN CustomerGroup cg ON c.group_id = cg.group_id
                    WHERE s.status = 'A' 
                      AND s.sale_type = 'SALE'
                      AND (
                        s.invoice_number LIKE @keyword
                        OR ISNULL(c.full_name, 'Walk-In Customer') LIKE @keyword
                        OR ISNULL(cg.group_name, 'None') LIKE @keyword
                        OR CONVERT(VARCHAR, s.grand_total) LIKE @keyword
                      )
                    ORDER BY s.created_date DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@keyword", "%" + keyword + "%")
                };

                return Connection.ExecuteQuery(query, parameters) ?? new DataTable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching sale payments: {ex.Message}", ex);
            }
        }
    }
}
