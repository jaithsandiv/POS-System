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
    public partial class UC_Store_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Store _bllStore = new BLL_Store();
        private bool isEditMode = false;
        private int editStoreId = 0;

        /// <summary>
        /// Constructor for Add mode (new store)
        /// </summary>
        public UC_Store_Registration()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Constructor for Edit mode (existing store)
        /// </summary>
        public UC_Store_Registration(int storeId)
        {
            InitializeComponent();
            InitializeForm();

            // Set edit mode
            isEditMode = true;
            editStoreId = storeId;

            // Load existing data
            LoadStoreData(storeId);

            // Update UI for edit mode
            labelControlHeader.Text = "Edit Store";
            labelControlSubHeader.Text = "Update store information";
            regBtn.Text = "Update";
        }

        /// <summary>
        /// Initialize form settings
        /// </summary>
        private void InitializeForm()
        {
            // Set focus to store name
            txtStoreName.Focus();
        }

        /// <summary>
        /// Load existing store data for editing
        /// </summary>
        private void LoadStoreData(int storeId)
        {
            try
            {
                DataTable storeData = _bllStore.GetStoreById(storeId);
                
                if (storeData != null && storeData.Rows.Count > 0)
                {
                    DataRow row = storeData.Rows[0];

                    txtStoreName.Text = row["store_name"]?.ToString();
                    txtEmail.Text = row["email"]?.ToString();
                    txtPhone.Text = row["phone"]?.ToString();
                    txtAddress.Text = row["address"]?.ToString();
                    txtCity.Text = row["city"]?.ToString();
                    txtState.Text = row["state"]?.ToString();
                    txtCountry.Text = row["country"]?.ToString();
                    txtPostalCode.Text = row["postal_code"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading store data: {ex.Message}",
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
                UpdateStore();
            }
            else
            {
                InsertStore();
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Confirm navigation if fields have data
            if (!string.IsNullOrWhiteSpace(txtStoreName.Text) || 
                !string.IsNullOrWhiteSpace(txtPhone.Text))
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

            Main.Instance.LoadUserControl(new UC_Store_Management());
        }

        /// <summary>
        /// Validates input fields
        /// </summary>
        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate store name
            if (string.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                errorMessage = "Store name is required.";
                txtStoreName.Focus();
                return false;
            }

            // Validate phone number
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                errorMessage = "Phone number is required.";
                txtPhone.Focus();
                return false;
            }

            // Validate email format if provided
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                    if (addr.Address != txtEmail.Text)
                    {
                        errorMessage = "Please enter a valid email address.";
                        txtEmail.Focus();
                        return false;
                    }
                }
                catch
                {
                    errorMessage = "Please enter a valid email address.";
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Inserts a new store
        /// </summary>
        private void InsertStore()
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
                string storeName = txtStoreName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string address = txtAddress.Text.Trim();
                string city = txtCity.Text.Trim();
                string state = txtState.Text.Trim();
                string country = txtCountry.Text.Trim();
                string postalCode = txtPostalCode.Text.Trim();

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Insert the store
                int newStoreId = _bllStore.InsertStore(
                    storeName, phone, email, address, city, state, country, postalCode, currentUserId
                );

                if (newStoreId > 0)
                {
                    // Show success message
                    XtraMessageBox.Show(
                        $"Store '{storeName}' created successfully!\n\nStore ID: {newStoreId}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to management screen
                    Main.Instance.LoadUserControl(new UC_Store_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to create store. Please try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error creating store: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates an existing store
        /// </summary>
        private void UpdateStore()
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
                string storeName = txtStoreName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string address = txtAddress.Text.Trim();
                string city = txtCity.Text.Trim();
                string state = txtState.Text.Trim();
                string country = txtCountry.Text.Trim();
                string postalCode = txtPostalCode.Text.Trim();

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Update the store
                bool success = _bllStore.UpdateStore(
                    editStoreId, storeName, phone, email, address, city, state, country, postalCode, currentUserId
                );

                if (success)
                {
                    // Show success message
                    XtraMessageBox.Show(
                        $"Store '{storeName}' updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to management screen
                    Main.Instance.LoadUserControl(new UC_Store_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to update store. Please try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating store: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
