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
    public partial class UC_CustomerGroup_Management : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_CustomerGroup_Management()
        {
            InitializeComponent();
        }

        private void btnAddCustomerGroup_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_CustomerGroup_Registration());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Dashboard());
        }
    }
}
