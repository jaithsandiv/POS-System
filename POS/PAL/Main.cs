using POS.PAL.USERCONTROL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public Main()
        {
            InitializeComponent();
            Login login = new Login();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(login);
            login.Dock = DockStyle.Fill;
        }

    }
}
