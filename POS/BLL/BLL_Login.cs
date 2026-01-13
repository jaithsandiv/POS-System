using System;
using System.Data;
using POS.DAL;
using BCrypt.Net;

namespace POS.BLL
{
    internal class BLL_Login
    {
        private readonly DAL_Login _dalLogin = new DAL_Login();
        private readonly BLL_SystemLog _logManager = new BLL_SystemLog();

        public bool Authenticate(string username, string password)
        {
            try
            {
                DataTable userTable = _dalLogin.GetUserByUsername(username);
                if (userTable.Rows.Count == 0)
                {
                    _logManager.LogWarning(
                        source: "LOGIN",
                        message: $"Failed login attempt - Username not found: {username}",
                        referenceId: null,
                        userId: null
                    );
                    return false;
                }

                var userRow = userTable.Rows[0];
                if (!BCrypt.Net.BCrypt.Verify(password, userRow["password_hash"].ToString()))
                {
                    _logManager.LogWarning(
                        source: "LOGIN",
                        message: $"Failed login attempt - Invalid password for username: {username}",
                        referenceId: null,
                        userId: null
                    );
                    return false;
                }

                // Check licensing and trial period restrictions
                // Note: Users can now login even if trial expired, but with restricted access
                var businessRow = Main.DataSetApp?.Business?[0];
                if (businessRow != null)
                {
                    bool isSuperAdmin = userRow["is_super_admin"].ToString() == "True";
                    
                    // Get trial status
                    var trialStatus = BLL_TrialManager.GetTrialStatus();
                    
                    // Log trial status for audit
                    if (!trialStatus.IsLicensed && trialStatus.IsTrialExpired)
                    {
                        _logManager.LogWarning(
                            source: "LOGIN",
                            message: $"User '{username}' logged in with expired trial - Restricted access mode",
                            referenceId: null,
                            userId: null
                        );
                    }
                    else if (!trialStatus.IsLicensed && trialStatus.IsExpiringSoon)
                    {
                        _logManager.LogInfo(
                            source: "LOGIN",
                            message: $"User '{username}' logged in - Trial expiring in {trialStatus.DaysRemaining} day(s)",
                            referenceId: null,
                            userId: null
                        );
                    }
                    
                    // NOTE: We no longer block login based on trial status
                    // Users can login even with expired trial, but will have restricted access
                    // The Main form will enforce navigation restrictions
                }

                // Load user data into dataset
                Main.DataSetApp.User.Clear();
                Main.DataSetApp.User.ImportRow(userRow);

                // Load role permissions into dataset
                string roleId = userRow["role_id"].ToString();
                DataTable rolePermissions = _dalLogin.GetRolePermissionsByRoleId(roleId);
                Main.DataSetApp.RolePermission.Clear();
                foreach (DataRow rolePermissionRow in rolePermissions.Rows)
                {
                    Main.DataSetApp.RolePermission.ImportRow(rolePermissionRow);
                }
        
                // Load store data into dataset
                string storeId = userRow["store_id"].ToString();
                DataTable storeData = _dalLogin.GetStoreByStoreId(storeId);
                Main.DataSetApp.Store.Clear();
                foreach (DataRow storeRow in storeData.Rows)
                {
                    Main.DataSetApp.Store.ImportRow(storeRow);
                }

                // Initialize permission manager with user's permissions
                PermissionManager.LoadUserPermissions();

                // Log successful login
                int userId = Convert.ToInt32(userRow["user_id"]);
                _logManager.LogAudit(
                    source: "LOGIN",
                    message: $"User '{username}' logged in successfully",
                    referenceId: null,
                    userId: userId
                );

                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "LOGIN",
                    ex: ex,
                    referenceId: null,
                    userId: null
                );
                throw;
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
