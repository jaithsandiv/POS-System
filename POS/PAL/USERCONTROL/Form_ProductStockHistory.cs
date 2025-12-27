using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace POS.PAL.USERCONTROL
{
    public partial class Form_ProductStockHistory : DevExpress.XtraEditors.XtraForm
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private readonly int _productId;
        private readonly string _productName;
        private DataTable stockHistoryTable;

        public Form_ProductStockHistory(int productId, string productName)
        {
            InitializeComponent();
            _productId = productId;
            _productName = productName;
            
            ConfigureGrid();
            LoadStockHistory();
        }

        /// <summary>
        /// Configures the grid columns for stock history
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewStockHistory == null) return;

            // Configure Transaction ID column
            colTransactionID.FieldName = "TransactionID";
            colTransactionID.Caption = "Transaction ID";
            colTransactionID.Width = 100;
            colTransactionID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTransactionID.OptionsColumn.AllowEdit = false;
            colTransactionID.OptionsColumn.AllowFocus = false;
            colTransactionID.OptionsColumn.FixedWidth = true;
            colTransactionID.Visible = true;

            // Configure Invoice Number column
            colInvoiceNumber.FieldName = "InvoiceNumber";
            colInvoiceNumber.Caption = "Invoice Number";
            colInvoiceNumber.Width = 150;
            colInvoiceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNumber.OptionsColumn.AllowEdit = false;
            colInvoiceNumber.OptionsColumn.AllowFocus = false;
            colInvoiceNumber.OptionsColumn.FixedWidth = true;
            colInvoiceNumber.Visible = true;

            // Configure Transaction Date column
            colTransactionDate.FieldName = "TransactionDate";
            colTransactionDate.Caption = "Transaction Date";
            colTransactionDate.Width = 150;
            colTransactionDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTransactionDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colTransactionDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            colTransactionDate.OptionsColumn.AllowEdit = false;
            colTransactionDate.OptionsColumn.AllowFocus = false;
            colTransactionDate.OptionsColumn.FixedWidth = true;
            colTransactionDate.Visible = true;

            // Configure Transaction Type column
            colTransactionType.FieldName = "TransactionType";
            colTransactionType.Caption = "Type";
            colTransactionType.Width = 80;
            colTransactionType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTransactionType.OptionsColumn.AllowEdit = false;
            colTransactionType.OptionsColumn.AllowFocus = false;
            colTransactionType.OptionsColumn.FixedWidth = true;
            colTransactionType.Visible = true;

            // Configure Quantity column
            colQuantity.FieldName = "Quantity";
            colQuantity.Caption = "Quantity";
            colQuantity.Width = 80;
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
            colUnitPrice.Width = 100;
            colUnitPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colUnitPrice.DisplayFormat.FormatString = "n2";
            colUnitPrice.OptionsColumn.AllowEdit = false;
            colUnitPrice.OptionsColumn.AllowFocus = false;
            colUnitPrice.OptionsColumn.FixedWidth = true;
            colUnitPrice.Visible = true;

            // Configure Total Amount column
            colTotalAmount.FieldName = "TotalAmount";
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.Width = 120;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;

            // Configure Customer column
            colCustomer.FieldName = "Customer";
            colCustomer.Caption = "Customer";
            colCustomer.Width = 180;
            colCustomer.OptionsColumn.AllowEdit = false;
            colCustomer.OptionsColumn.AllowFocus = false;
            colCustomer.Visible = true;

            // Configure Processed By column
            colProcessedBy.FieldName = "ProcessedBy";
            colProcessedBy.Caption = "Processed By";
            colProcessedBy.Width = 150;
            colProcessedBy.OptionsColumn.AllowEdit = false;
            colProcessedBy.OptionsColumn.AllowFocus = false;
            colProcessedBy.Visible = true;

            // Apply grid appearance styling
            gridViewStockHistory.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewStockHistory.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewStockHistory.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewStockHistory.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewStockHistory.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewStockHistory.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewStockHistory.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewStockHistory.ColumnPanelRowHeight = 44;
            gridViewStockHistory.RowHeight = 44;

            // Grid view options
            gridViewStockHistory.OptionsView.ShowGroupPanel = false;
            gridViewStockHistory.OptionsView.ShowIndicator = false;
            gridViewStockHistory.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewStockHistory.OptionsView.ShowAutoFilterRow = false;
            
            gridViewStockHistory.OptionsBehavior.Editable = false;
            gridViewStockHistory.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewStockHistory.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewStockHistory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewStockHistory.OptionsSelection.MultiSelect = false;
            
            gridViewStockHistory.OptionsCustomization.AllowFilter = false;
            gridViewStockHistory.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads stock history for the product
        /// </summary>
        private void LoadStockHistory()
        {
            try
            {
                // Set form title
                this.Text = $"Stock History - {_productName}";
                lblProductName.Text = _productName;

                // Load data
                stockHistoryTable = _bllProducts.GetProductStockHistory(_productId);
                gridStockHistory.DataSource = stockHistoryTable;
                gridViewStockHistory.BestFitColumns();

                // Update record count
                lblRecordCount.Text = $"Total Records: {stockHistoryTable.Rows.Count}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading stock history: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockHistoryTable == null || stockHistoryTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock history data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"StockHistory_{_productName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gridViewStockHistory.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Stock history exported successfully to:\n{saveFileDialog.FileName}",
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockHistoryTable == null || stockHistoryTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock history data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"StockHistory_{_productName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gridViewStockHistory.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Stock history exported successfully to:\n{saveFileDialog.FileName}",
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

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockHistoryTable == null || stockHistoryTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock history data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"StockHistory_{_productName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gridViewStockHistory.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Stock history exported successfully to:\n{saveFileDialog.FileName}",
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockHistoryTable == null || stockHistoryTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock history data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a PrintableComponentLink to print the grid
                DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                    new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                
                printLink.Component = gridStockHistory;
                
                // Configure print settings
                printLink.Landscape = true;
                printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                
                // Set margins
                printLink.Margins.Left = 50;
                printLink.Margins.Right = 50;
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
                        $"Stock History - {_productName}",
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing stock history: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
