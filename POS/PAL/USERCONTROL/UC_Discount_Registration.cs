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

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Discount_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Discount _bllDiscount = new BLL_Discount();

        public UC_Discount_Registration()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Set default values
            cmbType.Properties.Items.AddRange(new string[] { "Percentage", "Fixed Amount" });
            cmbType.SelectedIndex = 0;

            cmbStatus.Properties.Items.AddRange(new string[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 0;

            dtStartDate.DateTime = DateTime.Today;
            dtEndDate.DateTime = DateTime.Today.AddDays(30);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Please enter a discount name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtValue.Text) || !decimal.TryParse(txtValue.Text, out decimal value))
            {
                XtraMessageBox.Show("Please enter a valid discount value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtName.Text.Trim();
            string type = cmbType.Text;
            DateTime startDate = dtStartDate.DateTime;
            DateTime endDate = dtEndDate.DateTime;
            string status = cmbStatus.Text;

            bool success = _bllDiscount.InsertDiscount(name, type, value, startDate, endDate, status);

            if (success)
            {
                XtraMessageBox.Show("Discount added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main.Instance.LoadUserControl(new UC_Discount_Management());
            }
            else
            {
                XtraMessageBox.Show("Failed to add discount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Discount_Management());
        }
    }
}
