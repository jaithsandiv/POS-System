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
using DevExpress.XtraPrinting;
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_CustomerGroup_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private DataTable customerGroupSalesTable;

        public UC_CustomerGroup_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadCustomerGroupSales();

            // Initialize search control
            InitializeSearchControl();

            // Wire up export button events
            InitializeExportButtons();
        }

        /// <summary>
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            if (txtSearch != null)
            {
                txtSearch.Properties.NullValuePrompt = "Search customer group sales...";
                txtSearch.Properties.ShowNullValuePromptWhenFocused = true;
                txtSearch.KeyPress += TxtSearch_KeyPress;
            }
            
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch(txtSearch.Text);
        }

        /// <summary>
        /// Performs search filtering on the customer group sales grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadCustomerGroupSales();
                    return;
                }

                // Use BLL search method
                customerGroupSalesTable = _bllContacts.SearchCustomerGroupSalesReport(searchText);
                gridCustoemrGroupReport.DataSource = customerGroupSalesTable;
                gridViewCustomerGroupReport.RefreshData();
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
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewCustomerGroupReport == null) return;

            // Configure Customer Group column
            colCustomerGroup.FieldName = "customer_group";
            colCustomerGroup.Caption = "Customer Group";
            colCustomerGroup.Width = 300;
            colCustomerGroup.OptionsColumn.AllowEdit = false;
            colCustomerGroup.OptionsColumn.AllowFocus = false;
            colCustomerGroup.Visible = true;

            // Configure Total Sales column
            colTotalSales.FieldName = "total_sales";
            colTotalSales.Caption = "Total Sales";
            colTotalSales.Width = 150;
            colTotalSales.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalSales.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalSales.DisplayFormat.FormatString = "n2";
            colTotalSales.OptionsColumn.AllowEdit = false;
            colTotalSales.OptionsColumn.AllowFocus = false;
            colTotalSales.OptionsColumn.FixedWidth = true;
            colTotalSales.Visible = true;

            // Apply grid appearance styling
            gridViewCustomerGroupReport.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewCustomerGroupReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewCustomerGroupReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewCustomerGroupReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewCustomerGroupReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewCustomerGroupReport.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewCustomerGroupReport.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewCustomerGroupReport.ColumnPanelRowHeight = 44;
            gridViewCustomerGroupReport.RowHeight = 44;

            // Grid view options
            gridViewCustomerGroupReport.OptionsView.ShowGroupPanel = false;
            gridViewCustomerGroupReport.OptionsView.ShowIndicator = false;
            gridViewCustomerGroupReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewCustomerGroupReport.OptionsView.ShowAutoFilterRow = false;
            
            gridViewCustomerGroupReport.OptionsBehavior.Editable = false;
            gridViewCustomerGroupReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewCustomerGroupReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewCustomerGroupReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewCustomerGroupReport.OptionsSelection.MultiSelect = false;
            
            gridViewCustomerGroupReport.OptionsCustomization.AllowFilter = false;
            gridViewCustomerGroupReport.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all customer group sales from database
        /// </summary>
        public void LoadCustomerGroupSales()
        {
            try
            {
                customerGroupSalesTable = _bllContacts.GetCustomerGroupSalesReport();
                gridCustoemrGroupReport.DataSource = customerGroupSalesTable;
                gridViewCustomerGroupReport.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading customer group sales: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports customer group sales data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupSalesTable == null || customerGroupSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group sales data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"CustomerGroupSales_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewCustomerGroupReport.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Customer group sales data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports customer group sales data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupSalesTable == null || customerGroupSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group sales data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"CustomerGroupSales_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewCustomerGroupReport.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Customer group sales data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports customer group sales data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupSalesTable == null || customerGroupSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group sales data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"CustomerGroupSales_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewCustomerGroupReport.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Customer group sales data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the customer group sales data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerGroupSalesTable == null || customerGroupSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No customer group sales data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Store original column widths
                var originalWidths = new Dictionary<string, int>
                {
                    { "customer_group", colCustomerGroup.Width },
                    { "total_sales", colTotalSales.Width }
                };

                try
                {
                    // Adjust column widths for printing
                    // Wider for text columns
                    colCustomerGroup.Width = 400;     // Wider for customer group names
                    
                    // Narrower for number columns
                    colTotalSales.Width = 120;        // Narrower for total sales

                    // Create print link using ReportHelper
                    PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                        gridCustoemrGroupReport, 
                        "Customer Group Sales Report", 
                        landscape: true);

                    // Show print preview dialog with print options
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore original column widths
                    colCustomerGroup.Width = originalWidths["customer_group"];
                    colTotalSales.Width = originalWidths["total_sales"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing customer group sales data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
