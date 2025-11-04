using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void UC_Login_Load(object sender, EventArgs e)
        {
            if (Main.DataSetApp?.Business.Rows.Count > 0 && !Main.DataSetApp.Business[0].IslogoNull())
            {
                byte[] logoBytes = Main.DataSetApp.Business[0].logo;
                using (MemoryStream ms = new MemoryStream(logoBytes))
                {
                    pictureEdit1.Image = Image.FromStream(ms);
                }
            }
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
                Main.Instance.LoadUserControl(dashboard, hideNavigation: false);
            }
            else
            {
                XtraMessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtUsername.Text != "" && txtPassword.Text != "")
            {
                btnSignIn.PerformClick();
                return;
            }
            if (e.KeyChar == (char)Keys.Enter && txtUsername.Text != "")
            {
                txtPassword.Focus();
                return;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtUsername.Text != "" && txtPassword.Text != "")
            {
                btnSignIn.PerformClick();
                return;
            }
            if (e.KeyChar == (char)Keys.Enter && txtPassword.Text != "")
            {
                txtUsername.Focus();
                return;
            }
        }
    }
}
