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
