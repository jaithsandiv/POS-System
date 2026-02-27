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

        // Credit fields added programmatically
        private DevExpress.XtraEditors.SpinEdit numCreditLimit;
        private DevExpress.XtraEditors.SpinEdit numOpeningBalance;
        private DevExpress.XtraEditors.LabelControl lblCreditLimitLabel;
        private DevExpress.XtraEditors.LabelControl lblOpeningBalanceLabel;

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

            // Relabel Opening Balance field to reflect it's overriding live balance
            if (lblOpeningBalanceLabel != null)
            {
                lblOpeningBalanceLabel.Text = "Current Balance (override):";
                if (numOpeningBalance != null)
                    numOpeningBalance.ToolTip = "Editing this directly overrides the live credit balance. Normally managed automatically by sales and payments.";
            }
        }

        /// <summary>
        /// Initialize form settings
        /// </summary>
        private void InitializeForm()
        {
            // Load customer groups for dropdown
            LoadCustomerGroups();

            // Configure customer group dropdown
            ConfigureCustomerGroupLookup();

            // Add credit limit / opening balance fields dynamically
            InitializeCreditFields();

            // Wire up the register/update button click event
            regBtn.Click += RegBtn_Click;

            // Set focus to full name
            txtfullName.Focus();
        }

        /// <summary>
        /// Adds Credit Limit and Opening Balance fields to the form at runtime.
        /// </summary>
        private void InitializeCreditFields()
        {
            var font = new System.Drawing.Font("Segoe UI", 9.75F);
            var fieldSize = new System.Drawing.Size(480, 44);

            // --- Credit Limit (left column) ---
            lblCreditLimitLabel = new DevExpress.XtraEditors.LabelControl
            {
                Name = "lblCreditLimitLabel",
                Text = "Credit Limit:",
                Location = new System.Drawing.Point(381, 780)
            };
            lblCreditLimitLabel.Appearance.Font = font;
            lblCreditLimitLabel.Appearance.Options.UseFont = true;
            panelControl1.Controls.Add(lblCreditLimitLabel);

            numCreditLimit = new DevExpress.XtraEditors.SpinEdit
            {
                Name = "numCreditLimit",
                Location = new System.Drawing.Point(381, 803),
                Size = fieldSize,
                TabIndex = 100
            };
            numCreditLimit.Properties.Appearance.Font = font;
            numCreditLimit.Properties.Appearance.Options.UseFont = true;
            numCreditLimit.Properties.Padding = new System.Windows.Forms.Padding(10);
            numCreditLimit.Properties.MaxValue = 9999999m;
            numCreditLimit.Properties.MinValue = 0m;
            numCreditLimit.Properties.Increment = 500m;
            panelControl1.Controls.Add(numCreditLimit);

            // --- Opening / Current Balance (right column) ---
            lblOpeningBalanceLabel = new DevExpress.XtraEditors.LabelControl
            {
                Name = "lblOpeningBalanceLabel",
                Text = "Opening Balance:",
                Location = new System.Drawing.Point(995, 780)
            };
            lblOpeningBalanceLabel.Appearance.Font = font;
            lblOpeningBalanceLabel.Appearance.Options.UseFont = true;
            panelControl1.Controls.Add(lblOpeningBalanceLabel);

            numOpeningBalance = new DevExpress.XtraEditors.SpinEdit
            {
                Name = "numOpeningBalance",
                Location = new System.Drawing.Point(995, 803),
                Size = fieldSize,
                TabIndex = 101
            };
            numOpeningBalance.Properties.Appearance.Font = font;
            numOpeningBalance.Properties.Appearance.Options.UseFont = true;
            numOpeningBalance.Properties.Padding = new System.Windows.Forms.Padding(10);
            numOpeningBalance.Properties.MaxValue = 9999999m;
            numOpeningBalance.Properties.MinValue = 0m;
            numOpeningBalance.Properties.Increment = 100m;
            panelControl1.Controls.Add(numOpeningBalance);
        }

        /// <summary>
        /// Load customer groups for the dropdown
        /// </summary>
        private void LoadCustomerGroups()
        {
            try
            {
                customerGroupsTable = _bllContacts.GetCustomerGroups();
                
                // Clear existing items
                comboboxCustomerGroup.Properties.Items.Clear();
                
                // Add empty option
                comboboxCustomerGroup.Properties.Items.Add(new CustomerGroupItem { GroupId = null, GroupName = "" });
                
                // Add customer groups to the combobox
                if (customerGroupsTable != null && customerGroupsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in customerGroupsTable.Rows)
                    {
                        int groupId = Convert.ToInt32(row["group_id"]);
                        string groupName = row["group_name"]?.ToString();
                        
                        comboboxCustomerGroup.Properties.Items.Add(new CustomerGroupItem 
                        { 
                            GroupId = groupId, 
                            GroupName = groupName 
                        });
                    }
                }
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
        /// Configure the customer group dropdown
        /// </summary>
        private void ConfigureCustomerGroupLookup()
        {
            // The combobox is already configured in the designer
            // Set the selected index to the first item (empty option)
            if (comboboxCustomerGroup.Properties.Items.Count > 0)
            {
                comboboxCustomerGroup.SelectedIndex = 0;
            }
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
                    
                    // Set the customer group in the combobox
                    if (row["group_id"] != DBNull.Value)
                    {
                        int groupId = Convert.ToInt32(row["group_id"]);
                        
                        // Find and select the matching customer group
                        for (int i = 0; i < comboboxCustomerGroup.Properties.Items.Count; i++)
                        {
                            var item = comboboxCustomerGroup.Properties.Items[i] as CustomerGroupItem;
                            if (item != null && item.GroupId.HasValue && item.GroupId.Value == groupId)
                            {
                                comboboxCustomerGroup.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        comboboxCustomerGroup.SelectedIndex = 0; // Select empty option
                    }

                    // Load credit fields
                    if (numCreditLimit != null && row.Table.Columns.Contains("credit_limit") &&
                        row["credit_limit"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["credit_limit"].ToString()))
                    {
                        if (decimal.TryParse(row["credit_limit"].ToString(), out decimal cl))
                            numCreditLimit.Value = cl;
                    }

                    if (numOpeningBalance != null && row.Table.Columns.Contains("credit_balance") &&
                        row["credit_balance"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["credit_balance"].ToString()))
                    {
                        if (decimal.TryParse(row["credit_balance"].ToString(), out decimal cb))
                            numOpeningBalance.Value = cb;
                    }
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
        /// Get the selected customer group ID from the combobox
        /// </summary>
        private int? GetSelectedCustomerGroupId()
        {
            if (comboboxCustomerGroup.SelectedItem is CustomerGroupItem selectedItem)
            {
                return selectedItem.GroupId;
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
                decimal creditLimit = numCreditLimit?.Value ?? 0m;
                decimal openingBalance = numOpeningBalance?.Value ?? 0m;

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Insert the customer
                int newCustomerId = _bllContacts.InsertCustomer(
                    groupId, fullName, companyName, email, phone, 
                    address, city, state, country, postalCode, creditLimit, openingBalance, currentUserId
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
                decimal creditLimit = numCreditLimit?.Value ?? 0m;
                decimal openingBalance = numOpeningBalance?.Value ?? 0m;

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Update the customer
                bool success = _bllContacts.UpdateCustomer(
                    editCustomerId, groupId, fullName, companyName, email, phone, 
                    address, city, state, country, postalCode, creditLimit, openingBalance, currentUserId
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
            comboboxCustomerGroup.SelectedIndex = 0;
            if (numCreditLimit != null) numCreditLimit.Value = 0;
            if (numOpeningBalance != null) numOpeningBalance.Value = 0;
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
    
    /// <summary>
    /// Helper class to store customer group information in the combobox
    /// </summary>
    internal class CustomerGroupItem
    {
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        
        public override string ToString()
        {
            return GroupName ?? string.Empty;
        }
    }
}
