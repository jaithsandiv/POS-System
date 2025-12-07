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
                    lblTableName.Text = row["company_name"]?.ToString() ?? "";
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
                    Main.Instance.LoadUserControl(new UC_Customer_Management());
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
