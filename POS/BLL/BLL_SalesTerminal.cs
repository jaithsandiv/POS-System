using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.DAL;
using System.Data;

namespace POS.BLL
{
    internal class BLL_SalesTerminal
    {
        private readonly DAL_SalesTerminal _dalSalesTerminal = new DAL_SalesTerminal();
        private readonly BLL_SystemLog _logManager = new BLL_SystemLog();

        public DataTable GetCategories()
        {
            return _dalSalesTerminal.GetCategories();
        }

        public DataTable GetBrands()
        {
            return _dalSalesTerminal.GetBrands();
        }

        public DataTable GetProducts()
        {
            return _dalSalesTerminal.GetProducts();
        }

        public DataTable GetStaffPin()
        {
            return _dalSalesTerminal.GetStaffPin();
        }

        public DataTable GetCustomers()
        {
            return _dalSalesTerminal.GetCustomers();
        }

        public DataTable GetTables()
        {
            return _dalSalesTerminal.GetTables();
        }

        public DataTable GetAvailableTables()
        {
            return _dalSalesTerminal.GetAvailableTables();
        }

        public bool IsKotEnabled()
        {
            return _dalSalesTerminal.IsKotEnabled();
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
                int resultSaleId = _dalSalesTerminal.SaveSale(storeId, billerId, customerId, saleType, 
                    discountType, discountValue, totalAmount, totalItems, grandTotal, notes, saleItems, 
                    totalPaid, changeDue, invoiceNumber, quotationNumber, orderType, tableNumber, saleId);

                // Log based on sale type
                string logMessage = saleId > 0 
                    ? $"{saleType} updated - Sale ID: {resultSaleId}, Total: LKR {grandTotal:N2}, Items: {totalItems}"
                    : $"{saleType} created - Sale ID: {resultSaleId}, Total: LKR {grandTotal:N2}, Items: {totalItems}";

                if (!string.IsNullOrEmpty(invoiceNumber))
                    logMessage += $", Invoice: {invoiceNumber}";
                if (!string.IsNullOrEmpty(quotationNumber))
                    logMessage += $", Quotation: {quotationNumber}";
                if (!string.IsNullOrEmpty(tableNumber))
                    logMessage += $", Table: {tableNumber}";

                string logSource = saleType == "DRAFT" ? "DRAFT" : 
                                  saleType == "QUOTATION" ? "QUOTATION" : 
                                  saleType == "CREDIT_SALE" ? "CREDIT_SALE" : "SALE";

                _logManager.LogInfo(
                    source: logSource,
                    message: logMessage,
                    referenceId: resultSaleId,
                    userId: billerId
                );

                return resultSaleId;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "SALE",
                    ex: ex,
                    referenceId: saleId > 0 ? saleId : (int?)null,
                    userId: billerId
                );
                throw;
            }
        }

        public string GetNextQuotationNumber()
        {
            return _dalSalesTerminal.GetNextQuotationNumber();
        }

        public string GetNextInvoiceNumber()
        {
            return _dalSalesTerminal.GetNextInvoiceNumber();
        }

        public void SavePayments(int saleId, DataTable payments, int createdBy)
        {
            try
            {
                _dalSalesTerminal.SavePayments(saleId, payments, createdBy);

                // Log payment details
                decimal totalPaid = 0;
                var paymentMethods = new List<string>();

                foreach (DataRow payment in payments.Rows)
                {
                    string method = payment["payment_method"]?.ToString();
                    decimal amount = 0;
                    if (payment["amount"] != DBNull.Value)
                        decimal.TryParse(payment["amount"].ToString(), out amount);

                    if (amount > 0 && !string.IsNullOrWhiteSpace(method))
                    {
                        totalPaid += amount;
                        paymentMethods.Add($"{method}: LKR {amount:N2}");
                    }
                }

                string paymentDetails = string.Join(", ", paymentMethods);
                _logManager.LogInfo(
                    source: "PAYMENT",
                    message: $"Payment processed - Sale ID: {saleId}, Total Paid: LKR {totalPaid:N2}, Methods: {paymentDetails}",
                    referenceId: saleId,
                    userId: createdBy
                );
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "PAYMENT",
                    ex: ex,
                    referenceId: saleId,
                    userId: createdBy
                );
                throw;
            }
        }

        public DataTable GetSales(string saleType)
        {
            return _dalSalesTerminal.GetSales(saleType);
        }

        public DataTable SearchSales(string saleType, string keyword)
        {
            return _dalSalesTerminal.SearchSales(saleType, keyword);
        }

        public DataTable GetSale(int saleId)
        {
            return _dalSalesTerminal.GetSale(saleId);
        }

        public DataTable GetSaleItems(int saleId)
        {
            return _dalSalesTerminal.GetSaleItems(saleId);
        }

        public DataTable GetSalePayments(int saleId)
        {
            return _dalSalesTerminal.GetSalePayments(saleId);
        }

        /// <summary>
        /// Gets table sales report - total sales grouped by table number
        /// </summary>
        public DataTable GetTableSalesReport()
        {
            return _dalSalesTerminal.GetTableSalesReport();
        }

        /// <summary>
        /// Searches table sales report by keyword
        /// </summary>
        public DataTable SearchTableSalesReport(string keyword)
        {
            return _dalSalesTerminal.SearchTableSalesReport(keyword);
        }

        /// <summary>
        /// Gets sale payments report - all sales with payment details
        /// </summary>
        public DataTable GetSalePayments()
        {
            return _dalSalesTerminal.GetSalePayments();
        }

        /// <summary>
        /// Searches sale payments report by keyword
        /// </summary>
        public DataTable SearchSalePayments(string keyword)
        {
            return _dalSalesTerminal.SearchSalePayments(keyword);
        }

        /// <summary>
        /// Gets product sales report - all sold products with customer and payment details
        /// </summary>
        public DataTable GetProductSalesReport()
        {
            return _dalSalesTerminal.GetProductSalesReport();
        }

        /// <summary>
        /// Searches product sales report by keyword
        /// </summary>
        public DataTable SearchProductSalesReport(string keyword)
        {
            return _dalSalesTerminal.SearchProductSalesReport(keyword);
        }

        /// <summary>
        /// Gets items report - all sold products with purchase and customer details
        /// </summary>
        public DataTable GetItemsReport()
        {
            return _dalSalesTerminal.GetItemsReport();
        }

        /// <summary>
        /// Searches items report by keyword
        /// </summary>
        public DataTable SearchItemsReport(string keyword)
        {
            return _dalSalesTerminal.SearchItemsReport(keyword);
        }

        /// <summary>
        /// Gets trending products report - products ordered by total quantity sold
        /// </summary>
        public DataTable GetTrendingProducts()
        {
            return _dalSalesTerminal.GetTrendingProducts();
        }

        /// <summary>
        /// Gets sales representative report - all sales with payment breakdown
        /// </summary>
        public DataTable GetSalesRepresentativeReport()
        {
            return _dalSalesTerminal.GetSalesRepresentativeReport();
        }

        /// <summary>
        /// Searches sales representative report by keyword in all columns
        /// </summary>
        public DataTable SearchSalesRepresentativeReport(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetSalesRepresentativeReport();
            }

            return _dalSalesTerminal.SearchSalesRepresentativeReport(keyword.Trim());
        }
    }
}
