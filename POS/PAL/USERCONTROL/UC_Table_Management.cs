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
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Table_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Table _bllTable = new BLL_Table();
        private DataTable tablesTable;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        public UC_Table_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadTables();
            
            // Hide old controls if they still exist in the designer file (best effort)
            HideOldControls();

            // Wire up search events
            if (searchControl1 != null)
                searchControl1.KeyDown += searchControl1_KeyDown;
        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            try
            {
                string keyword = searchControl1.Text.Trim();
                tablesTable = _bllTable.SearchTables(keyword);
                gridControlTables.DataSource = tablesTable;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideOldControls()
        {
            if (Controls.ContainsKey("pnlInput")) Controls["pnlInput"].Visible = false;
        }

        /// <summary>
        /// Initializes the repository items for button columns
        /// </summary>
        private void InitializeRepositoryItems()
        {
            repositoryItemButtonEdit_Edit = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit_Delete = new RepositoryItemButtonEdit();
            
            if (gridControlTables != null)
            {
                gridControlTables.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
                gridControlTables.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
            }
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewTables == null) return;

            // Clear existing columns
            gridViewTables.Columns.Clear();

            // Add columns matching the database schema
            var colTableId = gridViewTables.Columns.AddVisible("table_id", "Table ID");
            colTableId.FieldName = "table_id";
            colTableId.Width = 80;
            colTableId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTableId.OptionsColumn.AllowEdit = false;
            colTableId.OptionsColumn.AllowFocus = false;
            colTableId.OptionsColumn.FixedWidth = true;

            var colTableNumber = gridViewTables.Columns.AddVisible("table_number", "Table Number");
            colTableNumber.FieldName = "table_number";
            colTableNumber.Width = 200;
            colTableNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTableNumber.OptionsColumn.AllowEdit = false;
            colTableNumber.OptionsColumn.AllowFocus = false;
            colTableNumber.OptionsColumn.FixedWidth = true;

            var colCapacity = gridViewTables.Columns.AddVisible("capacity", "Capacity");
            colCapacity.FieldName = "capacity";
            colCapacity.Width = 120;
            colCapacity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCapacity.OptionsColumn.AllowEdit = false;
            colCapacity.OptionsColumn.AllowFocus = false;
            colCapacity.OptionsColumn.FixedWidth = true;

            // Add Edit button column
            var colEdit = gridViewTables.Columns.AddVisible("Edit", "Edit");
            colEdit.Caption = "Edit";
            colEdit.Width = 80;
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEdit.OptionsColumn.AllowEdit = true;
            colEdit.OptionsColumn.AllowFocus = true;
            colEdit.OptionsColumn.FixedWidth = true;
            colEdit.OptionsColumn.ShowCaption = true;

            // Add Delete button column
            var colDelete = gridViewTables.Columns.AddVisible("Delete", "Delete");
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
            gridViewTables.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewTables.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewTables.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewTables.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewTables.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewTables.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewTables.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewTables.ColumnPanelRowHeight = 44;
            gridViewTables.RowHeight = 44;

            // Grid view options
            gridViewTables.OptionsView.ShowGroupPanel = false;
            gridViewTables.OptionsView.ShowIndicator = false;
            gridViewTables.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewTables.OptionsView.ShowAutoFilterRow = false;
            
            gridViewTables.OptionsBehavior.Editable = true;
            gridViewTables.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewTables.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewTables.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewTables.OptionsSelection.MultiSelect = false;
            
            gridViewTables.OptionsCustomization.AllowFilter = false;
            gridViewTables.OptionsCustomization.AllowGroup = false;

            // Set default sort order by table_id ascending
            colTableId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridViewTables.DoubleClick += GridViewTables_DoubleClick;

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
            EditSelectedTable();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedTable();
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
            refreshMenuItem.Click += (s, e) => LoadTables();
            contextMenu.Items.Add(refreshMenuItem);

            gridControlTables.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all tables from database
        /// </summary>
        public void LoadTables()
        {
            try
            {
                tablesTable = _bllTable.GetTables();
                gridControlTables.DataSource = tablesTable;
                gridViewTables.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading tables: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle double-click to edit
        /// </summary>
        private void GridViewTables_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedTable();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedTable();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTable();
        }

        /// <summary>
        /// Edits the selected table
        /// </summary>
        private void EditSelectedTable()
        {
            try
            {
                if (gridViewTables.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a table to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridViewTables.GetDataRow(gridViewTables.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int tableId = Convert.ToInt32(selectedRow["table_id"]);

                // Navigate to registration form in edit mode
                var registrationForm = new UC_Table_Registration(tableId);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing table: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected table
        /// </summary>
        private void DeleteSelectedTable()
        {
            try
            {
                if (gridViewTables.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a table to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridViewTables.GetDataRow(gridViewTables.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int tableId = Convert.ToInt32(selectedRow["table_id"]);
                string tableNumber = selectedRow["table_number"]?.ToString();

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete table '{tableNumber}'?\n\nThis will mark the table as inactive.",
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

                // Delete the table (soft delete)
                bool success = _bllTable.DeleteTable(tableId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"Table '{tableNumber}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadTables();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete table.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting table: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Table_Registration());
        }

        // Keep these for compatibility if they were linked in designer, but they are now handled by HideOldControls or new logic
        private void btnUpdate_Click(object sender, EventArgs e) { }
        private void btnDelete_Click(object sender, EventArgs e) { }
        private void btnReset_Click(object sender, EventArgs e) { }
        private void gridViewTables_RowClick(object sender, RowClickEventArgs e) { }
    }
}
