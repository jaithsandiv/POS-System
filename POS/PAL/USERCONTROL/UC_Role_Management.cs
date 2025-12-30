using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.BLL;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Role_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private const int ADMIN_ROLE_ID = 1; // Super Admin role ID - cannot be edited or deleted
        
        private DataTable rolesTable;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        public UC_Role_Management()
        {
            InitializeComponent();
            
            // Check permission to view roles
            if (!PermissionManager.HasPermission(PermissionManager.Permissions.VIEW_ROLES))
            {
                XtraMessageBox.Show(
                    "You do not have permission to view roles.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                Main.Instance.LoadUserControl(new UC_Dashboard());
                return;
            }
            
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadRoles();
            InitializeSearchControl();
            InitializeExportButtons();
            
            // Apply permission-based button visibility
            ApplyPermissionBasedVisibility();
        }
        
        /// <summary>
        /// Apply permission-based visibility to buttons
        /// </summary>
        private void ApplyPermissionBasedVisibility()
        {
            // Hide Add button if user doesn't have ADD_ROLES permission
            if (btnAddRoles != null)
                btnAddRoles.Visible = PermissionManager.HasPermission(PermissionManager.Permissions.ADD_ROLES);
                
            // Hide export buttons if user doesn't have VIEW_EXPORT_BUTTONS permission
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
            gridRoles.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
            gridRoles.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            // Clear existing columns
            gridView1.Columns.Clear();

            // Add columns matching the database schema
            var colRoleId = gridView1.Columns.AddVisible("role_id", "Role ID");
            colRoleId.FieldName = "role_id";
            colRoleId.Caption = "Role ID";
            colRoleId.Width = 100;
            colRoleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colRoleId.OptionsColumn.AllowEdit = false;
            colRoleId.OptionsColumn.AllowFocus = false;
            colRoleId.OptionsColumn.FixedWidth = true;

            var colRoleName = gridView1.Columns.AddVisible("role_name", "Role Name");
            colRoleName.FieldName = "role_name";
            colRoleName.Caption = "Role Name";
            colRoleName.Width = 250;
            colRoleName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colRoleName.OptionsColumn.AllowEdit = false;
            colRoleName.OptionsColumn.AllowFocus = false;

            var colDescription = gridView1.Columns.AddVisible("description", "Description");
            colDescription.FieldName = "description";
            colDescription.Caption = "Description";
            colDescription.Width = 400;
            colDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDescription.OptionsColumn.AllowEdit = false;
            colDescription.OptionsColumn.AllowFocus = false;

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

            // Set default sort order by role_id ascending
            colRoleId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridView1.DoubleClick += GridView1_DoubleClick;

            // Add context menu for Edit and Delete
            CreateContextMenu();
            
            // Handle custom row cell style to disable buttons for Admin role
            gridView1.CustomRowCellEdit += GridView1_CustomRowCellEdit;
        }

        /// <summary>
        /// Customizes cell editors for specific rows (disables Edit/Delete for Admin role)
        /// </summary>
        private void GridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Edit" || e.Column.FieldName == "Delete")
            {
                DataRow row = gridView1.GetDataRow(e.RowHandle);
                if (row != null)
                {
                    int roleId = Convert.ToInt32(row["role_id"]);
                    if (roleId == ADMIN_ROLE_ID)
                    {
                        // Create a disabled button editor for Admin role
                        RepositoryItemButtonEdit disabledButtonEdit = new RepositoryItemButtonEdit();
                        disabledButtonEdit.Buttons.Clear();
                        var button = new EditorButton(ButtonPredefines.Glyph);
                        button.Enabled = false;
                        disabledButtonEdit.Buttons.Add(button);
                        disabledButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
                        disabledButtonEdit.ReadOnly = true;
                        
                        e.RepositoryItem = disabledButtonEdit;
                    }
                }
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
            EditSelectedRole();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedRole();
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
            refreshMenuItem.Click += (s, e) => LoadRoles();
            contextMenu.Items.Add(refreshMenuItem);

            gridRoles.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            txtSearch.Properties.NullValuePrompt = "Search roles...";
            txtSearch.Properties.ShowNullValuePromptWhenFocused = true;
            txtSearch.KeyPress += TxtSearch_KeyPress;
            btnSearch.Click += BtnSearch_Click;
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
        /// Loads all roles from database
        /// </summary>
        public void LoadRoles()
        {
            try
            {
                rolesTable = BLL_Role.GetRoles();
                gridRoles.DataSource = rolesTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading roles: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle search button click
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearch.Text);
        }

        /// <summary>
        /// Performs search filtering on the roles grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (rolesTable == null || rolesTable.Rows.Count == 0)
                    return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    gridRoles.DataSource = rolesTable;
                    gridView1.RefreshData();
                    return;
                }

                // Escape special characters for LIKE expression
                searchText = searchText.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

                // Create a filtered view based on search text
                DataView dataView = new DataView(rolesTable);
                
                // Build filter expression to search across multiple columns
                StringBuilder filterExpression = new StringBuilder();
                
                // Add conditions for each searchable column
                List<string> conditions = new List<string>();
                
                conditions.Add($"CONVERT(role_id, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"role_name LIKE '%{searchText}%'");
                conditions.Add($"description LIKE '%{searchText}%'");
                conditions.Add($"status LIKE '%{searchText}%'");

                // Join all conditions with OR
                filterExpression.Append(string.Join(" OR ", conditions));

                // Apply filter
                dataView.RowFilter = filterExpression.ToString();
                
                // Create a new DataTable from the filtered view
                DataTable filteredTable = dataView.ToTable();
                
                // Update grid data source
                gridRoles.DataSource = filteredTable;
                
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
            EditSelectedRole();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedRole();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedRole();
        }

        /// <summary>
        /// Edits the selected role
        /// </summary>
        private void EditSelectedRole()
        {
            try
            {
                // Check EDIT_ROLES permission
                if (!PermissionManager.HasPermission(PermissionManager.Permissions.EDIT_ROLES))
                {
                    XtraMessageBox.Show(
                        "You do not have permission to edit roles.",
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a role to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int roleId = Convert.ToInt32(selectedRow["role_id"]);
                
                // Prevent editing Admin role
                if (roleId == ADMIN_ROLE_ID)
                {
                    XtraMessageBox.Show(
                        "The Admin role cannot be edited as it is a system role.",
                        "Operation Not Allowed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Navigate to role registration form in edit mode
                var registrationForm = new UC_Roles_Registration(roleId);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing role: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected role
        /// </summary>
        private void DeleteSelectedRole()
        {
            try
            {
                // Check DELETE_ROLES permission
                if (!PermissionManager.HasPermission(PermissionManager.Permissions.DELETE_ROLES))
                {
                    XtraMessageBox.Show(
                        "You do not have permission to delete roles.",
                        "Access Denied",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a role to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int roleId = Convert.ToInt32(selectedRow["role_id"]);
                string roleName = selectedRow["role_name"]?.ToString();
                
                // Prevent deleting Admin role
                if (roleId == ADMIN_ROLE_ID)
                {
                    XtraMessageBox.Show(
                        "The Admin role cannot be deleted as it is a system role.",
                        "Operation Not Allowed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the role '{roleName}'?\n\nThis will mark the role as inactive.",
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

                // Delete the role (soft delete)
                bool success = BLL_Role.DeleteRole(roleId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"Role '{roleName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadRoles();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete role.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting role: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports roles data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolesTable == null || rolesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No roles data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Roles_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
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
                        gridView1.ExportToCsv(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Roles data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports roles data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolesTable == null || rolesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No roles data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Roles_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
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
                        gridView1.ExportToXlsx(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Roles data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports roles data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolesTable == null || rolesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No roles data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Roles_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
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

                    try
                    {
                        gridView1.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Roles data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export PDF",
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
                    $"Error exporting to PDF: {ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Prints the roles data with dynamic column fitting
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolesTable == null || rolesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No roles data to print.",
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

                try
                {
                    // Create a PrintableComponentLink to print the grid
                    PrintableComponentLink printLink = 
                        new PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                    
                    printLink.Component = gridRoles;
                    
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
                            "Role List",
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

                    // Show print preview dialog
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore column visibility
                    if (editColumn != null) editColumn.Visible = editVisible;
                    if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing roles data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAddRoles_Click(object sender, EventArgs e)
        {
            // Check ADD_ROLES permission
            if (!PermissionManager.HasPermission(PermissionManager.Permissions.ADD_ROLES))
            {
                XtraMessageBox.Show(
                    "You do not have permission to add roles.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            Main.Instance.LoadUserControl(new UC_Roles_Registration());
        }
    }
}
