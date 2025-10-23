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
    }
}
