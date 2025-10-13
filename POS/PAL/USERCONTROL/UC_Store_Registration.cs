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

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Store_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_Store_Registration()
        {
            InitializeComponent();
            LoadPreviousData();
        }

        private void LoadPreviousData()
        {
            // Load previously entered data if navigating back
            txtStoreName.Text = RegistrationData.StoreName;
            txtManagerName.Text = RegistrationData.ManagerName;
            txtPhoneNumber.Text = RegistrationData.StorePhone;
            txtEmail.Text = RegistrationData.StoreEmail;
            txtAddress.Text = RegistrationData.StoreAddress;
            txtCity.Text = RegistrationData.StoreCity;
            txtState.Text = RegistrationData.StoreState;
            txtCountry.Text = RegistrationData.StoreCountry;
            txtPostalCode.Text = RegistrationData.StorePostalCode;
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            UC_Business_Registration businessRegistration = new UC_Business_Registration();
            Control parentPanel = this.Parent;

            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(businessRegistration);
                businessRegistration.Dock = DockStyle.Fill;
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            // Validate Store Name is required
            if (string.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                XtraMessageBox.Show("Store Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStoreName.Focus();
                return;
            }

            // Save store data to static class
            RegistrationData.StoreName = txtStoreName.Text.Trim();
            RegistrationData.ManagerName = txtManagerName.Text.Trim();
            RegistrationData.StorePhone = txtPhoneNumber.Text.Trim();
            RegistrationData.StoreEmail = txtEmail.Text.Trim();
            RegistrationData.StoreAddress = txtAddress.Text.Trim();
            RegistrationData.StoreCity = txtCity.Text.Trim();
            RegistrationData.StoreState = txtState.Text.Trim();
            RegistrationData.StoreCountry = txtCountry.Text.Trim();
            RegistrationData.StorePostalCode = txtPostalCode.Text.Trim();

            // Navigate to User Registration
            UC_User_Registration userRegistration = new UC_User_Registration();
            Control parentPanel = this.Parent;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(userRegistration);
                userRegistration.Dock = DockStyle.Fill;
            }
        }
    }
}
