using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL
{
    internal class DAL_SalesReturn
    {
        public DAL_SalesReturn()
        {
        }

        public DataTable GetSaleReturns()
        {
            string query = @"
                SELECT 
                    sr.return_id,
                    sr.sale_id,
                    sr.total_amount,
                    sr.reason,
                    sr.processed_by,
                    sr.status,
                    sr.created_by,
                    CONVERT(varchar, sr.created_date, 23) AS created_date,
                    sr.updated_by,
                    CONVERT(varchar, sr.updated_date, 23) AS updated_date
                FROM SaleReturn sr
                WHERE sr.status = 'A'
                ORDER BY sr.created_date DESC";

            return Connection.ExecuteQuery(query);
        }

        public DataTable GetSaleReturnById(int returnId)
        {
            string query = @"
                SELECT 
                    sr.return_id,
                    sr.sale_id,
                    sr.total_amount,
                    sr.reason,
                    sr.processed_by,
                    sr.status,
                    sr.created_by,
                    CONVERT(varchar, sr.created_date, 23) AS created_date,
                    sr.updated_by,
                    CONVERT(varchar, sr.updated_date, 23) AS updated_date
                FROM SaleReturn sr
                WHERE sr.return_id = @return_id AND sr.status = 'A'";

            var parameters = new SqlParameter[] { new SqlParameter("@return_id", returnId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        public DataTable GetReturnItems(int returnId)
        {
            string query = @"
                SELECT 
                    ri.return_item_id,
                    ri.return_id,
                    ri.sale_item_id,
                    ri.product_id,
                    ri.quantity,
                    ri.refund_amount,
                    ri.status,
                    ri.created_by,
                    CONVERT(varchar, ri.created_date, 23) AS created_date,
                    ri.updated_by,
                    CONVERT(varchar, ri.updated_date, 23) AS updated_date
                FROM ReturnItem ri
                WHERE ri.return_id = @return_id AND ri.status = 'A'";

            var parameters = new SqlParameter[] { new SqlParameter("@return_id", returnId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        public int SaveSaleReturn(int saleId, decimal totalAmount, string reason, int processedBy, DataTable returnItems)
        {
            try
            {
                // Insert SaleReturn header
                string returnQuery = @"
                    INSERT INTO SaleReturn (
                        sale_id, total_amount, reason, processed_by, status, created_by, created_date
                    )
                    VALUES (
                        @sale_id, @total_amount, @reason, @processed_by, @status, @created_by, GETDATE()
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var returnParams = new SqlParameter[]
                {
                    new SqlParameter("@sale_id", saleId),
                    new SqlParameter("@total_amount", totalAmount),
                    new SqlParameter("@reason", reason ?? (object)DBNull.Value),
                    new SqlParameter("@processed_by", processedBy),
                    new SqlParameter("@status", "A"),
                    new SqlParameter("@created_by", processedBy)
                };

                DataTable result = Connection.ExecuteQuery(returnQuery, returnParams);
                if (result.Rows.Count == 0)
                    throw new Exception("Failed to create sale return record.");

                int returnId = Convert.ToInt32(result.Rows[0][0]);

                // Insert ReturnItems
                if (returnItems != null && returnItems.Rows.Count > 0)
                {
                    foreach (DataRow item in returnItems.Rows)
                    {
                        string itemQuery = @"
                            INSERT INTO ReturnItem (
                                return_id, sale_item_id, product_id, quantity, refund_amount, 
                                status, created_by, created_date
                            )
                            VALUES (
                                @return_id, @sale_item_id, @product_id, @quantity, @refund_amount,
                                @status, @created_by, GETDATE()
                            );";

                        var itemParams = new SqlParameter[]
                        {
                            new SqlParameter("@return_id", returnId),
                            new SqlParameter("@sale_item_id", item["sale_item_id"]),
                            new SqlParameter("@product_id", item["product_id"]),
                            new SqlParameter("@quantity", decimal.Parse(item["quantity"]?.ToString() ?? "0")),
                            new SqlParameter("@refund_amount", decimal.Parse(item["refund_amount"]?.ToString() ?? "0")),
                            new SqlParameter("@status", "A"),
                            new SqlParameter("@created_by", processedBy)
                        };

                        Connection.ExecuteNonQuery(itemQuery, itemParams);

                        // Update product stock quantity (add back returned quantity)
                        string updateStockQuery = @"
                            UPDATE Product
                            SET stock_quantity = stock_quantity + @quantity,
                                updated_by = @updated_by,
                                updated_date = GETDATE()
                            WHERE product_id = @product_id";

                        var stockParams = new SqlParameter[]
                        {
                            new SqlParameter("@quantity", decimal.Parse(item["quantity"]?.ToString() ?? "0")),
                            new SqlParameter("@updated_by", processedBy),
                            new SqlParameter("@product_id", item["product_id"])
                        };

                        Connection.ExecuteNonQuery(updateStockQuery, stockParams);
                    }
                }

                return returnId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving sale return: {ex.Message}", ex);
            }
        }

        public DataTable SearchSaleReturns(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetSaleReturns();
            }

            string query = @"
                SELECT 
                    sr.return_id,
                    sr.sale_id,
                    sr.total_amount,
                    sr.reason,
                    sr.processed_by,
                    sr.status,
                    sr.created_by,
                    CONVERT(varchar, sr.created_date, 23) AS created_date,
                    sr.updated_by,
                    CONVERT(varchar, sr.updated_date, 23) AS updated_date
                FROM SaleReturn sr
                LEFT JOIN Sale s ON sr.sale_id = s.sale_id
                WHERE sr.status = 'A' 
                AND (
                    CAST(sr.return_id AS NVARCHAR) LIKE @keyword 
                    OR CAST(sr.sale_id AS NVARCHAR) LIKE @keyword
                    OR s.invoice_number LIKE @keyword
                    OR sr.reason LIKE @keyword
                )
                ORDER BY sr.created_date DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return Connection.ExecuteQuery(query, parameters);
        }
    }
}
