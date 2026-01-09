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
using DevExpress.XtraGrid.Columns;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_User_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable usersTable;

        public UC_User_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadUsers();

            // Wire up the Add button click event
            if (btnAddUsers != null)
            {
                btnAddUsers.Click += BtnAddUsers_Click;
            }

            // Wire up search textbox events
            txtSearch.KeyDown += TxtSearch_KeyDown;

            // Wire up search button click event
            if (btnSearch != null)
            {
                btnSearch.Click += BtnSearch_Click;
            }
            
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
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            // Clear existing columns
            gridView1.Columns.Clear();

            // Add columns matching the database schema
            var colUserId = gridView1.Columns.AddVisible("user_id", "User ID");
            colUserId.FieldName = "user_id";
            colUserId.Caption = "User ID";
            colUserId.Width = 80;
            colUserId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colUserId.OptionsColumn.AllowEdit = false;
            colUserId.OptionsColumn.AllowFocus = false;
            colUserId.OptionsColumn.FixedWidth = true;

            var colFullName = gridView1.Columns.AddVisible("full_name", "Full Name");
            colFullName.FieldName = "full_name";
            colFullName.Caption = "Full Name";
            colFullName.Width = 200;
            colFullName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFullName.OptionsColumn.AllowEdit = false;
            colFullName.OptionsColumn.AllowFocus = false;
            colFullName.OptionsColumn.FixedWidth = true;

            var colUsername = gridView1.Columns.AddVisible("username", "Username");
            colUsername.FieldName = "username";
            colUsername.Caption = "Username";
            colUsername.Width = 150;
            colUsername.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colUsername.OptionsColumn.AllowEdit = false;
            colUsername.OptionsColumn.AllowFocus = false;
            colUsername.OptionsColumn.FixedWidth = true;

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

            var colStoreName = gridView1.Columns.AddVisible("store_name", "Store");
            colStoreName.FieldName = "store_name";
            colStoreName.Caption = "Store";
            colStoreName.Width = 150;
            colStoreName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStoreName.OptionsColumn.AllowEdit = false;
            colStoreName.OptionsColumn.AllowFocus = false;
            colStoreName.OptionsColumn.FixedWidth = true;

            var colRoleName = gridView1.Columns.AddVisible("role_name", "Role");
            colRoleName.FieldName = "role_name";
            colRoleName.Caption = "Role";
            colRoleName.Width = 120;
            colRoleName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colRoleName.OptionsColumn.AllowEdit = false;
            colRoleName.OptionsColumn.AllowFocus = false;
            colRoleName.OptionsColumn.FixedWidth = true;

            var colStatus = gridView1.Columns.AddVisible("status", "Status");
            colStatus.FieldName = "status";
            colStatus.Caption = "Status";
            colStatus.Width = 80;
            colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.OptionsColumn.AllowFocus = false;
            colStatus.OptionsColumn.FixedWidth = true;

            // Add Edit button column (using repository item from Designer)
            var colEdit = gridView1.Columns.AddVisible("Edit", "Edit");
            colEdit.Caption = "Edit";
            colEdit.Width = 80;
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEdit.OptionsColumn.AllowEdit = true;
            colEdit.OptionsColumn.AllowFocus = true;
            colEdit.OptionsColumn.FixedWidth = true;
            colEdit.OptionsColumn.ShowCaption = true;

            // Add Delete button column (using repository item from Designer)
            var colDelete = gridView1.Columns.AddVisible("Delete", "Delete");
            colDelete.Caption = "Delete";
            colDelete.Width = 80;
            colDelete.ColumnEdit = repositoryItemButtonEdit_Delete;
            colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDelete.OptionsColumn.AllowEdit = true;
            colDelete.OptionsColumn.AllowFocus = true;
            colDelete.OptionsColumn.FixedWidth = true;
            colDelete.OptionsColumn.ShowCaption = true;

            // Configure button editors (now uses Designer-created repository items)
            ConfigureButtonEditors();

            // Note: Grid appearance styling is now set in Designer.cs for design-time visibility
            // Runtime behavior remains identical due to ConfigureGrid being called in constructor

            // Set default sort order by user_id ascending
            colUserId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridView1.DoubleClick += GridView1_DoubleClick;

            // Add context menu for Edit and Delete
            CreateContextMenu();
            
            // Handle custom row cell style to disable buttons for super admin users
            gridView1.CustomRowCellEdit += GridView1_CustomRowCellEdit;
        }

        /// <summary>
        /// Customizes cell editors for specific rows (disables Edit/Delete for super admin users unless current user is the super admin)
        /// </summary>
        private void GridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Edit" || e.Column.FieldName == "Delete")
            {
                DataRow row = gridView1.GetDataRow(e.RowHandle);
                if (row != null)
                {
                    bool isRowSuperAdmin = IsUserSuperAdmin(row);
                    int rowUserId = Convert.ToInt32(row["user_id"]);
                    int currentUserId = GetCurrentUserId();
                    bool isCurrentUserSuperAdmin = PermissionManager.IsSuperAdmin();
                    
                    // For super admin users:
                    // - Delete button is always disabled (super admin cannot be deleted)
                    // - Edit button is only enabled if the current user is the super admin editing themselves
                    if (isRowSuperAdmin)
                    {
                        if (e.Column.FieldName == "Delete")
                        {
                            // Super admin cannot be deleted - always disable
                            e.RepositoryItem = CreateDisabledButtonEditor();
                        }
                        else if (e.Column.FieldName == "Edit")
                        {
                            // Only the super admin can edit themselves
                            if (!isCurrentUserSuperAdmin || rowUserId != currentUserId)
                            {
                                e.RepositoryItem = CreateDisabledButtonEditor();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a disabled button editor for grid cells
        /// </summary>
        private RepositoryItemButtonEdit CreateDisabledButtonEditor()
        {
            RepositoryItemButtonEdit disabledButtonEdit = new RepositoryItemButtonEdit();
            disabledButtonEdit.Buttons.Clear();
            var button = new EditorButton(ButtonPredefines.Glyph);
            button.Enabled = false;
            disabledButtonEdit.Buttons.Add(button);
            disabledButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            disabledButtonEdit.ReadOnly = true;
            return disabledButtonEdit;
        }

        /// <summary>
        /// Checks if a user row represents a super admin
        /// </summary>
        private bool IsUserSuperAdmin(DataRow row)
        {
            if (row == null || row["is_super_admin"] == DBNull.Value)
                return false;

            var value = row["is_super_admin"];
            if (value is bool boolValue)
                return boolValue;
            if (value is string strValue)
                return strValue == "True" || strValue == "1";
            if (value is int intValue)
                return intValue == 1;
            
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Gets the current logged-in user ID
        /// </summary>
        private int GetCurrentUserId()
        {
            if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
            {
                return Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
            }
            return 0;
        }

        /// <summary>
        /// Configures the button editors for Edit and Delete columns
        /// </summary>
        private void ConfigureButtonEditors()
        {
            // Configure Edit button (using Designer-created repository item)
            repositoryItemButtonEdit_Edit.Buttons.Clear();
            var editButton = new EditorButton(ButtonPredefines.Glyph);
            editButton.Caption = "Edit";
            editButton.Kind = ButtonPredefines.Glyph;
            repositoryItemButtonEdit_Edit.Buttons.Add(editButton);
            repositoryItemButtonEdit_Edit.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Edit.ButtonClick += RepositoryItemButtonEdit_Edit_ButtonClick;

            // Configure Delete button (using Designer-created repository item)
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
            EditSelectedUser();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedUser();
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
            refreshMenuItem.Click += (s, e) => LoadUsers();
            contextMenu.Items.Add(refreshMenuItem);

            gridUsers.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all users from database
        /// </summary>
        public void LoadUsers()
        {
            try
            {
                usersTable = BLL_User.GetUsers();
                gridUsers.DataSource = usersTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading users: {ex.Message}",
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
            EditSelectedUser();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedUser();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedUser();
        }

        /// <summary>
        /// Edits the selected user
        /// </summary>
        private void EditSelectedUser()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a user to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int userId = Convert.ToInt32(selectedRow["user_id"]);
                
                // Check if the selected user is a super admin
                if (IsUserSuperAdmin(selectedRow))
                {
                    int currentUserId = GetCurrentUserId();
                    bool isCurrentUserSuperAdmin = PermissionManager.IsSuperAdmin();
                    
                    // Only the super admin can edit themselves
                    if (!isCurrentUserSuperAdmin || userId != currentUserId)
                    {
                        XtraMessageBox.Show(
                            "The Super Admin user can only be edited by themselves.",
                            "Operation Not Allowed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                // Navigate to user edit form
                var registrationForm = new UC_User_RegistrationSub(userId);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing user: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected user
        /// </summary>
        private void DeleteSelectedUser()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a user to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int userId = Convert.ToInt32(selectedRow["user_id"]);
                string fullName = selectedRow["full_name"]?.ToString();
                
                // Prevent deleting super admin user
                if (IsUserSuperAdmin(selectedRow))
                {
                    XtraMessageBox.Show(
                        "The Super Admin user cannot be deleted as it is a system user.",
                        "Operation Not Allowed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the user '{fullName}'?\n\nThis will mark the user as inactive.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Get current user ID
                int currentUserId = GetCurrentUserId();
                if (currentUserId == 0)
                    currentUserId = 1; // Default fallback

                // Delete the user (soft delete)
                bool success = BLL_User.DeleteUser(userId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"User '{fullName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadUsers();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete user.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting user: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Add Users button click
        /// </summary>
        private void BtnAddUsers_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_User_RegistrationSub());
        }

        /// <summary>
        /// Searches users based on the search text
        /// </summary>
        private void SearchUsers()
        {
            try
            {
                string searchKeyword = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(searchKeyword))
                {
                    // If search is empty, load all users
                    LoadUsers();
                    return;
                }

                usersTable = BLL_User.SearchUsers(searchKeyword);
                gridUsers.DataSource = usersTable;
                gridView1.BestFitColumns();

                // Show message if no results found
                if (usersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        $"No users found matching '{searchKeyword}'.",
                        "Search Results",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error searching users: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Enter key press in search textbox
        /// </summary>
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchUsers();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Handle search button click
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchUsers();
        }
        
        /// <summary>
        /// Exports users data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (usersTable == null || usersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No users data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Users_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
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
                            $"Users data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports users data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (usersTable == null || usersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No users data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Users_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
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
                            $"Users data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports users data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (usersTable == null || usersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No users data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Users_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
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
                        PrintableComponentLink printLink = new PrintableComponentLink(new PrintingSystem());
                        printLink.Component = gridUsers;
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
                            $"Users data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the users data with dynamic column fitting
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (usersTable == null || usersTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No users data to print.",
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
                        new PrintableComponentLink(new PrintingSystem());
                    
                    printLink.Component = gridUsers;
                    
                    // Configure print settings for dynamic column fitting (portrait orientation)
                    printLink.Landscape = false;
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
                            "User List",
                            "",
                            $"Printed: {DateTime.Now:dd/MM/yyyy HH:mm}"
                        });
                        header.Header.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                        header.Header.LineAlignment = BrickAlignment.Center;
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
                        header.Footer.LineAlignment = BrickAlignment.Center;
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
                    $"Error printing users data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
