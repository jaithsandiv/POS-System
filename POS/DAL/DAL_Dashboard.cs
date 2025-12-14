using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_Dashboard
    {
        public DataTable GetDashboardKpis(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC usp_GetDashboardKpis @FromDate, @ToDate";
            SqlParameter[] parameters = {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };
            return Connection.ExecuteQuery(query, parameters);
        }

        public DataTable GetSalesTrend(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC usp_GetDashboardSalesTrend @FromDate, @ToDate";
            SqlParameter[] parameters = {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };
            return Connection.ExecuteQuery(query, parameters);
        }

        public DataTable GetTopProducts(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC usp_GetDashboardTopProducts @FromDate, @ToDate";
            SqlParameter[] parameters = {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            };
            return Connection.ExecuteQuery(query, parameters);
        }
    }
}
