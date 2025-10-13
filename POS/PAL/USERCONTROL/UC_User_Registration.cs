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
    public partial class UC_User_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Registration _bllRegistration = new BLL_Registration();

        public UC_User_Registration()
        {
            InitializeComponent();
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            UC_Store_Registration storeRegistration = new UC_Store_Registration();
            Control parentPanel = this.Parent;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(storeRegistration);
                storeRegistration.Dock = DockStyle.Fill;
            }
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtfullName.Text))
            {
                XtraMessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtfullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                XtraMessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUEmail.Text))
            {
                XtraMessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                XtraMessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Validate password confirmation
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                XtraMessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            // Validate password length
            if (txtPassword.Text.Length < 6)
            {
                XtraMessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Call the registration method
            bool success = _bllRegistration.RegisterComplete(
                // Business data from RegistrationData
                RegistrationData.BusinessName,
                RegistrationData.BusinessPhone,
                RegistrationData.BusinessEmail,
                RegistrationData.BusinessAddress,
                RegistrationData.BusinessCity,
                RegistrationData.BusinessCountry,
                RegistrationData.BusinessTaxNo,
                // Store data from RegistrationData
                RegistrationData.StoreName,
                RegistrationData.ManagerName,
                RegistrationData.StorePhone,
                RegistrationData.StoreEmail,
                RegistrationData.StoreAddress,
                RegistrationData.StoreCity,
                RegistrationData.StoreState,
                RegistrationData.StoreCountry,
                RegistrationData.StorePostalCode,
                // User data from current form
                txtfullName.Text.Trim(),
                txtUsername.Text.Trim(),
                txtUEmail.Text.Trim(),
                txtUPhoneNumber.Text.Trim(),
                txtPassword.Text,
                txtPin.Text.Trim(),
                out string errorMessage
            );

            if (success)
            {
                XtraMessageBox.Show("Registration successful! You can now sign in with your credentials.", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear registration data
                RegistrationData.Clear();

                // Navigate to login
                UC_Login login = new UC_Login();
                Control parentPanel = this.Parent;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();
                    parentPanel.Controls.Add(login);
                    login.Dock = DockStyle.Fill;
                }
            }
            else
            {
                XtraMessageBox.Show(errorMessage, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
