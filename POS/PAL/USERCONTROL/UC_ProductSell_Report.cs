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
    public partial class UC_ProductSell_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable productSalesTable;

        public UC_ProductSell_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadProductSales();

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
                txtSearch.Properties.NullValuePrompt = "Search product sales...";
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
        /// Performs search filtering on the product sales grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadProductSales();
                    return;
                }

                // Use BLL search method
                productSalesTable = _bllSalesTerminal.SearchProductSalesReport(searchText);
                gridProductSell.DataSource = productSalesTable;
                gridViewProductSell.RefreshData();
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
            if (gridViewProductSell == null) return;

            // Configure Product column
            colProduct.FieldName = "Product";
            colProduct.Caption = "Product";
            colProduct.Width = 200;
            colProduct.OptionsColumn.AllowEdit = false;
            colProduct.OptionsColumn.AllowFocus = false;
            colProduct.Visible = true;

            // Configure Customer Name column
            colCustomerName.FieldName = "CustomerName";
            colCustomerName.Caption = "Customer Name";
            colCustomerName.Width = 180;
            colCustomerName.OptionsColumn.AllowEdit = false;
            colCustomerName.OptionsColumn.AllowFocus = false;
            colCustomerName.Visible = true;

            // Configure Customer ID column
            colCustomerID.FieldName = "CustomerID";
            colCustomerID.Caption = "Customer ID";
            colCustomerID.Width = 100;
            colCustomerID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomerID.OptionsColumn.AllowEdit = false;
            colCustomerID.OptionsColumn.AllowFocus = false;
            colCustomerID.OptionsColumn.FixedWidth = true;
            colCustomerID.Visible = true;

            // Configure Invoice No column
            colInvoiceNo.FieldName = "InvoiceNo";
            colInvoiceNo.Caption = "Invoice No.";
            colInvoiceNo.Width = 150;
            colInvoiceNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNo.OptionsColumn.AllowEdit = false;
            colInvoiceNo.OptionsColumn.AllowFocus = false;
            colInvoiceNo.OptionsColumn.FixedWidth = true;
            colInvoiceNo.Visible = true;

            // Configure Date column
            colDate.FieldName = "Date";
            colDate.Caption = "Date";
            colDate.Width = 150;
            colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            colDate.OptionsColumn.FixedWidth = true;
            colDate.Visible = true;

            // Configure Quantity column
            colQuantity.FieldName = "Quantity";
            colQuantity.Caption = "Quantity";
            colQuantity.Width = 100;
            colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colQuantity.DisplayFormat.FormatString = "n2";
            colQuantity.OptionsColumn.AllowEdit = false;
            colQuantity.OptionsColumn.AllowFocus = false;
            colQuantity.OptionsColumn.FixedWidth = true;
            colQuantity.Visible = true;

            // Configure Unit Price column
            colUnitPrice.FieldName = "UnitPrice";
            colUnitPrice.Caption = "Unit Price";
            colUnitPrice.Width = 120;
            colUnitPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colUnitPrice.DisplayFormat.FormatString = "n2";
            colUnitPrice.OptionsColumn.AllowEdit = false;
            colUnitPrice.OptionsColumn.AllowFocus = false;
            colUnitPrice.OptionsColumn.FixedWidth = true;
            colUnitPrice.Visible = true;

            // Configure Discount column
            colDiscount.FieldName = "Discount";
            colDiscount.Caption = "Discount";
            colDiscount.Width = 100;
            colDiscount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colDiscount.DisplayFormat.FormatString = "n2";
            colDiscount.OptionsColumn.AllowEdit = false;
            colDiscount.OptionsColumn.AllowFocus = false;
            colDiscount.OptionsColumn.FixedWidth = true;
            colDiscount.Visible = true;

            // Configure Total Amount column
            colTotalAmount.FieldName = "TotalAmount";
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.Width = 150;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;

            // Configure Payment column
            colPayment.FieldName = "Payment";
            colPayment.Caption = "Payment";
            colPayment.Width = 150;
            colPayment.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPayment.OptionsColumn.AllowEdit = false;
            colPayment.OptionsColumn.AllowFocus = false;
            colPayment.OptionsColumn.FixedWidth = true;
            colPayment.Visible = true;

            // Apply grid appearance styling
            gridViewProductSell.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewProductSell.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewProductSell.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewProductSell.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewProductSell.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewProductSell.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewProductSell.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewProductSell.ColumnPanelRowHeight = 44;
            gridViewProductSell.RowHeight = 44;

            // Grid view options
            gridViewProductSell.OptionsView.ShowGroupPanel = false;
            gridViewProductSell.OptionsView.ShowIndicator = false;
            gridViewProductSell.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewProductSell.OptionsView.ShowAutoFilterRow = false;
            
            gridViewProductSell.OptionsBehavior.Editable = false;
            gridViewProductSell.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewProductSell.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewProductSell.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewProductSell.OptionsSelection.MultiSelect = false;
            
            gridViewProductSell.OptionsCustomization.AllowFilter = false;
            gridViewProductSell.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all product sales from database
        /// </summary>
        public void LoadProductSales()
        {
            try
            {
                productSalesTable = _bllSalesTerminal.GetProductSalesReport();
                gridProductSell.DataSource = productSalesTable;
                gridViewProductSell.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading product sales: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports product sales data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (productSalesTable == null || productSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No product sales data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"ProductSales_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewProductSell.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Product sales data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports product sales data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (productSalesTable == null || productSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No product sales data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"ProductSales_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewProductSell.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Product sales data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports product sales data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (productSalesTable == null || productSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No product sales data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"ProductSales_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewProductSell.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Product sales data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the product sales data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (productSalesTable == null || productSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No product sales data to print.",
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
                    { "CustomerName", colCustomerName.Width },
                    { "CustomerID", colCustomerID.Width },
                    { "InvoiceNo", colInvoiceNo.Width },
                    { "Date", colDate.Width },
                    { "Quantity", colQuantity.Width },
                    { "UnitPrice", colUnitPrice.Width },
                    { "Discount", colDiscount.Width },
                    { "TotalAmount", colTotalAmount.Width },
                    { "Payment", colPayment.Width }
                };

                try
                {
                    // Adjust column widths for printing
                    // Wider for text columns
                    colProduct.Width = 250;           // Wider for product names
                    colCustomerName.Width = 200;      // Wider for customer names
                    
                    // Narrower for number columns
                    colCustomerID.Width = 60;         // Narrower for ID
                    colInvoiceNo.Width = 120;         // Narrower for invoice
                    colDate.Width = 120;              // Narrower for date
                    colQuantity.Width = 70;           // Narrower for quantity
                    colUnitPrice.Width = 90;          // Narrower for unit price
                    colDiscount.Width = 70;           // Narrower for discount
                    colTotalAmount.Width = 100;       // Narrower for total
                    colPayment.Width = 100;           // Narrower for payment

                    // Create a PrintableComponentLink to print the grid
                    DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                        new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                    
                    printLink.Component = gridProductSell;
                    
                    // Configure print settings
                    printLink.Landscape = true;
                    printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                    
                    // Set margins
                    printLink.Margins.Left = 30;      // Reduced margins for more space
                    printLink.Margins.Right = 30;
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
                            "Product Sales Report",
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
                    colCustomerName.Width = originalWidths["CustomerName"];
                    colCustomerID.Width = originalWidths["CustomerID"];
                    colInvoiceNo.Width = originalWidths["InvoiceNo"];
                    colDate.Width = originalWidths["Date"];
                    colQuantity.Width = originalWidths["Quantity"];
                    colUnitPrice.Width = originalWidths["UnitPrice"];
                    colDiscount.Width = originalWidths["Discount"];
                    colTotalAmount.Width = originalWidths["TotalAmount"];
                    colPayment.Width = originalWidths["Payment"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing product sales data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
