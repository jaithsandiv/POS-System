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
    }
}
