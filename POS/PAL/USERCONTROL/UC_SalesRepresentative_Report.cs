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
    public partial class UC_SalesRepresentative_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable salesRepresentativeTable;

        public UC_SalesRepresentative_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadSalesRepresentativeReport();

            // Initialize search control
            InitializeSearchControl();

            // Wire up export button events
            InitializeExportButtons();
        }

        /// <summary>
        /// Calculates and updates all summary labels based on current grid data
        /// </summary>
        private void UpdateSummaryLabels()
        {
            try
            {
                if (salesRepresentativeTable == null || salesRepresentativeTable.Rows.Count == 0)
                {
                    // Set default values when no data
                    lblTotalSales.Text = "Rs. 0.00";
                    lblTotalSalesReturn.Text = "Rs. 0.00";
                    lblResult.Text = "Rs. 0.00";
                    
                    // Adjust label positions for empty state
                    AdjustLabelPositions();
                    return;
                }

                decimal totalSales = 0;
                decimal totalSalesReturn = 0;

                foreach (DataRow row in salesRepresentativeTable.Rows)
                {
                    // Get total amount (this is the total sales)
                    decimal totalAmount = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0;
                    totalSales += totalAmount;

                    // Get total remaining (unpaid amount)
                    decimal totalRemaining = row["TotalRemaining"] != DBNull.Value ? Convert.ToDecimal(row["TotalRemaining"]) : 0;
                    totalSalesReturn += totalRemaining;
                }

                // Ensure totalSalesReturn is positive for display
                totalSalesReturn = Math.Abs(totalSalesReturn);

                // Calculate result: Total Sales - Total Sales Return
                decimal result = totalSales - totalSalesReturn;

                // Update labels
                lblTotalSales.Text = $"Rs. {totalSales:N2}";
                lblTotalSalesReturn.Text = $"Rs. {totalSalesReturn:N2}";
                lblResult.Text = $"Rs. {result:N2}";

                // Adjust label positions dynamically based on content width
                AdjustLabelPositions();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error updating summary labels: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Dynamically adjusts the position of labels to accommodate varying text widths
        /// </summary>
        private void AdjustLabelPositions()
        {
            try
            {
                // Measure the width of each label's text
                using (Graphics g = labelControl1.CreateGraphics())
                {
                    // Get the width of "Total Sales - Total Sales Return :
                    SizeF labelSize = g.MeasureString(labelControl1.Text, labelControl1.Appearance.Font);
                    int label1Width = (int)Math.Ceiling(labelSize.Width);

                    // Position lblTotalSales after labelControl1
                    lblTotalSales.Left = labelControl1.Left + label1Width + 5;

                    // Measure width of lblTotalSales
                    SizeF totalSalesSize = g.MeasureString(lblTotalSales.Text, lblTotalSales.Appearance.Font);
                    int totalSalesWidth = (int)Math.Ceiling(totalSalesSize.Width);

                    // Position labelControl2 (minus sign) after lblTotalSales
                    labelControl2.Left = lblTotalSales.Left + totalSalesWidth + 10;

                    // Measure width of labelControl2
                    SizeF minusSize = g.MeasureString(labelControl2.Text, labelControl2.Appearance.Font);
                    int minusWidth = (int)Math.Ceiling(minusSize.Width);

                    // Position lblTotalSalesReturn after labelControl2
                    lblTotalSalesReturn.Left = labelControl2.Left + minusWidth + 10;

                    // Measure width of lblTotalSalesReturn
                    SizeF totalReturnSize = g.MeasureString(lblTotalSalesReturn.Text, lblTotalSalesReturn.Appearance.Font);
                    int totalReturnWidth = (int)Math.Ceiling(totalReturnSize.Width);

                    // Position labelControl3 (equals sign) after lblTotalSalesReturn
                    labelControl3.Left = lblTotalSalesReturn.Left + totalReturnWidth + 10;

                    // Measure width of labelControl3
                    SizeF equalsSize = g.MeasureString(labelControl3.Text, labelControl3.Appearance.Font);
                    int equalsWidth = (int)Math.Ceiling(equalsSize.Width);

                    // Position lblResult after labelControl3
                    lblResult.Left = labelControl3.Left + equalsWidth + 10;
                }
            }
            catch (Exception ex)
            {
                // If positioning fails, use default positions
                System.Diagnostics.Debug.WriteLine($"Error adjusting label positions: {ex.Message}");
            }
        }

        /// <summary>
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            if (txtSearch != null)
            {
                txtSearch.Properties.NullValuePrompt = "Search sales representative report...";
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
        /// Performs search filtering on the sales representative report grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadSalesRepresentativeReport();
                    return;
                }

                // Use BLL search method
                salesRepresentativeTable = _bllSalesTerminal.SearchSalesRepresentativeReport(searchText);
                gridSalesRepresentative.DataSource = salesRepresentativeTable;
                gridViewSalesRepresentative.RefreshData();
                
                // Update summary labels after search
                UpdateSummaryLabels();
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
            if (gridViewSalesRepresentative == null) return;

            // Configure Date column
            colDate.FieldName = "Date";
            colDate.Caption = "Date";
            colDate.Width = 150;
            colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            colDate.OptionsColumn.FixedWidth = true;
            colDate.Visible = true;

            // Configure Invoice Number column
            colInvoiceNumber.FieldName = "InvoiceNumber";
            colInvoiceNumber.Caption = "Invoice Number";
            colInvoiceNumber.Width = 150;
            colInvoiceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNumber.OptionsColumn.AllowEdit = false;
            colInvoiceNumber.OptionsColumn.AllowFocus = false;
            colInvoiceNumber.OptionsColumn.FixedWidth = true;
            colInvoiceNumber.Visible = true;

            // Configure Customer Name column
            colCustomerName.FieldName = "CustomerName";
            colCustomerName.Caption = "Customer Name";
            colCustomerName.Width = 200;
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

            // Configure Payment Status column
            colPaymentStatus.FieldName = "PaymentStatus";
            colPaymentStatus.Caption = "Payment Status";
            colPaymentStatus.Width = 120;
            colPaymentStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaymentStatus.OptionsColumn.AllowEdit = false;
            colPaymentStatus.OptionsColumn.AllowFocus = false;
            colPaymentStatus.OptionsColumn.FixedWidth = true;
            colPaymentStatus.Visible = true;

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

            // Configure Total Paid column
            colTotalPaid.FieldName = "TotalPaid";
            colTotalPaid.Caption = "Total Paid";
            colTotalPaid.Width = 120;
            colTotalPaid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalPaid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalPaid.DisplayFormat.FormatString = "n2";
            colTotalPaid.OptionsColumn.AllowEdit = false;
            colTotalPaid.OptionsColumn.AllowFocus = false;
            colTotalPaid.OptionsColumn.FixedWidth = true;
            colTotalPaid.Visible = true;

            // Configure Total Remaining column
            colTotalRemaining.FieldName = "TotalRemaining";
            colTotalRemaining.Caption = "Total Remaining";
            colTotalRemaining.Width = 120;
            colTotalRemaining.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalRemaining.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalRemaining.DisplayFormat.FormatString = "n2";
            colTotalRemaining.OptionsColumn.AllowEdit = false;
            colTotalRemaining.OptionsColumn.AllowFocus = false;
            colTotalRemaining.OptionsColumn.FixedWidth = true;
            colTotalRemaining.Visible = true;

            // Apply grid appearance styling
            gridViewSalesRepresentative.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewSalesRepresentative.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSalesRepresentative.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSalesRepresentative.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSalesRepresentative.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewSalesRepresentative.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewSalesRepresentative.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewSalesRepresentative.ColumnPanelRowHeight = 44;
            gridViewSalesRepresentative.RowHeight = 44;

            // Grid view options
            gridViewSalesRepresentative.OptionsView.ShowGroupPanel = false;
            gridViewSalesRepresentative.OptionsView.ShowIndicator = false;
            gridViewSalesRepresentative.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSalesRepresentative.OptionsView.ShowAutoFilterRow = false;
            
            gridViewSalesRepresentative.OptionsBehavior.Editable = false;
            gridViewSalesRepresentative.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSalesRepresentative.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSalesRepresentative.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSalesRepresentative.OptionsSelection.MultiSelect = false;
            
            gridViewSalesRepresentative.OptionsCustomization.AllowFilter = false;
            gridViewSalesRepresentative.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads sales representative report from database
        /// </summary>
        public void LoadSalesRepresentativeReport()
        {
            try
            {
                salesRepresentativeTable = _bllSalesTerminal.GetSalesRepresentativeReport();
                gridSalesRepresentative.DataSource = salesRepresentativeTable;
                gridViewSalesRepresentative.BestFitColumns();
                
                // Update summary labels after loading data
                UpdateSummaryLabels();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading sales representative report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports sales representative report data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesRepresentativeTable == null || salesRepresentativeTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales representative report data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"SalesRepresentativeReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewSalesRepresentative.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sales representative report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports sales representative report data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesRepresentativeTable == null || salesRepresentativeTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales representative report data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"SalesRepresentativeReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewSalesRepresentative.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sales representative report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports sales representative report data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesRepresentativeTable == null || salesRepresentativeTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales representative report data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"SalesRepresentativeReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewSalesRepresentative.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sales representative report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the sales representative report data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (salesRepresentativeTable == null || salesRepresentativeTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales representative report data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Store original column widths
                var originalWidths = new Dictionary<string, int>
                {
                    { "Date", colDate.Width },
                    { "InvoiceNumber", colInvoiceNumber.Width },
                    { "CustomerName", colCustomerName.Width },
                    { "Location", colLocation.Width },
                    { "PaymentStatus", colPaymentStatus.Width },
                    { "TotalAmount", colTotalAmount.Width },
                    { "TotalPaid", colTotalPaid.Width },
                    { "TotalRemaining", colTotalRemaining.Width }
                };

                try
                {
                    // Adjust column widths for printing
                    // Wider for text columns
                    colInvoiceNumber.Width = 120;     // Invoice number
                    colCustomerName.Width = 200;      // Customer name (wider)
                    colLocation.Width = 150;          // Location (wider)
                    colPaymentStatus.Width = 100;     // Payment status
                    
                    // Narrower for date/number columns
                    colDate.Width = 90;               // Date
                    colTotalAmount.Width = 90;        // Total amount
                    colTotalPaid.Width = 90;          // Total paid
                    colTotalRemaining.Width = 90;     // Total remaining

                    // Create a PrintableComponentLink to print the grid
                    DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                        new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                    
                    printLink.Component = gridSalesRepresentative;
                    
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
                            "Sales Representative Report",
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
                    colDate.Width = originalWidths["Date"];
                    colInvoiceNumber.Width = originalWidths["InvoiceNumber"];
                    colCustomerName.Width = originalWidths["CustomerName"];
                    colLocation.Width = originalWidths["Location"];
                    colPaymentStatus.Width = originalWidths["PaymentStatus"];
                    colTotalAmount.Width = originalWidths["TotalAmount"];
                    colTotalPaid.Width = originalWidths["TotalPaid"];
                    colTotalRemaining.Width = originalWidths["TotalRemaining"];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing sales representative report: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
