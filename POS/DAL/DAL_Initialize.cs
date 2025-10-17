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
    internal class DAL_Initialize
    {
        

        public DAL_Initialize()
        {
        }

        internal DataTable GetBusiness()
        {
            DAL_DS_Initialize ds = new DAL_DS_Initialize();
            DataTable dt = ds.Business;
            dt.Clear();

            string query = @"
                    SELECT 
                        business_id, 
                        business_name, 
                        logo, 
                        CONVERT(varchar, trial_start_date, 23) AS trial_start_date,
                        CONVERT(varchar, trial_end_date, 23) AS trial_end_date,
                        is_licensed,
                        status, 
                        created_by, 
                        CONVERT(varchar, created_date, 23) AS created_date,
                        updated_by, 
                        CONVERT(varchar, updated_date, 23) AS updated_date
                    FROM Business";

            DataTable result = Connection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                DataRow r = dt.NewRow();

                r["business_id"] = row["business_id"]?.ToString();
                r["business_name"] = row["business_name"]?.ToString();
                r["logo"] = row["logo"] is DBNull ? null : (byte[])row["logo"];
                r["trial_start_date"] = row["trial_start_date"]?.ToString();
                r["trial_end_date"] = row["trial_end_date"]?.ToString();
                r["is_licensed"] = row["is_licensed"]?.ToString();
                r["status"] = row["status"]?.ToString();
                r["created_by"] = row["created_by"]?.ToString();
                r["created_date"] = row["created_date"]?.ToString();
                r["updated_by"] = row["updated_by"]?.ToString();
                r["updated_date"] = row["updated_date"]?.ToString();

                dt.Rows.Add(r);
            }

            return dt;
        }
    }
}
