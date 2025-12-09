using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL
{
    internal class DAL_Discount
    {
        public DataTable GetDiscounts()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("discount_id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("start_date", typeof(DateTime));
            dt.Columns.Add("end_date", typeof(DateTime));
            dt.Columns.Add("status", typeof(string));

            string query = @"
                SELECT 
                    promotion_id,
                    promotion_name,
                    description,
                    start_date,
                    end_date,
                    status
                FROM Promotion
                WHERE status = 'A'
                ORDER BY promotion_name";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["discount_id"] = row["promotion_id"];
                r["name"] = row["promotion_name"];
                r["description"] = row["description"];
                r["start_date"] = row["start_date"];
                r["end_date"] = row["end_date"];
                r["status"] = row["status"].ToString() == "A" ? "Active" : "Inactive";

                dt.Rows.Add(r);
            }

            return dt;
        }

        public DataTable SearchDiscounts(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("discount_id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("start_date", typeof(DateTime));
            dt.Columns.Add("end_date", typeof(DateTime));
            dt.Columns.Add("status", typeof(string));

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetDiscounts();
            }

            string query = @"
                SELECT 
                    promotion_id,
                    promotion_name,
                    description,
                    start_date,
                    end_date,
                    status
                FROM Promotion
                WHERE status = 'A' AND (promotion_name LIKE @keyword OR description LIKE @keyword)
                ORDER BY promotion_name";

            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();
                r["discount_id"] = row["promotion_id"];
                r["name"] = row["promotion_name"];
                r["description"] = row["description"];
                r["start_date"] = row["start_date"];
                r["end_date"] = row["end_date"];
                r["status"] = row["status"].ToString() == "A" ? "Active" : "Inactive";

                dt.Rows.Add(r);
            }

            return dt;
        }

        public bool InsertDiscount(string name, string description, DateTime startDate, DateTime endDate, string status)
        {
            string query = @"
                INSERT INTO Promotion (promotion_name, description, start_date, end_date, status, created_date)
                VALUES (@name, @description, @start_date, @end_date, @status, GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@name", name),
                new SqlParameter("@description", description),
                new SqlParameter("@start_date", startDate),
                new SqlParameter("@end_date", endDate),
                new SqlParameter("@status", status == "Active" ? "A" : "I")
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        #region Product Promotion Methods

        public DataTable GetProductsByPromotionID(int promotionId)
        {
            string query = @"
                SELECT
                    pp.promotion_product_id,
                    pp.promotion_id,
                    pp.product_id,
                    p.product_name,
                    p.product_code,
                    pp.promotion_type,
                    pp.discount_value
                FROM ProductPromotion pp
                JOIN Product p ON pp.product_id = p.product_id
                WHERE pp.promotion_id = @promotion_id AND pp.status = 'A'
                ORDER BY p.product_name";

            SqlParameter[] parameters = {
                new SqlParameter("@promotion_id", promotionId)
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        public bool AddProductToPromotion(int promotionId, int productId, string type, decimal value)
        {
            // Check if exists first to avoid exception or update existing
            string checkQuery = "SELECT COUNT(*) FROM ProductPromotion WHERE promotion_id = @promotion_id AND product_id = @product_id";
            SqlParameter[] checkParams = {
                new SqlParameter("@promotion_id", promotionId),
                new SqlParameter("@product_id", productId)
            };

            int count = Convert.ToInt32(Connection.ExecuteQuery(checkQuery, checkParams).Rows[0][0]);

            if (count > 0)
            {
                // Update existing
                string updateQuery = @"
                    UPDATE ProductPromotion
                    SET promotion_type = @type,
                        discount_value = @value,
                        status = 'A',
                        updated_date = GETDATE()
                    WHERE promotion_id = @promotion_id AND product_id = @product_id";

                SqlParameter[] updateParams = {
                    new SqlParameter("@promotion_id", promotionId),
                    new SqlParameter("@product_id", productId),
                    new SqlParameter("@type", type),
                    new SqlParameter("@value", value)
                };
                return Connection.ExecuteNonQuery(updateQuery, updateParams) > 0;
            }
            else
            {
                // Insert new
                string insertQuery = @"
                    INSERT INTO ProductPromotion (promotion_id, product_id, promotion_type, discount_value, status, created_date)
                    VALUES (@promotion_id, @product_id, @type, @value, 'A', GETDATE())";

                SqlParameter[] insertParams = {
                    new SqlParameter("@promotion_id", promotionId),
                    new SqlParameter("@product_id", productId),
                    new SqlParameter("@type", type),
                    new SqlParameter("@value", value)
                };
                return Connection.ExecuteNonQuery(insertQuery, insertParams) > 0;
            }
        }

        public bool RemoveProductFromPromotion(int promotionProductId)
        {
            string query = "DELETE FROM ProductPromotion WHERE promotion_product_id = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", promotionProductId)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        #endregion
    }
}
