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
        private Timer clockTimer;

        public Main()
        {
            InitializeComponent();

            DataSetApp = new DAL_DS_Initialize();
            LoadBusinessData();
            LoadStoreData();
            LoadSystemSettings();
            UpdateBusinessName();

            Instance = this;

            // Set current date and time
            UpdateDateTime();

            // Setup timer for real-time clock updates
            clockTimer = new Timer();
            clockTimer.Interval = 1000; // Update every second
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

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

            // Products Submenu
            if (btnBrands != null) btnBrands.Click += btnBrands_Click;
            if (btnCategories != null) btnCategories.Click += btnCategories_Click;
            if (btnUnits != null) btnUnits.Click += btnUnits_Click;
            if (btnPrintLabels != null) btnPrintLabels.Click += btnPrintLabels_Click;
            if (btnAddProducts != null) btnAddProducts.Click += btnAddProducts_Click;
            if (btnListProducts != null) btnListProducts.Click += btnListProducts_Click;

            // Profile and Sign Out
            if (btnProfile != null) btnProfile.Click += btnProfile_Click;
            if (btnSignOut != null) btnSignOut.Click += btnSignOut_Click;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            lblDate.Text = DateTime.Now.ToString("ddd, MMM dd, yyyy");
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
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
        /// Update the user's first name in the top bar
        /// </summary>
        public void UpdateUserFirstName()
        {
            if (DataSetApp?.User != null && DataSetApp.User.Rows.Count > 0)
            {
                var userRow = DataSetApp.User[0];
                if (!userRow.Isfull_nameNull() && !string.IsNullOrWhiteSpace(userRow.full_name))
                {
                    // Extract first name from full name
                    string fullName = userRow.full_name;
                    string firstName = fullName.Split(' ')[0];
                    lblUserFirstname.Text = firstName;
                }
                else
                {
                    lblUserFirstname.Text = "User";
                }
            }
            else
            {
                lblUserFirstname.Text = "User";
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

            // Update user name when navigation is shown
            if (!hideNavigation)
            {
                UpdateUserFirstName();
                ApplyPermissionBasedVisibility();
            }
        }

        /// <summary>
        /// Apply permission-based visibility to all menu items and buttons
        /// </summary>
        private void ApplyPermissionBasedVisibility()
        {
            try
            {
                // Suspend layout to prevent flickering
                panelSideBar.SuspendLayout();

                // User Management
                bool hasUserPermissions = BLL.PermissionManager.HasAnyPermission(
                    BLL.PermissionManager.Permissions.VIEW_USERS,
                    BLL.PermissionManager.Permissions.VIEW_ROLES);
                    
                if (panelUserManagementHeader != null)
                    panelUserManagementHeader.Visible = hasUserPermissions;
                if (btnUserManagement != null)
                    btnUserManagement.Visible = hasUserPermissions;

                if (btnUsers != null)
                    btnUsers.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_USERS);

                if (btnRoles != null)
                    btnRoles.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_ROLES);

                // Hide submenu panel if no child buttons are visible
                if (panelUserManagementSubmenu != null)
                {
                    bool hasVisibleChildren = false;
                    foreach (Control ctrl in panelUserManagementSubmenu.Controls)
                    {
                        if (ctrl.Visible) { hasVisibleChildren = true; break; }
                    }
                    if (!hasVisibleChildren) panelUserManagementSubmenu.Visible = false;
                }

                // Contacts
                bool hasContactPermissions = BLL.PermissionManager.HasAnyPermission(
                    BLL.PermissionManager.Permissions.VIEW_SUPPLIERS,
                    BLL.PermissionManager.Permissions.VIEW_CUSTOMERS,
                    BLL.PermissionManager.Permissions.VIEW_CUSTOMER_GROUPS);
                    
                if (panelContactsHeader != null)
                    panelContactsHeader.Visible = hasContactPermissions;
                if (btnContacts != null)
                    btnContacts.Visible = hasContactPermissions;

                if (btnSuppliers != null)
                    btnSuppliers.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_SUPPLIERS);

                if (btnCustomers != null)
                    btnCustomers.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_CUSTOMERS);

                if (btnCustomerGroups != null)
                    btnCustomerGroups.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_CUSTOMER_GROUPS);

                if (panelContactsSubmenu != null)
                {
                    bool hasVisibleChildren = false;
                    foreach (Control ctrl in panelContactsSubmenu.Controls)
                    {
                        if (ctrl.Visible) { hasVisibleChildren = true; break; }
                    }
                    if (!hasVisibleChildren) panelContactsSubmenu.Visible = false;
                }

                // Products
                bool hasProductPermissions = BLL.PermissionManager.HasAnyPermission(
                    BLL.PermissionManager.Permissions.VIEW_PRODUCTS,
                    BLL.PermissionManager.Permissions.VIEW_CATEGORIES,
                    BLL.PermissionManager.Permissions.VIEW_BRANDS,
                    BLL.PermissionManager.Permissions.VIEW_UNITS,
                    BLL.PermissionManager.Permissions.VIEW_PRINT_LABELS);
                    
                if (panelProductsHeader != null)
                    panelProductsHeader.Visible = hasProductPermissions;
                if (btnProducts != null)
                    btnProducts.Visible = hasProductPermissions;

                if (btnListProducts != null)
                    btnListProducts.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_PRODUCTS);

                if (btnAddProducts != null)
                    btnAddProducts.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.ADD_PRODUCTS);

                if (btnCategories != null)
                    btnCategories.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_CATEGORIES);

                if (btnBrands != null)
                    btnBrands.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_BRANDS);

                if (btnUnits != null)
                    btnUnits.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_UNITS);

                if (btnPrintLabels != null)
                    btnPrintLabels.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_PRINT_LABELS);

                if (panelProductsSubmenu != null)
                {
                    bool hasVisibleChildren = false;
                    foreach (Control ctrl in panelProductsSubmenu.Controls)
                    {
                        if (ctrl.Visible) { hasVisibleChildren = true; break; }
                    }
                    if (!hasVisibleChildren) panelProductsSubmenu.Visible = false;
                }

                // Sell
                bool hasSellPermissions = BLL.PermissionManager.HasAnyPermission(
                    BLL.PermissionManager.Permissions.VIEW_SALES,
                    BLL.PermissionManager.Permissions.VIEW_DRAFTS,
                    BLL.PermissionManager.Permissions.VIEW_QUOTATIONS,
                    BLL.PermissionManager.Permissions.VIEW_SELL_RETURNS,
                    BLL.PermissionManager.Permissions.VIEW_DISCOUNTS);
                    
                if (panelSellHeader != null)
                    panelSellHeader.Visible = hasSellPermissions;
                if (btnSell != null)
                    btnSell.Visible = hasSellPermissions;

                if (btnAllSales != null)
                    btnAllSales.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_SALES);

                if (btnListDrafts != null)
                    btnListDrafts.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_DRAFTS);

                if (btnListQuotations != null)
                    btnListQuotations.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_QUOTATIONS);

                if (btnSellReturns != null)
                    btnSellReturns.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_SELL_RETURNS);

                if (btnDiscounts != null)
                    btnDiscounts.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_DISCOUNTS);

                if (panelSellSubmenu != null)
                {
                    bool hasVisibleChildren = false;
                    foreach (Control ctrl in panelSellSubmenu.Controls)
                    {
                        if (ctrl.Visible) { hasVisibleChildren = true; break; }
                    }
                    if (!hasVisibleChildren) panelSellSubmenu.Visible = false;
                }

                // POS
                if (btnPOS != null)
                    btnPOS.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.ACCESS_SALES_TERMINAL);

                // Reports
                bool hasReportPermissions = BLL.PermissionManager.HasAnyPermission(
                    BLL.PermissionManager.Permissions.VIEW_SUPPLIER_CUSTOMER_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_ITEMS_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_TRENDING_PRODUCTS,
                    BLL.PermissionManager.Permissions.VIEW_STOCK_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_CUSTOMER_GROUP_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_PRODUCT_SELL_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_ACTIVITY_LOG,
                    BLL.PermissionManager.Permissions.VIEW_TABLE_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_SALES_REPRESENTATIVE_REPORT,
                    BLL.PermissionManager.Permissions.VIEW_SELL_PAYMENT_REPORT);
                    
                if (panelReportsHeader != null)
                    panelReportsHeader.Visible = hasReportPermissions;
                if (btnReports != null)
                    btnReports.Visible = hasReportPermissions;

                if (btnSupplierAndCustomerReport != null)
                    btnSupplierAndCustomerReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_SUPPLIER_CUSTOMER_REPORT);

                if (btnItemsReport != null)
                    btnItemsReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_ITEMS_REPORT);

                if (btnTrendingProducts != null)
                    btnTrendingProducts.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_TRENDING_PRODUCTS);

                if (btnStockReport != null)
                    btnStockReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_STOCK_REPORT);

                if (btnCustomerGroupReport != null)
                    btnCustomerGroupReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_CUSTOMER_GROUP_REPORT);

                if (btnProductSellReport != null)
                    btnProductSellReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_PRODUCT_SELL_REPORT);

                if (btnActivityLog != null)
                    btnActivityLog.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_ACTIVITY_LOG);

                if (btnTableReport != null)
                    btnTableReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_TABLE_REPORT);

                if (btnSalesRepresentativeReport != null)
                    btnSalesRepresentativeReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_SALES_REPRESENTATIVE_REPORT);

                if (btnSellPaymentReport != null)
                    btnSellPaymentReport.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_SELL_PAYMENT_REPORT);

                if (panelReportsSubmenu != null)
                {
                    bool hasVisibleChildren = false;
                    foreach (Control ctrl in panelReportsSubmenu.Controls)
                    {
                        if (ctrl.Visible) { hasVisibleChildren = true; break; }
                    }
                    if (!hasVisibleChildren) panelReportsSubmenu.Visible = false;
                }

                // Settings
                bool hasSettingsPermissions = BLL.PermissionManager.HasAnyPermission(
                    BLL.PermissionManager.Permissions.VIEW_BUSINESS_SETTINGS,
                    BLL.PermissionManager.Permissions.VIEW_TABLES,
                    BLL.PermissionManager.Permissions.VIEW_BUSINESS_LOCATIONS);
                    
                if (panelSettingsHeader != null)
                    panelSettingsHeader.Visible = hasSettingsPermissions;
                if (btnSettings != null)
                    btnSettings.Visible = hasSettingsPermissions;

                if (btnBusinessSettings != null)
                    btnBusinessSettings.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_BUSINESS_SETTINGS);

                if (btnTables != null)
                    btnTables.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_TABLES);

                if (btnBusinessLocations != null)
                    btnBusinessLocations.Visible = BLL.PermissionManager.HasPermission(BLL.PermissionManager.Permissions.VIEW_BUSINESS_LOCATIONS);

                if (panelSettingsSubmenu != null)
                {
                    bool hasVisibleChildren = false;
                    foreach (Control ctrl in panelSettingsSubmenu.Controls)
                    {
                        if (ctrl.Visible) { hasVisibleChildren = true; break; }
                    }
                    if (!hasVisibleChildren) panelSettingsSubmenu.Visible = false;
                }

                // Resume layout and force refresh
                panelSideBar.ResumeLayout(true);
                panelSideBar.PerformLayout();
                panelSideBar.Refresh();
            }
            catch (Exception)
            {
                // If there's an error applying permissions, fail safely by showing all menus
                panelSideBar.ResumeLayout(true);
            }
        }

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            ToggleSidebar();
        }

        private void ToggleSidebar()
        {
            if (isCollapsed)
            {
                panelSideBar.Width = expandedWidth;
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
            else
            {
                panelSideBar.Width = collapsedWidth;
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

        private void btnUsers_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_User_Management());
        }

        private void btnBrands_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Brand_Management());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Category_Management());
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Unit_Management());
        }

        private void btnPrintLabels_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_BarcodePrint());
        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Product_Registration());
        }

        private void btnListProducts_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Product_Management());
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Role_Management());
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Profile_Management());
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            // Confirm sign out
            var result = XtraMessageBox.Show(
                "Are you sure you want to sign out?",
                "Confirm Sign Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Clear user data
                DataSetApp.User.Clear();
                DataSetApp.RolePermission.Clear();
                
                // Clear permissions from PermissionManager
                BLL.PermissionManager.ClearPermissions();

                // Load login screen
                UC_Login login = new UC_Login();
                LoadUserControl(login, hideNavigation: true);
            }
        }

        private void btnSellReturns_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SellReturn_Management());
        }

        private void btnProfitLossReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_ProfitLoss_Report());
        }

        private void btnSupplierAndCustomerReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SupplierCustomer_Report());
        }

        private void btnCustomerGroupReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_CustomerGroup_Report());
        }

        private void btnStockReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Stock_Report());
        }

        private void btnTrendingProducts_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_TrendingProducts_Report());
        }

        private void btnItemsReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Items_Report());
        }

        private void btnProductSellReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_ProductSell_Report());
        }

        private void btnSellPaymentReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SellPayment_Report());
        }

        private void btnSalesRepresentativeReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SalesRepresentative_Report());
        }

        private void btnTableReport_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Table_Report());
        }

        private void btnActivityLog_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_ActivityLog_Report());
        }
    }
}
