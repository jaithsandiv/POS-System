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
    public partial class UC_Table_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Table _bllTable = new BLL_Table();
        private bool isEditMode = false;
        private int editTableId = 0;

        /// <summary>
        /// Constructor for Add mode (new table)
        /// </summary>
        public UC_Table_Registration()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Constructor for Edit mode (existing table)
        /// </summary>
        public UC_Table_Registration(int tableId)
        {
            InitializeComponent();
            InitializeForm();

            // Set edit mode
            isEditMode = true;
            editTableId = tableId;

            // Load existing data
            LoadTableData(tableId);

            // Update UI for edit mode
            labelControlHeader.Text = "Edit Table";
            labelControlSubHeader.Text = "Update table information";
            regBtn.Text = "Update";
        }

        /// <summary>
        /// Initialize form settings
        /// </summary>
        private void InitializeForm()
        {
            // Set focus to table number
            txtTableNumber.Focus();
        }

        /// <summary>
        /// Load existing table data for editing
        /// </summary>
        private void LoadTableData(int tableId)
        {
            try
            {
                DataTable tableData = _bllTable.GetTableById(tableId);
                
                if (tableData != null && tableData.Rows.Count > 0)
                {
                    DataRow row = tableData.Rows[0];

                    txtTableNumber.Text = row["table_number"]?.ToString();
                    txtCapacity.Text = row["capacity"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading table data: {ex.Message}",
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
                UpdateTable();
            }
            else
            {
                InsertTable();
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Confirm navigation if fields have data
            if (!string.IsNullOrWhiteSpace(txtTableNumber.Text))
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

            Main.Instance.LoadUserControl(new UC_Table_Management());
        }

        /// <summary>
        /// Validates input fields
        /// </summary>
        private bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validate table number
            if (string.IsNullOrWhiteSpace(txtTableNumber.Text))
            {
                errorMessage = "Table number is required.";
                txtTableNumber.Focus();
                return false;
            }

            // Validate capacity
            if (string.IsNullOrWhiteSpace(txtCapacity.Text))
            {
                errorMessage = "Capacity is required.";
                txtCapacity.Focus();
                return false;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity <= 0)
            {
                errorMessage = "Capacity must be a valid positive number.";
                txtCapacity.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Inserts a new table
        /// </summary>
        private void InsertTable()
        {
            if (!ValidateInput(out string errorMessage))
            {
                XtraMessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                int capacity = int.Parse(txtCapacity.Text);

                int newId = _bllTable.InsertTable(
                    txtTableNumber.Text.Trim(),
                    capacity,
                    currentUserId
                );

                if (newId > 0)
                {
                    XtraMessageBox.Show("Table registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main.Instance.LoadUserControl(new UC_Table_Management());
                }
                else
                {
                    XtraMessageBox.Show("Failed to register table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error registering table: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates an existing table
        /// </summary>
        private void UpdateTable()
        {
            if (!ValidateInput(out string errorMessage))
            {
                XtraMessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                int capacity = int.Parse(txtCapacity.Text);

                bool success = _bllTable.UpdateTable(
                    editTableId,
                    txtTableNumber.Text.Trim(),
                    capacity,
                    currentUserId
                );

                if (success)
                {
                    XtraMessageBox.Show("Table updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main.Instance.LoadUserControl(new UC_Table_Management());
                }
                else
                {
                    XtraMessageBox.Show("Failed to update table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating table: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
