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
    public partial class UC_Table_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable tableSalesTable;

        public UC_Table_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadTableSalesReport();
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
                txtSearch.Properties.NullValuePrompt = "Search table number...";
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
        /// Performs search filtering on the table sales report grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadTableSalesReport();
                    return;
                }

                // Use BLL search method
                tableSalesTable = _bllSalesTerminal.SearchTableSalesReport(searchText);
                gridTableReport.DataSource = tableSalesTable;
                gridViewTableReport.RefreshData();
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
            if (gridViewTableReport == null) return;

            // Configure Table Number column
            colTableNumber.FieldName = "table_number";
            colTableNumber.Caption = "Table Number";
            colTableNumber.Width = 150;
            colTableNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTableNumber.OptionsColumn.AllowEdit = false;
            colTableNumber.OptionsColumn.AllowFocus = false;
            colTableNumber.OptionsColumn.FixedWidth = true;
            colTableNumber.Visible = true;
            colTableNumber.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Configure Total Orders column
            colTotalOrders.FieldName = "total_orders";
            colTotalOrders.Caption = "Total Orders";
            colTotalOrders.Width = 120;
            colTotalOrders.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTotalOrders.OptionsColumn.AllowEdit = false;
            colTotalOrders.OptionsColumn.AllowFocus = false;
            colTotalOrders.OptionsColumn.FixedWidth = true;
            colTotalOrders.Visible = true;

            // Configure Total Items column
            colTotalItems.FieldName = "total_items";
            colTotalItems.Caption = "Total Items";
            colTotalItems.Width = 120;
            colTotalItems.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTotalItems.OptionsColumn.AllowEdit = false;
            colTotalItems.OptionsColumn.AllowFocus = false;
            colTotalItems.OptionsColumn.FixedWidth = true;
            colTotalItems.Visible = true;

            // Configure Total Amount column
            colTotalAmount.FieldName = "total_amount";
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.Width = 150;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;

            // Configure Grand Total column
            colGrandTotal.FieldName = "grand_total";
            colGrandTotal.Caption = "Grand Total";
            colGrandTotal.Width = 150;
            colGrandTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colGrandTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colGrandTotal.DisplayFormat.FormatString = "n2";
            colGrandTotal.OptionsColumn.AllowEdit = false;
            colGrandTotal.OptionsColumn.AllowFocus = false;
            colGrandTotal.OptionsColumn.FixedWidth = true;
            colGrandTotal.Visible = true;

            // Configure First Order Date column
            colFirstOrderDate.FieldName = "first_order_date";
            colFirstOrderDate.Caption = "First Order";
            colFirstOrderDate.Width = 150;
            colFirstOrderDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFirstOrderDate.OptionsColumn.AllowEdit = false;
            colFirstOrderDate.OptionsColumn.AllowFocus = false;
            colFirstOrderDate.OptionsColumn.FixedWidth = true;
            colFirstOrderDate.Visible = true;

            // Configure Last Order Date column
            colLastOrderDate.FieldName = "last_order_date";
            colLastOrderDate.Caption = "Last Order";
            colLastOrderDate.Width = 150;
            colLastOrderDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLastOrderDate.OptionsColumn.AllowEdit = false;
            colLastOrderDate.OptionsColumn.AllowFocus = false;
            colLastOrderDate.OptionsColumn.FixedWidth = true;
            colLastOrderDate.Visible = true;

            // Apply grid appearance styling
            gridViewTableReport.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewTableReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewTableReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewTableReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewTableReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewTableReport.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewTableReport.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewTableReport.ColumnPanelRowHeight = 44;
            gridViewTableReport.RowHeight = 44;

            // Grid view options
            gridViewTableReport.OptionsView.ShowGroupPanel = false;
            gridViewTableReport.OptionsView.ShowIndicator = false;
            gridViewTableReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewTableReport.OptionsView.ShowAutoFilterRow = false;
            
            gridViewTableReport.OptionsBehavior.Editable = false;
            gridViewTableReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewTableReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewTableReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewTableReport.OptionsSelection.MultiSelect = false;
            
            gridViewTableReport.OptionsCustomization.AllowFilter = false;
            gridViewTableReport.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads table sales report from database
        /// </summary>
        public void LoadTableSalesReport()
        {
            try
            {
                tableSalesTable = _bllSalesTerminal.GetTableSalesReport();
                gridTableReport.DataSource = tableSalesTable;
                gridViewTableReport.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading table sales report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports table sales report data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableSalesTable == null || tableSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No table sales data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"TableSalesReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewTableReport.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Table sales report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports table sales report data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableSalesTable == null || tableSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No table sales data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"TableSalesReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewTableReport.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Table sales report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports table sales report data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableSalesTable == null || tableSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No table sales data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"TableSalesReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewTableReport.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Table sales report exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the table sales report data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableSalesTable == null || tableSalesTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No table sales data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a PrintableComponentLink to print the grid
                DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                    new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                
                printLink.Component = gridTableReport;
                
                // Configure print settings (portrait orientation)
                printLink.Landscape = false;
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
                        "Table Sales Report",
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
                    $"Error printing table sales report: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
