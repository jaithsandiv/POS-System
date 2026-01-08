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

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Items_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable itemsReportTable;

        public UC_Items_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadItemsReport();
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
                txtSearch.Properties.NullValuePrompt = "Search items report...";
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
        /// Performs search filtering on the items report grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadItemsReport();
                    return;
                }

                // Use BLL search method
                itemsReportTable = _bllSalesTerminal.SearchItemsReport(searchText);
                gridItemsReport.DataSource = itemsReportTable;
                gridViewItemsReport.RefreshData();
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
            if (gridViewItemsReport == null) return;

            // Configure Product column
            colProduct.FieldName = "Product";
            colProduct.Caption = "Product";
            colProduct.Width = 200;
            colProduct.OptionsColumn.AllowEdit = false;
            colProduct.OptionsColumn.AllowFocus = false;
            colProduct.Visible = true;

            // Configure Purchase Date column
            colPurchaseDate.FieldName = "PurchaseDate";
            colPurchaseDate.Caption = "Purchase Date";
            colPurchaseDate.Width = 120;
            colPurchaseDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPurchaseDate.OptionsColumn.AllowEdit = false;
            colPurchaseDate.OptionsColumn.AllowFocus = false;
            colPurchaseDate.OptionsColumn.FixedWidth = true;
            colPurchaseDate.Visible = true;

            // Configure Purchase column
            colPurchase.FieldName = "Purchase";
            colPurchase.Caption = "Purchase";
            colPurchase.Width = 150;
            colPurchase.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPurchase.OptionsColumn.AllowEdit = false;
            colPurchase.OptionsColumn.AllowFocus = false;
            colPurchase.Visible = true;

            // Configure Supplier column
            colSupplier.FieldName = "Supplier";
            colSupplier.Caption = "Supplier";
            colSupplier.Width = 150;
            colSupplier.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSupplier.OptionsColumn.AllowEdit = false;
            colSupplier.OptionsColumn.AllowFocus = false;
            colSupplier.Visible = true;

            // Configure Purchase Price column
            colPurchasePrice.FieldName = "PurchasePrice";
            colPurchasePrice.Caption = "Purchase Price";
            colPurchasePrice.Width = 100;
            colPurchasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colPurchasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPurchasePrice.DisplayFormat.FormatString = "n2";
            colPurchasePrice.OptionsColumn.AllowEdit = false;
            colPurchasePrice.OptionsColumn.AllowFocus = false;
            colPurchasePrice.OptionsColumn.FixedWidth = true;
            colPurchasePrice.Visible = true;

            // Configure Sell Date column
            colSellDate.FieldName = "SellDate";
            colSellDate.Caption = "Sell Date";
            colSellDate.Width = 120;
            colSellDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSellDate.OptionsColumn.AllowEdit = false;
            colSellDate.OptionsColumn.AllowFocus = false;
            colSellDate.OptionsColumn.FixedWidth = true;
            colSellDate.Visible = true;

            // Configure Sale ID column
            colSaleID.FieldName = "SaleID";
            colSaleID.Caption = "Sale ID";
            colSaleID.Width = 80;
            colSaleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleID.OptionsColumn.AllowEdit = false;
            colSaleID.OptionsColumn.AllowFocus = false;
            colSaleID.OptionsColumn.FixedWidth = true;
            colSaleID.Visible = true;

            // Configure Customer Name column
            colCustomerName.FieldName = "CustomerName";
            colCustomerName.Caption = "Customer Name";
            colCustomerName.Width = 180;
            colCustomerName.OptionsColumn.AllowEdit = false;
            colCustomerName.OptionsColumn.AllowFocus = false;
            colCustomerName.Visible = true;

            // Configure Location column
            colLocation.FieldName = "Location";
            colLocation.Caption = "Location";
            colLocation.Width = 150;
            colLocation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLocation.OptionsColumn.AllowEdit = false;
            colLocation.OptionsColumn.AllowFocus = false;
            colLocation.Visible = true;

            // Configure Sell Quantity column
            colSellQuantity.FieldName = "SellQuantity";
            colSellQuantity.Caption = "Sell Quantity";
            colSellQuantity.Width = 80;
            colSellQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSellQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSellQuantity.DisplayFormat.FormatString = "n2";
            colSellQuantity.OptionsColumn.AllowEdit = false;
            colSellQuantity.OptionsColumn.AllowFocus = false;
            colSellQuantity.OptionsColumn.FixedWidth = true;
            colSellQuantity.Visible = true;

            // Configure Sell Price column
            colSellPrice.FieldName = "SellPrice";
            colSellPrice.Caption = "Sell Price";
            colSellPrice.Width = 100;
            colSellPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colSellPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSellPrice.DisplayFormat.FormatString = "n2";
            colSellPrice.OptionsColumn.AllowEdit = false;
            colSellPrice.OptionsColumn.AllowFocus = false;
            colSellPrice.OptionsColumn.FixedWidth = true;
            colSellPrice.Visible = true;

            // Configure Total Amount column
            colTotalAmount.FieldName = "TotalAmount";
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.Width = 100;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;

            // Apply grid appearance styling
            gridViewItemsReport.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewItemsReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewItemsReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewItemsReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewItemsReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewItemsReport.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewItemsReport.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewItemsReport.ColumnPanelRowHeight = 44;
            gridViewItemsReport.RowHeight = 44;

            // Grid view options
            gridViewItemsReport.OptionsView.ShowGroupPanel = false;
            gridViewItemsReport.OptionsView.ShowIndicator = false;
            gridViewItemsReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewItemsReport.OptionsView.ShowAutoFilterRow = false;
            
            gridViewItemsReport.OptionsBehavior.Editable = false;
            gridViewItemsReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewItemsReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewItemsReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewItemsReport.OptionsSelection.MultiSelect = false;
            
            gridViewItemsReport.OptionsCustomization.AllowFilter = false;
            gridViewItemsReport.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all items report from database
        /// </summary>
        public void LoadItemsReport()
        {
            try
            {
                itemsReportTable = _bllSalesTerminal.GetItemsReport();
                gridItemsReport.DataSource = itemsReportTable;
                gridViewItemsReport.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading items report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports items report data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemsReportTable == null || itemsReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No items report data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"ItemsReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewItemsReport.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Items report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports items report data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemsReportTable == null || itemsReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No items report data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"ItemsReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewItemsReport.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Items report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports items report data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemsReportTable == null || itemsReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No items report data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"ItemsReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewItemsReport.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Items report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the items report data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemsReportTable == null || itemsReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No items report data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Store original column widths
                var originalWidths = new Dictionary<string, int>
                {
                    { "Product", colProduct.Width },
                    { "PurchaseDate", colPurchaseDate.Width },
                    { "Purchase", colPurchase.Width },
                    { "Supplier", colSupplier.Width },
                    { "PurchasePrice", colPurchasePrice.Width },
                    { "SellDate", colSellDate.Width },
                    { "SaleID", colSaleID.Width },
                    { "CustomerName", colCustomerName.Width },
                    { "Location", colLocation.Width },
                    { "SellQuantity", colSellQuantity.Width },
                    { "SellPrice", colSellPrice.Width },
                    { "TotalAmount", colTotalAmount.Width }
                };

                try
                {
                    // Adjust column widths for printing
                    // Wider for text columns
                    colProduct.Width = 180;          // Product name
                    colPurchase.Width = 100;         // Purchase/Brand
                    colSupplier.Width = 120;         // Supplier
                    colCustomerName.Width = 140;     // Customer name
                    colLocation.Width = 100;         // Location
                    
                    // Narrower for date/number columns
                    colPurchaseDate.Width = 80;      // Purchase date
                    colPurchasePrice.Width = 70;     // Purchase price
                    colSellDate.Width = 80;          // Sell date
                    colSaleID.Width = 50;            // Sale ID
                    colSellQuantity.Width = 50;      // Quantity
                    colSellPrice.Width = 70;         // Sell price
                    colTotalAmount.Width = 80;       // Total amount

                    // Create a PrintableComponentLink to print the grid
                    DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                        new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                    
                    printLink.Component = gridItemsReport;
                    
                    // Configure print settings (portrait orientation)
                    printLink.Landscape = false;
                    printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                    
                    // Set margins - reduced for more space
                    printLink.Margins.Left = 20;
                    printLink.Margins.Right = 20;
                    printLink.Margins.Top = 50;
                    printLink.Margins.Bottom = 50;
                    
                    // Create document
                    printLink.CreateDocument();
                    printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    
                    // Add header
                    DevExpress.XtraPrinting.PageHeaderFooter header = printLink.PageHeaderFooter as DevExpress.XtraPrinting.PageHeaderFooter;
                    if (header != null)
                    {
                        header.Header.Content.Clear();
                        header.Header.Content.AddRange(new string[] {
                            "Items Report",
                            "",
                            $"Printed: {DateTime.Now:dd/MM/yyyy HH:mm}"
                        });
                        header.Header.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                        header.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
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
                        header.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
                    }

                    // Show print preview dialog with print options
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore original column widths
                    colProduct.Width = originalWidths["Product"];
                    colPurchaseDate.Width = originalWidths["PurchaseDate"];
                    colPurchase.Width = originalWidths["Purchase"];
                    colSupplier.Width = originalWidths["Supplier"];
                    colPurchasePrice.Width = originalWidths["PurchasePrice"];
                    colSellDate.Width = originalWidths["SellDate"];
                    colSaleID.Width = originalWidths["SaleID"];
                    colCustomerName.Width = originalWidths["CustomerName"];
                    colLocation.Width = originalWidths["Location"];
                    colSellQuantity.Width = originalWidths["SellQuantity"];
                    colSellPrice.Width = originalWidths["SellPrice"];
                    colTotalAmount.Width = originalWidths["TotalAmount"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing items report: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
