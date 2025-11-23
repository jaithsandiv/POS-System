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
    public partial class UC_Customer_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private bool isEditMode = false;
        private int editCustomerId = 0;
        private DataTable customerGroupsTable;

        /// <summary>
        /// Constructor for Add mode (new customer)
        /// </summary>
        public UC_Customer_Registration()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Constructor for Edit mode (existing customer)
        /// </summary>
        public UC_Customer_Registration(int customerId)
        {
            InitializeComponent();
            InitializeForm();

            // Set edit mode
            isEditMode = true;
            editCustomerId = customerId;

            // Load existing data
            LoadCustomerData(customerId);

            // Update UI for edit mode
            labelControl3.Text = "Edit Customer";
            labelControl2.Text = "Update customer information";
            regBtn.Text = "Update";
        }

        /// <summary>
        /// Initialize form settings
        /// </summary>
        private void InitializeForm()
        {
            // Load customer groups for dropdown
            LoadCustomerGroups();

            // Configure customer group dropdown as a LookUpEdit
            ConfigureCustomerGroupLookup();

            // Wire up the register/update button click event
            regBtn.Click += RegBtn_Click;

            // Set focus to full name
            txtfullName.Focus();
        }

        /// <summary>
        /// Load customer groups for the dropdown
        /// </summary>
        private void LoadCustomerGroups()
        {
            try
            {
                customerGroupsTable = _bllContacts.GetCustomerGroups();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customer groups: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Configure the customer group dropdown (convert TextEdit to LookUpEdit behavior)
        /// </summary>
        private void ConfigureCustomerGroupLookup()
        {
            // Since txtCustomerGroup is a TextEdit in the designer, we'll add autocomplete
            // For a better implementation, consider changing it to LookUpEdit in the designer
            
            // For now, we'll use it as a simple text field
            // In a production scenario, replace txtCustomerGroup with a LookUpEdit or ComboBoxEdit
        }

        /// <summary>
        /// Load existing customer data for editing
        /// </summary>
        private void LoadCustomerData(int customerId)
        {
            try
            {
                DataTable customerData = _bllContacts.GetCustomerById(customerId);
                
                if (customerData != null && customerData.Rows.Count > 0)
                {
                    DataRow row = customerData.Rows[0];

                    txtfullName.Text = row["full_name"]?.ToString();
                    txtCompanyName.Text = row["company_name"]?.ToString();
                    txtUEmail.Text = row["email"]?.ToString();
                    txtUPhoneNumber.Text = row["phone"]?.ToString();
                    txtAddress.Text = row["address"]?.ToString();
                    txtCity.Text = row["city"]?.ToString();
                    txtState.Text = row["state"]?.ToString();
                    txtPostalCode.Text = row["postal_code"]?.ToString();
                    txtCustomerGroup.Text = row["group_name"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customer data: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Register/Update button click
        /// </summary>
        private void RegBtn_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                UpdateCustomer();
            }
            else
            {
                InsertCustomer();
            }
        }

        /// <summary>
        /// Validates input fields
        /// </summary>
        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate full name
            if (string.IsNullOrWhiteSpace(txtfullName.Text))
            {
                errorMessage = "Full name is required.";
                txtfullName.Focus();
                return false;
            }

            // Validate phone number
            if (string.IsNullOrWhiteSpace(txtUPhoneNumber.Text))
            {
                errorMessage = "Phone number is required.";
                txtUPhoneNumber.Focus();
                return false;
            }

            // Validate email format if provided
            if (!string.IsNullOrWhiteSpace(txtUEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtUEmail.Text);
                    if (addr.Address != txtUEmail.Text)
                    {
                        errorMessage = "Please enter a valid email address.";
                        txtUEmail.Focus();
                        return false;
                    }
                }
                catch
                {
                    errorMessage = "Please enter a valid email address.";
                    txtUEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get the selected customer group ID from the text
        /// </summary>
        private int? GetSelectedCustomerGroupId()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerGroup.Text))
                return null;

            try
            {
                // Find the group by name
                if (customerGroupsTable != null)
                {
                    DataRow[] rows = customerGroupsTable.Select($"group_name = '{txtCustomerGroup.Text.Replace("'", "''")}'");
                    if (rows.Length > 0)
                    {
                        return Convert.ToInt32(rows[0]["group_id"]);
                    }
                }
            }
            catch
            {
                // If group name not found, return null
            }

            return null;
        }

        /// <summary>
        /// Inserts a new customer
        /// </summary>
        private void InsertCustomer()
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
                int? groupId = GetSelectedCustomerGroupId();
                string fullName = txtfullName.Text.Trim();
                string companyName = txtCompanyName.Text.Trim();
                string email = txtUEmail.Text.Trim();
                string phone = txtUPhoneNumber.Text.Trim();
                string address = txtAddress.Text.Trim();
                string city = txtCity.Text.Trim();
                string state = txtState.Text.Trim();
                string country = string.Empty; // Not in the form, can be added later
                string postalCode = txtPostalCode.Text.Trim();

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Insert the customer
                int newCustomerId = _bllContacts.InsertCustomer(
                    groupId, fullName, companyName, email, phone, 
                    address, city, state, country, postalCode, currentUserId
                );

                // Show success message
                XtraMessageBox.Show(
                    $"Customer '{fullName}' created successfully!\n\nCustomer ID: {newCustomerId}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Navigate back to management screen
                Main.Instance.LoadUserControl(new UC_Customer_Management());
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error creating customer: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        private void UpdateCustomer()
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
                int? groupId = GetSelectedCustomerGroupId();
                string fullName = txtfullName.Text.Trim();
                string companyName = txtCompanyName.Text.Trim();
                string email = txtUEmail.Text.Trim();
                string phone = txtUPhoneNumber.Text.Trim();
                string address = txtAddress.Text.Trim();
                string city = txtCity.Text.Trim();
                string state = txtState.Text.Trim();
                string country = string.Empty; // Not in the form, can be added later
                string postalCode = txtPostalCode.Text.Trim();

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Update the customer
                bool success = _bllContacts.UpdateCustomer(
                    editCustomerId, groupId, fullName, companyName, email, phone, 
                    address, city, state, country, postalCode, currentUserId
                );

                if (success)
                {
                    // Show success message
                    XtraMessageBox.Show(
                        $"Customer '{fullName}' updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to management screen
                    Main.Instance.LoadUserControl(new UC_Customer_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to update customer. Please try again.",
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
                    $"Error updating customer: {ex.Message}",
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
            txtfullName.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtUEmail.Text = string.Empty;
            txtUPhoneNumber.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtCustomerGroup.Text = string.Empty;
            txtfullName.Focus();
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            // Confirm navigation if fields have data
            if (!string.IsNullOrWhiteSpace(txtfullName.Text) || 
                !string.IsNullOrWhiteSpace(txtUPhoneNumber.Text))
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

            Main.Instance.LoadUserControl(new UC_Customer_Management());
        }
    }
}
