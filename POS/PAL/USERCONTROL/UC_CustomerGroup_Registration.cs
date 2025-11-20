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
    public partial class UC_CustomerGroup_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private bool isEditMode = false;
        private int editGroupId = 0;

        /// <summary>
        /// Constructor for Add mode (new customer group)
        /// </summary>
        public UC_CustomerGroup_Registration()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Constructor for Edit mode (existing customer group)
        /// </summary>
        public UC_CustomerGroup_Registration(int groupId, string groupName, decimal discountPercent)
        {
            InitializeComponent();
            InitializeForm();

            // Set edit mode
            isEditMode = true;
            editGroupId = groupId;

            // Load existing data
            txtGroupName.Text = groupName;
            txtDiscountPercentage.Text = discountPercent.ToString("F2");

            // Update UI for edit mode
            labelControl3.Text = "Edit Customer Group";
            labelControl2.Text = "Update customer group information";
            regBtn.Text = "Update";
        }

        /// <summary>
        /// Initialize form settings
        /// </summary>
        private void InitializeForm()
        {
            // Set decimal mask for discount percentage
            txtDiscountPercentage.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDiscountPercentage.Properties.Mask.EditMask = "n2";
            txtDiscountPercentage.Properties.Mask.UseMaskAsDisplayFormat = true;

            // Wire up the register/update button click event
            regBtn.Click += RegBtn_Click;

            // Set focus to group name
            txtGroupName.Focus();
        }

        /// <summary>
        /// Handle Register/Update button click
        /// </summary>
        private void RegBtn_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                UpdateCustomerGroup();
            }
            else
            {
                InsertCustomerGroup();
            }
        }

        /// <summary>
        /// Validates input fields
        /// </summary>
        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate group name
            if (string.IsNullOrWhiteSpace(txtGroupName.Text))
            {
                errorMessage = "Group name is required.";
                txtGroupName.Focus();
                return false;
            }

            // Validate discount percentage
            if (string.IsNullOrWhiteSpace(txtDiscountPercentage.Text))
            {
                errorMessage = "Discount percentage is required.";
                txtDiscountPercentage.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDiscountPercentage.Text, out decimal discountPercent))
            {
                errorMessage = "Please enter a valid discount percentage.";
                txtDiscountPercentage.Focus();
                return false;
            }

            if (discountPercent < 0 || discountPercent > 100)
            {
                errorMessage = "Discount percentage must be between 0 and 100.";
                txtDiscountPercentage.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Inserts a new customer group
        /// </summary>
        private void InsertCustomerGroup()
        {
            try
            {
                // Validate input
                if (!ValidateInput(out string errorMessage))
                {
                    XtraMessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get input values
                string groupName = txtGroupName.Text.Trim();
                decimal discountPercent = decimal.Parse(txtDiscountPercentage.Text);

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Insert the customer group
                int newGroupId = _bllContacts.InsertCustomerGroup(groupName, discountPercent, currentUserId);

                // Show success message
                XtraMessageBox.Show(
                    $"Customer group '{groupName}' created successfully!\n\nGroup ID: {newGroupId}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Navigate back to management screen
                Main.Instance.LoadUserControl(new UC_CustomerGroup_Management());
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error creating customer group: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates an existing customer group
        /// </summary>
        private void UpdateCustomerGroup()
        {
            try
            {
                // Validate input
                if (!ValidateInput(out string errorMessage))
                {
                    XtraMessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get input values
                string groupName = txtGroupName.Text.Trim();
                decimal discountPercent = decimal.Parse(txtDiscountPercentage.Text);

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Update the customer group
                bool success = _bllContacts.UpdateCustomerGroup(editGroupId, groupName, discountPercent, currentUserId);

                if (success)
                {
                    // Show success message
                    XtraMessageBox.Show(
                        $"Customer group '{groupName}' updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to management screen
                    Main.Instance.LoadUserControl(new UC_CustomerGroup_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to update customer group. Please try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating customer group: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Clears all input fields
        /// </summary>
        private void ClearFields()
        {
            txtGroupName.Text = string.Empty;
            txtDiscountPercentage.Text = "0.00";
            txtGroupName.Focus();
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            // Confirm navigation if fields have data
            if (!string.IsNullOrWhiteSpace(txtGroupName.Text) || 
                !string.IsNullOrWhiteSpace(txtDiscountPercentage.Text))
            {
                var result = XtraMessageBox.Show(
                    "Are you sure you want to go back? Any unsaved changes will be lost.",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;
            }

            Main.Instance.LoadUserControl(new UC_CustomerGroup_Management());
        }
    }
}
