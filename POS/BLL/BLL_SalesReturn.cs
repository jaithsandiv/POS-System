using POS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BLL
{
    internal class BLL_SalesReturn
    {
        private readonly DAL_SalesReturn _dalSalesReturn = new DAL_SalesReturn();

        public DataTable GetSaleReturns()
        {
            return _dalSalesReturn.GetSaleReturns();
        }

        public DataTable GetSaleReturnById(int returnId)
        {
            if (returnId <= 0)
                throw new ArgumentException("Return ID must be greater than zero.");

            return _dalSalesReturn.GetSaleReturnById(returnId);
        }

        public DataTable GetReturnItems(int returnId)
        {
            if (returnId <= 0)
                throw new ArgumentException("Return ID must be greater than zero.");

            return _dalSalesReturn.GetReturnItems(returnId);
        }

        public int SaveSaleReturn(int saleId, decimal totalAmount, string reason, int processedBy, DataTable returnItems)
        {
            if (saleId <= 0)
                throw new ArgumentException("Sale ID must be greater than zero.");

            if (totalAmount < 0)
                throw new ArgumentException("Total amount cannot be negative.");

            if (processedBy <= 0)
                throw new ArgumentException("Processed by user ID must be greater than zero.");

            if (returnItems == null || returnItems.Rows.Count == 0)
                throw new ArgumentException("Return items cannot be empty.");

            return _dalSalesReturn.SaveSaleReturn(saleId, totalAmount, reason, processedBy, returnItems);
        }

        public DataTable SearchSaleReturns(string keyword)
        {
            return _dalSalesReturn.SearchSaleReturns(keyword);
        }
    }
}
