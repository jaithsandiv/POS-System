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
                return false;

            var userRow = userTable.Rows[0];
            if (!BCrypt.Net.BCrypt.Verify(password, userRow["password_hash"].ToString()))
                return false;

            var businessRow = Main.DataSetApp?.Business?[0];
            if (businessRow != null)
            {
                DateTime trialEnd;
                bool trialExpired = DateTime.TryParse(businessRow["trial_end_date"].ToString(), out trialEnd) && trialEnd < DateTime.Now;
                bool isLicensed = businessRow["is_licensed"].ToString() == "True";
                bool isSuperAdmin = userRow["is_super_admin"].ToString() == "True";

                if (trialExpired && !isLicensed && !isSuperAdmin)
                    return false;
            }

            Main.DataSetApp.User.Clear();
            Main.DataSetApp.User.ImportRow(userRow);

            string roleId = userRow["role_id"].ToString();
            DataTable rolePermissions = _dalLogin.GetRolePermissionsByRoleId(roleId);
            Main.DataSetApp.RolePermission.Clear();
            foreach (DataRow rolePermissionRow in rolePermissions.Rows)
            {
                Main.DataSetApp.RolePermission.ImportRow(rolePermissionRow);
            }
    
            string storeId = userRow["store_id"].ToString();
            DataTable storeData = _dalLogin.GetStoreByStoreId(storeId);
            Main.DataSetApp.Store.Clear();
            foreach (DataRow storeRow in storeData.Rows)
            {
                Main.DataSetApp.Store.ImportRow(storeRow);
            }

            return true;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
