using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.BLL;
using DevExpress.XtraPrinting;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SellReturn_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesReturn _bllSalesReturn = new BLL_SalesReturn();
        private DataTable saleReturnsTable;

        public UC_SellReturn_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadSaleReturns();

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
                txtSearch.Properties.NullValuePrompt = "Search sale returns...";
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch(txtSearch.Text);
            }
        }

        /// <summary>
        /// Performs search filtering on the sale returns grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (saleReturnsTable == null || saleReturnsTable.Rows.Count == 0)
                    return;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    gridControlSellReturns.DataSource = saleReturnsTable;
                    gridViewSellReturns.RefreshData();
                    return;
                }

                // Escape special characters for LIKE expression
                searchText = searchText.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

                // Create a filtered view based on search text
                DataView dataView = new DataView(saleReturnsTable);
                
                // Build filter expression to search across multiple columns
                StringBuilder filterExpression = new StringBuilder();
                
                // Add conditions for each searchable column
                List<string> conditions = new List<string>();
                
                conditions.Add($"CONVERT(return_id, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"CONVERT(sale_id, 'System.String') LIKE '%{searchText}%'");
                conditions.Add($"CONVERT(total_amount, 'System.String') LIKE '%{searchText}%'");
                
                // Add reason column if it's not null
                conditions.Add($"(reason IS NOT NULL AND reason LIKE '%{searchText}%')");
                
                // Add processed_by column if it's not null
                conditions.Add($"(processed_by IS NOT NULL AND CONVERT(processed_by, 'System.String') LIKE '%{searchText}%')");

                // Join all conditions with OR
                filterExpression.Append(string.Join(" OR ", conditions));

                // Apply filter
                dataView.RowFilter = filterExpression.ToString();
                
                // Create a new DataTable from the filtered view
                DataTable filteredTable = dataView.ToTable();
                
                // Update grid data source
                gridControlSellReturns.DataSource = filteredTable;
                
                // Refresh the grid view
                gridViewSellReturns.RefreshData();
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
            if (gridViewSellReturns == null) return;

            // Clear existing columns (except designer-defined ones)
            // We'll update the existing columns instead
            
            // Configure Return ID column
            colReturnId.FieldName = "return_id";
            colReturnId.Caption = "Return ID";
            colReturnId.Width = 100;
            colReturnId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colReturnId.OptionsColumn.AllowEdit = false;
            colReturnId.OptionsColumn.AllowFocus = false;
            colReturnId.OptionsColumn.FixedWidth = true;
            colReturnId.Visible = true;
            colReturnId.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            // Configure Sale ID column
            colSaleId.FieldName = "sale_id";
            colSaleId.Caption = "Sale ID";
            colSaleId.Width = 100;
            colSaleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleId.OptionsColumn.AllowEdit = false;
            colSaleId.OptionsColumn.AllowFocus = false;
            colSaleId.OptionsColumn.FixedWidth = true;
            colSaleId.Visible = true;

            // Configure Total Amount column
            colTotalAmount.FieldName = "total_amount";
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.Width = 120;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;

            // Configure Reason column
            colReason.FieldName = "reason";
            colReason.Caption = "Reason";
            colReason.Width = 300;
            colReason.OptionsColumn.AllowEdit = false;
            colReason.OptionsColumn.AllowFocus = false;
            colReason.Visible = true;

            // Configure Processed By column
            colProcessedBy.FieldName = "processed_by";
            colProcessedBy.Caption = "Processed By";
            colProcessedBy.Width = 120;
            colProcessedBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colProcessedBy.OptionsColumn.AllowEdit = false;
            colProcessedBy.OptionsColumn.AllowFocus = false;
            colProcessedBy.OptionsColumn.FixedWidth = true;
            colProcessedBy.Visible = true;

            // Configure Created Date column
            colCreatedDate.FieldName = "created_date";
            colCreatedDate.Caption = "Date";
            colCreatedDate.Width = 150;
            colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCreatedDate.OptionsColumn.AllowEdit = false;
            colCreatedDate.OptionsColumn.AllowFocus = false;
            colCreatedDate.OptionsColumn.FixedWidth = true;
            colCreatedDate.Visible = true;

            // Apply grid appearance styling
            gridViewSellReturns.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewSellReturns.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSellReturns.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSellReturns.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSellReturns.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewSellReturns.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewSellReturns.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewSellReturns.ColumnPanelRowHeight = 44;
            gridViewSellReturns.RowHeight = 44;

            // Grid view options
            gridViewSellReturns.OptionsView.ShowGroupPanel = false;
            gridViewSellReturns.OptionsView.ShowIndicator = false;
            gridViewSellReturns.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSellReturns.OptionsView.ShowAutoFilterRow = false;
            
            gridViewSellReturns.OptionsBehavior.Editable = false;
            gridViewSellReturns.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSellReturns.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSellReturns.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSellReturns.OptionsSelection.MultiSelect = false;
            
            gridViewSellReturns.OptionsCustomization.AllowFilter = false;
            gridViewSellReturns.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all sale returns from database
        /// </summary>
        public void LoadSaleReturns()
        {
            try
            {
                saleReturnsTable = _bllSalesReturn.GetSaleReturns();
                gridControlSellReturns.DataSource = saleReturnsTable;
                gridViewSellReturns.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading sale returns: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports sale returns data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (saleReturnsTable == null || saleReturnsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale returns data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"SaleReturns_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewSellReturns.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sale returns data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports sale returns data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (saleReturnsTable == null || saleReturnsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale returns data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"SaleReturns_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewSellReturns.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sale returns data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports sale returns data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (saleReturnsTable == null || saleReturnsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale returns data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"SaleReturns_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewSellReturns.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Sale returns data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the sale returns data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (saleReturnsTable == null || saleReturnsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale returns data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a PrintableComponentLink to print the grid
                DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                    new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                
                printLink.Component = gridControlSellReturns;
                
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
                        "Sale Returns Report",
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
                    $"Error printing sale returns data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
