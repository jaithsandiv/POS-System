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
        private readonly BLL_Store _bllStore = new BLL_Store();
        private int _supplierId;
        private DateTime _selectedStartDate;
        private DateTime _selectedEndDate;
        private int? _selectedStoreId = null;
        private DataTable _storesTable;

        /// <summary>
        /// Constructor that accepts supplier ID
        /// </summary>
        public UC_Supplier_Details(int supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            
            // Initialize default date range (last 30 days)
            _selectedEndDate = DateTime.Today;
            _selectedStartDate = _selectedEndDate.AddDays(-30);
            
            InitializeDateFilters();
            SetupGrid();
            LoadStoresDropdown();
            LoadSupplierDetails();
            LoadSuppliersDropdown();
            UpdateDateLabels();
        }

        /// <summary>
        /// Default constructor (for designer)
        /// </summary>
        public UC_Supplier_Details()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Configures the grid columns
        /// </summary>
        private void SetupGrid()
        {
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.RowAutoHeight = true;
            
            // Clear existing columns if any
            gridView1.Columns.Clear();

            // Add columns
            gridView1.Columns.AddVisible("TransactionDate", "Date");
            gridView1.Columns.AddVisible("InvoiceNumber", "Invoice #");
            gridView1.Columns.AddVisible("TransactionType", "Type");
            gridView1.Columns.AddVisible("Description", "Description");
            gridView1.Columns.AddVisible("Debit", "Debit");
            gridView1.Columns.AddVisible("Credit", "Credit");
            gridView1.Columns.AddVisible("Status", "Status");

            // Format columns
            gridView1.Columns["TransactionDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["TransactionDate"].DisplayFormat.FormatString = "dd/MM/yyyy";
            
            gridView1.Columns["Debit"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns["Debit"].DisplayFormat.FormatString = "N2";
            
            gridView1.Columns["Credit"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns["Credit"].DisplayFormat.FormatString = "N2";
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
                // 1. Fetch Transactions
                DataTable transactions = _bllContacts.GetSupplierTransactions(_supplierId, _selectedStartDate, _selectedEndDate, _selectedStoreId);
                gridControl1.DataSource = transactions;

                // 2. Account Summary (Placeholder for now as we don't have supplier transactions)
                // We set them to 0 to avoid confusion.
                lblOpeningBalanceS.Text = "Rs. 0.00";
                lblTotalInvoiceS.Text = "Rs. 0.00";
                lblTotalPaidS.Text = "Rs. 0.00";
                lblAdvanceBalanceS.Text = "Rs. 0.00";
                lblBalanceDueS.Text = "Rs. 0.00";

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
                    lblSupplierName1.Text = row["supplier_name"]?.ToString() ?? "N/A";
                    lblTableName.Text = supplierData.TableName;
                    lblSupplierAddress.Text = row["address"]?.ToString() ?? "N/A";
                    lblSupplierPhoneNo.Text = row["phone"]?.ToString() ?? "N/A";
                    lblCustomerPhoneNumber.Text = row["phone"]?.ToString() ?? "N/A";
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
                            LoadFilteredData();
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
                // comboBoxStoreLocation: Location (1519, 24), Size (287, 44)
                int comboBoxLeft = comboBoxStoreLocation.Location.X;
                int comboBoxWidth = comboBoxStoreLocation.Width;

                // Configure lblBusinesName
                lblBusinesName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblBusinesName.Location = new System.Drawing.Point(comboBoxLeft, lblBusinesName.Location.Y);
                lblBusinesName.Size = new System.Drawing.Size(comboBoxWidth, lblBusinesName.Size.Height);
                lblBusinesName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblBusinesName.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
                lblBusinesName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;

                // Configure lblStoreAddressS
                lblStoreAddressS.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblStoreAddressS.Location = new System.Drawing.Point(comboBoxLeft, lblStoreAddressS.Location.Y);
                lblStoreAddressS.Size = new System.Drawing.Size(comboBoxWidth, lblStoreAddressS.Size.Height);
                lblStoreAddressS.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblStoreAddressS.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
                lblStoreAddressS.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;

                // Configure lblStoreCountryS
                lblStoreCountryS.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblStoreCountryS.Location = new System.Drawing.Point(comboBoxLeft, lblStoreCountryS.Location.Y);
                lblStoreCountryS.Size = new System.Drawing.Size(comboBoxWidth, lblStoreCountryS.Size.Height);
                lblStoreCountryS.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                lblStoreCountryS.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
                lblStoreCountryS.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;

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
                        lblStoreAddressS.Text = FormatStoreAddress(storeRow);

                        // Update country
                        lblStoreCountryS.Text = storeRow["country"]?.ToString() ?? "N/A";
                    }
                    else
                    {
                        SetDefaultStoreLabelsText();
                    }
                }
                else
                {
                    // "All Stores" is selected
                    SetDefaultStoreLabelsText();
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
                SetDefaultStoreLabelsText();
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
            SetDefaultStoreLabelsText();
        }

        /// <summary>
        /// Sets default text values for store labels
        /// </summary>
        private void SetDefaultStoreLabelsText()
        {
            lblBusinesName.Text = "All Stores";
            lblStoreAddressS.Text = "Multiple Locations";
            lblStoreCountryS.Text = "Various";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Supplier_Management());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Navigate to Add/Edit Supplier screen with current ID
            // Main.Instance.LoadUserControl(new UC_Add_Supplier(_supplierId));
            XtraMessageBox.Show("Edit Supplier feature will be implemented in the next phase.", "Feature Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
