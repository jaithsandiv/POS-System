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
    public partial class UC_Supplier_Details : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private int _supplierId;

        /// <summary>
        /// Constructor that accepts supplier ID
        /// </summary>
        public UC_Supplier_Details(int supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            LoadSupplierDetails();
            LoadSuppliersDropdown();
        }

        /// <summary>
        /// Default constructor (for designer)
        /// </summary>
        public UC_Supplier_Details()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the supplier details for the given supplier ID
        /// </summary>
        private void LoadSupplierDetails()
        {
            try
            {
                DataTable supplierData = _bllContacts.GetSupplierById(_supplierId);
                
                if (supplierData != null && supplierData.Rows.Count > 0)
                {
                    DataRow row = supplierData.Rows[0];

                    // Update labels with supplier information
                    lblCustomerName.Text = row["supplier_name"]?.ToString() ?? "N/A";
                    lblTableName.Text = supplierData.TableName;
                    lblSupplierAddress.Text = row["address"]?.ToString() ?? "N/A";
                    lblSupplierPhoneNo.Text = row["phone"]?.ToString() ?? "N/A";
                    lblCustomerEmail.Text = row["email"]?.ToString() ?? "N/A";
                }
                else
                {
                    XtraMessageBox.Show(
                        "Supplier not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    btnBack_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading supplier details: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Load all suppliers into the dropdown for quick switching
        /// </summary>
        private void LoadSuppliersDropdown()
        {
            try
            {
                DataTable suppliers = _bllContacts.GetSuppliers();
                
                if (suppliers == null || suppliers.Rows.Count == 0)
                {
                    return;
                }

                // Wire up selection changed event BEFORE setting data source
                comboboxSuppliers.SelectedIndexChanged -= ComboboxSuppliers_SelectedIndexChanged;
                comboboxSuppliers.SelectedIndexChanged += ComboboxSuppliers_SelectedIndexChanged;

                // Use data binding instead of manually adding items
                comboboxSuppliers.Properties.Items.Clear();
                
                foreach (DataRow row in suppliers.Rows)
                {
                    string supplierName = row["supplier_name"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(supplierName))
                    {
                        comboboxSuppliers.Properties.Items.Add(supplierName);
                    }
                }

                // Find and select current supplier
                DataTable currentSupplier = _bllContacts.GetSupplierById(_supplierId);
                if (currentSupplier != null && currentSupplier.Rows.Count > 0)
                {
                    string currentName = currentSupplier.Rows[0]["supplier_name"]?.ToString();
                    
                    // Find the index of the current supplier in the dropdown
                    int index = comboboxSuppliers.Properties.Items.IndexOf(currentName);
                    if (index >= 0)
                    {
                        comboboxSuppliers.SelectedIndex = index;
                    }
                    else
                    {
                        comboboxSuppliers.Text = currentName;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading suppliers dropdown: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle supplier selection change in dropdown
        /// </summary>
        private void ComboboxSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Check if an item is selected
                if (comboboxSuppliers.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboboxSuppliers.Text))
                {
                    return;
                }

                string selectedName = comboboxSuppliers.Text.Trim();
                
                // Find supplier by name
                DataTable suppliers = _bllContacts.GetSuppliers();
                if (suppliers == null || suppliers.Rows.Count == 0)
                {
                    return;
                }

                foreach (DataRow row in suppliers.Rows)
                {
                    if (row["supplier_name"]?.ToString()?.Trim() == selectedName)
                    {
                        int newSupplierId = Convert.ToInt32(row["supplier_id"]);
                        
                        // Only reload if it's a different supplier
                        if (newSupplierId != _supplierId)
                        {
                            _supplierId = newSupplierId;
                            LoadSupplierDetails();
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error switching supplier: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Supplier_Management());
        }
    }
}
