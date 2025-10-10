using System;
using System.Data;
using POS.DAL;
using BCrypt.Net;

namespace POS.BLL
{
    internal class BLL_Login
    {
        private readonly DAL_Login _dalLogin = new DAL_Login();

        public bool Authenticate(string username, string password)
        {
            DataTable userTable = _dalLogin.GetUserByUsername(username);

            if (userTable.Rows.Count == 0)
            {
                return false;
            }

            string storedHash = userTable.Rows[0]["password_hash"].ToString();

            return VerifyPassword(password, storedHash);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
