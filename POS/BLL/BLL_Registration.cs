using POS.DAL;
using POS.DAL.DataSource;
using System;

namespace POS.BLL
{
    internal class BLL_Registration
    {
        private readonly DAL_Registration _dalRegistration = new DAL_Registration();

        public bool RegisterComplete(
            DAL_DS_Initialize.BusinessRow businessRow,
            DAL_DS_Initialize.StoreRow storeRow,
            DAL_DS_Initialize.UserRow userRow,
            out string errorMessage)
        {
            // Hash the password before saving
            userRow.password_hash = BCrypt.Net.BCrypt.HashPassword(userRow.password_hash);

            // Call DAL to insert data
            return _dalRegistration.RegisterComplete(businessRow, storeRow, userRow, out errorMessage);
        }
    }
}
