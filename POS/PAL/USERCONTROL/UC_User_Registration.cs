using DevExpress.XtraEditors;
using POS.BLL;
using POS.DAL.DataSource;
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
            LoadPreviousData();
        }

        private void LoadPreviousData()
        {
            // Load previously entered data if navigating back
            var dataSet = UC_Business_Registration.RegistrationDataSet;
            
            if (dataSet.User.Count > 0)
            {
                var userRow = dataSet.User[0];
                txtfullName.Text = userRow.full_name;
                txtUsername.Text = userRow.username;
                txtUEmail.Text = userRow.email;
                txtUPhoneNumber.Text = userRow.phone;
                txtPin.Text = userRow.pin_code;
                // Don't load passwords for security
            }
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            UC_Business_Registration businessRegistration = new UC_Business_Registration();
            Main.Instance.LoadUserControl(businessRegistration, hideNavigation: true);
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

            // Get the DataSet with Business and Store data
            var dataSet = UC_Business_Registration.RegistrationDataSet;

            // Validate that Business and Store data exists
            if (dataSet.Business.Count == 0 || dataSet.Store.Count == 0)
            {
                XtraMessageBox.Show("Business and Store information is missing. Please start from the beginning.", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                UC_Business_Registration businessRegistration = new UC_Business_Registration();
                Main.Instance.LoadUserControl(businessRegistration, hideNavigation: true);
                return;
            }

            // Create User row
            dataSet.User.Clear();
            var userRow = dataSet.User.NewUserRow();
            userRow.full_name = txtfullName.Text.Trim();
            userRow.username = txtUsername.Text.Trim();
            userRow.email = txtUEmail.Text.Trim();
            userRow.phone = txtUPhoneNumber.Text.Trim();
            userRow.password_hash = txtPassword.Text; // Will be hashed in BLL
            userRow.pin_code = txtPin.Text.Trim();
            userRow.role_id = "1"; // Default role_id = 1
            
            dataSet.User.AddUserRow(userRow);

            // Call the registration method
            bool success = _bllRegistration.RegisterComplete(
                dataSet.Business[0],
                dataSet.Store[0],
                dataSet.User[0],
                out string errorMessage
            );

            if (success)
            {
                XtraMessageBox.Show("Registration successful! You can now sign in with your credentials.", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload business data in Main form and update the topbar
                Main.Instance.LoadBusinessData();
                Main.Instance.LoadStoreData();

                // Clear registration data
                dataSet.Clear();

                // Navigate to login
                UC_Login login = new UC_Login();
                Main.Instance.LoadUserControl(login, hideNavigation: true);
            }
            else
            {
                XtraMessageBox.Show(errorMessage, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
