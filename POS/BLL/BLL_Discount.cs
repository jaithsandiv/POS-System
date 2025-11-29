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

        public bool InsertDiscount(string name, string description, DateTime startDate, DateTime endDate, string status)
        {
            return _dalDiscount.InsertDiscount(name, description, startDate, endDate, status);
        }

        public DataTable GetProductsByPromotionID(int promotionId)
        {
            return _dalDiscount.GetProductsByPromotionID(promotionId);
        }

        public bool AddProductToPromotion(int promotionId, int productId, string type, decimal value)
        {
            return _dalDiscount.AddProductToPromotion(promotionId, productId, type, value);
        }

        public bool RemoveProductFromPromotion(int promotionProductId)
        {
            return _dalDiscount.RemoveProductFromPromotion(promotionProductId);
        }
    }
}
