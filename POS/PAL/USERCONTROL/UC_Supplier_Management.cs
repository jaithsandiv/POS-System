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
using DevExpress.XtraGrid.Columns;
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Supplier_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private DataTable suppliersTable;

        public UC_Supplier_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadSuppliers();
            
            // Initialize search control
            InitializeSearchControl();
            
            // Initialize export buttons
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
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            txtSearch.Properties.NullValuePrompt = "Search suppliers...";
            txtSearch.Properties.ShowNullValuePromptWhenFocused = true;
            txtSearch.EditValueChanged += TxtSearch_EditValueChanged;
            txtSearch.KeyPress += TxtSearch_KeyPress;
            btnSearch.Click += BtnSearch_Click;
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
        /// Exports supplier data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (suppliersTable == null || suppliersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Suppliers_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns and save column widths
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;
                    
                    // Store original column settings
                    Dictionary<GridColumn, int> originalWidths = new Dictionary<GridColumn, int>();
                    Dictionary<GridColumn, bool> originalFixedWidth = new Dictionary<GridColumn, bool>();
                    
                    foreach (GridColumn column in gridView1.Columns)
                    {
                        if (column.Visible)
                        {
                            originalWidths[column] = column.Width;
                            originalFixedWidth[column] = column.OptionsColumn.FixedWidth;
                        }
                    }

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        // Make columns auto-fit for export
                        foreach (GridColumn column in gridView1.Columns)
                        {
                            if (column.Visible)
                            {
                                column.OptionsColumn.FixedWidth = false;
                            }
                        }
                        
                        gridView1.BestFitColumns();
                        
                        // Export grid to CSV using DevExpress export functionality
                        gridView1.ExportToCsv(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Supplier data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export CSV",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility and widths
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                        
                        foreach (var kvp in originalWidths)
                        {
                            kvp.Key.Width = kvp.Value;
                            kvp.Key.OptionsColumn.FixedWidth = originalFixedWidth[kvp.Key];
                        }
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
        /// Exports supplier data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (suppliersTable == null || suppliersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Suppliers_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns and save column widths
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;
                    
                    // Store original column settings
                    Dictionary<GridColumn, int> originalWidths = new Dictionary<GridColumn, int>();
                    Dictionary<GridColumn, bool> originalFixedWidth = new Dictionary<GridColumn, bool>();
                    
                    foreach (GridColumn column in gridView1.Columns)
                    {
                        if (column.Visible)
                        {
                            originalWidths[column] = column.Width;
                            originalFixedWidth[column] = column.OptionsColumn.FixedWidth;
                        }
                    }

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        // Make columns auto-fit for export
                        foreach (GridColumn column in gridView1.Columns)
                        {
                            if (column.Visible)
                            {
                                column.OptionsColumn.FixedWidth = false;
                            }
                        }
                        
                        gridView1.BestFitColumns();
                        
                        // Export grid to Excel using DevExpress export functionality
                        gridView1.ExportToXlsx(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Supplier data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export Excel",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility and widths
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                        
                        foreach (var kvp in originalWidths)
                        {
                            kvp.Key.Width = kvp.Value;
                            kvp.Key.OptionsColumn.FixedWidth = originalFixedWidth[kvp.Key];
                        }
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
        /// Exports supplier data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (suppliersTable == null || suppliersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Suppliers_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns and save column widths
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;
                    
                    // Store original column settings
                    Dictionary<GridColumn, int> originalWidths = new Dictionary<GridColumn, int>();
                    Dictionary<GridColumn, bool> originalFixedWidth = new Dictionary<GridColumn, bool>();
                    
                    foreach (GridColumn column in gridView1.Columns)
                    {
                        if (column.Visible)
                        {
                            originalWidths[column] = column.Width;
                            originalFixedWidth[column] = column.OptionsColumn.FixedWidth;
                        }
                    }

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        // Make columns auto-fit for export
                        foreach (GridColumn column in gridView1.Columns)
                        {
                            if (column.Visible)
                            {
                                column.OptionsColumn.FixedWidth = false;
                            }
                        }
                        
                        gridView1.BestFitColumns();
                        
                        // Create print link for better control over PDF export
                        PrintableComponentLink printLink = new PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                        printLink.Component = gridSuppliers;
                        printLink.Landscape = true;
                        printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                        
                        // Set margins
                        printLink.Margins.Left = 50;
                        printLink.Margins.Right = 50;
                        printLink.Margins.Top = 50;
                        printLink.Margins.Bottom = 50;
                        
                        // Create document and fit to page width
                        printLink.CreateDocument();
                        printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        
                        // Export to PDF
                        printLink.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Supplier data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export PDF",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility and widths
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                        
                        foreach (var kvp in originalWidths)
                        {
                            kvp.Key.Width = kvp.Value;
                            kvp.Key.OptionsColumn.FixedWidth = originalFixedWidth[kvp.Key];
                        }
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
        /// Prints the supplier data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (suppliersTable == null || suppliersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Temporarily hide Edit and Delete columns and save column widths
                var editColumn = gridView1.Columns["Edit"];
                var deleteColumn = gridView1.Columns["Delete"];
                bool editVisible = editColumn?.Visible ?? false;
                bool deleteVisible = deleteColumn?.Visible ?? false;
                
                // Store original column settings
                Dictionary<GridColumn, int> originalWidths = new Dictionary<GridColumn, int>();
                Dictionary<GridColumn, bool> originalFixedWidth = new Dictionary<GridColumn, bool>();
                
                foreach (GridColumn column in gridView1.Columns)
                {
                    if (column.Visible)
                    {
                        originalWidths[column] = column.Width;
                        originalFixedWidth[column] = column.OptionsColumn.FixedWidth;
                    }
                }

                if (editColumn != null) editColumn.Visible = false;
                if (deleteColumn != null) deleteColumn.Visible = false;

                try
                {
                    // Make columns auto-fit for printing
                    foreach (GridColumn column in gridView1.Columns)
                    {
                        if (column.Visible)
                        {
                            column.OptionsColumn.FixedWidth = false;
                        }
                    }
                    
                    gridView1.BestFitColumns();

                    // Create print link using ReportHelper
                    PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                        gridSuppliers, 
                        "Supplier List", 
                        landscape: false);

                    // Show print preview dialog with print options
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore column visibility and widths
                    if (editColumn != null) editColumn.Visible = editVisible;
                    if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                    
                    foreach (var kvp in originalWidths)
                    {
                        kvp.Key.Width = kvp.Value;
                        kvp.Key.OptionsColumn.FixedWidth = originalFixedWidth[kvp.Key];
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing supplier data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Supplier_Registration());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearch.Text);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearch.Text);
        }

        /// <summary>
        /// Performs search filtering on the supplier grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (suppliersTable == null || suppliersTable.Rows.Count == 0)
                    return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    gridSuppliers.DataSource = suppliersTable;
                    gridView1.RefreshData();
                    return;
                }

                // Escape special characters for LIKE expression
                searchText = searchText.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

                // Create a filtered view based on search text
                DataView dataView = new DataView(suppliersTable);
                
                // Build filter expression to search across multiple columns
                StringBuilder filterExpression = new StringBuilder();
                
                // Add conditions for each searchable column
                List<string> conditions = new List<string>();
                
                conditions.Add($"CONVERT(supplier_id, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"supplier_name LIKE '%{searchText}%'");
                conditions.Add($"company_name LIKE '%{searchText}%'");
                conditions.Add($"email LIKE '%{searchText}%'");
                conditions.Add($"phone LIKE '%{searchText}%'");
                conditions.Add($"address LIKE '%{searchText}%'");
                conditions.Add($"status LIKE '%{searchText}%'");

                // Join all conditions with OR
                filterExpression.Append(string.Join(" OR ", conditions));

                // Apply filter
                dataView.RowFilter = filterExpression.ToString();
                
                // Create a new DataTable from the filtered view
                DataTable filteredTable = dataView.ToTable();
                
                // Update grid data source
                gridSuppliers.DataSource = filteredTable;
                
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
        /// Initializes the repository items for button columns
        /// </summary>
        private void InitializeRepositoryItems()
        {
            // Repository items are already created in Designer.cs, just configure them
            // No need to create new instances or add them to gridSuppliers.RepositoryItems
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            // Clear existing columns
            gridView1.Columns.Clear();

            // Add columns matching the database schema
            var colSupplierId = gridView1.Columns.AddVisible("supplier_id", "Supplier ID");
            colSupplierId.FieldName = "supplier_id";
            colSupplierId.Caption = "Supplier ID";
            colSupplierId.Width = 100;
            colSupplierId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSupplierId.OptionsColumn.AllowEdit = false;
            colSupplierId.OptionsColumn.AllowFocus = false;
            colSupplierId.OptionsColumn.FixedWidth = true;

            var colSupplierName = gridView1.Columns.AddVisible("supplier_name", "Supplier Name");
            colSupplierName.FieldName = "supplier_name";
            colSupplierName.Caption = "Supplier Name";
            colSupplierName.Width = 200;
            colSupplierName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSupplierName.OptionsColumn.AllowEdit = false;
            colSupplierName.OptionsColumn.AllowFocus = false;
            colSupplierName.OptionsColumn.FixedWidth = true;

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

            // Set default sort order by supplier_id ascending
            colSupplierId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridView1.DoubleClick += GridView1_DoubleClick;

            // Add context menu for Edit and Delete
            CreateContextMenu();
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
            EditSelectedSupplier();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedSupplier();
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
            refreshMenuItem.Click += (s, e) => LoadSuppliers();
            contextMenu.Items.Add(refreshMenuItem);

            gridSuppliers.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all suppliers from database
        /// </summary>
        public void LoadSuppliers()
        {
            try
            {
                suppliersTable = _bllContacts.GetSuppliers();
                gridSuppliers.DataSource = suppliersTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading suppliers: {ex.Message}",
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
            EditSelectedSupplier();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedSupplier();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedSupplier();
        }

        /// <summary>
        /// Edits the selected supplier
        /// </summary>
        private void EditSelectedSupplier()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a supplier to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int supplierId = Convert.ToInt32(selectedRow["supplier_id"]);

                // Navigate to registration form in edit mode
                var registrationForm = new UC_Supplier_Registration(supplierId);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing supplier: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected supplier
        /// </summary>
        private void DeleteSelectedSupplier()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a supplier to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int supplierId = Convert.ToInt32(selectedRow["supplier_id"]);
                string supplierName = selectedRow["supplier_name"]?.ToString();

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the supplier '{supplierName}'?\n\nThis will mark the supplier as inactive.",
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

                // Delete the supplier (soft delete)
                bool success = _bllContacts.DeleteSupplier(supplierId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"Supplier '{supplierName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadSuppliers();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete supplier.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting supplier: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
