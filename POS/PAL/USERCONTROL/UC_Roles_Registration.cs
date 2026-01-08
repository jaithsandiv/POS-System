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

        // Declare the discount-related checkboxes that have "this." prefix in Designer
        private DevExpress.XtraEditors.CheckEdit checkEditViewDiscounts;

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

            // Select All checkboxes
            checkEditSelectAllOther.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllOther, checkEditViewExportButtons);
            
            checkEditSelectAllUsers.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllUsers, 
                checkEditViewUser, checkEditAddUser, checkEditEditUser, checkEditDeleteUser);
            
            checkEditSelectAllRoles.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllRoles, 
                checkEditViewRoles, checkEditAddRoles, checkEditEditRoles, checkEditDeleteRoles);
            
            checkEditSelectAllSuppliers.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllSuppliers, 
                checkEditViewSuppliers, checkEditAddSuppliers, checkEditEditSuppliers, checkEditDeleteSuppliers);
            
            checkEditSelectAllCustomers.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllCustomers, 
                checkEditViewCustomers, checkEditAddCustomers, checkEditEditCustomers, checkEditDeleteCustomers, checkEditViewCustomersDetails);
            
            checkEditSelectAllProducts.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllProducts, 
                checkEditViewProducts, checkEditAddProducts, checkEdit16, checkEdit17);
            
            checkEditSelectAllCustomerGroups.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllCustomerGroups, 
                checkEditViewCustomerGroups, checkEditAddCustomerGroups, checkEditEditCustomerGroups, checkEditDeleteCustomerGroups);
            
            checkEditSelectAllCategory.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllCategory, 
                checkEditViewCategories, checkEditAddCategories, checkEditEditCategories, checkEditDeleteCategories);
            
            checkEditSelectAllPrintLabels.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllPrintLabels, 
                checkEditAccesstoPrintLabelsForm);
            
            checkEditSelectAllUnits.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllUnits, 
                checkEditViewUnits, checkEditAddUnits, checkEditEditUnits, checkEditDeleteUnits);
            
            checkEditSelectAllBrands.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllBrands, 
                checkEditViewBrands, checkEditAddBrands, checkEditEditBrands, checkEditDeleteBrands);
            
            checkEditSelectAllSales.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllSales, 
                checkEditViewSales, checkEditAddSales);
            
            checkEditSelectAllDrafts.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllDrafts, 
                checkEditViewDrafts, checkEditAddDrafts);
            
            checkEditSelectAllQuotations.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllQuotations, 
                checkEditViewQuotations, checkEditQuotations);
            
            checkEditSelectAllSellReturns.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllSellReturns, 
                checkEditSellReturns, checkEditAddSellReturns);
            
            checkEditSelectAllDiscounts.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllDiscounts, 
                checkEditViewDiscounts, checkEditAddDiscounts, checkEditAssignDiscounts);
            
            checkEdit42.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEdit42, 
                checkEditAccesstoSalesTerminal);
            
            checkEditSelectAllReports.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllReports, 
                checkEditViewSupplierCustomerReport, checkEditViewItemsReport, checkEditViewTrendingProducts, 
                checkEditViewStockReport, checkEditViewCustomerGroupReport, checkEditViewProductSellReport, 
                checkEditViewActivityLog, checkEditViewTableReport, checkEditViewSalesRepresentativeReport, 
                checkEditViewSellPaymentReport);
            
            checkEditSelectAllSettings.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllSettings, 
                checkEditViewBusinessSettings, checkEditViewTables, checkEditViewBusinessLocations);
            
            checkEditSelectAllHome.CheckedChanged += (s, e) => ToggleGroupCheckboxes(checkEditSelectAllHome, 
                checkEditViewDashboard);
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
            if (checkEditAddUser != null)
                checkEditAddUser.Checked = permissions.Contains("ADD_USERS");
            if (checkEditEditUser != null)
                checkEditEditUser.Checked = permissions.Contains("EDIT_USERS");
            if (checkEditDeleteUser != null)
                checkEditDeleteUser.Checked = permissions.Contains("DELETE_USERS");

            // Roles
            if (checkEditViewRoles != null)
                checkEditViewRoles.Checked = permissions.Contains("VIEW_ROLES");
            if (checkEditAddRoles != null)
                checkEditAddRoles.Checked = permissions.Contains("ADD_ROLES");
            if (checkEditEditRoles != null)
                checkEditEditRoles.Checked = permissions.Contains("EDIT_ROLES");
            if (checkEditDeleteRoles != null)
                checkEditDeleteRoles.Checked = permissions.Contains("DELETE_ROLES");

            // Suppliers
            if (checkEditViewSuppliers != null)
                checkEditViewSuppliers.Checked = permissions.Contains("VIEW_SUPPLIERS");
            if (checkEditAddSuppliers != null)
                checkEditAddSuppliers.Checked = permissions.Contains("ADD_SUPPLIERS");
            if (checkEditEditSuppliers != null)
                checkEditEditSuppliers.Checked = permissions.Contains("EDIT_SUPPLIERS");
            if (checkEditDeleteSuppliers != null)
                checkEditDeleteSuppliers.Checked = permissions.Contains("DELETE_SUPPLIERS");

            // Customers
            if (checkEditViewCustomers != null)
                checkEditViewCustomers.Checked = permissions.Contains("VIEW_CUSTOMERS");
            if (checkEditAddCustomers != null)
                checkEditAddCustomers.Checked = permissions.Contains("ADD_CUSTOMERS");
            if (checkEditEditCustomers != null)
                checkEditEditCustomers.Checked = permissions.Contains("EDIT_CUSTOMERS");
            if (checkEditDeleteCustomers != null)
                checkEditDeleteCustomers.Checked = permissions.Contains("DELETE_CUSTOMERS");
            if (checkEditViewCustomersDetails != null)
                checkEditViewCustomersDetails.Checked = permissions.Contains("VIEW_CUSTOMER_DETAILS");

            // Products
            if (checkEditViewProducts != null)
                checkEditViewProducts.Checked = permissions.Contains("VIEW_PRODUCTS");
            if (checkEditAddProducts != null)
                checkEditAddProducts.Checked = permissions.Contains("ADD_PRODUCTS");
            if (checkEdit16 != null)
                checkEdit16.Checked = permissions.Contains("EDIT_PRODUCTS");
            if (checkEdit17 != null)
                checkEdit17.Checked = permissions.Contains("DELETE_PRODUCTS");

            // Customer Groups
            if (checkEditViewCustomerGroups != null)
                checkEditViewCustomerGroups.Checked = permissions.Contains("VIEW_CUSTOMER_GROUPS");
            if (checkEditAddCustomerGroups != null)
                checkEditAddCustomerGroups.Checked = permissions.Contains("ADD_CUSTOMER_GROUPS");
            if (checkEditEditCustomerGroups != null)
                checkEditEditCustomerGroups.Checked = permissions.Contains("EDIT_CUSTOMER_GROUPS");
            if (checkEditDeleteCustomerGroups != null)
                checkEditDeleteCustomerGroups.Checked = permissions.Contains("DELETE_CUSTOMER_GROUPS");

            // Categories
            if (checkEditViewCategories != null)
                checkEditViewCategories.Checked = permissions.Contains("VIEW_CATEGORIES");
            if (checkEditAddCategories != null)
                checkEditAddCategories.Checked = permissions.Contains("ADD_CATEGORIES");
            if (checkEditEditCategories != null)
                checkEditEditCategories.Checked = permissions.Contains("EDIT_CATEGORIES");
            if (checkEditDeleteCategories != null)
                checkEditDeleteCategories.Checked = permissions.Contains("DELETE_CATEGORIES");

            // Print Labels
            if (checkEditAccesstoPrintLabelsForm != null)
                checkEditAccesstoPrintLabelsForm.Checked = permissions.Contains("VIEW_PRINT_LABELS");

            // Units
            if (checkEditViewUnits != null)
                checkEditViewUnits.Checked = permissions.Contains("VIEW_UNITS");
            if (checkEditAddUnits != null)
                checkEditAddUnits.Checked = permissions.Contains("ADD_UNITS");
            if (checkEditEditUnits != null)
                checkEditEditUnits.Checked = permissions.Contains("EDIT_UNITS");
            if (checkEditDeleteUnits != null)
                checkEditDeleteUnits.Checked = permissions.Contains("DELETE_UNITS");

            // Brands
            if (checkEditViewBrands != null)
                checkEditViewBrands.Checked = permissions.Contains("VIEW_BRANDS");
            if (checkEditAddBrands != null)
                checkEditAddBrands.Checked = permissions.Contains("ADD_BRANDS");
            if (checkEditEditBrands != null)
                checkEditEditBrands.Checked = permissions.Contains("EDIT_BRANDS");
            if (checkEditDeleteBrands != null)
                checkEditDeleteBrands.Checked = permissions.Contains("DELETE_BRANDS");

            // Sales
            if (checkEditViewSales != null)
                checkEditViewSales.Checked = permissions.Contains("VIEW_SALES");
            if (checkEditAddSales != null)
                checkEditAddSales.Checked = permissions.Contains("ADD_SALES");

            // Drafts
            if (checkEditViewDrafts != null)
                checkEditViewDrafts.Checked = permissions.Contains("VIEW_DRAFTS");
            if (checkEditAddDrafts != null)
                checkEditAddDrafts.Checked = permissions.Contains("ADD_DRAFTS");

            // Quotations
            if (checkEditViewQuotations != null)
                checkEditViewQuotations.Checked = permissions.Contains("VIEW_QUOTATIONS");
            if (checkEditQuotations != null)
                checkEditQuotations.Checked = permissions.Contains("ADD_QUOTATIONS");

            // Sell Returns
            if (checkEditSellReturns != null)
                checkEditSellReturns.Checked = permissions.Contains("VIEW_SELL_RETURNS");
            if (checkEditAddSellReturns != null)
                checkEditAddSellReturns.Checked = permissions.Contains("ADD_SELL_RETURNS");

            // Discounts
            if (checkEditViewDiscounts != null)
                checkEditViewDiscounts.Checked = permissions.Contains("VIEW_DISCOUNTS");
            if (checkEditAddDiscounts != null)
                checkEditAddDiscounts.Checked = permissions.Contains("ADD_DISCOUNTS");
            if (checkEditAssignDiscounts != null)
                checkEditAssignDiscounts.Checked = permissions.Contains("ASSIGN_DISCOUNTS");

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
            if (checkEditAddUser?.Checked == true) permissions.Add("ADD_USERS");
            if (checkEditEditUser?.Checked == true) permissions.Add("EDIT_USERS");
            if (checkEditDeleteUser?.Checked == true) permissions.Add("DELETE_USERS");

            // Roles
            if (checkEditViewRoles?.Checked == true) permissions.Add("VIEW_ROLES");
            if (checkEditAddRoles?.Checked == true) permissions.Add("ADD_ROLES");
            if (checkEditEditRoles?.Checked == true) permissions.Add("EDIT_ROLES");
            if (checkEditDeleteRoles?.Checked == true) permissions.Add("DELETE_ROLES");

            // Suppliers
            if (checkEditViewSuppliers?.Checked == true) permissions.Add("VIEW_SUPPLIERS");
            if (checkEditAddSuppliers?.Checked == true) permissions.Add("ADD_SUPPLIERS");
            if (checkEditEditSuppliers?.Checked == true) permissions.Add("EDIT_SUPPLIERS");
            if (checkEditDeleteSuppliers?.Checked == true) permissions.Add("DELETE_SUPPLIERS");

            // Customers
            if (checkEditViewCustomers?.Checked == true) permissions.Add("VIEW_CUSTOMERS");
            if (checkEditAddCustomers?.Checked == true) permissions.Add("ADD_CUSTOMERS");
            if (checkEditEditCustomers?.Checked == true) permissions.Add("EDIT_CUSTOMERS");
            if (checkEditDeleteCustomers?.Checked == true) permissions.Add("DELETE_CUSTOMERS");
            if (checkEditViewCustomersDetails?.Checked == true) permissions.Add("VIEW_CUSTOMER_DETAILS");

            // Products
            if (checkEditViewProducts?.Checked == true) permissions.Add("VIEW_PRODUCTS");
            if (checkEditAddProducts?.Checked == true) permissions.Add("ADD_PRODUCTS");
            if (checkEdit16?.Checked == true) permissions.Add("EDIT_PRODUCTS");
            if (checkEdit17?.Checked == true) permissions.Add("DELETE_PRODUCTS");

            // Customer Groups
            if (checkEditViewCustomerGroups?.Checked == true) permissions.Add("VIEW_CUSTOMER_GROUPS");
            if (checkEditAddCustomerGroups?.Checked == true) permissions.Add("ADD_CUSTOMER_GROUPS");
            if (checkEditEditCustomerGroups?.Checked == true) permissions.Add("EDIT_CUSTOMER_GROUPS");
            if (checkEditDeleteCustomerGroups?.Checked == true) permissions.Add("DELETE_CUSTOMER_GROUPS");

            // Categories
            if (checkEditViewCategories?.Checked == true) permissions.Add("VIEW_CATEGORIES");
            if (checkEditAddCategories?.Checked == true) permissions.Add("ADD_CATEGORIES");
            if (checkEditEditCategories?.Checked == true) permissions.Add("EDIT_CATEGORIES");
            if (checkEditDeleteCategories?.Checked == true) permissions.Add("DELETE_CATEGORIES");

            // Print Labels
            if (checkEditAccesstoPrintLabelsForm?.Checked == true) permissions.Add("VIEW_PRINT_LABELS");

            // Units
            if (checkEditViewUnits?.Checked == true) permissions.Add("VIEW_UNITS");
            if (checkEditAddUnits?.Checked == true) permissions.Add("ADD_UNITS");
            if (checkEditEditUnits?.Checked == true) permissions.Add("EDIT_UNITS");
            if (checkEditDeleteUnits?.Checked == true) permissions.Add("DELETE_UNITS");

            // Brands
            if (checkEditViewBrands?.Checked == true) permissions.Add("VIEW_BRANDS");
            if (checkEditAddBrands?.Checked == true) permissions.Add("ADD_BRANDS");
            if (checkEditEditBrands?.Checked == true) permissions.Add("EDIT_BRANDS");
            if (checkEditDeleteBrands?.Checked == true) permissions.Add("DELETE_BRANDS");

            // Sales
            if (checkEditViewSales?.Checked == true) permissions.Add("VIEW_SALES");
            if (checkEditAddSales?.Checked == true) permissions.Add("ADD_SALES");

            // Drafts
            if (checkEditViewDrafts?.Checked == true) permissions.Add("VIEW_DRAFTS");
            if (checkEditAddDrafts?.Checked == true) permissions.Add("ADD_DRAFTS");

            // Quotations
            if (checkEditViewQuotations?.Checked == true) permissions.Add("VIEW_QUOTATIONS");
            if (checkEditQuotations?.Checked == true) permissions.Add("ADD_QUOTATIONS");

            // Sell Returns
            if (checkEditSellReturns?.Checked == true) permissions.Add("VIEW_SELL_RETURNS");
            if (checkEditAddSellReturns?.Checked == true) permissions.Add("ADD_SELL_RETURNS");

            // Discounts
            if (checkEditViewDiscounts?.Checked == true) permissions.Add("VIEW_DISCOUNTS");
            if (checkEditAddDiscounts?.Checked == true) permissions.Add("ADD_DISCOUNTS");
            if (checkEditAssignDiscounts?.Checked == true) permissions.Add("ASSIGN_DISCOUNTS");

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
