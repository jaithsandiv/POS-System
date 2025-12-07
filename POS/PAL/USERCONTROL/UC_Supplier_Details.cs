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
    public partial class UC_Supplier_Details : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_Supplier_Details()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Supplier_Management());
        }
    }
}
