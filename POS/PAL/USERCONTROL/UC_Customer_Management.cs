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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Customer_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private DataTable customersTable;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        public UC_Customer_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadCustomers();
            InitializeSearchControl();
            InitializeExportButtons();
            
            // Apply permission-based visibility for export buttons
            ApplyExportButtonVisibility();
        }

        /// <summary>
        /// Apply permission-based visibility to export buttons
        /// </summary>
        private void ApplyExportButtonVisibility()
        {
            bool canExport = PermissionManager.HasPermission(PermissionManager.Permissions.VIEW_EXPORT_BUTTONS);
            if (btnExportCSV != null) btnExportCSV.Visible = canExport;
            if (btnExportExcel != null) btnExportExcel.Visible = canExport;
            if (btnExportPDF != null) btnExportPDF.Visible = canExport;
            if (btnPrint != null) btnPrint.Visible = canExport;
        }

        /// <summary>
        /// Initializes the repository items for button columns
        /// </summary>
        private void InitializeRepositoryItems()
        {
            repositoryItemButtonEdit_Edit = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit_Delete = new RepositoryItemButtonEdit();
            gridCustomers.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
            gridCustomers.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            // Clear existing columns
            gridView1.Columns.Clear();

            // Add columns matching the database schema
            var colCustomerId = gridView1.Columns.AddVisible("customer_id", "Customer ID");
            colCustomerId.FieldName = "customer_id";
            colCustomerId.Caption = "Customer ID";
            colCustomerId.Width = 100;
            colCustomerId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomerId.OptionsColumn.AllowEdit = false;
            colCustomerId.OptionsColumn.AllowFocus = false;
            colCustomerId.OptionsColumn.FixedWidth = true;

            var colFullName = gridView1.Columns.AddVisible("full_name", "Full Name");
            colFullName.FieldName = "full_name";
            colFullName.Caption = "Full Name";
            colFullName.Width = 200;
            colFullName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFullName.AppearanceCell.ForeColor = Color.FromArgb(3, 167, 140);
            colFullName.AppearanceCell.Font = new Font("Segoe UI", 9.75F, FontStyle.Underline);
            colFullName.OptionsColumn.AllowEdit = false;
            colFullName.OptionsColumn.AllowFocus = true;
            colFullName.OptionsColumn.FixedWidth = true;

            var colCompanyName = gridView1.Columns.AddVisible("company_name", "Company Name");
            colCompanyName.FieldName = "company_name";
            colCompanyName.Caption = "Company Name";
            colCompanyName.Width = 200;
            colCompanyName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCompanyName.OptionsColumn.AllowEdit = false;
            colCompanyName.OptionsColumn.AllowFocus = false;
            colCompanyName.OptionsColumn.FixedWidth = true;

            var colEmail = gridView1.Columns.AddVisible("email", "Email");
            colEmail.FieldName = "email";
            colEmail.Caption = "Email";
            colEmail.Width = 200;
            colEmail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEmail.OptionsColumn.AllowEdit = false;
            colEmail.OptionsColumn.AllowFocus = false;
            colEmail.OptionsColumn.FixedWidth = true;

            var colPhone = gridView1.Columns.AddVisible("phone", "Phone");
            colPhone.FieldName = "phone";
            colPhone.Caption = "Phone";
            colPhone.Width = 150;
            colPhone.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhone.OptionsColumn.AllowEdit = false;
            colPhone.OptionsColumn.AllowFocus = false;
            colPhone.OptionsColumn.FixedWidth = true;

            var colAddress = gridView1.Columns.AddVisible("address", "Address");
            colAddress.FieldName = "address";
            colAddress.Caption = "Address";
            colAddress.Width = 250;
            colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colAddress.OptionsColumn.AllowEdit = false;
            colAddress.OptionsColumn.AllowFocus = false;
            colAddress.OptionsColumn.FixedWidth = true;

            var colCity = gridView1.Columns.AddVisible("city", "City");
            colCity.FieldName = "city";
            colCity.Caption = "City";
            colCity.Width = 120;
            colCity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCity.OptionsColumn.AllowEdit = false;
            colCity.OptionsColumn.AllowFocus = false;
            colCity.OptionsColumn.FixedWidth = true;

            var colGroupName = gridView1.Columns.AddVisible("group_name", "Customer Group");
            colGroupName.FieldName = "group_name";
            colGroupName.Caption = "Customer Group";
            colGroupName.Width = 150;
            colGroupName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colGroupName.OptionsColumn.AllowEdit = false;
            colGroupName.OptionsColumn.AllowFocus = false;
            colGroupName.OptionsColumn.FixedWidth = true;

            var colStatus = gridView1.Columns.AddVisible("status", "Status");
            colStatus.FieldName = "status";
            colStatus.Caption = "Status";
            colStatus.Width = 80;
            colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.OptionsColumn.AllowFocus = false;
            colStatus.OptionsColumn.FixedWidth = true;

            // Add Edit button column
            var colEdit = gridView1.Columns.AddVisible("Edit", "Edit");
            colEdit.Caption = "Edit";
            colEdit.Width = 80;
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEdit.OptionsColumn.AllowEdit = true;
            colEdit.OptionsColumn.AllowFocus = true;
            colEdit.OptionsColumn.FixedWidth = true;
            colEdit.OptionsColumn.ShowCaption = true;

            // Add Delete button column
            var colDelete = gridView1.Columns.AddVisible("Delete", "Delete");
            colDelete.Caption = "Delete";
            colDelete.Width = 80;
            colDelete.ColumnEdit = repositoryItemButtonEdit_Delete;
            colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDelete.OptionsColumn.AllowEdit = true;
            colDelete.OptionsColumn.AllowFocus = true;
            colDelete.OptionsColumn.FixedWidth = true;
            colDelete.OptionsColumn.ShowCaption = true;

            // Configure button editors
            ConfigureButtonEditors();

            // Apply grid appearance styling
            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridView1.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridView1.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridView1.ColumnPanelRowHeight = 44;
            gridView1.RowHeight = 44;

            // Grid view options
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.OptionsSelection.MultiSelect = false;
            
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;

            // Set default sort order by customer_id ascending
            colCustomerId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridView1.DoubleClick += GridView1_DoubleClick;

            // Handle single-click on customer name to view details
            gridView1.Click += GridView1_Click;

            // Add context menu for Edit and Delete
            CreateContextMenu();
        }

        /// <summary>
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            txtSearch.Properties.NullValuePrompt = "Search customers...";
            txtSearch.Properties.ShowNullValuePromptWhenFocused = true;
            txtSearch.EditValueChanged += TxtSearch_EditValueChanged;
            txtSearch.KeyPress += TxtSearch_KeyPress;
            btnSearch.Click += BtnSearch_Click;
        }

        /// <summary>
        /// Initializes the export button event handlers
        /// </summary>
        private void InitializeExportButtons()
        {
            // Wire up export button events
            if (btnExportCSV != null)
                btnExportCSV.Click += BtnExportCSV_Click;
            
            if (btnExportExcel != null)
                btnExportExcel.Click += BtnExportExcel_Click;
            
            if (btnExportPDF != null)
                btnExportPDF.Click += BtnExportPDF_Click;
            
            if (btnPrint != null)
                btnPrint.Click += BtnPrint_Click;
        }

        /// <summary>
        /// Handles the search text box value change event
        /// </summary>
        private void TxtSearch_EditValueChanged(object sender, EventArgs e)
        {
            // Optional: Real-time search as user types
            // Uncomment if you want search to happen while typing
            // PerformSearch(txtSearch.Text);
        }

        /// <summary>
        /// Handles the Enter key press in search text box
        /// </summary>
        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                PerformSearch(txtSearch.Text);
            }
        }

        /// <summary>
        /// Handles the search button click event
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearch.Text);
        }

        /// <summary>
        /// Performs search filtering on the customer grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (customersTable == null || customersTable.Rows.Count == 0)
                    return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    gridCustomers.DataSource = customersTable;
                    gridView1.RefreshData();
                    return;
                }

                // Escape special characters for LIKE expression
                searchText = searchText.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

                // Create a filtered view based on search text
                DataView dataView = new DataView(customersTable);
                
                // Build filter expression to search across multiple columns
                StringBuilder filterExpression = new StringBuilder();
                
                // Add conditions for each searchable column
                List<string> conditions = new List<string>();
                
                conditions.Add($"CONVERT(customer_id, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"full_name LIKE '%{searchText}%'");
                conditions.Add($"company_name LIKE '%{searchText}%'");
                conditions.Add($"email LIKE '%{searchText}%'");
                conditions.Add($"phone LIKE '%{searchText}%'");
                conditions.Add($"address LIKE '%{searchText}%'");
                conditions.Add($"city LIKE '%{searchText}%'");
                conditions.Add($"group_name LIKE '%{searchText}%'");
                conditions.Add($"status LIKE '%{searchText}%'");

                // Join all conditions with OR
                filterExpression.Append(string.Join(" OR ", conditions));

                // Apply filter
                dataView.RowFilter = filterExpression.ToString();
                
                // Create a new DataTable from the filtered view
                DataTable filteredTable = dataView.ToTable();
                
                // Update grid data source
                gridCustomers.DataSource = filteredTable;
                
                // Refresh the grid view
                gridView1.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error performing search: {ex.Message}",
                    "Search Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports customer data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (customersTable == null || customersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Customers_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        // Export grid to CSV using DevExpress export functionality
                        gridView1.ExportToCsv(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Customer data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export CSV",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error exporting to CSV: {ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports customer data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (customersTable == null || customersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Customers_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        // Export grid to Excel using DevExpress export functionality
                        gridView1.ExportToXlsx(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Customer data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export Excel",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error exporting to Excel: {ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports customer data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (customersTable == null || customersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Customers_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    // Store original column widths
                    var originalWidths = new Dictionary<string, int>();
                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
                    {
                        if (col.Visible)
                        {
                            originalWidths[col.FieldName] = col.Width;
                        }
                    }

                    try
                    {
                        // Adjust column widths for better PDF fitting
                        var colCustomerId = gridView1.Columns["customer_id"];
                        var colFullName = gridView1.Columns["full_name"];
                        var colCompanyName = gridView1.Columns["company_name"];
                        var colEmail = gridView1.Columns["email"];
                        var colPhone = gridView1.Columns["phone"];
                        var colAddress = gridView1.Columns["address"];
                        var colCity = gridView1.Columns["city"];
                        var colGroupName = gridView1.Columns["group_name"];
                        var colStatus = gridView1.Columns["status"];

                        if (colCustomerId != null) colCustomerId.Width = 60;
                        if (colFullName != null) colFullName.Width = 120;
                        if (colCompanyName != null) colCompanyName.Width = 120;
                        if (colEmail != null) colEmail.Width = 130;
                        if (colPhone != null) colPhone.Width = 90;
                        if (colAddress != null) colAddress.Width = 150;
                        if (colCity != null) colCity.Width = 80;
                        if (colGroupName != null) colGroupName.Width = 100;
                        if (colStatus != null) colStatus.Width = 60;

                        // Export grid to PDF using DevExpress export functionality
                        gridView1.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Customer data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export PDF",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore original column widths
                        foreach (var kvp in originalWidths)
                        {
                            var col = gridView1.Columns[kvp.Key];
                            if (col != null)
                            {
                                col.Width = kvp.Value;
                            }
                        }

                        // Restore column visibility
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error exporting to PDF: {ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Prints the customer data using Windows default print dialog with preview and dynamic column fitting
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (customersTable == null || customersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Temporarily hide Edit and Delete columns
                var editColumn = gridView1.Columns["Edit"];
                var deleteColumn = gridView1.Columns["Delete"];
                bool editVisible = editColumn?.Visible ?? false;
                bool deleteVisible = deleteColumn?.Visible ?? false;

                if (editColumn != null) editColumn.Visible = false;
                if (deleteColumn != null) deleteColumn.Visible = false;

                // Store original column widths
                var originalWidths = new Dictionary<string, int>();
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
                {
                    if (col.Visible)
                    {
                        originalWidths[col.FieldName] = col.Width;
                    }
                }

                try
                {
                    // Adjust column widths for better printing
                    var colCustomerId = gridView1.Columns["customer_id"];
                    var colFullName = gridView1.Columns["full_name"];
                    var colCompanyName = gridView1.Columns["company_name"];
                    var colEmail = gridView1.Columns["email"];
                    var colPhone = gridView1.Columns["phone"];
                    var colAddress = gridView1.Columns["address"];
                    var colCity = gridView1.Columns["city"];
                    var colGroupName = gridView1.Columns["group_name"];
                    var colStatus = gridView1.Columns["status"];

                    if (colCustomerId != null) colCustomerId.Width = 60;
                    if (colFullName != null) colFullName.Width = 120;
                    if (colCompanyName != null) colCompanyName.Width = 120;
                    if (colEmail != null) colEmail.Width = 130;
                    if (colPhone != null) colPhone.Width = 90;
                    if (colAddress != null) colAddress.Width = 150;
                    if (colCity != null) colCity.Width = 80;
                    if (colGroupName != null) colGroupName.Width = 100;
                    if (colStatus != null) colStatus.Width = 60;

                    // Create print link using ReportHelper
                    PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                        gridCustomers, 
                        "Customer List", 
                        landscape: false);

                    // Show print preview dialog with print options
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore original column widths
                    foreach (var kvp in originalWidths)
                    {
                        var col = gridView1.Columns[kvp.Key];
                        if (col != null)
                        {
                            col.Width = kvp.Value;
                        }
                    }

                    // Restore column visibility
                    if (editColumn != null) editColumn.Visible = editVisible;
                    if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing customer data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Configures the button editors for Edit and Delete columns
        /// </summary>
        private void ConfigureButtonEditors()
        {
            // Configure Edit button
            repositoryItemButtonEdit_Edit.Buttons.Clear();
            var editButton = new EditorButton(ButtonPredefines.Glyph);
            editButton.Caption = "Edit";
            editButton.Kind = ButtonPredefines.Glyph;
            repositoryItemButtonEdit_Edit.Buttons.Add(editButton);
            repositoryItemButtonEdit_Edit.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Edit.ButtonClick += RepositoryItemButtonEdit_Edit_ButtonClick;

            // Configure Delete button
            repositoryItemButtonEdit_Delete.Buttons.Clear();
            var deleteButton = new EditorButton(ButtonPredefines.Glyph);
            deleteButton.Caption = "Delete";
            deleteButton.Kind = ButtonPredefines.Glyph;
            repositoryItemButtonEdit_Delete.Buttons.Add(deleteButton);
            repositoryItemButtonEdit_Delete.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Delete.ButtonClick += RepositoryItemButtonEdit_Delete_ButtonClick;
        }

        /// <summary>
        /// Handle Edit button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Edit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            EditSelectedCustomer();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedCustomer();
        }

        /// <summary>
        /// Creates context menu for grid operations
        /// </summary>
        private void CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            var editMenuItem = new ToolStripMenuItem("Edit");
            editMenuItem.Click += EditMenuItem_Click;
            contextMenu.Items.Add(editMenuItem);

            var deleteMenuItem = new ToolStripMenuItem("Delete");
            deleteMenuItem.Click += DeleteMenuItem_Click;
            contextMenu.Items.Add(deleteMenuItem);

            var refreshMenuItem = new ToolStripMenuItem("Refresh");
            refreshMenuItem.Click += (s, e) => LoadCustomers();
            contextMenu.Items.Add(refreshMenuItem);

            gridCustomers.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all customers from database
        /// </summary>
        public void LoadCustomers()
        {
            try
            {
                customersTable = _bllContacts.GetCustomers();
                gridCustomers.DataSource = customersTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customers: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle double-click to edit
        /// </summary>
        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedCustomer();
        }

        /// <summary>
        /// Handle single-click on customer name to view details
        /// </summary>
        private void GridView1_Click(object sender, EventArgs e)
        {
            try
            {
                var view = gridView1;
                var hitInfo = view.CalcHitInfo((e as MouseEventArgs).Location);

                // Check if the click was on a cell
                if (hitInfo.InRowCell)
                {
                    // Check if the clicked column is the full_name column
                    if (hitInfo.Column != null && hitInfo.Column.FieldName == "full_name")
                    {
                        ViewCustomerDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error handling click: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedCustomer();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedCustomer();
        }

        /// <summary>
        /// Edits the selected customer
        /// </summary>
        private void EditSelectedCustomer()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a customer to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int customerId = Convert.ToInt32(selectedRow["customer_id"]);

                // Navigate to registration form in edit mode
                var registrationForm = new UC_Customer_Registration(customerId);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing customer: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Views the selected customer details
        /// </summary>
        private void ViewCustomerDetails()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a customer to view.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int customerId = Convert.ToInt32(selectedRow["customer_id"]);

                // Navigate to customer details form
                var detailsForm = new UC_Customer_Details(customerId);
                Main.Instance.LoadUserControl(detailsForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error viewing customer details: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected customer
        /// </summary>
        private void DeleteSelectedCustomer()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a customer to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int customerId = Convert.ToInt32(selectedRow["customer_id"]);
                string fullName = selectedRow["full_name"]?.ToString();

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the customer '{fullName}'?\n\nThis will mark the customer as inactive.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Delete the customer (soft delete)
                bool success = _bllContacts.DeleteCustomer(customerId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"Customer '{fullName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadCustomers();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete customer.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting customer: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn25Filter_Click(object sender, EventArgs e) { }
        private void btn50Filter_Click(object sender, EventArgs e) { }
        private void btn100Filter_Click(object sender, EventArgs e) { }
        private void btn200Filter_Click(object sender, EventArgs e) { }
        private void btn500Filter_Click(object sender, EventArgs e) { }
        private void btn1000Filter_Click(object sender, EventArgs e) { }
        private void btnAllFilter_Click(object sender, EventArgs e) { }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Customer_Registration());
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Customer_Registration());
        }
    }
}
