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
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("value", typeof(decimal));
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
                
                // Parse description for type and value
                string desc = row["description"]?.ToString() ?? "";
                string type = "Percentage";
                decimal value = 0;
                
                if (!string.IsNullOrEmpty(desc) && desc.Contains(":"))
                {
                    var parts = desc.Split(':');
                    if (parts.Length == 2)
                    {
                        type = parts[0];
                        decimal.TryParse(parts[1], out value);
                    }
                }

                r["type"] = type;
                r["value"] = value;
                r["start_date"] = row["start_date"];
                r["end_date"] = row["end_date"];
                r["status"] = row["status"].ToString() == "A" ? "Active" : "Inactive";

                dt.Rows.Add(r);
            }

            return dt;
        }

        public bool InsertDiscount(string name, string type, decimal value, DateTime startDate, DateTime endDate, string status)
        {
            string description = $"{type}:{value}";
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
    }
}
