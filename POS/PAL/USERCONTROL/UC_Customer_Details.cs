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
        private readonly BLL_Store _bllStore = new BLL_Store();
        private int _customerId;
        private DateTime _selectedStartDate;
        private DateTime _selectedEndDate;
        private int? _selectedStoreId = null;
        private DataTable _storesTable;

        /// <summary>
        /// Constructor for viewing customer details
        /// </summary>
        public UC_Customer_Details(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;

            // Initialize default date range (last 30 days)
            _selectedEndDate = DateTime.Today;
            _selectedStartDate = _selectedEndDate.AddDays(-30);

            InitializeDateFilters();
            LoadStoresDropdown();
            LoadCustomerDetails();
            LoadCustomersDropdown();
            UpdateDateLabels();
        }

        /// <summary>
        /// Default constructor (for designer)
        /// </summary>
        public UC_Customer_Details()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the date filter combo boxes with predefined date ranges
        /// </summary>
        private void InitializeDateFilters()
        {
            // Clear existing items
            comboBoxStartDateFilter.Properties.Items.Clear();
            comboBoxEndDateFilter.Properties.Items.Clear();

            // Define common date ranges
            var dateRanges = new Dictionary<string, DateTime>
            {
                { "Today", DateTime.Today },
                { "Yesterday", DateTime.Today.AddDays(-1) },
                { "Last 7 Days", DateTime.Today.AddDays(-7) },
                { "Last 30 Days", DateTime.Today.AddDays(-30) },
                { "Last 60 Days", DateTime.Today.AddDays(-60) },
                { "Last 90 Days", DateTime.Today.AddDays(-90) },
                { "Last 6 Months", DateTime.Today.AddMonths(-6) },
                { "Last Year", DateTime.Today.AddYears(-1) },
                { "This Month", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1) },
                { "Last Month", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1) },
                { "This Year", new DateTime(DateTime.Today.Year, 1, 1) }
            };

            // Add items to start date filter
            foreach (var range in dateRanges)
            {
                comboBoxStartDateFilter.Properties.Items.Add(range.Key);
            }

            // Add items to end date filter (typically just "Today" or custom dates)
            var endDateRanges = new Dictionary<string, DateTime>
            {
                { "Today", DateTime.Today },
                { "Yesterday", DateTime.Today.AddDays(-1) },
                { "Last 7 Days", DateTime.Today.AddDays(-7) },
                { "Last 30 Days", DateTime.Today.AddDays(-30) },
                { "End of This Month", new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)) },
                { "End of Last Month", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1) }
            };

            foreach (var range in endDateRanges)
            {
                comboBoxEndDateFilter.Properties.Items.Add(range.Key);
            }

            // Set default values
            comboBoxStartDateFilter.SelectedItem = "Last 30 Days";
            comboBoxEndDateFilter.SelectedItem = "Today";

            // Wire up event handlers
            comboBoxStartDateFilter.SelectedIndexChanged += ComboBoxStartDateFilter_SelectedIndexChanged;
            comboBoxEndDateFilter.SelectedIndexChanged += ComboBoxEndDateFilter_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles start date filter selection change
        /// </summary>
        private void ComboBoxStartDateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStartDateFilter.SelectedItem == null)
                return;

            string selectedRange = comboBoxStartDateFilter.SelectedItem.ToString();
            _selectedStartDate = GetDateFromRange(selectedRange);

            // Ensure start date is not after end date
            if (_selectedStartDate > _selectedEndDate)
            {
                XtraMessageBox.Show(
                    "Start date cannot be after end date. Please adjust your selection.",
                    "Invalid Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            UpdateDateLabels();
            LoadFilteredData();
        }

        /// <summary>
        /// Handles end date filter selection change
        /// </summary>
        private void ComboBoxEndDateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEndDateFilter.SelectedItem == null)
                return;

            string selectedRange = comboBoxEndDateFilter.SelectedItem.ToString();
            _selectedEndDate = GetDateFromRange(selectedRange);

            // Ensure end date is not before start date
            if (_selectedEndDate < _selectedStartDate)
            {
                XtraMessageBox.Show(
                    "End date cannot be before start date. Please adjust your selection.",
                    "Invalid Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            UpdateDateLabels();
            LoadFilteredData();
        }

        /// <summary>
        /// Converts a date range string to an actual DateTime
        /// </summary>
        private DateTime GetDateFromRange(string range)
        {
            switch (range)
            {
                case "Today":
                    return DateTime.Today;
                case "Yesterday":
                    return DateTime.Today.AddDays(-1);
                case "Last 7 Days":
                    return DateTime.Today.AddDays(-7);
                case "Last 30 Days":
                    return DateTime.Today.AddDays(-30);
                case "Last 60 Days":
                    return DateTime.Today.AddDays(-60);
                case "Last 90 Days":
                    return DateTime.Today.AddDays(-90);
                case "Last 6 Months":
                    return DateTime.Today.AddMonths(-6);
                case "Last Year":
                    return DateTime.Today.AddYears(-1);
                case "This Month":
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                case "Last Month":
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                case "This Year":
                    return new DateTime(DateTime.Today.Year, 1, 1);
                case "End of This Month":
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
                case "End of Last Month":
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                default:
                    return DateTime.Today;
            }
        }

        /// <summary>
        /// Updates all date labels with the currently selected dates
        /// </summary>
        private void UpdateDateLabels()
        {
            string startDateStr = _selectedStartDate.ToString("dd/MM/yyyy");
            string endDateStr = _selectedEndDate.ToString("dd/MM/yyyy");

            // Update all date labels
            lblStartDate.Text = startDateStr;
            lblEndDate.Text = endDateStr;
            lblStartDate1.Text = startDateStr;
            lblEndDate1.Text = endDateStr;
        }

        /// <summary>
        /// Loads filtered data based on the selected date range
        /// </summary>
        private void LoadFilteredData()
        {
            try
            {
                // TODO: In the future, implement fetching customer transactions/invoices
                // filtered by the date range (_selectedStartDate to _selectedEndDate)
                // and populate the gridControl1 with the filtered data

                // For now, just update the Account Summary section with placeholder data
                // In a real implementation, you would:
                // 1. Fetch invoices/transactions from database filtered by date range
                // 2. Calculate totals (opening balance, total invoice, total paid, etc.)
                // 3. Update the labels and grid accordingly

                // Example (commented out - needs actual BLL method):
                // DataTable transactions = _bllContacts.GetCustomerTransactions(_customerId, _selectedStartDate, _selectedEndDate);
                // gridControl1.DataSource = transactions;
                // CalculateAccountSummary(transactions);

                // Update the labels to show the filter is working
                labelControl30.Text = $"Showing all invoices and payments between";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading filtered data: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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
                    lblCustomerName1.Text = row["full_name"]?.ToString() ?? "N/A";
                    lblTableName.Text = customerData.TableName;
                    lblCustomerAddress.Text = FormatAddress(row);
                    lblCustomerPhoneNo.Text = row["phone"]?.ToString() ?? "N/A";
                    lblCustomerPhoneNumber.Text = row["phone"]?.ToString() ?? "N/A";
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
                            LoadFilteredData();
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

        /// <summary>
        /// Loads all stores into the dropdown for filtering
        /// </summary>
        private void LoadStoresDropdown()
        {
            try
            {
                // Get all stores
                _storesTable = _bllStore.GetStores();

                // Clear existing items
                comboBoxStoreLocation.Properties.Items.Clear();

                // Add "All Stores" as the first option
                comboBoxStoreLocation.Properties.Items.Add("All Stores");

                // Add each store to the dropdown
                if (_storesTable != null && _storesTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _storesTable.Rows)
                    {
                        string storeName = row["store_name"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(storeName))
                        {
                            comboBoxStoreLocation.Properties.Items.Add(storeName);
                        }
                    }
                }

                // Set default selection to "All Stores"
                comboBoxStoreLocation.SelectedIndex = 0;

                // Wire up event handler
                comboBoxStoreLocation.SelectedIndexChanged -= ComboBoxStoreLocation_SelectedIndexChanged;
                comboBoxStoreLocation.SelectedIndexChanged += ComboBoxStoreLocation_SelectedIndexChanged;

                // Update store labels for "All Stores"
                UpdateStoreLabels();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading stores dropdown: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handles store location selection change
        /// </summary>
        private void ComboBoxStoreLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxStoreLocation.SelectedIndex < 0 || string.IsNullOrWhiteSpace(comboBoxStoreLocation.Text))
                {
                    return;
                }

                string selectedStoreName = comboBoxStoreLocation.Text.Trim();

                // Check if "All Stores" is selected
                if (selectedStoreName == "All Stores")
                {
                    _selectedStoreId = null;
                }
                else
                {
                    // Find the store by name and get its ID
                    if (_storesTable != null && _storesTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in _storesTable.Rows)
                        {
                            if (row["store_name"]?.ToString()?.Trim() == selectedStoreName)
                            {
                                _selectedStoreId = Convert.ToInt32(row["store_id"]);
                                break;
                            }
                        }
                    }
                }

                // Update the store labels
                UpdateStoreLabels();

                // Reload filtered data
                LoadFilteredData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error changing store location: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Updates the store address and country labels based on selected store
        /// </summary>
        private void UpdateStoreLabels()
        {
            try
            {
                // Constrain labels to match combobox location and width (comboBoxStoreLocation)
                // comboBoxStoreLocation: Location (1532, 27), Size (287, 44)
                int comboBoxLeft = 1532;
                int comboBoxWidth = 287;

                // Set label properties to constrain within combobox bounds
                lblBusinesName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblBusinesName.Location = new System.Drawing.Point(comboBoxLeft, lblBusinesName.Location.Y);
                lblBusinesName.Size = new System.Drawing.Size(comboBoxWidth, lblBusinesName.Size.Height);
                lblBusinesName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblBusinesName.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;

                lblStoreAddressC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblStoreAddressC.Location = new System.Drawing.Point(comboBoxLeft, lblStoreAddressC.Location.Y);
                lblStoreAddressC.Size = new System.Drawing.Size(comboBoxWidth, lblStoreAddressC.Size.Height);
                lblStoreAddressC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblStoreAddressC.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;

                lblStoreCountryC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblStoreCountryC.Location = new System.Drawing.Point(comboBoxLeft, lblStoreCountryC.Location.Y);
                lblStoreCountryC.Size = new System.Drawing.Size(comboBoxWidth, lblStoreCountryC.Size.Height);
                lblStoreCountryC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblStoreCountryC.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;

                if (_selectedStoreId.HasValue)
                {
                    // Get specific store details
                    DataTable storeData = _bllStore.GetStoreById(_selectedStoreId.Value);

                    if (storeData != null && storeData.Rows.Count > 0)
                    {
                        DataRow storeRow = storeData.Rows[0];

                        // Update business name
                        lblBusinesName.Text = storeRow["store_name"]?.ToString() ?? "N/A";

                        // Format and update address
                        lblStoreAddressC.Text = FormatStoreAddress(storeRow);

                        // Update country
                        lblStoreCountryC.Text = storeRow["country"]?.ToString() ?? "N/A";
                    }
                    else
                    {
                        SetDefaultStoreLabels();
                    }
                }
                else
                {
                    // "All Stores" is selected
                    SetDefaultStoreLabels();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating store labels: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                SetDefaultStoreLabels();
            }
        }

        /// <summary>
        /// Formats the store address from multiple fields
        /// </summary>
        private string FormatStoreAddress(DataRow row)
        {
            List<string> addressParts = new List<string>();

            string address = row["address"]?.ToString();
            string city = row["city"]?.ToString();
            string state = row["state"]?.ToString();
            string postalCode = row["postal_code"]?.ToString();

            if (!string.IsNullOrWhiteSpace(address))
                addressParts.Add(address);

            if (!string.IsNullOrWhiteSpace(city))
                addressParts.Add(city);

            if (!string.IsNullOrWhiteSpace(state))
                addressParts.Add(state);

            if (!string.IsNullOrWhiteSpace(postalCode))
                addressParts.Add(postalCode);

            return addressParts.Count > 0 ? string.Join(", ", addressParts) : "N/A";
        }

        /// <summary>
        /// Sets default values for store labels when "All Stores" is selected
        /// </summary>
        private void SetDefaultStoreLabels()
        {
            // Constrain labels to match combobox location and width
            int comboBoxLeft = 1532;
            int comboBoxWidth = 287;

            lblBusinesName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblBusinesName.Location = new System.Drawing.Point(comboBoxLeft, lblBusinesName.Location.Y);
            lblBusinesName.Size = new System.Drawing.Size(comboBoxWidth, lblBusinesName.Size.Height);
            lblBusinesName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblBusinesName.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;

            lblStoreAddressC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblStoreAddressC.Location = new System.Drawing.Point(comboBoxLeft, lblStoreAddressC.Location.Y);
            lblStoreAddressC.Size = new System.Drawing.Size(comboBoxWidth, lblStoreAddressC.Size.Height);
            lblStoreAddressC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblStoreAddressC.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;

            lblStoreCountryC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblStoreCountryC.Location = new System.Drawing.Point(comboBoxLeft, lblStoreCountryC.Location.Y);
            lblStoreCountryC.Size = new System.Drawing.Size(comboBoxWidth, lblStoreCountryC.Size.Height);
            lblStoreCountryC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblStoreCountryC.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;

            lblBusinesName.Text = "All Stores";
            lblStoreAddressC.Text = "Multiple Locations";
            lblStoreCountryC.Text = "Various";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Customer_Management());
        }
    }
}
