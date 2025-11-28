using DevExpress.XtraEditors;
using POS.BLL;
using POS.DAL.DataSource;
using POS.PAL.USERCONTROL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace POS
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public static Main Instance { get; private set; }

        private readonly BLL_Initialize _bllInitialize = new BLL_Initialize();

        public static DAL_DS_Initialize DataSetApp { get; private set; }

        // Sidebar state fields
        private bool isCollapsed = false;
        private const int collapsedWidth = 0;
        private const int expandedWidth = 250;
        private Timer animationTimer;

        public Main()
        {
            InitializeComponent();

            DataSetApp = new DAL_DS_Initialize();
            LoadBusinessData();
            LoadStoreData();
            LoadSystemSettings();
            UpdateBusinessName();

            Instance = this;

            if (DataSetApp.Business.Rows.Count > 0)
            {
                var firstBusinessRow = DataSetApp.Business[0];
                if (!firstBusinessRow.Isbusiness_idNull() && !string.IsNullOrWhiteSpace(firstBusinessRow.business_id))
                {
                    UC_Login login = new UC_Login();
                    LoadUserControl(login, hideNavigation: true);
                }
                else
                {
                    UC_Business_Registration businessRegistration = new UC_Business_Registration();
                    LoadUserControl(businessRegistration, hideNavigation: true);
                }
            }
            else
            {
                UC_Business_Registration businessRegistration = new UC_Business_Registration();
                LoadUserControl(businessRegistration, hideNavigation: true);
            }

            // Wire up settings button
            if (btnBusinessSettings != null)
            {
                btnBusinessSettings.Click += btnBusinessSettings_Click;
            }
            if (btnBusinessLocations != null)
            {
                btnBusinessLocations.Click += btnBusinessLocations_Click;
            }
            if (btnTables != null)
            {
                btnTables.Click += btnTables_Click;
            }
            if (btnAllSales != null)
            {
                btnAllSales.Click += btnAllSales_Click;
            }
            if (btnDiscounts != null)
            {
                btnDiscounts.Click += btnDiscounts_Click;
            }
        }

        public void LoadBusinessData()
        {
            DataSetApp.Business.Clear();
            DataSetApp.Business.Merge(_bllInitialize.GetBusiness());

            if (DataSetApp.Business.Rows.Count == 0)
                DataSetApp.Business.AddBusinessRow(DataSetApp.Business.NewBusinessRow());

            UpdateBusinessName();
        }

        public void LoadStoreData()
        {
            DataSetApp.Store.Clear();
            DataSetApp.Store.Merge(_bllInitialize.GetStore());

            if (DataSetApp.Store.Rows.Count == 0)
                DataSetApp.Store.AddStoreRow(DataSetApp.Store.NewStoreRow());
        }

        public void LoadSystemSettings()
        {
            DataSetApp.SystemSetting.Clear();
            DataSetApp.SystemSetting.Merge(_bllInitialize.GetSystemSettings());
        }

        /// <summary>
        /// Get system setting value by key
        /// </summary>
        public static string GetSetting(string key, string defaultValue = "")
        {
            if (DataSetApp?.SystemSetting == null)
                return defaultValue;

            var settingRow = DataSetApp.SystemSetting
                .FirstOrDefault(r => !r.Issetting_keyNull() &&
                                    r.setting_key.Equals(key, StringComparison.OrdinalIgnoreCase) &&
                                    !r.IsstatusNull() &&
                                    r.status == "A");

            if (settingRow != null && !settingRow.Issetting_valueNull())
                return settingRow.setting_value;

            return defaultValue;
        }

        private void UpdateBusinessName()
        {
            if (DataSetApp.Business.Rows.Count > 0)
            {
                var businessRow = DataSetApp.Business[0];
                if (!businessRow.Isbusiness_nameNull() && !string.IsNullOrWhiteSpace(businessRow.business_name))
                {
                    lblBusinessName.Text = businessRow.business_name;
                }
                else
                {
                    lblBusinessName.Text = "Business Name";
                }
            }
            else
            {
                lblBusinessName.Text = "Business Name";
            }
        }

        /// <summary>
        /// Loads a UserControl into the content panel while keeping topbar and sidebar persistent
        /// </summary>
        /// <param name="control">The UserControl to load</param>
        /// <param name="hideNavigation">Set to true to hide topbar and sidebar (for login/registration screens)</param>
        public void LoadUserControl(UserControl control, bool hideNavigation = false)
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            // Show/hide navigation based on the parameter
            panelTopBar.Visible = !hideNavigation;
            panelSideBar.Visible = !hideNavigation;
        }

        /// <summary>
        /// Legacy method for backward compatibility - redirects to LoadUserControl
        /// </summary>
        [Obsolete("Use LoadUserControl instead")]
        public void SwitchToControl(UserControl control)
        {
            LoadUserControl(control);
        }

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            if (isCollapsed)
                AnimateSidebar(true);
            else
                AnimateSidebar(false);
        }

        private void AnimateSidebar(bool expand)
        {
            // Stop any existing animation
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
                animationTimer = null;
            }

            animationTimer = new Timer { Interval = 30 };
            animationTimer.Tick += (s, e) =>
            {
                if (expand)
                {
                    panelSideBar.Width += 30;
                    if (panelSideBar.Width >= expandedWidth)
                    {
                        panelSideBar.Width = expandedWidth; // Ensure exact width
                        animationTimer.Stop();
                        animationTimer.Dispose();
                        animationTimer = null;
                        isCollapsed = false;

                        // Restore button text after expansion
                        foreach (Control ctrl in panelSideBar.Controls)
                        {
                            if (ctrl is SimpleButton btn)
                            {
                                btn.Text = btn.Tag?.ToString();
                            }
                        }
                    }
                }
                else
                {
                    panelSideBar.Width -= 30;
                    if (panelSideBar.Width <= collapsedWidth)
                    {
                        panelSideBar.Width = collapsedWidth; // Ensure exact width
                        animationTimer.Stop();
                        animationTimer.Dispose();
                        animationTimer = null;
                        isCollapsed = true;

                        // Hide button text after collapse
                        foreach (Control ctrl in panelSideBar.Controls)
                        {
                            if (ctrl is SimpleButton btn)
                            {
                                btn.Text = "";
                                btn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
                            }
                        }
                    }
                }
            };
            animationTimer.Start();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SalesTerminal());
        }
        // Sidebar button click handlers
        private void btnHome_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Dashboard());
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            panelUserManagementSubmenu.Visible = !panelUserManagementSubmenu.Visible;
        }

        private void btnContacts_Click(object sender, EventArgs e)
        {
            panelContactsSubmenu.Visible = !panelContactsSubmenu.Visible;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Customer_Management());
        }

        private void btnCustomerGroups_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_CustomerGroup_Management());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            panelProductsSubmenu.Visible = !panelProductsSubmenu.Visible;
        }

        private void btnPurchases_Click(object sender, EventArgs e)
        {
            panelPurchasesSubmenu.Visible = !panelPurchasesSubmenu.Visible;
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            panelSellSubmenu.Visible = !panelSellSubmenu.Visible;
        }

        private void btnStockTransfers_Click(object sender, EventArgs e)
        {
            panelStockTransfersSubmenu.Visible = !panelStockTransfersSubmenu.Visible;
        }

        private void btnStockAdjustment_Click(object sender, EventArgs e)
        {
            panelStockAdjustmentSubmenu.Visible = !panelStockAdjustmentSubmenu.Visible;
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            panelExpensesSubmenu.Visible = !panelExpensesSubmenu.Visible;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            panelPaymentAccountsSubmenu.Visible = !panelPaymentAccountsSubmenu.Visible;
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            panelReportsSubmenu.Visible = !panelReportsSubmenu.Visible;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            panelSettingsSubmenu.Visible = !panelSettingsSubmenu.Visible;
        }

        private void btnBusinessSettings_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SystemSettings());
        }

        private void btnBusinessLocations_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Store_Management());
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Table_Management());
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Supplier_Management());
        }

        private void btnAllSales_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Sales_Management());
        }

        private void btnListDrafts_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Draft_Management());
        }

        private void btnListQuotations_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Quotation_Management());
        }

        private void btnDiscounts_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Discount_Management());
        }
    }
}
