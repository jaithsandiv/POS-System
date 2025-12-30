using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace POS.BLL
{
    /// <summary>
    /// Centralized permission management system for role-based access control
    /// </summary>
    public static class PermissionManager
    {
        private static HashSet<string> _currentUserPermissions = new HashSet<string>();
        private static bool _isSuperAdmin = false;

        /// <summary>
        /// Initialize permissions for the current logged-in user
        /// </summary>
        public static void LoadUserPermissions()
        {
            _currentUserPermissions.Clear();
            _isSuperAdmin = false;

            try
            {
                // Check if user is super admin
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    var userRow = Main.DataSetApp.User[0];
                    if (!userRow.Isis_super_adminNull())
                    {
                        string isSuperAdminValue = userRow.is_super_admin?.ToString();
                        _isSuperAdmin = isSuperAdminValue == "True" || isSuperAdminValue == "1";
                    }
                }

                // Load permissions from RolePermission table
                if (Main.DataSetApp?.RolePermission != null)
                {
                    foreach (var permRow in Main.DataSetApp.RolePermission)
                    {
                        if (!permRow.Ispermission_codeNull())
                        {
                            _currentUserPermissions.Add(permRow.permission_code);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // If there's an error loading permissions, start with empty set
                _currentUserPermissions.Clear();
            }
        }

        /// <summary>
        /// Clear all loaded permissions (call on logout)
        /// </summary>
        public static void ClearPermissions()
        {
            _currentUserPermissions.Clear();
            _isSuperAdmin = false;
        }

        /// <summary>
        /// Check if the current user has a specific permission
        /// Super admins always have all permissions
        /// </summary>
        public static bool HasPermission(string permissionCode)
        {
            if (_isSuperAdmin)
                return true;

            if (string.IsNullOrWhiteSpace(permissionCode))
                return false;

            return _currentUserPermissions.Contains(permissionCode);
        }

        /// <summary>
        /// Check if the current user has any of the specified permissions
        /// </summary>
        public static bool HasAnyPermission(params string[] permissionCodes)
        {
            if (_isSuperAdmin)
                return true;

            if (permissionCodes == null || permissionCodes.Length == 0)
                return false;

            return permissionCodes.Any(p => _currentUserPermissions.Contains(p));
        }

        /// <summary>
        /// Check if the current user has all of the specified permissions
        /// </summary>
        public static bool HasAllPermissions(params string[] permissionCodes)
        {
            if (_isSuperAdmin)
                return true;

            if (permissionCodes == null || permissionCodes.Length == 0)
                return true;

            return permissionCodes.All(p => _currentUserPermissions.Contains(p));
        }

        /// <summary>
        /// Check if the current user is a super admin
        /// </summary>
        public static bool IsSuperAdmin()
        {
            return _isSuperAdmin;
        }

        /// <summary>
        /// Get all permissions for the current user
        /// </summary>
        public static IEnumerable<string> GetCurrentUserPermissions()
        {
            return _currentUserPermissions.AsEnumerable();
        }

        // Permission Constants
        public static class Permissions
        {
            // Dashboard
            public const string VIEW_DASHBOARD = "VIEW_DASHBOARD";

            // Users
            public const string VIEW_USERS = "VIEW_USERS";
            public const string ADD_USERS = "ADD_USERS";
            public const string EDIT_USERS = "EDIT_USERS";
            public const string DELETE_USERS = "DELETE_USERS";

            // Roles
            public const string VIEW_ROLES = "VIEW_ROLES";
            public const string ADD_ROLES = "ADD_ROLES";
            public const string EDIT_ROLES = "EDIT_ROLES";
            public const string DELETE_ROLES = "DELETE_ROLES";

            // Suppliers
            public const string VIEW_SUPPLIERS = "VIEW_SUPPLIERS";
            public const string ADD_SUPPLIERS = "ADD_SUPPLIERS";
            public const string EDIT_SUPPLIERS = "EDIT_SUPPLIERS";
            public const string DELETE_SUPPLIERS = "DELETE_SUPPLIERS";

            // Customers
            public const string VIEW_CUSTOMERS = "VIEW_CUSTOMERS";
            public const string ADD_CUSTOMERS = "ADD_CUSTOMERS";
            public const string EDIT_CUSTOMERS = "EDIT_CUSTOMERS";
            public const string DELETE_CUSTOMERS = "DELETE_CUSTOMERS";
            public const string VIEW_CUSTOMER_DETAILS = "VIEW_CUSTOMER_DETAILS";

            // Products
            public const string VIEW_PRODUCTS = "VIEW_PRODUCTS";
            public const string ADD_PRODUCTS = "ADD_PRODUCTS";
            public const string EDIT_PRODUCTS = "EDIT_PRODUCTS";
            public const string DELETE_PRODUCTS = "DELETE_PRODUCTS";

            // Customer Groups
            public const string VIEW_CUSTOMER_GROUPS = "VIEW_CUSTOMER_GROUPS";
            public const string ADD_CUSTOMER_GROUPS = "ADD_CUSTOMER_GROUPS";
            public const string EDIT_CUSTOMER_GROUPS = "EDIT_CUSTOMER_GROUPS";
            public const string DELETE_CUSTOMER_GROUPS = "DELETE_CUSTOMER_GROUPS";

            // Categories
            public const string VIEW_CATEGORIES = "VIEW_CATEGORIES";
            public const string ADD_CATEGORIES = "ADD_CATEGORIES";
            public const string EDIT_CATEGORIES = "EDIT_CATEGORIES";
            public const string DELETE_CATEGORIES = "DELETE_CATEGORIES";

            // Print Labels
            public const string VIEW_PRINT_LABELS = "VIEW_PRINT_LABELS";

            // Units
            public const string VIEW_UNITS = "VIEW_UNITS";
            public const string ADD_UNITS = "ADD_UNITS";
            public const string EDIT_UNITS = "EDIT_UNITS";
            public const string DELETE_UNITS = "DELETE_UNITS";

            // Brands
            public const string VIEW_BRANDS = "VIEW_BRANDS";
            public const string ADD_BRANDS = "ADD_BRANDS";
            public const string EDIT_BRANDS = "EDIT_BRANDS";
            public const string DELETE_BRANDS = "DELETE_BRANDS";

            // Sales
            public const string VIEW_SALES = "VIEW_SALES";
            public const string ADD_SALES = "ADD_SALES";

            // Drafts
            public const string VIEW_DRAFTS = "VIEW_DRAFTS";
            public const string ADD_DRAFTS = "ADD_DRAFTS";

            // Quotations
            public const string VIEW_QUOTATIONS = "VIEW_QUOTATIONS";
            public const string ADD_QUOTATIONS = "ADD_QUOTATIONS";

            // Sell Returns
            public const string VIEW_SELL_RETURNS = "VIEW_SELL_RETURNS";
            public const string ADD_SELL_RETURNS = "ADD_SELL_RETURNS";

            // Discounts
            public const string VIEW_DISCOUNTS = "VIEW_DISCOUNTS";
            public const string ADD_DISCOUNTS = "ADD_DISCOUNTS";
            public const string ASSIGN_DISCOUNTS = "ASSIGN_DISCOUNTS";

            // POS
            public const string ACCESS_SALES_TERMINAL = "ACCESS_SALES_TERMINAL";

            // Reports
            public const string VIEW_SUPPLIER_CUSTOMER_REPORT = "VIEW_SUPPLIER_CUSTOMER_REPORT";
            public const string VIEW_ITEMS_REPORT = "VIEW_ITEMS_REPORT";
            public const string VIEW_TRENDING_PRODUCTS = "VIEW_TRENDING_PRODUCTS";
            public const string VIEW_STOCK_REPORT = "VIEW_STOCK_REPORT";
            public const string VIEW_CUSTOMER_GROUP_REPORT = "VIEW_CUSTOMER_GROUP_REPORT";
            public const string VIEW_PRODUCT_SELL_REPORT = "VIEW_PRODUCT_SELL_REPORT";
            public const string VIEW_ACTIVITY_LOG = "VIEW_ACTIVITY_LOG";
            public const string VIEW_TABLE_REPORT = "VIEW_TABLE_REPORT";
            public const string VIEW_SALES_REPRESENTATIVE_REPORT = "VIEW_SALES_REPRESENTATIVE_REPORT";
            public const string VIEW_SELL_PAYMENT_REPORT = "VIEW_SELL_PAYMENT_REPORT";

            // Settings
            public const string VIEW_BUSINESS_SETTINGS = "VIEW_BUSINESS_SETTINGS";
            public const string VIEW_TABLES = "VIEW_TABLES";
            public const string VIEW_BUSINESS_LOCATIONS = "VIEW_BUSINESS_LOCATIONS";

            // Other
            public const string VIEW_EXPORT_BUTTONS = "VIEW_EXPORT_BUTTONS";
        }
    }
}
