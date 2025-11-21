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
    public partial class UC_CustomerGroup_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private DataTable customerGroupsTable;

        public UC_CustomerGroup_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadCustomerGroups();
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
            
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.OptionsSelection.MultiSelect = false;
            
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;

            // Enable double-click to edit
            gridView1.DoubleClick += GridView1_DoubleClick;

            // Add context menu for Edit and Delete
            CreateContextMenu();
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

            gridControl1.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all customer groups from database
        /// </summary>
        public void LoadCustomerGroups()
        {
            try
            {
                customerGroupsTable = _bllContacts.GetCustomerGroups();
                gridControl1.DataSource = customerGroupsTable;
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

        private void btnAddCustomerGroup_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_CustomerGroup_Registration());
        }
    }
}
