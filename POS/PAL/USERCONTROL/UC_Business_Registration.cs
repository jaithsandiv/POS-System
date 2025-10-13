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
    public partial class UC_Business_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_Business_Registration()
        {
            InitializeComponent();
            LoadPreviousData();
        }

        private void LoadPreviousData()
        {
            // Load previously entered data if navigating back
            txtBusinessName.Text = RegistrationData.BusinessName;
            txtPhoneNumber.Text = RegistrationData.BusinessPhone;
            txtEmail.Text = RegistrationData.BusinessEmail;
            txtAddress.Text = RegistrationData.BusinessAddress;
            txtCity.Text = RegistrationData.BusinessCity;
            txtCountry.Text = RegistrationData.BusinessCountry;
            txtTaxNo.Text = RegistrationData.BusinessTaxNo;
        }

        private void backBtn1_Click(object sender, EventArgs e)
        {
            // Clear registration data when going back to login
            RegistrationData.Clear();
            
            UC_Login login = new UC_Login();
            Control parentPanel = this.Parent;

            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(login);
                login.Dock = DockStyle.Fill;
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            // Save business data to static class
            RegistrationData.BusinessName = txtBusinessName.Text.Trim();
            RegistrationData.BusinessPhone = txtPhoneNumber.Text.Trim();
            RegistrationData.BusinessEmail = txtEmail.Text.Trim();
            RegistrationData.BusinessAddress = txtAddress.Text.Trim();
            RegistrationData.BusinessCity = txtCity.Text.Trim();
            RegistrationData.BusinessCountry = txtCountry.Text.Trim();
            RegistrationData.BusinessTaxNo = txtTaxNo.Text.Trim();

            // Navigate to Store Registration
            UC_Store_Registration storeRegistration = new UC_Store_Registration();
            Control parentPanel = this.Parent;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(storeRegistration);
                storeRegistration.Dock = DockStyle.Fill;
            }
        }
    }
}
