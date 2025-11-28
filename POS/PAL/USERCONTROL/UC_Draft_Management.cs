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
    public partial class UC_Draft_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable draftsTable;

        public UC_Draft_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadDrafts();
            
            // Hide old controls if they still exist in the designer file (best effort)
            HideOldControls();
        }

        private void HideOldControls()
        {
            if (Controls.ContainsKey("pnlInput")) Controls["pnlInput"].Visible = false;
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewDrafts == null) return;

            // Clear existing columns
            gridViewDrafts.Columns.Clear();

            // Add columns matching the database schema
            var colSaleId = gridViewDrafts.Columns.AddVisible("sale_id", "Draft ID");
            colSaleId.FieldName = "sale_id";
            colSaleId.Width = 80;
            colSaleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleId.OptionsColumn.AllowEdit = false;
            colSaleId.OptionsColumn.AllowFocus = false;
            colSaleId.OptionsColumn.FixedWidth = true;

            var colInvoiceNumber = gridViewDrafts.Columns.AddVisible("invoice_number", "Reference No");
            colInvoiceNumber.FieldName = "invoice_number";
            colInvoiceNumber.Width = 150;
            colInvoiceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNumber.OptionsColumn.AllowEdit = false;
            colInvoiceNumber.OptionsColumn.AllowFocus = false;
            colInvoiceNumber.OptionsColumn.FixedWidth = true;

            var colCustomer = gridViewDrafts.Columns.AddVisible("customer_name", "Customer");
            colCustomer.FieldName = "customer_name";
            colCustomer.Width = 200;
            colCustomer.OptionsColumn.AllowEdit = false;
            colCustomer.OptionsColumn.AllowFocus = false;

            var colDate = gridViewDrafts.Columns.AddVisible("sale_date", "Date");
            colDate.FieldName = "sale_date";
            colDate.Width = 150;
            colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            colDate.OptionsColumn.FixedWidth = true;

            var colTotal = gridViewDrafts.Columns.AddVisible("grand_total", "Total");
            colTotal.FieldName = "grand_total";
            colTotal.Width = 120;
            colTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotal.DisplayFormat.FormatString = "n2";
            colTotal.OptionsColumn.AllowEdit = false;
            colTotal.OptionsColumn.AllowFocus = false;
            colTotal.OptionsColumn.FixedWidth = true;

            var colPaymentStatus = gridViewDrafts.Columns.AddVisible("payment_status", "Payment Status");
            colPaymentStatus.FieldName = "payment_status";
            colPaymentStatus.Width = 120;
            colPaymentStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaymentStatus.OptionsColumn.AllowEdit = false;
            colPaymentStatus.OptionsColumn.AllowFocus = false;
            colPaymentStatus.OptionsColumn.FixedWidth = true;

            var colBiller = gridViewDrafts.Columns.AddVisible("biller_name", "Biller");
            colBiller.FieldName = "biller_name";
            colBiller.Width = 150;
            colBiller.OptionsColumn.AllowEdit = false;
            colBiller.OptionsColumn.AllowFocus = false;

            // Apply grid appearance styling
            gridViewDrafts.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewDrafts.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewDrafts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewDrafts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewDrafts.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewDrafts.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewDrafts.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewDrafts.ColumnPanelRowHeight = 44;
            gridViewDrafts.RowHeight = 44;

            // Grid view options
            gridViewDrafts.OptionsView.ShowGroupPanel = false;
            gridViewDrafts.OptionsView.ShowIndicator = false;
            gridViewDrafts.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewDrafts.OptionsView.ShowAutoFilterRow = false;
            
            gridViewDrafts.OptionsBehavior.Editable = true;
            gridViewDrafts.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDrafts.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewDrafts.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewDrafts.OptionsSelection.MultiSelect = false;
            
            gridViewDrafts.OptionsCustomization.AllowFilter = false;
            gridViewDrafts.OptionsCustomization.AllowGroup = false;

            // Set default sort order by sale_id descending
            colSaleId.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        }

        /// <summary>
        /// Loads all drafts from database
        /// </summary>
        public void LoadDrafts()
        {
            try
            {
                draftsTable = _bllSalesTerminal.GetSales("DRAFT");
                gridControlDrafts.DataSource = draftsTable;
                gridViewDrafts.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading drafts: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_SalesTerminal());
        }
    }
}
