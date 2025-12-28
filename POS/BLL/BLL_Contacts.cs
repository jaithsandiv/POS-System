using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.DAL;
using System.Data;

namespace POS.BLL
{
    internal class BLL_Contacts
    {
        private readonly DAL_Contacts _dalContacts = new DAL_Contacts();

        /// <summary>
        /// Gets all active CustomerGroups
        /// </summary>
        public DataTable GetCustomerGroups()
        {
            return _dalContacts.GetCustomerGroups();
        }

        /// <summary>
        /// Gets all active Suppliers
        /// </summary>
        public DataTable GetSuppliers()
        {
            return _dalContacts.GetSuppliers();
        }

        /// <summary>
        /// Searches suppliers by keyword
        /// </summary>
        public DataTable SearchSuppliers(string searchKeyword)
        {
            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                // If search is empty, return all suppliers
                return GetSuppliers();
            }

            return _dalContacts.SearchSuppliers(searchKeyword.Trim());
        }

        /// <summary>
        /// Gets a specific CustomerGroup by ID
        /// </summary>
        public DataTable GetCustomerGroupById(int groupId)
        {
            return _dalContacts.GetCustomerGroupById(groupId);
        }

        /// <summary>
        /// Gets a specific Supplier by ID
        /// </summary>
        public DataTable GetSupplierById(int supplierId)
        {
            return _dalContacts.GetSupplierById(supplierId);
        }

        /// <summary>
        /// Inserts a new CustomerGroup
        /// </summary>
        public int InsertCustomerGroup(string groupName, decimal discountPercent, int createdBy)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentException("Group name is required.", nameof(groupName));

            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Discount percent must be between 0 and 100.", nameof(discountPercent));

            return _dalContacts.InsertCustomerGroup(groupName, discountPercent, createdBy);
        }

        /// <summary>
        /// Inserts a new Supplier
        /// </summary>
        public int InsertSupplier(string supplierName, string companyName, string email, 
                                   string phone, string address, int createdBy)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(supplierName))
                throw new ArgumentException("Supplier name is required.", nameof(supplierName));

            return _dalContacts.InsertSupplier(supplierName, companyName, email, phone, address, createdBy);
        }

        /// <summary>
        /// Updates an existing CustomerGroup
        /// </summary>
        public bool UpdateCustomerGroup(int groupId, string groupName, decimal discountPercent, int updatedBy)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentException("Group name is required.", nameof(groupName));

            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Discount percent must be between 0 and 100.", nameof(discountPercent));

            return _dalContacts.UpdateCustomerGroup(groupId, groupName, discountPercent, updatedBy);
        }

        /// <summary>
        /// Updates an existing Supplier
        /// </summary>
        public bool UpdateSupplier(int supplierId, string supplierName, string companyName, 
                                    string email, string phone, string address, int updatedBy)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(supplierName))
                throw new ArgumentException("Supplier name is required.", nameof(supplierName));

            return _dalContacts.UpdateSupplier(supplierId, supplierName, companyName, email, phone, address, updatedBy);
        }

        /// <summary>
        /// Soft deletes a CustomerGroup
        /// </summary>
        public bool DeleteCustomerGroup(int groupId, int updatedBy)
        {
            return _dalContacts.DeleteCustomerGroup(groupId, updatedBy);
        }

        /// <summary>
        /// Soft deletes a Supplier
        /// </summary>
        public bool DeleteSupplier(int supplierId, int updatedBy)
        {
            return _dalContacts.DeleteSupplier(supplierId, updatedBy);
        }

        /// <summary>
        /// Gets all active Customers
        /// </summary>
        public DataTable GetCustomers()
        {
            return _dalContacts.GetCustomers();
        }

        /// <summary>
        /// Gets a specific Customer by ID
        /// </summary>
        public DataTable GetCustomerById(int customerId)
        {
            return _dalContacts.GetCustomerById(customerId);
        }

        /// <summary>
        /// Inserts a new Customer
        /// </summary>
        public int InsertCustomer(int? groupId, string fullName, string companyName, string email, 
                                   string phone, string address, string city, string state, 
                                   string country, string postalCode, int createdBy)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone number is required.", nameof(phone));

            return _dalContacts.InsertCustomer(groupId, fullName, companyName, email, 
                                                phone, address, city, state, country, postalCode, createdBy);
        }

        /// <summary>
        /// Updates an existing Customer
        /// </summary>
        public bool UpdateCustomer(int customerId, int? groupId, string fullName, string companyName, 
                                    string email, string phone, string address, string city, 
                                    string state, string country, string postalCode, int updatedBy)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone number is required.", nameof(phone));

            return _dalContacts.UpdateCustomer(customerId, groupId, fullName, companyName, 
                                                email, phone, address, city, state, country, postalCode, updatedBy);
        }

        /// <summary>
        /// Soft deletes a Customer
        /// </summary>
        public bool DeleteCustomer(int customerId, int updatedBy)
        {
            return _dalContacts.DeleteCustomer(customerId, updatedBy);
        }

        /// <summary>
        /// Gets customer group sales report - total sales grouped by customer group
        /// </summary>
        public DataTable GetCustomerGroupSalesReport()
        {
            return _dalContacts.GetCustomerGroupSalesReport();
        }

        /// <summary>
        /// Searches customer group sales report by keyword
        /// </summary>
        public DataTable SearchCustomerGroupSalesReport(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetCustomerGroupSalesReport();
            }

            return _dalContacts.SearchCustomerGroupSalesReport(keyword.Trim());
        }

        /// <summary>
        /// Gets supplier/customer report - all customers with their purchase and sales data
        /// </summary>
        public DataTable GetSupplierCustomerReport()
        {
            return _dalContacts.GetSupplierCustomerReport();
        }

        /// <summary>
        /// Searches supplier/customer report by keyword
        /// </summary>
        public DataTable SearchSupplierCustomerReport(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetSupplierCustomerReport();
            }

            return _dalContacts.SearchSupplierCustomerReport(keyword.Trim());
        }

        /// <summary>
        /// Gets customer transactions (sales, payments, returns) within a date range
        /// </summary>
        public DataTable GetCustomerTransactions(int customerId, DateTime startDate, DateTime endDate, int? storeId = null)
        {
            return _dalContacts.GetCustomerTransactions(customerId, startDate, endDate, storeId);
        }

        /// <summary>
        /// Gets customer account summary (opening balance, totals, current balance)
        /// </summary>
        public DataTable GetCustomerAccountSummary(int customerId, DateTime startDate, DateTime endDate, int? storeId = null)
        {
            return _dalContacts.GetCustomerAccountSummary(customerId, startDate, endDate, storeId);
        }

        /// <summary>
        /// Gets supplier transactions (placeholder)
        /// </summary>
        public DataTable GetSupplierTransactions(int supplierId, DateTime startDate, DateTime endDate, int? storeId = null)
        {
            return _dalContacts.GetSupplierTransactions(supplierId, startDate, endDate, storeId);
        }
    }
}
