using POS.BLL;
using POS.DAL.DataSource;
using POS.PAL.USERCONTROL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POS
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public static Main Instance { get; private set; }

        private readonly BLL_Initialize _bllInitialize = new BLL_Initialize();

        public static DAL_DS_Initialize DataSetApp { get; private set; }

        public Main()
        {
            InitializeComponent();

            DataSetApp = new DAL_DS_Initialize();
            LoadBusinessData();

            Instance = this;

            UC_Login login = new UC_Login();
            SwitchToControl(login);
        }

        private void LoadBusinessData()
        {
            try
            {
                DataSetApp.Business.Clear();
                DataSetApp.Business.Merge(_bllInitialize.GetBusiness());

                if (DataSetApp.Business.Rows.Count == 0)
                    DataSetApp.Business.AddBusinessRow(DataSetApp.Business.NewBusinessRow());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business data: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SwitchToControl(UserControl control)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }
    }
}
