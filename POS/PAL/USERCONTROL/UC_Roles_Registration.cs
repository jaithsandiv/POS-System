using DevExpress.XtraEditors;
using POS.BLL;
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
    public partial class UC_Roles_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private int? _editRoleId = null;
        private bool _isEditMode = false;

        public UC_Roles_Registration()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        public UC_Roles_Registration(int roleId) : this()
        {
            _editRoleId = roleId;
            _isEditMode = true;
            LoadRoleData();
        }

        /// <summary>
        /// Initialize event handlers for "Select All" checkboxes
        /// </summary>
        private void InitializeEventHandlers()
        {
            // Register button click event
            regBtn.Click += regBtn_Click;

            // Only keep Reports and Settings "Select All" handlers
            
            checkEditSelectAllReports.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllReports, 
                checkEditViewSupplierCustomerReport, checkEditViewItemsReport, checkEditViewTrendingProducts, 
                checkEditViewStockReport, checkEditViewCustomerGroupReport, checkEditViewProductSellReport, 
                checkEditViewActivityLog, checkEditViewTableReport, checkEditViewSalesRepresentativeReport, 
                checkEditViewSellPaymentReport);
            
            checkEditSelectAllSettings.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllSettings, 
                checkEditViewBusinessSettings, checkEditViewTables, checkEditViewBusinessLocations);
        }

        /// <summary>
        /// Toggle all checkboxes in a group
        /// </summary>
        private void ToggleGroupCheckboxes(CheckEdit selectAllCheckbox, params CheckEdit[] checkboxes)
        {
            bool isChecked = selectAllCheckbox.Checked;
            foreach (var checkbox in checkboxes)
            {
                if (checkbox != null)
                    checkbox.Checked = isChecked;
            }
        }

        /// <summary>
        /// Load role data for editing
        /// </summary>
        private void LoadRoleData()
        {
            try
            {
                if (!_editRoleId.HasValue)
                    return;

                DataTable roleData = BLL_Role.GetRoleById(_editRoleId.Value);
                if (roleData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Role not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow role = roleData.Rows[0];
                txtRoleName.Text = role["role_name"]?.ToString();
                txtDescription.Text = role["description"]?.ToString();

                // Load permissions
                DataTable permissions = BLL_Role.GetRolePermissions(_editRoleId.Value);
                HashSet<string> permissionSet = new HashSet<string>();
                
                foreach (DataRow permRow in permissions.Rows)
                {
                    permissionSet.Add(permRow["permission_code"].ToString());
                }

                // Map permissions to checkboxes
                MapPermissionsToCheckboxes(permissionSet);

                // Update UI
                regBtn.Text = "Update";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading role data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Map permission codes to checkboxes
        /// </summary>
        private void MapPermissionsToCheckboxes(HashSet<string> permissions)
        {
            // Other
            if (checkEditViewExportButtons != null)
                checkEditViewExportButtons.Checked = permissions.Contains("VIEW_EXPORT_BUTTONS");

            // Users
            if (checkEditViewUser != null)
                checkEditViewUser.Checked = permissions.Contains("VIEW_USERS");

            // Roles
            if (checkEditViewRoles != null)
                checkEditViewRoles.Checked = permissions.Contains("VIEW_ROLES");

            // Suppliers
            if (checkEditViewSuppliers != null)
                checkEditViewSuppliers.Checked = permissions.Contains("VIEW_SUPPLIERS");

            // Customers
            if (checkEditViewCustomers != null)
                checkEditViewCustomers.Checked = permissions.Contains("VIEW_CUSTOMERS");

            // Products
            if (checkEditViewProducts != null)
                checkEditViewProducts.Checked = permissions.Contains("VIEW_PRODUCTS");

            // Customer Groups
            if (checkEditViewCustomerGroups != null)
                checkEditViewCustomerGroups.Checked = permissions.Contains("VIEW_CUSTOMER_GROUPS");

            // Categories
            if (checkEditViewCategories != null)
                checkEditViewCategories.Checked = permissions.Contains("VIEW_CATEGORIES");

            // Print Labels
            if (checkEditAccesstoPrintLabelsForm != null)
                checkEditAccesstoPrintLabelsForm.Checked = permissions.Contains("VIEW_PRINT_LABELS");

            // Units
            if (checkEditViewUnits != null)
                checkEditViewUnits.Checked = permissions.Contains("VIEW_UNITS");

            // Brands
            if (checkEditViewBrands != null)
                checkEditViewBrands.Checked = permissions.Contains("VIEW_BRANDS");

            // Sales
            if (checkEditViewSales != null)
                checkEditViewSales.Checked = permissions.Contains("VIEW_SALES");

            // Drafts
            if (checkEditViewDrafts != null)
                checkEditViewDrafts.Checked = permissions.Contains("VIEW_DRAFTS");

            // Quotations
            if (checkEditViewQuotations != null)
                checkEditViewQuotations.Checked = permissions.Contains("VIEW_QUOTATIONS");

            // Sell Returns
            if (checkEditSellReturns != null)
                checkEditSellReturns.Checked = permissions.Contains("VIEW_SELL_RETURNS");

            // Discounts
            if (checkEditViewDiscounts != null)
                checkEditViewDiscounts.Checked = permissions.Contains("VIEW_DISCOUNTS");

            // POS
            if (checkEditAccesstoSalesTerminal != null)
                checkEditAccesstoSalesTerminal.Checked = permissions.Contains("ACCESS_SALES_TERMINAL");

            // Reports
            if (checkEditViewSupplierCustomerReport != null)
                checkEditViewSupplierCustomerReport.Checked = permissions.Contains("VIEW_SUPPLIER_CUSTOMER_REPORT");
            if (checkEditViewItemsReport != null)
                checkEditViewItemsReport.Checked = permissions.Contains("VIEW_ITEMS_REPORT");
            if (checkEditViewTrendingProducts != null)
                checkEditViewTrendingProducts.Checked = permissions.Contains("VIEW_TRENDING_PRODUCTS");
            if (checkEditViewStockReport != null)
                checkEditViewStockReport.Checked = permissions.Contains("VIEW_STOCK_REPORT");
            if (checkEditViewCustomerGroupReport != null)
                checkEditViewCustomerGroupReport.Checked = permissions.Contains("VIEW_CUSTOMER_GROUP_REPORT");
            if (checkEditViewProductSellReport != null)
                checkEditViewProductSellReport.Checked = permissions.Contains("VIEW_PRODUCT_SELL_REPORT");
            if (checkEditViewActivityLog != null)
                checkEditViewActivityLog.Checked = permissions.Contains("VIEW_ACTIVITY_LOG");
            if (checkEditViewTableReport != null)
                checkEditViewTableReport.Checked = permissions.Contains("VIEW_TABLE_REPORT");
            if (checkEditViewSalesRepresentativeReport != null)
                checkEditViewSalesRepresentativeReport.Checked = permissions.Contains("VIEW_SALES_REPRESENTATIVE_REPORT");
            if (checkEditViewSellPaymentReport != null)
                checkEditViewSellPaymentReport.Checked = permissions.Contains("VIEW_SELL_PAYMENT_REPORT");

            // Settings
            if (checkEditViewBusinessSettings != null)
                checkEditViewBusinessSettings.Checked = permissions.Contains("VIEW_BUSINESS_SETTINGS");
            if (checkEditViewTables != null)
                checkEditViewTables.Checked = permissions.Contains("VIEW_TABLES");
            if (checkEditViewBusinessLocations != null)
                checkEditViewBusinessLocations.Checked = permissions.Contains("VIEW_BUSINESS_LOCATIONS");

            // Dashboard
            if (checkEditViewDashboard != null)
                checkEditViewDashboard.Checked = permissions.Contains("VIEW_DASHBOARD");
        }

        /// <summary>
        /// Collect all selected permissions from checkboxes
        /// </summary>
        private List<string> CollectSelectedPermissions()
        {
            List<string> permissions = new List<string>();

            // Other
            if (checkEditViewExportButtons?.Checked == true) permissions.Add("VIEW_EXPORT_BUTTONS");

            // Users
            if (checkEditViewUser?.Checked == true) permissions.Add("VIEW_USERS");

            // Roles
            if (checkEditViewRoles?.Checked == true) permissions.Add("VIEW_ROLES");

            // Suppliers
            if (checkEditViewSuppliers?.Checked == true) permissions.Add("VIEW_SUPPLIERS");

            // Customers
            if (checkEditViewCustomers?.Checked == true) permissions.Add("VIEW_CUSTOMERS");

            // Products
            if (checkEditViewProducts?.Checked == true) permissions.Add("VIEW_PRODUCTS");

            // Customer Groups
            if (checkEditViewCustomerGroups?.Checked == true) permissions.Add("VIEW_CUSTOMER_GROUPS");

            // Categories
            if (checkEditViewCategories?.Checked == true) permissions.Add("VIEW_CATEGORIES");

            // Print Labels
            if (checkEditAccesstoPrintLabelsForm?.Checked == true) permissions.Add("VIEW_PRINT_LABELS");

            // Units
            if (checkEditViewUnits?.Checked == true) permissions.Add("VIEW_UNITS");

            // Brands
            if (checkEditViewBrands?.Checked == true) permissions.Add("VIEW_BRANDS");

            // Sales
            if (checkEditViewSales?.Checked == true) permissions.Add("VIEW_SALES");

            // Drafts
            if (checkEditViewDrafts?.Checked == true) permissions.Add("VIEW_DRAFTS");

            // Quotations
            if (checkEditViewQuotations?.Checked == true) permissions.Add("VIEW_QUOTATIONS");

            // Sell Returns
            if (checkEditSellReturns?.Checked == true) permissions.Add("VIEW_SELL_RETURNS");

            // Discounts
            if (checkEditViewDiscounts?.Checked == true) permissions.Add("VIEW_DISCOUNTS");

            // POS
            if (checkEditAccesstoSalesTerminal?.Checked == true) permissions.Add("ACCESS_SALES_TERMINAL");

            // Reports
            if (checkEditViewSupplierCustomerReport?.Checked == true) permissions.Add("VIEW_SUPPLIER_CUSTOMER_REPORT");
            if (checkEditViewItemsReport?.Checked == true) permissions.Add("VIEW_ITEMS_REPORT");
            if (checkEditViewTrendingProducts?.Checked == true) permissions.Add("VIEW_TRENDING_PRODUCTS");
            if (checkEditViewStockReport?.Checked == true) permissions.Add("VIEW_STOCK_REPORT");
            if (checkEditViewCustomerGroupReport?.Checked == true) permissions.Add("VIEW_CUSTOMER_GROUP_REPORT");
            if (checkEditViewProductSellReport?.Checked == true) permissions.Add("VIEW_PRODUCT_SELL_REPORT");
            if (checkEditViewActivityLog?.Checked == true) permissions.Add("VIEW_ACTIVITY_LOG");
            if (checkEditViewTableReport?.Checked == true) permissions.Add("VIEW_TABLE_REPORT");
            if (checkEditViewSalesRepresentativeReport?.Checked == true) permissions.Add("VIEW_SALES_REPRESENTATIVE_REPORT");
            if (checkEditViewSellPaymentReport?.Checked == true) permissions.Add("VIEW_SELL_PAYMENT_REPORT");

            // Settings
            if (checkEditViewBusinessSettings?.Checked == true) permissions.Add("VIEW_BUSINESS_SETTINGS");
            if (checkEditViewTables?.Checked == true) permissions.Add("VIEW_TABLES");
            if (checkEditViewBusinessLocations?.Checked == true) permissions.Add("VIEW_BUSINESS_LOCATIONS");

            // Dashboard
            if (checkEditViewDashboard?.Checked == true) permissions.Add("VIEW_DASHBOARD");

            return permissions;
        }

        /// <summary>
        /// Validate role name input
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                XtraMessageBox.Show("Role name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handle Register/Update button click
        /// </summary>
        private void regBtn_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
                UpdateRole();
            else
                RegisterRole();
        }

        /// <summary>
        /// Register a new role
        /// </summary>
        private void RegisterRole()
        {
            try
            {
                if (!ValidateInput())
                    return;

                // Get current user ID
                int currentUserId = 1;
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Collect selected permissions
                List<string> permissions = CollectSelectedPermissions();

                // Get description (can be null or empty)
                string description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();

                // Insert role with permissions
                int roleId = BLL_Role.InsertRole(
                    txtRoleName.Text.Trim(),
                    description,
                    permissions,
                    currentUserId
                );

                if (roleId > 0)
                {
                    XtraMessageBox.Show(
                        "Role registered successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to role management
                    Main.Instance.LoadUserControl(new UC_Role_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to register role.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error registering role: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Update an existing role
        /// </summary>
        private void UpdateRole()
        {
            try
            {
                if (!ValidateInput())
                    return;

                if (!_editRoleId.HasValue)
                    return;

                // Get current user ID
                int currentUserId = 1;
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Collect selected permissions
                List<string> permissions = CollectSelectedPermissions();

                // Get description (can be null or empty)
                string description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim();

                // Update role with permissions
                bool success = BLL_Role.UpdateRole(
                    _editRoleId.Value,
                    txtRoleName.Text.Trim(),
                    description,
                    permissions,
                    currentUserId
                );

                if (success)
                {
                    XtraMessageBox.Show(
                        "Role updated successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Navigate back to role management
                    Main.Instance.LoadUserControl(new UC_Role_Management());
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to update role.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating role: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void backBtn2_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Role_Management());
        }
    }
}
