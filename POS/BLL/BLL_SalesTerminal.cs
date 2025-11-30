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
    }
}
