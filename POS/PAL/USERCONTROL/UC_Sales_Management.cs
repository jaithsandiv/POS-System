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
using DevExpress.XtraPrinting;
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Sales_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable salesTable;

        public UC_Sales_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadSales();
            
            // Hide old controls if they still exist in the designer file (best effort)
            HideOldControls();

            // Wire up search events
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
            if (txtSearch != null)
                txtSearch.KeyDown += txtSearch_KeyDown;

            // Wire up export button events
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
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
                string keyword = txtSearch.Text.Trim();
                salesTable = _bllSalesTerminal.SearchSales("SALE", keyword);
                gridControlSales.DataSource = salesTable;
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
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewSales == null) return;

            // Clear existing columns
            gridViewSales.Columns.Clear();

            // Add columns matching the database schema
            var colSaleId = gridViewSales.Columns.AddVisible("sale_id", "Sale ID");
            colSaleId.FieldName = "sale_id";
            colSaleId.Width = 80;
            colSaleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleId.OptionsColumn.AllowEdit = false;
            colSaleId.OptionsColumn.AllowFocus = false;
            colSaleId.OptionsColumn.FixedWidth = true;

            var colInvoiceNumber = gridViewSales.Columns.AddVisible("invoice_number", "Invoice No");
            colInvoiceNumber.FieldName = "invoice_number";
            colInvoiceNumber.Width = 150;
            colInvoiceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNumber.OptionsColumn.AllowEdit = false;
            colInvoiceNumber.OptionsColumn.AllowFocus = false;
            colInvoiceNumber.OptionsColumn.FixedWidth = true;

            var colCustomer = gridViewSales.Columns.AddVisible("customer_name", "Customer");
            colCustomer.FieldName = "customer_name";
            colCustomer.Width = 200;
            colCustomer.OptionsColumn.AllowEdit = false;
            colCustomer.OptionsColumn.AllowFocus = false;

            var colDate = gridViewSales.Columns.AddVisible("sale_date", "Date");
            colDate.FieldName = "sale_date";
            colDate.Width = 150;
            colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            colDate.OptionsColumn.FixedWidth = true;

            var colTotal = gridViewSales.Columns.AddVisible("grand_total", "Total");
            colTotal.FieldName = "grand_total";
            colTotal.Width = 120;
            colTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotal.DisplayFormat.FormatString = "n2";
            colTotal.OptionsColumn.AllowEdit = false;
            colTotal.OptionsColumn.AllowFocus = false;
            colTotal.OptionsColumn.FixedWidth = true;

            var colPaymentStatus = gridViewSales.Columns.AddVisible("payment_status", "Payment Status");
            colPaymentStatus.FieldName = "payment_status";
            colPaymentStatus.Width = 120;
            colPaymentStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaymentStatus.OptionsColumn.AllowEdit = false;
            colPaymentStatus.OptionsColumn.AllowFocus = false;
            colPaymentStatus.OptionsColumn.FixedWidth = true;

            var colBiller = gridViewSales.Columns.AddVisible("biller_name", "Biller");
            colBiller.FieldName = "biller_name";
            colBiller.Width = 150;
            colBiller.OptionsColumn.AllowEdit = false;
            colBiller.OptionsColumn.AllowFocus = false;

            // Apply grid appearance styling
            gridViewSales.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewSales.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSales.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSales.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSales.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewSales.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewSales.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewSales.ColumnPanelRowHeight = 44;
            gridViewSales.RowHeight = 44;

            // Grid view options
            gridViewSales.OptionsView.ShowGroupPanel = false;
            gridViewSales.OptionsView.ShowIndicator = false;
            gridViewSales.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSales.OptionsView.ShowAutoFilterRow = false;
            
            gridViewSales.OptionsBehavior.Editable = true;
            gridViewSales.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSales.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSales.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSales.OptionsSelection.MultiSelect = false;
            
            gridViewSales.OptionsCustomization.AllowFilter = false;
            gridViewSales.OptionsCustomization.AllowGroup = false;

            // Set default sort order by sale_id descending
            colSaleId.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        }

        /// <summary>
        /// Loads all sales from database
        /// </summary>
        public void LoadSales()
        {
            try
            {
                salesTable = _bllSalesTerminal.GetSales("SALE");
                gridControlSales.DataSource = salesTable;
                gridViewSales.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading sales: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports sales data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesTable == null || salesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Sales_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewSales.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sales data exported successfully to:\n{saveFileDialog.FileName}",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
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
        /// Exports sales data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesTable == null || salesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Sales_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewSales.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sales data exported successfully to:\n{saveFileDialog.FileName}",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
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
        /// Exports sales data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesTable == null || salesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Sales_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewSales.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sales data exported successfully to:\n{saveFileDialog.FileName}",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
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
        /// Prints the sales data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesTable == null || salesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create print link using ReportHelper
                PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                    gridControlSales, 
                    "Sales List", 
                    landscape: false);

                // Show print preview dialog with print options
                printLink.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing sales data: {ex.Message}",
                    "Print Error",
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
