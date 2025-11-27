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
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Table_Management : UserControl
    {
        private readonly BLL_Table _bllTable = new BLL_Table();
        private int _selectedTableId = 0;

        public UC_Table_Management()
        {
            InitializeComponent();
            LoadTables();
        }

        private void LoadTables()
        {
            DataTable dt = _bllTable.GetTables();
            gridControlTables.DataSource = dt;

            if (gridViewTables.Columns["table_id"] != null)
                gridViewTables.Columns["table_id"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int userId = GetCurrentUserId();
                int newId = _bllTable.InsertTable(
                    txtTableNumber.Text.Trim(),
                    (int)nudCapacity.Value,
                    userId
                );

                if (newId > 0)
                {
                    XtraMessageBox.Show("Table added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTables();
                    ResetForm();
                }
                else
                {
                    XtraMessageBox.Show("Failed to add table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedTableId > 0 && ValidateInput())
            {
                int userId = GetCurrentUserId();
                bool success = _bllTable.UpdateTable(
                    _selectedTableId,
                    txtTableNumber.Text.Trim(),
                    (int)nudCapacity.Value,
                    userId
                );

                if (success)
                {
                    XtraMessageBox.Show("Table updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTables();
                    ResetForm();
                }
                else
                {
                    XtraMessageBox.Show("Failed to update table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Please select a table to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTableId > 0)
            {
                if (XtraMessageBox.Show("Are you sure you want to delete this table?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int userId = GetCurrentUserId();
                    bool success = _bllTable.DeleteTable(_selectedTableId, userId);

                    if (success)
                    {
                        XtraMessageBox.Show("Table deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTables();
                        ResetForm();
                    }
                    else
                    {
                        XtraMessageBox.Show("Failed to delete table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Please select a table to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void gridViewTables_RowClick(object sender, RowClickEventArgs e)
        {
            var row = gridViewTables.GetFocusedDataRow();
            if (row != null)
            {
                _selectedTableId = Convert.ToInt32(row["table_id"]);
                txtTableNumber.Text = row["table_number"]?.ToString();
                if (row["capacity"] != DBNull.Value)
                    nudCapacity.Value = Convert.ToDecimal(row["capacity"]);
                else
                    nudCapacity.Value = 0;

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void ResetForm()
        {
            _selectedTableId = 0;
            txtTableNumber.Text = "";
            nudCapacity.Value = 4;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTableNumber.Text))
            {
                XtraMessageBox.Show("Table Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private int GetCurrentUserId()
        {
            if (Main.DataSetApp.User.Rows.Count > 0)
            {
                var userRow = Main.DataSetApp.User[0];
                if (!userRow.Isuser_idNull())
                {
                    return int.Parse(userRow.user_id);
                }
            }
            return 1;
        }
    }
}
