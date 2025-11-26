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
    public partial class UC_Supplier_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private bool isEditMode = false;
        private int editSupplierId = 0;

        /// <summary>
        /// Constructor for Add mode (new supplier)
        /// </summary>
        public UC_Supplier_Registration()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Constructor for Edit mode (existing supplier)
        /// </summary>
        public UC_Supplier_Registration(int supplierId)
        {
            InitializeComponent();
            InitializeForm();

            // Set edit mode
            isEditMode = true;
            editSupplierId = supplierId;

            // Load existing data
            LoadSupplierData(supplierId);

            // Update UI for edit mode
            labelControl3.Text = "Edit Supplier";
            labelControl2.Text = "Update supplier information";
            regBtn.Text = "Update";
        }

        /// <summary>
        /// Initialize form settings
        /// </summary>
        private void InitializeForm()
        {
            // Wire up the register/update button click event
            regBtn.Click += RegBtn_Click;

            // Set focus to supplier name
            txtSupplierName.Focus();
        }

        /// <summary>
        /// Load existing supplier data for editing
        /// </summary>
        private void LoadSupplierData(int supplierId)
        {
            try
            {
                DataTable supplierData = _bllContacts.GetSupplierById(supplierId);
                
                if (supplierData != null && supplierData.Rows.Count > 0)
                {
                    DataRow row = supplierData.Rows[0];

                    txtSupplierName.Text = row["supplier_name"]?.ToString();
                    txtCompanyName.Text = row["company_name"]?.ToString();
                    txtSupplierEmail.Text = row["email"]?.ToString();
                    txtSupplierPhoneNumber.Text = row["phone"]?.ToString();
                    txtSupplierAddress.Text = row["address"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading supplier data: {ex.Message}",
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
                UpdateSupplier();
            }
            else
            {
                InsertSupplier();
            }
        }

        /// <summary>
        /// Validates input fields
        /// </summary>
        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate supplier name
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                errorMessage = "Supplier name is required.";
                txtSupplierName.Focus();
                return false;
            }

            // Validate email format if provided
            if (!string.IsNullOrWhiteSpace(txtSupplierEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtSupplierEmail.Text);
                    if (addr.Address != txtSupplierEmail.Text)
                    {
                        errorMessage = "Please enter a valid email address.";
                        txtSupplierEmail.Focus();
                        return false;
                    }
                }
                catch
                {
                    errorMessage = "Please enter a valid email address.";
                    txtSupplierEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Inserts a new supplier
        /// </summary>
        private void InsertSupplier()
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
                string supplierName = txtSupplierName.Text.Trim();
                string companyName = txtCompanyName.Text.Trim();
                string email = txtSupplierEmail.Text.Trim();
                string phone = txtSupplierPhoneNumber.Text.Trim();
                string address = txtSupplierAddress.Text.Trim();

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Insert the supplier
                int newSupplierId = _bllContacts.InsertSupplier(
                    supplierName, companyName, email, phone, address, currentUserId
                );

                // Show success message
                XtraMessageBox.Show(
                    $"Supplier '{supplierName}' created successfully!\n\nSupplier ID: {newSupplierId}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Navigate back to management screen
                Main.Instance.LoadUserControl(new UC_Supplier_Management());
            }
            catch (ArgumentException ex)
            {
                XtraMessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error creating supplier: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates an existing supplier
        /// </summary>
        private void UpdateSupplier()
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
                string supplierName = txtSupplierName.Text.Trim();
                string companyName = txtCompanyName.Text.Trim();
                string email = txtSupplierEmail.Text.Trim();
                string phone = txtSupplierPhoneNumber.Text.Trim();
                string address = txtSupplierAddress.Text.Trim();

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Update the supplier
                bool success = _bllContacts.UpdateSupplier(
                    editSupplierId, supplierName, companyName, email, phone, address, currentUserId
                );

                if (success)
                {
                    // Show success message
                    XtraMessageBox.Show(
                        $"Supplier '{supplierName}' updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to management screen
                    Main.Instance.LoadUserControl(new UC_Supplier_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to update supplier. Please try again.",
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
                    $"Error updating supplier: {ex.Message}",
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
            txtSupplierName.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtSupplierEmail.Text = string.Empty;
            txtSupplierPhoneNumber.Text = string.Empty;
            txtSupplierAddress.Text = string.Empty;
            txtSupplierName.Focus();
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            // Confirm navigation if fields have data
            if (!string.IsNullOrWhiteSpace(txtSupplierName.Text) || 
                !string.IsNullOrWhiteSpace(txtSupplierPhoneNumber.Text))
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

            Main.Instance.LoadUserControl(new UC_Supplier_Management());
        }
    }
}
