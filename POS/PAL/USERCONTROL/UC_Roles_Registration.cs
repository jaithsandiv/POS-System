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
        private DevExpress.XtraEditors.CheckEdit checkEditAddDiscounts;
        private DevExpress.XtraEditors.CheckEdit checkEditAssignDiscounts;

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
                checkEditViewHome);
        }

        /// <summary>
        /// Toggle all checkboxes in a group
        /// </summary>
        private void ToggleGroupCheckboxes(CheckEdit selectAllCheckbox, params CheckEdit[] checkboxes)
        {
            bool isChecked = selectAllCheckbox.Checked;
            foreach (var checkbox in checkboxes)
            {
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
            checkEditViewExportButtons.Checked = permissions.Contains("VIEW_EXPORT_BUTTONS");

            // Users
            checkEditViewUser.Checked = permissions.Contains("VIEW_USERS");
            checkEditAddUser.Checked = permissions.Contains("ADD_USERS");
            checkEditEditUser.Checked = permissions.Contains("EDIT_USERS");
            checkEditDeleteUser.Checked = permissions.Contains("DELETE_USERS");

            // Roles
            checkEditViewRoles.Checked = permissions.Contains("VIEW_ROLES");
            checkEditAddRoles.Checked = permissions.Contains("ADD_ROLES");
            checkEditEditRoles.Checked = permissions.Contains("EDIT_ROLES");
            checkEditDeleteRoles.Checked = permissions.Contains("DELETE_ROLES");

            // Suppliers
            checkEditViewSuppliers.Checked = permissions.Contains("VIEW_SUPPLIERS");
            checkEditAddSuppliers.Checked = permissions.Contains("ADD_SUPPLIERS");
            checkEditEditSuppliers.Checked = permissions.Contains("EDIT_SUPPLIERS");
            checkEditDeleteSuppliers.Checked = permissions.Contains("DELETE_SUPPLIERS");

            // Customers
            checkEditViewCustomers.Checked = permissions.Contains("VIEW_CUSTOMERS");
            checkEditAddCustomers.Checked = permissions.Contains("ADD_CUSTOMERS");
            checkEditEditCustomers.Checked = permissions.Contains("EDIT_CUSTOMERS");
            checkEditDeleteCustomers.Checked = permissions.Contains("DELETE_CUSTOMERS");
            checkEditViewCustomersDetails.Checked = permissions.Contains("VIEW_CUSTOMER_DETAILS");

            // Products
            checkEditViewProducts.Checked = permissions.Contains("VIEW_PRODUCTS");
            checkEditAddProducts.Checked = permissions.Contains("ADD_PRODUCTS");
            checkEdit16.Checked = permissions.Contains("EDIT_PRODUCTS");
            checkEdit17.Checked = permissions.Contains("DELETE_PRODUCTS");

            // Customer Groups
            checkEditViewCustomerGroups.Checked = permissions.Contains("VIEW_CUSTOMER_GROUPS");
            checkEditAddCustomerGroups.Checked = permissions.Contains("ADD_CUSTOMER_GROUPS");
            checkEditEditCustomerGroups.Checked = permissions.Contains("EDIT_CUSTOMER_GROUPS");
            checkEditDeleteCustomerGroups.Checked = permissions.Contains("DELETE_CUSTOMER_GROUPS");

            // Categories
            checkEditViewCategories.Checked = permissions.Contains("VIEW_CATEGORIES");
            checkEditAddCategories.Checked = permissions.Contains("ADD_CATEGORIES");
            checkEditEditCategories.Checked = permissions.Contains("EDIT_CATEGORIES");
            checkEditDeleteCategories.Checked = permissions.Contains("DELETE_CATEGORIES");

            // Print Labels
            checkEditAccesstoPrintLabelsForm.Checked = permissions.Contains("VIEW_PRINT_LABELS");

            // Units
            checkEditViewUnits.Checked = permissions.Contains("VIEW_UNITS");
            checkEditAddUnits.Checked = permissions.Contains("ADD_UNITS");
            checkEditEditUnits.Checked = permissions.Contains("EDIT_UNITS");
            checkEditDeleteUnits.Checked = permissions.Contains("DELETE_UNITS");

            // Brands
            checkEditViewBrands.Checked = permissions.Contains("VIEW_BRANDS");
            checkEditAddBrands.Checked = permissions.Contains("ADD_BRANDS");
            checkEditEditBrands.Checked = permissions.Contains("EDIT_BRANDS");
            checkEditDeleteBrands.Checked = permissions.Contains("DELETE_BRANDS");

            // Sales
            checkEditViewSales.Checked = permissions.Contains("VIEW_SALES");
            checkEditAddSales.Checked = permissions.Contains("ADD_SALES");

            // Drafts
            checkEditViewDrafts.Checked = permissions.Contains("VIEW_DRAFTS");
            checkEditAddDrafts.Checked = permissions.Contains("ADD_DRAFTS");

            // Quotations
            checkEditViewQuotations.Checked = permissions.Contains("VIEW_QUOTATIONS");
            checkEditQuotations.Checked = permissions.Contains("ADD_QUOTATIONS");

            // Sell Returns
            checkEditSellReturns.Checked = permissions.Contains("VIEW_SELL_RETURNS");
            checkEditAddSellReturns.Checked = permissions.Contains("ADD_SELL_RETURNS");

            // Discounts
            checkEditViewDiscounts.Checked = permissions.Contains("VIEW_DISCOUNTS");
            checkEditAddDiscounts.Checked = permissions.Contains("ADD_DISCOUNTS");
            checkEditAssignDiscounts.Checked = permissions.Contains("ASSIGN_DISCOUNTS");

            // POS
            checkEditAccesstoSalesTerminal.Checked = permissions.Contains("ACCESS_SALES_TERMINAL");

            // Reports
            checkEditViewSupplierCustomerReport.Checked = permissions.Contains("VIEW_SUPPLIER_CUSTOMER_REPORT");
            checkEditViewItemsReport.Checked = permissions.Contains("VIEW_ITEMS_REPORT");
            checkEditViewTrendingProducts.Checked = permissions.Contains("VIEW_TRENDING_PRODUCTS");
            checkEditViewStockReport.Checked = permissions.Contains("VIEW_STOCK_REPORT");
            checkEditViewCustomerGroupReport.Checked = permissions.Contains("VIEW_CUSTOMER_GROUP_REPORT");
            checkEditViewProductSellReport.Checked = permissions.Contains("VIEW_PRODUCT_SELL_REPORT");
            checkEditViewActivityLog.Checked = permissions.Contains("VIEW_ACTIVITY_LOG");
            checkEditViewTableReport.Checked = permissions.Contains("VIEW_TABLE_REPORT");
            checkEditViewSalesRepresentativeReport.Checked = permissions.Contains("VIEW_SALES_REPRESENTATIVE_REPORT");
            checkEditViewSellPaymentReport.Checked = permissions.Contains("VIEW_SELL_PAYMENT_REPORT");

            // Settings
            checkEditViewBusinessSettings.Checked = permissions.Contains("VIEW_BUSINESS_SETTINGS");
            checkEditViewTables.Checked = permissions.Contains("VIEW_TABLES");
            checkEditViewBusinessLocations.Checked = permissions.Contains("VIEW_BUSINESS_LOCATIONS");

            // Home
            checkEditViewHome.Checked = permissions.Contains("VIEW_HOME");
        }

        /// <summary>
        /// Collect all selected permissions from checkboxes
        /// </summary>
        private List<string> CollectSelectedPermissions()
        {
            List<string> permissions = new List<string>();

            // Other
            if (checkEditViewExportButtons.Checked) permissions.Add("VIEW_EXPORT_BUTTONS");

            // Users
            if (checkEditViewUser.Checked) permissions.Add("VIEW_USERS");
            if (checkEditAddUser.Checked) permissions.Add("ADD_USERS");
            if (checkEditEditUser.Checked) permissions.Add("EDIT_USERS");
            if (checkEditDeleteUser.Checked) permissions.Add("DELETE_USERS");

            // Roles
            if (checkEditViewRoles.Checked) permissions.Add("VIEW_ROLES");
            if (checkEditAddRoles.Checked) permissions.Add("ADD_ROLES");
            if (checkEditEditRoles.Checked) permissions.Add("EDIT_ROLES");
            if (checkEditDeleteRoles.Checked) permissions.Add("DELETE_ROLES");

            // Suppliers
            if (checkEditViewSuppliers.Checked) permissions.Add("VIEW_SUPPLIERS");
            if (checkEditAddSuppliers.Checked) permissions.Add("ADD_SUPPLIERS");
            if (checkEditEditSuppliers.Checked) permissions.Add("EDIT_SUPPLIERS");
            if (checkEditDeleteSuppliers.Checked) permissions.Add("DELETE_SUPPLIERS");

            // Customers
            if (checkEditViewCustomers.Checked) permissions.Add("VIEW_CUSTOMERS");
            if (checkEditAddCustomers.Checked) permissions.Add("ADD_CUSTOMERS");
            if (checkEditEditCustomers.Checked) permissions.Add("EDIT_CUSTOMERS");
            if (checkEditDeleteCustomers.Checked) permissions.Add("DELETE_CUSTOMERS");
            if (checkEditViewCustomersDetails.Checked) permissions.Add("VIEW_CUSTOMER_DETAILS");

            // Products
            if (checkEditViewProducts.Checked) permissions.Add("VIEW_PRODUCTS");
            if (checkEditAddProducts.Checked) permissions.Add("ADD_PRODUCTS");
            if (checkEdit16.Checked) permissions.Add("EDIT_PRODUCTS");
            if (checkEdit17.Checked) permissions.Add("DELETE_PRODUCTS");

            // Customer Groups
            if (checkEditViewCustomerGroups.Checked) permissions.Add("VIEW_CUSTOMER_GROUPS");
            if (checkEditAddCustomerGroups.Checked) permissions.Add("ADD_CUSTOMER_GROUPS");
            if (checkEditEditCustomerGroups.Checked) permissions.Add("EDIT_CUSTOMER_GROUPS");
            if (checkEditDeleteCustomerGroups.Checked) permissions.Add("DELETE_CUSTOMER_GROUPS");

            // Categories
            if (checkEditViewCategories.Checked) permissions.Add("VIEW_CATEGORIES");
            if (checkEditAddCategories.Checked) permissions.Add("ADD_CATEGORIES");
            if (checkEditEditCategories.Checked) permissions.Add("EDIT_CATEGORIES");
            if (checkEditDeleteCategories.Checked) permissions.Add("DELETE_CATEGORIES");

            // Print Labels
            if (checkEditAccesstoPrintLabelsForm.Checked) permissions.Add("VIEW_PRINT_LABELS");

            // Units
            if (checkEditViewUnits.Checked) permissions.Add("VIEW_UNITS");
            if (checkEditAddUnits.Checked) permissions.Add("ADD_UNITS");
            if (checkEditEditUnits.Checked) permissions.Add("EDIT_UNITS");
            if (checkEditDeleteUnits.Checked) permissions.Add("DELETE_UNITS");

            // Brands
            if (checkEditViewBrands.Checked) permissions.Add("VIEW_BRANDS");
            if (checkEditAddBrands.Checked) permissions.Add("ADD_BRANDS");
            if (checkEditEditBrands.Checked) permissions.Add("EDIT_BRANDS");
            if (checkEditDeleteBrands.Checked) permissions.Add("DELETE_BRANDS");

            // Sales
            if (checkEditViewSales.Checked) permissions.Add("VIEW_SALES");
            if (checkEditAddSales.Checked) permissions.Add("ADD_SALES");

            // Drafts
            if (checkEditViewDrafts.Checked) permissions.Add("VIEW_DRAFTS");
            if (checkEditAddDrafts.Checked) permissions.Add("ADD_DRAFTS");

            // Quotations
            if (checkEditViewQuotations.Checked) permissions.Add("VIEW_QUOTATIONS");
            if (checkEditQuotations.Checked) permissions.Add("ADD_QUOTATIONS");

            // Sell Returns
            if (checkEditSellReturns.Checked) permissions.Add("VIEW_SELL_RETURNS");
            if (checkEditAddSellReturns.Checked) permissions.Add("ADD_SELL_RETURNS");

            // Discounts
            if (checkEditViewDiscounts.Checked) permissions.Add("VIEW_DISCOUNTS");
            if (checkEditAddDiscounts.Checked) permissions.Add("ADD_DISCOUNTS");
            if (checkEditAssignDiscounts.Checked) permissions.Add("ASSIGN_DISCOUNTS");

            // POS
            if (checkEditAccesstoSalesTerminal.Checked) permissions.Add("ACCESS_SALES_TERMINAL");

            // Reports
            if (checkEditViewSupplierCustomerReport.Checked) permissions.Add("VIEW_SUPPLIER_CUSTOMER_REPORT");
            if (checkEditViewItemsReport.Checked) permissions.Add("VIEW_ITEMS_REPORT");
            if (checkEditViewTrendingProducts.Checked) permissions.Add("VIEW_TRENDING_PRODUCTS");
            if (checkEditViewStockReport.Checked) permissions.Add("VIEW_STOCK_REPORT");
            if (checkEditViewCustomerGroupReport.Checked) permissions.Add("VIEW_CUSTOMER_GROUP_REPORT");
            if (checkEditViewProductSellReport.Checked) permissions.Add("VIEW_PRODUCT_SELL_REPORT");
            if (checkEditViewActivityLog.Checked) permissions.Add("VIEW_ACTIVITY_LOG");
            if (checkEditViewTableReport.Checked) permissions.Add("VIEW_TABLE_REPORT");
            if (checkEditViewSalesRepresentativeReport.Checked) permissions.Add("VIEW_SALES_REPRESENTATIVE_REPORT");
            if (checkEditViewSellPaymentReport.Checked) permissions.Add("VIEW_SELL_PAYMENT_REPORT");

            // Settings
            if (checkEditViewBusinessSettings.Checked) permissions.Add("VIEW_BUSINESS_SETTINGS");
            if (checkEditViewTables.Checked) permissions.Add("VIEW_TABLES");
            if (checkEditViewBusinessLocations.Checked) permissions.Add("VIEW_BUSINESS_LOCATIONS");

            // Home
            if (checkEditViewHome.Checked) permissions.Add("VIEW_HOME");

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

                // Insert role with permissions
                int roleId = BLL_Role.InsertRole(
                    txtRoleName.Text.Trim(),
                    null, // description
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

                // Update role with permissions
                bool success = BLL_Role.UpdateRole(
                    _editRoleId.Value,
                    txtRoleName.Text.Trim(),
                    null, // description
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
