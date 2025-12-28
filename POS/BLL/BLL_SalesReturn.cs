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
        private readonly BLL_SystemLog _logManager = new BLL_SystemLog();

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

            try
            {
                int returnId = _dalSalesReturn.SaveSaleReturn(saleId, totalAmount, reason, processedBy, returnItems);

                // Log return details
                int itemCount = returnItems.Rows.Count;
                string returnReason = string.IsNullOrEmpty(reason) ? "No reason provided" : reason;

                _logManager.LogAudit(
                    source: "RETURN",
                    message: $"Sale return processed - Return ID: {returnId}, Sale ID: {saleId}, Amount: LKR {totalAmount:N2}, Items: {itemCount}, Reason: {returnReason}",
                    referenceId: returnId,
                    userId: processedBy
                );

                return returnId;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "RETURN",
                    ex: ex,
                    referenceId: saleId,
                    userId: processedBy
                );
                throw;
            }
        }

        public DataTable SearchSaleReturns(string keyword)
        {
            return _dalSalesReturn.SearchSaleReturns(keyword);
        }
    }
}
