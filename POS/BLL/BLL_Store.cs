using POS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BLL
{
    internal class BLL_Store
    {
        private readonly DAL_Store _dalStore = new DAL_Store();

        public DataTable GetStores()
        {
            return _dalStore.GetStores();
        }

        public int InsertStore(string name, string phone, string email, string address, string city, string state, string country, string postalCode, int createdBy)
        {
            return _dalStore.InsertStore(name, phone, email, address, city, state, country, postalCode, createdBy);
        }

        public bool UpdateStore(int storeId, string name, string phone, string email, string address, string city, string state, string country, string postalCode, int updatedBy)
        {
            return _dalStore.UpdateStore(storeId, name, phone, email, address, city, state, country, postalCode, updatedBy);
        }

        public bool DeleteStore(int storeId, int updatedBy)
        {
            return _dalStore.DeleteStore(storeId, updatedBy);
        }
    }
}
