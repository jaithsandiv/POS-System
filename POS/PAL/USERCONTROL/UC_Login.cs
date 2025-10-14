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
    public partial class UC_Login : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL.BLL_Login _bllLogin = new BLL.BLL_Login();

        public UC_Login()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (String.IsNullOrEmpty(username))
            {
                XtraMessageBox.Show("Please enter your username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            if (String.IsNullOrEmpty(password))
            {
                XtraMessageBox.Show("Please enter your password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            bool isAuthenticated = _bllLogin.Authenticate(username, password);
            if (isAuthenticated)
            {
                UC_Dashboard dashboard = new UC_Dashboard();
                Main.Instance.SwitchToControl(dashboard);
            }
            else
            {
                XtraMessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hlblSignUp_Click(object sender, EventArgs e)
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
    }
}
