using POS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BLL
{
    internal class BLL_Discount
    {
        private readonly DAL_Discount _dalDiscount = new DAL_Discount();

        public DataTable GetDiscounts()
        {
            return _dalDiscount.GetDiscounts();
        }

        public bool InsertDiscount(string name, string type, decimal value, DateTime startDate, DateTime endDate, string status)
        {
            return _dalDiscount.InsertDiscount(name, type, value, startDate, endDate, status);
        }
    }
}
