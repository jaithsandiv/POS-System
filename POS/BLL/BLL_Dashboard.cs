using POS.DAL;
using System;
using System.Data;

namespace POS.BLL
{
    internal class BLL_Dashboard
    {
        private readonly DAL_Dashboard _dal = new DAL_Dashboard();

        public DataTable GetKpis(DateTime fromDate, DateTime toDate)
        {
            return _dal.GetDashboardKpis(fromDate, toDate);
        }

        public DataTable GetSalesTrend(DateTime fromDate, DateTime toDate)
        {
            return _dal.GetSalesTrend(fromDate, toDate);
        }

        public DataTable GetTopProducts(DateTime fromDate, DateTime toDate)
        {
            return _dal.GetTopProducts(fromDate, toDate);
        }
    }
}
