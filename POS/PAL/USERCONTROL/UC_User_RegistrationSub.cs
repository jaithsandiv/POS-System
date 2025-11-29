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
                
                foreach (DataRow row in _rolesTable.Rows)
                {
                    comboboxUserRole.Properties.Items.Add(row["role_name"].ToString());
                }

                // Load Stores
                _storesTable = new BLL_Store().GetStores();
                // Note: Add a store combobox in the Designer if needed
                // For now, we'll use the logged-in user's store as default
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
                    comboboxUserRole.EditValue = roleName;
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

                // Get store ID from logged-in user
                int storeId = 1;
                if (Main.DataSetApp?.Store != null && Main.DataSetApp.Store.Rows.Count > 0)
                {
                    storeId = Convert.ToInt32(Main.DataSetApp.Store[0]["store_id"]);
                }

                // Get role ID from combobox
                int? roleId = null;
                if (comboboxUserRole.EditValue != null && !string.IsNullOrEmpty(comboboxUserRole.EditValue.ToString()))
                {
                    string selectedRoleName = comboboxUserRole.EditValue.ToString();
                    DataRow[] roleRows = _rolesTable.Select($"role_name = '{selectedRoleName.Replace("'", "''")}'");
                    if (roleRows.Length > 0)
                    {
                        roleId = Convert.ToInt32(roleRows[0]["role_id"]);
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

                // Get store ID from logged-in user
                int storeId = 1;
                if (Main.DataSetApp?.Store != null && Main.DataSetApp.Store.Rows.Count > 0)
                {
                    storeId = Convert.ToInt32(Main.DataSetApp.Store[0]["store_id"]);
                }

                // Get role ID from combobox
                int? roleId = null;
                if (comboboxUserRole.EditValue != null && !string.IsNullOrEmpty(comboboxUserRole.EditValue.ToString()))
                {
                    string selectedRoleName = comboboxUserRole.EditValue.ToString();
                    DataRow[] roleRows = _rolesTable.Select($"role_name = '{selectedRoleName.Replace("'", "''")}'");
                    if (roleRows.Length > 0)
                    {
                        roleId = Convert.ToInt32(roleRows[0]["role_id"]);
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
