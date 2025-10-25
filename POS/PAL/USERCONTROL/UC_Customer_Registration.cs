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
    public partial class UC_Customer_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_Customer_Registration()
        {
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Dashboard());
        }
        private void btn25Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn50Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn100Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn200Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn500Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn1000Filter_Click(object sender, EventArgs e)
        {

        }
        private void btnAllFilter_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
