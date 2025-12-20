using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.BLL;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Profile_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private int _currentUserId;

        public UC_Profile_Management()
        {
            InitializeComponent();
            LoadUserProfile();
            WireUpEventHandlers();
        }

        /// <summary>
        /// Wires up event handlers for buttons
        /// </summary>
        private void WireUpEventHandlers()
        {
            if (btnUpdatePassword != null)
                btnUpdatePassword.Click += btnUpdatePassword_Click;

            if (btnUpdateUser != null)
                btnUpdateUser.Click += btnUpdateUser_Click;

            if (btnUpdatePIN != null)
                btnUpdatePIN.Click += btnUpdatePIN_Click;
        }

        /// <summary>
        /// Loads current user profile data
        /// </summary>
        private void LoadUserProfile()
        {
            try
            {
                // Get current user ID from DataSetApp
                if (Main.DataSetApp?.User == null || Main.DataSetApp.User.Rows.Count == 0)
                {
                    XtraMessageBox.Show("User session not found. Please login again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);

                // Fetch user details
                DataTable userData = BLL_User.GetUserById(_currentUserId);
                if (userData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("User profile not found.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow user = userData.Rows[0];

                // Populate profile fields
                txtfullName.Text = user["full_name"]?.ToString();
                txtUsername.Text = user["username"]?.ToString();
                txtUEmail.Text = user["email"]?.ToString();
                txtUPhoneNumber.Text = user["phone"]?.ToString();

                // Set current PIN (masked for security)
                string currentPin = user["pin_code"]?.ToString();
                if (!string.IsNullOrEmpty(currentPin))
                {
                    txtPin.Text = "****"; // Mask current PIN
                }
                else
                {
                    txtPin.Text = string.Empty;
                }

                // Make username read-only (usually not editable)
                txtUsername.Properties.ReadOnly = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading profile: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles Change Password button click
        /// </summary>
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
                {
                    XtraMessageBox.Show("Please enter your current password.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCurrentPassword.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    XtraMessageBox.Show("Please enter a new password.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    XtraMessageBox.Show("Please confirm your new password.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return;
                }

                // Validate password match
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    XtraMessageBox.Show("New password and confirmation do not match.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return;
                }

                // Validate password strength (minimum 6 characters)
                if (txtPassword.Text.Length < 6)
                {
                    XtraMessageBox.Show("Password must be at least 6 characters long.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Verify current password
                if (!BLL_User.VerifyCurrentPassword(_currentUserId, txtCurrentPassword.Text))
                {
                    XtraMessageBox.Show("Current password is incorrect.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCurrentPassword.Focus();
                    return;
                }

                // Update password
                bool success = BLL_User.UpdateUserPassword(_currentUserId, txtPassword.Text, _currentUserId);

                if (success)
                {
                    XtraMessageBox.Show("Password updated successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear password fields
                    txtCurrentPassword.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtConfirmPassword.Text = string.Empty;
                }
                else
                {
                    XtraMessageBox.Show("Failed to update password. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error updating password: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles Edit Profile button click
        /// </summary>
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtfullName.Text))
                {
                    XtraMessageBox.Show("Full name is required.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtfullName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUEmail.Text))
                {
                    XtraMessageBox.Show("Email is required.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUEmail.Focus();
                    return;
                }

                // Get current user data from DataSetApp
                DataRow currentUser = Main.DataSetApp.User[0];
                int storeId = Convert.ToInt32(currentUser["store_id"]);
                int? roleId = currentUser["role_id"] != DBNull.Value ? 
                    (int?)Convert.ToInt32(currentUser["role_id"]) : null;
                string pinCode = currentUser["pin_code"] != DBNull.Value ? 
                    currentUser["pin_code"].ToString() : null;

                // Update user profile (excluding password and PIN)
                bool success = BLL_User.UpdateUser(
                    _currentUserId,
                    storeId,
                    roleId,
                    txtfullName.Text.Trim(),
                    txtUsername.Text.Trim(),
                    txtUEmail.Text.Trim(),
                    txtUPhoneNumber.Text.Trim(),
                    pinCode,
                    _currentUserId
                );

                if (success)
                {
                    XtraMessageBox.Show("Profile updated successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update the DataSetApp with new values
                    currentUser["full_name"] = txtfullName.Text.Trim();
                    currentUser["email"] = txtUEmail.Text.Trim();
                    currentUser["phone"] = txtUPhoneNumber.Text.Trim();

                    // Update user name in top bar
                    Main.Instance.UpdateUserFirstName();
                }
                else
                {
                    XtraMessageBox.Show("Failed to update profile. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error updating profile: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles Change PIN button click
        /// </summary>
        private void btnUpdatePIN_Click(object sender, EventArgs e)
        {
            try
            {
                // Get current PIN from database
                DataTable userData = BLL_User.GetUserById(_currentUserId);
                string currentPinFromDb = userData.Rows[0]["pin_code"]?.ToString();

                // Validate current PIN (textEdit1 is the new PIN field, txtPin is current)
                if (!string.IsNullOrEmpty(currentPinFromDb))
                {
                    // If user has a PIN, they must enter the current one
                    if (string.IsNullOrWhiteSpace(txtPin.Text) || txtPin.Text == "****")
                    {
                        XtraMessageBox.Show("Please enter your current PIN.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPin.Focus();
                        return;
                    }

                    // Verify current PIN
                    if (txtPin.Text != currentPinFromDb)
                    {
                        XtraMessageBox.Show("Current PIN is incorrect.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPin.Focus();
                        return;
                    }
                }

                // Validate new PIN
                if (string.IsNullOrWhiteSpace(textEdit1.Text))
                {
                    XtraMessageBox.Show("Please enter a new PIN.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEdit1.Focus();
                    return;
                }

                // Validate confirm PIN
                if (string.IsNullOrWhiteSpace(textEdit2.Text))
                {
                    XtraMessageBox.Show("Please confirm your new PIN.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEdit2.Focus();
                    return;
                }

                // Validate PIN match
                if (textEdit1.Text != textEdit2.Text)
                {
                    XtraMessageBox.Show("New PIN and confirmation do not match.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textEdit2.Focus();
                    return;
                }

                // Update PIN
                bool success = BLL_User.UpdateUserPin(_currentUserId, textEdit1.Text, _currentUserId);

                if (success)
                {
                    XtraMessageBox.Show("PIN updated successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update DataSetApp
                    Main.DataSetApp.User[0]["pin_code"] = textEdit1.Text;

                    // Clear PIN fields
                    txtPin.Text = "****";
                    textEdit1.Text = string.Empty;
                    textEdit2.Text = string.Empty;
                }
                else
                {
                    XtraMessageBox.Show("Failed to update PIN. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error updating PIN: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
