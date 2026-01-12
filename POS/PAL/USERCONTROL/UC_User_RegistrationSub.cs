using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_User_RegistrationSub : DevExpress.XtraEditors.XtraUserControl
    {
        private const int SUPER_ADMIN_ROLE_ID = 1; // Super Admin role ID - restricted visibility
        
        private int? _editUserId = null;
        private DataTable _rolesTable;
        private DataTable _storesTable;
        private bool _isEditMode = false;

        public UC_User_RegistrationSub()
        {
            InitializeComponent();
            LoadComboboxData();
        }

        public UC_User_RegistrationSub(int userId) : this()
        {
            _editUserId = userId;
            _isEditMode = true;
            LoadUserData();
        }

        /// <summary>
        /// Loads data for comboboxes (roles and stores)
        /// </summary>
        private void LoadComboboxData()
        {
            try
            {
                // Load Roles
                _rolesTable = BLL_Role.GetRoles();
                comboboxUserRole.Properties.Items.Clear();
                
                // Check if current user is a super admin
                bool isCurrentUserSuperAdmin = PermissionManager.IsSuperAdmin();
                
                foreach (DataRow row in _rolesTable.Rows)
                {
                    int roleId = Convert.ToInt32(row["role_id"]);
                    string roleName = row["role_name"].ToString();
                    
                    // Only show Super Admin role if the current user is a super admin
                    if (roleId == SUPER_ADMIN_ROLE_ID && !isCurrentUserSuperAdmin)
                    {
                        continue; // Skip adding Super Admin role for non-super admin users
                    }
                    
                    comboboxUserRole.Properties.Items.Add(roleName);
                }

                // Load Stores
                _storesTable = new BLL_Store().GetStores();
                comboboxStore.Properties.Items.Clear();
                
                foreach (DataRow row in _storesTable.Rows)
                {
                    comboboxStore.Properties.Items.Add(row["store_name"].ToString());
                }

                // Set default store to current user's store
                if (Main.DataSetApp?.Store != null && Main.DataSetApp.Store.Rows.Count > 0)
                {
                    string currentStoreName = Main.DataSetApp.Store[0]["store_name"]?.ToString();
                    if (!string.IsNullOrEmpty(currentStoreName))
                    {
                        comboboxStore.EditValue = currentStoreName;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading combobox data: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Loads user data for editing
        /// </summary>
        private void LoadUserData()
        {
            try
            {
                if (!_editUserId.HasValue)
                    return;

                DataTable userData = BLL_User.GetUserById(_editUserId.Value);
                if (userData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow user = userData.Rows[0];

                // Populate fields
                txtfullName.Text = user["full_name"]?.ToString();
                txtUsername.Text = user["username"]?.ToString();
                txtUEmail.Text = user["email"]?.ToString();
                txtUPhoneNumber.Text = user["phone"]?.ToString();
                txtPin.Text = user["pin_code"]?.ToString();

                // Set role combobox
                if (user["role_name"] != DBNull.Value)
                {
                    string roleName = user["role_name"].ToString();
                    
                    // Check if the role is in the combobox items (it may have been filtered out)
                    if (comboboxUserRole.Properties.Items.Contains(roleName))
                    {
                        comboboxUserRole.EditValue = roleName;
                    }
                    else
                    {
                        // Role is not available in the dropdown (e.g., Super Admin role for non-super admin users)
                        // Show a message and disable role selection
                        comboboxUserRole.EditValue = roleName;
                        comboboxUserRole.Enabled = false;
                        
                        XtraMessageBox.Show(
                            $"The role '{roleName}' is not available for modification. Only Super Admin users can modify this role assignment.",
                            "Role Restriction",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }

                // Set store combobox
                if (user["store_name"] != DBNull.Value)
                {
                    string storeName = user["store_name"].ToString();
                    comboboxStore.EditValue = storeName;
                }

                // Disable password fields in edit mode
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                txtPassword.Text = "********";
                txtConfirmPassword.Text = "********";

                // Update button text
                regBtn.Text = "Update";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading user data: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_User_Management());
        }

        /// <summary>
        /// Handles Register/Update button click
        /// </summary>
        private void regBtn_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
                UpdateUser();
            else
                RegisterUser();
        }

        /// <summary>
        /// Validates user input
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtfullName.Text))
            {
                XtraMessageBox.Show("Full name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtfullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                XtraMessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUEmail.Text))
            {
                XtraMessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUEmail.Focus();
                return false;
            }

            // Validate store selection
            if (comboboxStore.EditValue == null || string.IsNullOrEmpty(comboboxStore.EditValue.ToString()))
            {
                XtraMessageBox.Show("Please select a store.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboboxStore.Focus();
                return false;
            }

            if (!_isEditMode)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    XtraMessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    XtraMessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the selected store ID from the combobox
        /// </summary>
        private int GetSelectedStoreId()
        {
            if (comboboxStore.EditValue != null && !string.IsNullOrEmpty(comboboxStore.EditValue.ToString()))
            {
                string selectedStoreName = comboboxStore.EditValue.ToString();
                DataRow[] storeRows = _storesTable.Select($"store_name = '{selectedStoreName.Replace("'", "''")}'");
                if (storeRows.Length > 0)
                {
                    return Convert.ToInt32(storeRows[0]["store_id"]);
                }
            }

            // Fallback to current user's store if selection fails
            if (Main.DataSetApp?.Store != null && Main.DataSetApp.Store.Rows.Count > 0)
            {
                return Convert.ToInt32(Main.DataSetApp.Store[0]["store_id"]);
            }

            return 1; // Default fallback
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        private void RegisterUser()
        {
            try
            {
                if (!ValidateInput())
                    return;

                // Get current user ID
                int currentUserId = 1;
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Get store ID from combobox selection
                int storeId = GetSelectedStoreId();

                // Get role ID from combobox
                int? roleId = null;
                if (comboboxUserRole.EditValue != null && !string.IsNullOrEmpty(comboboxUserRole.EditValue.ToString()))
                {
                    string selectedRoleName = comboboxUserRole.EditValue.ToString();
                    DataRow[] roleRows = _rolesTable.Select($"role_name = '{selectedRoleName.Replace("'", "''")}'");
                    if (roleRows.Length > 0)
                    {
                        roleId = Convert.ToInt32(roleRows[0]["role_id"]);
                        
                        // Validate: Only super admin can assign Super Admin role
                        if (roleId == SUPER_ADMIN_ROLE_ID && !PermissionManager.IsSuperAdmin())
                        {
                            XtraMessageBox.Show(
                                "Only Super Admin users can assign the Super Admin role to other users.",
                                "Permission Denied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }
                }

                // Insert user
                int userId = BLL_User.InsertUser(
                    storeId,
                    roleId,
                    txtfullName.Text.Trim(),
                    txtUsername.Text.Trim(),
                    txtUEmail.Text.Trim(),
                    txtUPhoneNumber.Text.Trim(),
                    txtPassword.Text,
                    txtPin.Text.Trim(),
                    false, // is_super_admin
                    currentUserId
                );

                if (userId > 0)
                {
                    XtraMessageBox.Show(
                        "User registered successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to user management
                    Main.Instance.LoadUserControl(new UC_User_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to register user.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error registering user: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        private void UpdateUser()
        {
            try
            {
                if (!ValidateInput())
                    return;

                if (!_editUserId.HasValue)
                    return;

                // Get current user ID
                int currentUserId = 1;
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Get store ID from combobox selection
                int storeId = GetSelectedStoreId();

                // Get role ID from combobox
                int? roleId = null;
                if (comboboxUserRole.EditValue != null && !string.IsNullOrEmpty(comboboxUserRole.EditValue.ToString()))
                {
                    string selectedRoleName = comboboxUserRole.EditValue.ToString();
                    DataRow[] roleRows = _rolesTable.Select($"role_name = '{selectedRoleName.Replace("'", "''")}'");
                    if (roleRows.Length > 0)
                    {
                        roleId = Convert.ToInt32(roleRows[0]["role_id"]);
                        
                        // Validate: Only super admin can assign Super Admin role
                        if (roleId == SUPER_ADMIN_ROLE_ID && !PermissionManager.IsSuperAdmin())
                        {
                            XtraMessageBox.Show(
                                "Only Super Admin users can assign the Super Admin role to other users.",
                                "Permission Denied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }
                    }
                }

                // Update user
                bool success = BLL_User.UpdateUser(
                    _editUserId.Value,
                    storeId,
                    roleId,
                    txtfullName.Text.Trim(),
                    txtUsername.Text.Trim(),
                    txtUEmail.Text.Trim(),
                    txtUPhoneNumber.Text.Trim(),
                    txtPin.Text.Trim(),
                    currentUserId
                );

                if (success)
                {
                    XtraMessageBox.Show(
                        "User updated successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to user management
                    Main.Instance.LoadUserControl(new UC_User_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to update user.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating user: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
