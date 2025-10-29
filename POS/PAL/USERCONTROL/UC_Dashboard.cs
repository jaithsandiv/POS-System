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
    public partial class UC_Dashboard : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_Dashboard()
        {
            InitializeComponent();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_SalesTerminal());
        }

        private void btnCustomerManagement_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Customer_Management());
        }

        private void btnCustomerGroups_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_CustomerGroup_Management());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Dashboard());
        }

        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            panelContactsSubmenu.Visible = !panelContactsSubmenu.Visible;
        }
    }
}
