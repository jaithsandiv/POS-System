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
    public partial class UC_Store_Management : UserControl
    {
        private readonly BLL_Store _bllStore = new BLL_Store();
        private int _selectedStoreId = 0;

        public UC_Store_Management()
        {
            InitializeComponent();
            LoadStores();
        }

        private void LoadStores()
        {
            DataTable dt = _bllStore.GetStores();
            dgvStores.DataSource = dt;
            
            // Hide ID column if desired, or format headers
            if (dgvStores.Columns["store_id"] != null)
                dgvStores.Columns["store_id"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int userId = GetCurrentUserId();
                int newId = _bllStore.InsertStore(
                    txtStoreName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtCity.Text.Trim(),
                    txtState.Text.Trim(),
                    txtCountry.Text.Trim(),
                    txtPostalCode.Text.Trim(),
                    userId
                );

                if (newId > 0)
                {
                    MessageBox.Show("Store added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStores();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Failed to add store.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedStoreId > 0 && ValidateInput())
            {
                int userId = GetCurrentUserId();
                bool success = _bllStore.UpdateStore(
                    _selectedStoreId,
                    txtStoreName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtCity.Text.Trim(),
                    txtState.Text.Trim(),
                    txtCountry.Text.Trim(),
                    txtPostalCode.Text.Trim(),
                    userId
                );

                if (success)
                {
                    MessageBox.Show("Store updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStores();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Failed to update store.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a store to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedStoreId > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this store?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int userId = GetCurrentUserId();
                    bool success = _bllStore.DeleteStore(_selectedStoreId, userId);

                    if (success)
                    {
                        MessageBox.Show("Store deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStores();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete store.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a store to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void dgvStores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStores.Rows[e.RowIndex];
                _selectedStoreId = Convert.ToInt32(row.Cells["store_id"].Value);
                txtStoreName.Text = row.Cells["store_name"].Value?.ToString();
                txtPhone.Text = row.Cells["phone"].Value?.ToString();
                txtEmail.Text = row.Cells["email"].Value?.ToString();
                txtAddress.Text = row.Cells["address"].Value?.ToString();
                txtCity.Text = row.Cells["city"].Value?.ToString();
                txtState.Text = row.Cells["state"].Value?.ToString();
                txtCountry.Text = row.Cells["country"].Value?.ToString();
                txtPostalCode.Text = row.Cells["postal_code"].Value?.ToString();

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void ResetForm()
        {
            _selectedStoreId = 0;
            txtStoreName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtCountry.Clear();
            txtPostalCode.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                MessageBox.Show("Store Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private int GetCurrentUserId()
        {
            // Assuming Main.DataSetApp.User has the current user
            if (Main.DataSetApp.User.Rows.Count > 0)
            {
                var userRow = Main.DataSetApp.User[0];
                if (!userRow.Isuser_idNull())
                {
                    return int.Parse(userRow.user_id);
                }
            }
            return 1; // Default/Fallback
        }
    }
}
