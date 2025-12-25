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
            return _dalSalesTerminal.SaveSale(storeId, billerId, customerId, saleType, discountType, 
                discountValue, totalAmount, totalItems, grandTotal, notes, saleItems, 
                totalPaid, changeDue, invoiceNumber, quotationNumber, orderType, tableNumber, saleId);
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
            _dalSalesTerminal.SavePayments(saleId, payments, createdBy);
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
    }
}
