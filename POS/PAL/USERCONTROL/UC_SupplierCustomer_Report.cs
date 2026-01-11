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
    public partial class UC_SupplierCustomer_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private DataTable supplierCustomerTable;

        public UC_SupplierCustomer_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadSupplierCustomerReport();
            InitializeSearchControl();
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
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            if (txtSearch != null)
            {
                txtSearch.Properties.NullValuePrompt = "Search supplier/customer report...";
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
        /// Performs search filtering on the supplier/customer report grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadSupplierCustomerReport();
                    return;
                }

                // Use BLL search method
                supplierCustomerTable = _bllContacts.SearchSupplierCustomerReport(searchText);
                gridSupplierCustomerReport.DataSource = supplierCustomerTable;
                gridViewSupplierCustomerReport.RefreshData();
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
            if (gridViewSupplierCustomerReport == null) return;

            // Configure Customer/Supplier Name column
            colCustomerName.FieldName = "customer_name";
            colCustomerName.Caption = "Customer/Supplier Name";
            colCustomerName.Width = 300;
            colCustomerName.OptionsColumn.AllowEdit = false;
            colCustomerName.OptionsColumn.AllowFocus = false;
            colCustomerName.Visible = true;

            // Configure Total Purchase column
            colTotalPurchase.FieldName = "total_purchase";
            colTotalPurchase.Caption = "Total Purchase";
            colTotalPurchase.Width = 150;
            colTotalPurchase.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalPurchase.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalPurchase.DisplayFormat.FormatString = "n2";
            colTotalPurchase.OptionsColumn.AllowEdit = false;
            colTotalPurchase.OptionsColumn.AllowFocus = false;
            colTotalPurchase.OptionsColumn.FixedWidth = true;
            colTotalPurchase.Visible = true;

            // Configure Total Purchase Return column
            colTotalPurchaseReturn.FieldName = "total_purchase_return";
            colTotalPurchaseReturn.Caption = "Total Purchase Return";
            colTotalPurchaseReturn.Width = 150;
            colTotalPurchaseReturn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalPurchaseReturn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalPurchaseReturn.DisplayFormat.FormatString = "n2";
            colTotalPurchaseReturn.OptionsColumn.AllowEdit = false;
            colTotalPurchaseReturn.OptionsColumn.AllowFocus = false;
            colTotalPurchaseReturn.OptionsColumn.FixedWidth = true;
            colTotalPurchaseReturn.Visible = true;

            // Configure Total Sale column
            colTotalSale.FieldName = "total_sale";
            colTotalSale.Caption = "Total Sale";
            colTotalSale.Width = 150;
            colTotalSale.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalSale.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalSale.DisplayFormat.FormatString = "n2";
            colTotalSale.OptionsColumn.AllowEdit = false;
            colTotalSale.OptionsColumn.AllowFocus = false;
            colTotalSale.OptionsColumn.FixedWidth = true;
            colTotalSale.Visible = true;

            // Configure Total Sell Return column
            colTotalSellReturn.FieldName = "total_sell_return";
            colTotalSellReturn.Caption = "Total Sell Return";
            colTotalSellReturn.Width = 150;
            colTotalSellReturn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalSellReturn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalSellReturn.DisplayFormat.FormatString = "n2";
            colTotalSellReturn.OptionsColumn.AllowEdit = false;
            colTotalSellReturn.OptionsColumn.AllowFocus = false;
            colTotalSellReturn.OptionsColumn.FixedWidth = true;
            colTotalSellReturn.Visible = true;

            // Configure Opening Balance column
            colOpeningBalance.FieldName = "opening_balance";
            colOpeningBalance.Caption = "Opening Balance";
            colOpeningBalance.Width = 150;
            colOpeningBalance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colOpeningBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colOpeningBalance.DisplayFormat.FormatString = "n2";
            colOpeningBalance.OptionsColumn.AllowEdit = false;
            colOpeningBalance.OptionsColumn.AllowFocus = false;
            colOpeningBalance.OptionsColumn.FixedWidth = true;
            colOpeningBalance.Visible = true;

            // Configure Due column
            colDue.FieldName = "due";
            colDue.Caption = "Due";
            colDue.Width = 150;
            colDue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colDue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colDue.DisplayFormat.FormatString = "n2";
            colDue.OptionsColumn.AllowEdit = false;
            colDue.OptionsColumn.AllowFocus = false;
            colDue.OptionsColumn.FixedWidth = true;
            colDue.Visible = true;

            // Apply grid appearance styling
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewSupplierCustomerReport.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewSupplierCustomerReport.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewSupplierCustomerReport.ColumnPanelRowHeight = 44;
            gridViewSupplierCustomerReport.RowHeight = 44;

            // Grid view options
            gridViewSupplierCustomerReport.OptionsView.ShowGroupPanel = false;
            gridViewSupplierCustomerReport.OptionsView.ShowIndicator = false;
            gridViewSupplierCustomerReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSupplierCustomerReport.OptionsView.ShowAutoFilterRow = false;
            
            gridViewSupplierCustomerReport.OptionsBehavior.Editable = false;
            gridViewSupplierCustomerReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSupplierCustomerReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSupplierCustomerReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSupplierCustomerReport.OptionsSelection.MultiSelect = false;
            
            gridViewSupplierCustomerReport.OptionsCustomization.AllowFilter = false;
            gridViewSupplierCustomerReport.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all supplier/customer report from database
        /// </summary>
        public void LoadSupplierCustomerReport()
        {
            try
            {
                supplierCustomerTable = _bllContacts.GetSupplierCustomerReport();
                gridSupplierCustomerReport.DataSource = supplierCustomerTable;
                gridViewSupplierCustomerReport.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading supplier/customer report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports supplier/customer report data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierCustomerTable == null || supplierCustomerTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier/customer report data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"SupplierCustomerReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewSupplierCustomerReport.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Supplier/Customer report data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports supplier/customer report data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierCustomerTable == null || supplierCustomerTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier/customer report data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"SupplierCustomerReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewSupplierCustomerReport.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Supplier/Customer report data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports supplier/customer report data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierCustomerTable == null || supplierCustomerTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier/customer report data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"SupplierCustomerReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewSupplierCustomerReport.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Supplier/Customer report data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the supplier/customer report data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierCustomerTable == null || supplierCustomerTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No supplier/customer report data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Store original column widths
                var originalWidths = new Dictionary<string, int>
                {
                    { "customer_name", colCustomerName.Width },
                    { "total_purchase", colTotalPurchase.Width },
                    { "total_purchase_return", colTotalPurchaseReturn.Width },
                    { "total_sale", colTotalSale.Width },
                    { "total_sell_return", colTotalSellReturn.Width },
                    { "opening_balance", colOpeningBalance.Width },
                    { "due", colDue.Width }
                };

                try
                {
                    // Adjust column widths for printing - wider for customer name, narrower for numbers
                    colCustomerName.Width = 400;          // Wider for customer/supplier name
                    colTotalPurchase.Width = 100;         // Narrower for total purchase
                    colTotalPurchaseReturn.Width = 100;   // Narrower for total purchase return
                    colTotalSale.Width = 100;             // Narrower for total sale
                    colTotalSellReturn.Width = 100;       // Narrower for total sell return
                    colOpeningBalance.Width = 100;        // Narrower for opening balance
                    colDue.Width = 100;                   // Narrower for due

                    // Create print link using ReportHelper
                    PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                        gridSupplierCustomerReport, 
                        "Supplier and Customer Report", 
                        landscape: false);

                    // Show print preview dialog with print options
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore original column widths
                    colCustomerName.Width = originalWidths["customer_name"];
                    colTotalPurchase.Width = originalWidths["total_purchase"];
                    colTotalPurchaseReturn.Width = originalWidths["total_purchase_return"];
                    colTotalSale.Width = originalWidths["total_sale"];
                    colTotalSellReturn.Width = originalWidths["total_sell_return"];
                    colOpeningBalance.Width = originalWidths["opening_balance"];
                    colDue.Width = originalWidths["due"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing supplier/customer report data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
