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
    public partial class UC_Customer_Details : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private int _customerId;

        /// <summary>
        /// Constructor for viewing customer details
        /// </summary>
        public UC_Customer_Details(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            LoadCustomerDetails();
            LoadCustomersDropdown();
        }

        /// <summary>
        /// Default constructor (for designer)
        /// </summary>
        public UC_Customer_Details()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads customer details from the database
        /// </summary>
        private void LoadCustomerDetails()
        {
            try
            {
                DataTable customerData = _bllContacts.GetCustomerById(_customerId);

                if (customerData != null && customerData.Rows.Count > 0)
                {
                    DataRow row = customerData.Rows[0];

                    // Display customer information
                    lblCustomerName.Text = row["full_name"]?.ToString() ?? "N/A";
                    lblTableName.Text = customerData.TableName;
                    lblCustomerAddress.Text = FormatAddress(row);
                    lblCustomerPhoneNo.Text = row["phone"]?.ToString() ?? "N/A";
                    lblCustomerEmail.Text = row["email"]?.ToString() ?? "N/A";
                }
                else
                {
                    XtraMessageBox.Show(
                        "Customer not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    
                    // Navigate back to customer management
                    btnBack_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customer details: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Load all customers into the dropdown for quick switching
        /// </summary>
        private void LoadCustomersDropdown()
        {
            try
            {
                DataTable customers = _bllContacts.GetCustomers();
                
                if (customers == null || customers.Rows.Count == 0)
                {
                    return;
                }

                // Wire up selection changed event BEFORE setting data source
                comboboxCustomers.SelectedIndexChanged -= ComboboxCustomers_SelectedIndexChanged;
                comboboxCustomers.SelectedIndexChanged += ComboboxCustomers_SelectedIndexChanged;

                // Use data binding instead of manually adding items
                comboboxCustomers.Properties.Items.Clear();
                
                foreach (DataRow row in customers.Rows)
                {
                    string customerName = row["full_name"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(customerName))
                    {
                        comboboxCustomers.Properties.Items.Add(customerName);
                    }
                }

                // Find and select current customer
                DataTable currentCustomer = _bllContacts.GetCustomerById(_customerId);
                if (currentCustomer != null && currentCustomer.Rows.Count > 0)
                {
                    string currentName = currentCustomer.Rows[0]["full_name"]?.ToString();
                    
                    // Find the index of the current customer in the dropdown
                    int index = comboboxCustomers.Properties.Items.IndexOf(currentName);
                    if (index >= 0)
                    {
                        comboboxCustomers.SelectedIndex = index;
                    }
                    else
                    {
                        comboboxCustomers.Text = currentName;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customers dropdown: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle customer selection change in dropdown
        /// </summary>
        private void ComboboxCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Check if an item is selected
                if (comboboxCustomers.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboboxCustomers.Text))
                {
                    return;
                }

                string selectedName = comboboxCustomers.Text.Trim();
                
                // Find customer by name
                DataTable customers = _bllContacts.GetCustomers();
                if (customers == null || customers.Rows.Count == 0)
                {
                    return;
                }

                foreach (DataRow row in customers.Rows)
                {
                    if (row["full_name"]?.ToString()?.Trim() == selectedName)
                    {
                        int newCustomerId = Convert.ToInt32(row["customer_id"]);
                        
                        // Only reload if it's a different customer
                        if (newCustomerId != _customerId)
                        {
                            _customerId = newCustomerId;
                            LoadCustomerDetails();
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error switching customer: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Formats the customer address from multiple fields
        /// </summary>
        private string FormatAddress(DataRow row)
        {
            List<string> addressParts = new List<string>();

            string address = row["address"]?.ToString();
            string city = row["city"]?.ToString();
            string state = row["state"]?.ToString();
            string country = row["country"]?.ToString();
            string postalCode = row["postal_code"]?.ToString();

            if (!string.IsNullOrWhiteSpace(address))
                addressParts.Add(address);
            
            if (!string.IsNullOrWhiteSpace(city))
                addressParts.Add(city);
            
            if (!string.IsNullOrWhiteSpace(state))
                addressParts.Add(state);
            
            if (!string.IsNullOrWhiteSpace(postalCode))
                addressParts.Add(postalCode);
            
            if (!string.IsNullOrWhiteSpace(country))
                addressParts.Add(country);

            return addressParts.Count > 0 ? string.Join(", ", addressParts) : "N/A";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Customer_Management());
        }
    }
}
