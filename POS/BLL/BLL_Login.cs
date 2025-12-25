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

            // Check licensing and trial period restrictions
            var businessRow = Main.DataSetApp?.Business?[0];
            if (businessRow != null)
            {
                bool isSuperAdmin = userRow["is_super_admin"].ToString() == "True";
                
                // Super admin can always login (for support/maintenance)
                if (!isSuperAdmin)
                {
                    // Check if system is licensed
                    bool isLicensed = false;
                    if (!businessRow.Isis_licensedNull())
                    {
                        string licensedValue = businessRow.is_licensed?.ToString();
                        isLicensed = licensedValue == "True" || licensedValue == "1";
                    }
                    
                    // If licensed, allow login
                    if (!isLicensed)
                    {
                        // Not licensed - check trial period
                        bool hasValidTrial = false;
                        
                        // Check if trial dates are set
                        if (!businessRow.Istrial_start_dateNull() && !businessRow.Istrial_end_dateNull())
                        {
                            string startDateStr = businessRow.trial_start_date?.ToString();
                            string endDateStr = businessRow.trial_end_date?.ToString();
                            
                            if (!string.IsNullOrWhiteSpace(startDateStr) && !string.IsNullOrWhiteSpace(endDateStr))
                            {
                                if (DateTime.TryParse(startDateStr, out DateTime trialStart) && 
                                    DateTime.TryParse(endDateStr, out DateTime trialEnd))
                                {
                                    DateTime now = DateTime.Now;
                                    // Trial is valid if current date is between start and end dates
                                    hasValidTrial = now >= trialStart && now <= trialEnd;
                                }
                            }
                        }
                        
                        // Block login if no valid trial and not licensed
                        if (!hasValidTrial)
                        {
                            return false;
                        }
                    }
                }
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
