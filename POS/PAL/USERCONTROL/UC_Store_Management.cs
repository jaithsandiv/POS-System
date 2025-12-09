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
    public partial class UC_Store_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Store _bllStore = new BLL_Store();
        private DataTable storesTable;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        public UC_Store_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadStores();
            
            // Hide the old input controls if they exist and are visible
            // This is a best-effort attempt since we can't modify the designer file directly to remove them
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
                storesTable = _bllStore.SearchStores(keyword);
                gridControlStores.DataSource = storesTable;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideOldControls()
        {
            // Assuming these controls exist from the previous version
            if (Controls.ContainsKey("txtStoreName")) Controls["txtStoreName"].Visible = false;
            if (Controls.ContainsKey("txtPhone")) Controls["txtPhone"].Visible = false;
            if (Controls.ContainsKey("txtEmail")) Controls["txtEmail"].Visible = false;
            if (Controls.ContainsKey("txtAddress")) Controls["txtAddress"].Visible = false;
            if (Controls.ContainsKey("txtCity")) Controls["txtCity"].Visible = false;
            if (Controls.ContainsKey("txtState")) Controls["txtState"].Visible = false;
            if (Controls.ContainsKey("txtCountry")) Controls["txtCountry"].Visible = false;
            if (Controls.ContainsKey("txtPostalCode")) Controls["txtPostalCode"].Visible = false;
            
            // Re-purpose btnAdd if possible, or hide others
            if (Controls.ContainsKey("btnUpdate")) Controls["btnUpdate"].Visible = false;
            if (Controls.ContainsKey("btnDelete")) Controls["btnDelete"].Visible = false;
            if (Controls.ContainsKey("btnReset")) Controls["btnReset"].Visible = false;
            
            // Ensure btnAdd is visible and set text
            if (Controls.ContainsKey("btnAdd"))
            {
                var btnAdd = Controls["btnAdd"] as SimpleButton;
                if (btnAdd != null)
                {
                    btnAdd.Text = "Add Store";
                    btnAdd.Visible = true;
                    // Remove old events and add new one
                    btnAdd.Click -= btnAdd_Click; 
                    btnAdd.Click += (s, e) => Main.Instance.LoadUserControl(new UC_Store_Registration());
                }
            }
        }

        /// <summary>
        /// Initializes the repository items for button columns
        /// </summary>
        private void InitializeRepositoryItems()
        {
            repositoryItemButtonEdit_Edit = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit_Delete = new RepositoryItemButtonEdit();
            
            // Check if gridControlStores is initialized
            if (gridControlStores != null)
            {
                gridControlStores.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
                gridControlStores.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
            }
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewStores == null) return;

            // Clear existing columns
            gridViewStores.Columns.Clear();

            // Add columns matching the database schema
            var colStoreId = gridViewStores.Columns.AddVisible("store_id", "Store ID");
            colStoreId.FieldName = "store_id";
            colStoreId.Width = 80;
            colStoreId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStoreId.OptionsColumn.AllowEdit = false;
            colStoreId.OptionsColumn.AllowFocus = false;
            colStoreId.OptionsColumn.FixedWidth = true;

            var colStoreName = gridViewStores.Columns.AddVisible("store_name", "Store Name");
            colStoreName.FieldName = "store_name";
            colStoreName.Width = 200;
            colStoreName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStoreName.OptionsColumn.AllowEdit = false;
            colStoreName.OptionsColumn.AllowFocus = false;
            colStoreName.OptionsColumn.FixedWidth = true;

            var colPhone = gridViewStores.Columns.AddVisible("phone", "Phone");
            colPhone.FieldName = "phone";
            colPhone.Width = 120;
            colPhone.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhone.OptionsColumn.AllowEdit = false;
            colPhone.OptionsColumn.AllowFocus = false;
            colPhone.OptionsColumn.FixedWidth = true;

            var colEmail = gridViewStores.Columns.AddVisible("email", "Email");
            colEmail.FieldName = "email";
            colEmail.Width = 180;
            colEmail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEmail.OptionsColumn.AllowEdit = false;
            colEmail.OptionsColumn.AllowFocus = false;
            colEmail.OptionsColumn.FixedWidth = true;

            var colAddress = gridViewStores.Columns.AddVisible("address", "Address");
            colAddress.FieldName = "address";
            colAddress.Width = 200;
            colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colAddress.OptionsColumn.AllowEdit = false;
            colAddress.OptionsColumn.AllowFocus = false;
            colAddress.OptionsColumn.FixedWidth = true;

            var colCity = gridViewStores.Columns.AddVisible("city", "City");
            colCity.FieldName = "city";
            colCity.Width = 100;
            colCity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCity.OptionsColumn.AllowEdit = false;
            colCity.OptionsColumn.AllowFocus = false;
            colCity.OptionsColumn.FixedWidth = true;

            var colState = gridViewStores.Columns.AddVisible("state", "State");
            colState.FieldName = "state";
            colState.Width = 100;
            colState.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colState.OptionsColumn.AllowEdit = false;
            colState.OptionsColumn.AllowFocus = false;
            colState.OptionsColumn.FixedWidth = true;

            var colCountry = gridViewStores.Columns.AddVisible("country", "Country");
            colCountry.FieldName = "country";
            colCountry.Width = 100;
            colCountry.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCountry.OptionsColumn.AllowEdit = false;
            colCountry.OptionsColumn.AllowFocus = false;
            colCountry.OptionsColumn.FixedWidth = true;

            // Add Edit button column
            var colEdit = gridViewStores.Columns.AddVisible("Edit", "Edit");
            colEdit.Caption = "Edit";
            colEdit.Width = 80;
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEdit.OptionsColumn.AllowEdit = true;
            colEdit.OptionsColumn.AllowFocus = true;
            colEdit.OptionsColumn.FixedWidth = true;
            colEdit.OptionsColumn.ShowCaption = true;

            // Add Delete button column
            var colDelete = gridViewStores.Columns.AddVisible("Delete", "Delete");
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
            gridViewStores.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewStores.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewStores.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewStores.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewStores.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewStores.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewStores.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewStores.ColumnPanelRowHeight = 44;
            gridViewStores.RowHeight = 44;

            // Grid view options
            gridViewStores.OptionsView.ShowGroupPanel = false;
            gridViewStores.OptionsView.ShowIndicator = false;
            gridViewStores.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewStores.OptionsView.ShowAutoFilterRow = false;
            
            gridViewStores.OptionsBehavior.Editable = true;
            gridViewStores.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewStores.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewStores.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewStores.OptionsSelection.MultiSelect = false;
            
            gridViewStores.OptionsCustomization.AllowFilter = false;
            gridViewStores.OptionsCustomization.AllowGroup = false;

            // Set default sort order by store_id ascending
            colStoreId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridViewStores.DoubleClick += GridViewStores_DoubleClick;

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
            EditSelectedStore();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedStore();
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
            refreshMenuItem.Click += (s, e) => LoadStores();
            contextMenu.Items.Add(refreshMenuItem);

            gridControlStores.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all stores from database
        /// </summary>
        public void LoadStores()
        {
            try
            {
                storesTable = _bllStore.GetStores();
                gridControlStores.DataSource = storesTable;
                gridViewStores.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading stores: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle double-click to edit
        /// </summary>
        private void GridViewStores_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedStore();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedStore();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedStore();
        }

        /// <summary>
        /// Edits the selected store
        /// </summary>
        private void EditSelectedStore()
        {
            try
            {
                if (gridViewStores.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a store to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridViewStores.GetDataRow(gridViewStores.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int storeId = Convert.ToInt32(selectedRow["store_id"]);

                // Navigate to registration form in edit mode
                var registrationForm = new UC_Store_Registration(storeId);
                Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing store: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected store
        /// </summary>
        private void DeleteSelectedStore()
        {
            try
            {
                if (gridViewStores.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a store to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridViewStores.GetDataRow(gridViewStores.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int storeId = Convert.ToInt32(selectedRow["store_id"]);
                string storeName = selectedRow["store_name"]?.ToString();

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the store '{storeName}'?\n\nThis will mark the store as inactive.",
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

                // Delete the store (soft delete)
                bool success = _bllStore.DeleteStore(storeId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"Store '{storeName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadStores();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete store.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting store: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Keep these for compatibility if they were linked in designer, but they are now handled by HideOldControls or new logic
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Store_Registration());
        }

        private void btnUpdate_Click(object sender, EventArgs e) { }
        private void btnDelete_Click(object sender, EventArgs e) { }
        private void btnReset_Click(object sender, EventArgs e) { }
        private void gridViewStores_RowClick(object sender, RowClickEventArgs e) { }
    }
}

