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
using DevExpress.XtraPrinting;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_CustomerGroup_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private DataTable customerGroupsTable;

        public UC_CustomerGroup_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadCustomerGroups();
            InitializeSearchControl();
            InitializeExportButtons();
        }

        /// <summary>
        /// Configures the grid columns to match database schema with Transaction Summary styling
        /// </summary>
        private void ConfigureGrid()
        {
            // Clear existing columns
            gridView1.Columns.Clear();

            // Add columns matching the database schema
            // group_id
            var colGroupId = gridView1.Columns.AddVisible("group_id", "Group ID");
            colGroupId.FieldName = "group_id";
            colGroupId.Caption = "Group ID";
            colGroupId.Width = 100;
            colGroupId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colGroupId.OptionsColumn.AllowEdit = false;
            colGroupId.OptionsColumn.AllowFocus = false;
            colGroupId.OptionsColumn.FixedWidth = true;

            // group_name
            var colGroupName = gridView1.Columns.AddVisible("group_name", "Group Name");
            colGroupName.FieldName = "group_name";
            colGroupName.Caption = "Group Name";
            colGroupName.Width = 300;
            colGroupName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colGroupName.OptionsColumn.AllowEdit = false;
            colGroupName.OptionsColumn.AllowFocus = false;
            colGroupName.OptionsColumn.FixedWidth = true;

            // discount_percent
            var colDiscountPercent = gridView1.Columns.AddVisible("discount_percent", "Discount %");
            colDiscountPercent.FieldName = "discount_percent";
            colDiscountPercent.Caption = "Discount %";
            colDiscountPercent.Width = 150;
            colDiscountPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colDiscountPercent.DisplayFormat.FormatString = "n2";
            colDiscountPercent.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDiscountPercent.OptionsColumn.AllowEdit = false;
            colDiscountPercent.OptionsColumn.AllowFocus = false;
            colDiscountPercent.OptionsColumn.FixedWidth = true;

            // status
            var colStatus = gridView1.Columns.AddVisible("status", "Status");
            colStatus.FieldName = "status";
            colStatus.Caption = "Status";
            colStatus.Width = 100;
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

            // Apply Transaction Summary grid appearance styling
            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridView1.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridView1.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights to match Transaction Summary
            gridView1.ColumnPanelRowHeight = 44;
            gridView1.RowHeight = 44;

            // Grid view options matching Transaction Summary
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            
            gridView1.OptionsBehavior.Editable = true; // Changed to true to allow button clicks
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.OptionsSelection.MultiSelect = false;
            
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;

            // Set default sort order by group_id ascending
            colGroupId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

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
            EditSelectedCustomerGroup();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedCustomerGroup();
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
            refreshMenuItem.Click += (s, e) => LoadCustomerGroups();
            contextMenu.Items.Add(refreshMenuItem);

            gridCustomerGroup.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all customer groups from database
        /// </summary>
        public void LoadCustomerGroups()
        {
            try
            {
                customerGroupsTable = _bllContacts.GetCustomerGroups();
                gridCustomerGroup.DataSource = customerGroupsTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customer groups: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            txtSearch.Properties.NullValuePrompt = "Search customer groups...";
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
        /// Handles the search button click event
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearch.Text);
        }

        /// <summary>
        /// Performs search filtering on the customer group grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (customerGroupsTable == null || customerGroupsTable.Rows.Count == 0)
                    return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    gridCustomerGroup.DataSource = customerGroupsTable;
                    gridView1.RefreshData();
                    return;
                }

                // Escape special characters for LIKE expression
                searchText = searchText.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

                // Create a filtered view based on search text
                DataView dataView = new DataView(customerGroupsTable);
                
                // Build filter expression to search across multiple columns
                StringBuilder filterExpression = new StringBuilder();
                
                // Add conditions for each searchable column
                List<string> conditions = new List<string>();
                
                conditions.Add($"CONVERT(group_id, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"group_name LIKE '%{searchText}%'");
                conditions.Add($"CONVERT(discount_percent, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"status LIKE '%{searchText}%'");

                // Join all conditions with OR
                filterExpression.Append(string.Join(" OR ", conditions));

                // Apply filter
                dataView.RowFilter = filterExpression.ToString();
                
                // Create a new DataTable from the filtered view
                DataTable filteredTable = dataView.ToTable();
                
                // Update grid data source
                gridCustomerGroup.DataSource = filteredTable;
                
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
        /// Handle double-click to edit
        /// </summary>
        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedCustomerGroup();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedCustomerGroup();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedCustomerGroup();
        }

        /// <summary>
        /// Edits the selected customer group
        /// </summary>
        private void EditSelectedCustomerGroup()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a customer group to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int groupId = Convert.ToInt32(selectedRow["group_id"]);
                string groupName = selectedRow["group_name"]?.ToString();
                decimal discountPercent = Convert.ToDecimal(selectedRow["discount_percent"]);

                // Navigate to registration form in edit mode
                var registrationForm = new UC_CustomerGroup_Registration(groupId, groupName, discountPercent);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing customer group: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected customer group
        /// </summary>
        private void DeleteSelectedCustomerGroup()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a customer group to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int groupId = Convert.ToInt32(selectedRow["group_id"]);
                string groupName = selectedRow["group_name"]?.ToString();

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the customer group '{groupName}'?\n\nThis will mark the group as inactive.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Get current user ID (assuming it's stored in Main.DataSetApp)
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Delete the customer group (soft delete)
                bool success = _bllContacts.DeleteCustomerGroup(groupId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"Customer group '{groupName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadCustomerGroups();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete customer group.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting customer group: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
        /// Exports customer group data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupsTable == null || customerGroupsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"CustomerGroups_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
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
                            $"Customer group data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports customer group data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupsTable == null || customerGroupsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"CustomerGroups_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
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
                            $"Customer group data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports customer group data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupsTable == null || customerGroupsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"CustomerGroups_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
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
                        var colGroupId = gridView1.Columns["group_id"];
                        var colGroupName = gridView1.Columns["group_name"];
                        var colDiscountPercent = gridView1.Columns["discount_percent"];
                        var colStatus = gridView1.Columns["status"];

                        if (colGroupId != null) colGroupId.Width = 80;
                        if (colGroupName != null) colGroupName.Width = 200;
                        if (colDiscountPercent != null) colDiscountPercent.Width = 100;
                        if (colStatus != null) colStatus.Width = 80;

                        // Export grid to PDF using DevExpress export functionality
                        gridView1.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Customer group data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the customer group data using Windows default print dialog with preview and dynamic column fitting
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupsTable == null || customerGroupsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group data to print.",
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
                    var colGroupId = gridView1.Columns["group_id"];
                    var colGroupName = gridView1.Columns["group_name"];
                    var colDiscountPercent = gridView1.Columns["discount_percent"];
                    var colStatus = gridView1.Columns["status"];

                    if (colGroupId != null) colGroupId.Width = 80;
                    if (colGroupName != null) colGroupName.Width = 200;
                    if (colDiscountPercent != null) colDiscountPercent.Width = 100;
                    if (colStatus != null) colStatus.Width = 80;

                    // Create a PrintableComponentLink to print the grid
                    PrintableComponentLink printLink = 
                        new PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                    
                    printLink.Component = gridCustomerGroup;
                    
                    // Configure print settings for dynamic column fitting
                    printLink.Landscape = true;
                    printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                    
                    // Set margins
                    printLink.Margins.Left = 50;
                    printLink.Margins.Right = 50;
                    printLink.Margins.Top = 50;
                    printLink.Margins.Bottom = 50;
                    
                    // Create document
                    printLink.CreateDocument();
                    
                    // Auto-fit columns to page width
                    printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    
                    // Add header
                    PageHeaderFooter header = printLink.PageHeaderFooter as PageHeaderFooter;
                    if (header != null)
                    {
                        header.Header.Content.Clear();
                        header.Header.Content.AddRange(new string[] {
                            "Customer Group List",
                            "",
                            $"Printed: {DateTime.Now:dd/MM/yyyy HH:mm}"
                        });
                        header.Header.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                        header.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
                    }
                    
                    // Add footer with page numbers
                    if (header != null)
                    {
                        header.Footer.Content.Clear();
                        header.Footer.Content.AddRange(new string[] {
                            "",
                            "[Page # of Pages #]",
                            ""
                        });
                        header.Footer.Font = new Font("Segoe UI", 9);
                        header.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
                    }

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
                    $"Error printing customer group data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAddCustomerGroup_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_CustomerGroup_Registration());
        }
    }
}
