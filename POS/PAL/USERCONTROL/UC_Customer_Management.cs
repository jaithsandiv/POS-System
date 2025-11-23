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
        }

        /// <summary>
        /// Initializes the repository items for button columns
        /// </summary>
        private void InitializeRepositoryItems()
        {
            repositoryItemButtonEdit_Edit = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit_Delete = new RepositoryItemButtonEdit();
            gridControl1.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
            gridControl1.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
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
            colFullName.OptionsColumn.AllowEdit = false;
            colFullName.OptionsColumn.AllowFocus = false;
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

            gridControl1.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all customers from database
        /// </summary>
        public void LoadCustomers()
        {
            try
            {
                customersTable = _bllContacts.GetCustomers();
                gridControl1.DataSource = customersTable;
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

        private void btn25Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn50Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn100Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn200Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn500Filter_Click(object sender, EventArgs e)
        {

        }
        private void btn1000Filter_Click(object sender, EventArgs e)
        {

        }
        private void btnAllFilter_Click(object sender, EventArgs e)
        {

        }

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
