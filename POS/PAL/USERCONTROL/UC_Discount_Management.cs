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
    public partial class UC_Discount_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Discount _bllDiscount = new BLL_Discount();
        private DataTable discountsTable;

        public UC_Discount_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadDiscounts();
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewDiscounts == null) return;

            // Clear existing columns
            gridViewDiscounts.Columns.Clear();

            // Add columns matching the database schema
            var colId = gridViewDiscounts.Columns.AddVisible("discount_id", "ID");
            colId.FieldName = "discount_id";
            colId.Width = 80;
            colId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colId.OptionsColumn.AllowEdit = false;
            colId.OptionsColumn.AllowFocus = false;
            colId.OptionsColumn.FixedWidth = true;

            var colName = gridViewDiscounts.Columns.AddVisible("name", "Name");
            colName.FieldName = "name";
            colName.Width = 200;
            colName.OptionsColumn.AllowEdit = false;
            colName.OptionsColumn.AllowFocus = false;

            var colStartDate = gridViewDiscounts.Columns.AddVisible("start_date", "Start Date");
            colStartDate.FieldName = "start_date";
            colStartDate.Width = 120;
            colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStartDate.OptionsColumn.AllowEdit = false;
            colStartDate.OptionsColumn.AllowFocus = false;

            var colEndDate = gridViewDiscounts.Columns.AddVisible("end_date", "End Date");
            colEndDate.FieldName = "end_date";
            colEndDate.Width = 120;
            colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEndDate.OptionsColumn.AllowEdit = false;
            colEndDate.OptionsColumn.AllowFocus = false;

            var colStatus = gridViewDiscounts.Columns.AddVisible("status", "Status");
            colStatus.FieldName = "status";
            colStatus.Width = 100;
            colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.OptionsColumn.AllowFocus = false;

            // Apply grid appearance styling
            gridViewDiscounts.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewDiscounts.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewDiscounts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewDiscounts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewDiscounts.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewDiscounts.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewDiscounts.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewDiscounts.ColumnPanelRowHeight = 44;
            gridViewDiscounts.RowHeight = 44;

            // Grid view options
            gridViewDiscounts.OptionsView.ShowGroupPanel = false;
            gridViewDiscounts.OptionsView.ShowIndicator = false;
            gridViewDiscounts.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewDiscounts.OptionsView.ShowAutoFilterRow = false;
            
            gridViewDiscounts.OptionsBehavior.Editable = true;
            gridViewDiscounts.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDiscounts.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridViewDiscounts.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewDiscounts.OptionsSelection.MultiSelect = false;
            
            gridViewDiscounts.OptionsCustomization.AllowFilter = false;
            gridViewDiscounts.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all discounts from database
        /// </summary>
        public void LoadDiscounts()
        {
            try
            {
                discountsTable = _bllDiscount.GetDiscounts();
                gridControlDiscounts.DataSource = discountsTable;
                gridViewDiscounts.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading discounts: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Discount_Registration());
        }

        private void btnAssignProducts_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewDiscounts.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Please select a discount to assign products.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowHandle = selectedRows[0];
            int discountId = Convert.ToInt32(gridViewDiscounts.GetRowCellValue(rowHandle, "discount_id"));
            string discountName = gridViewDiscounts.GetRowCellValue(rowHandle, "name").ToString();

            Main.Instance.LoadUserControl(new UC_Promotion_Products(discountId, discountName));
        }
    }
}
