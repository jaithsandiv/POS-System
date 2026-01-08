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
using DevExpress.XtraEditors.Controls;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Stock_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private DataTable stockReportTable;

        public UC_Stock_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadStockReport();

            // Initialize search control
            InitializeSearchControl();

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
        /// Calculates and updates all summary labels based on current grid data
        /// </summary>
        private void UpdateSummaryLabels()
        {
            try
            {
                if (stockReportTable == null || stockReportTable.Rows.Count == 0)
                {
                    // Set default values when no data
                    lblClosingStockByPurchasePrice.Text = "Rs. 0.00";
                    lblClosingStockBySalePrice.Text = "Rs. 0.00";
                    lblPotentialProfit.Text = "Rs. 0.00";
                    lblProfitMargin.Text = "0%";
                    return;
                }

                decimal closingStockByPurchasePrice = 0;
                decimal closingStockBySalePrice = 0;
                decimal potentialProfit = 0;

                foreach (DataRow row in stockReportTable.Rows)
                {
                    // Get stock quantity and selling price
                    decimal currentStock = row["CurrentStock"] != DBNull.Value ? Convert.ToDecimal(row["CurrentStock"]) : 0;
                    decimal sellingPrice = row["UnitSellingPrice"] != DBNull.Value ? Convert.ToDecimal(row["UnitSellingPrice"]) : 0;
                    
                    // Calculate closing stock by sale price (CurrentStockValue from grid)
                    decimal currentStockValue = row["CurrentStockValue"] != DBNull.Value ? Convert.ToDecimal(row["CurrentStockValue"]) : 0;
                    closingStockBySalePrice += currentStockValue;

                    // Calculate potential profit from grid
                    decimal itemPotentialProfit = row["PotentialProfit"] != DBNull.Value ? Convert.ToDecimal(row["PotentialProfit"]) : 0;
                    potentialProfit += itemPotentialProfit;
                }

                // Calculate closing stock by purchase price
                // This is: closingStockBySalePrice - potentialProfit
                closingStockByPurchasePrice = closingStockBySalePrice - potentialProfit;

                // Calculate profit margin percentage
                decimal profitMargin = 0;
                if (closingStockBySalePrice > 0)
                {
                    profitMargin = (potentialProfit / closingStockBySalePrice) * 100;
                }

                // Update labels
                lblClosingStockByPurchasePrice.Text = $"Rs. {closingStockByPurchasePrice:N2}";
                lblClosingStockBySalePrice.Text = $"Rs. {closingStockBySalePrice:N2}";
                lblPotentialProfit.Text = $"Rs. {potentialProfit:N2}";
                lblProfitMargin.Text = $"{profitMargin:N2}%";
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
        /// Initializes the search control event handlers
        /// </summary>
        private void InitializeSearchControl()
        {
            if (txtSearch != null)
            {
                txtSearch.Properties.NullValuePrompt = "Search stock report...";
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
        /// Performs search filtering on the stock report grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadStockReport();
                    return;
                }

                // Use BLL search method
                stockReportTable = _bllProducts.SearchStockReport(searchText);
                
                gridStockReport.DataSource = stockReportTable;
                gridViewStockReport.RefreshData();
                
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
        /// Configures the grid columns to match Sales control design
        /// </summary>
        private void ConfigureGrid()
        {
            if (gridViewStockReport == null) return;

            // Configure View button column
            ConfigureViewButton();

            // Configure Product column
            colProduct.FieldName = "Product";
            colProduct.Caption = "Product";
            colProduct.Width = 250;
            colProduct.OptionsColumn.AllowEdit = false;
            colProduct.OptionsColumn.AllowFocus = false;
            colProduct.Visible = true;

            // Configure Category column
            colCategory.FieldName = "Category";
            colCategory.Caption = "Category";
            colCategory.Width = 150;
            colCategory.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCategory.OptionsColumn.AllowEdit = false;
            colCategory.OptionsColumn.AllowFocus = false;
            colCategory.OptionsColumn.FixedWidth = true;
            colCategory.Visible = true;

            // Configure Location column
            colLocation.FieldName = "Location";
            colLocation.Caption = "Location";
            colLocation.Width = 150;
            colLocation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLocation.OptionsColumn.AllowEdit = false;
            colLocation.OptionsColumn.AllowFocus = false;
            colLocation.OptionsColumn.FixedWidth = true;
            colLocation.Visible = true;

            // Configure Unit Selling Price column
            colUnitSellingPrice.FieldName = "UnitSellingPrice";
            colUnitSellingPrice.Caption = "Unit Selling Price";
            colUnitSellingPrice.Width = 120;
            colUnitSellingPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colUnitSellingPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colUnitSellingPrice.DisplayFormat.FormatString = "n2";
            colUnitSellingPrice.OptionsColumn.AllowEdit = false;
            colUnitSellingPrice.OptionsColumn.AllowFocus = false;
            colUnitSellingPrice.OptionsColumn.FixedWidth = true;
            colUnitSellingPrice.Visible = true;

            // Configure Current Stock column
            colCurrentStock.FieldName = "CurrentStock";
            colCurrentStock.Caption = "Current Stock";
            colCurrentStock.Width = 100;
            colCurrentStock.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCurrentStock.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colCurrentStock.DisplayFormat.FormatString = "n2";
            colCurrentStock.OptionsColumn.AllowEdit = false;
            colCurrentStock.OptionsColumn.AllowFocus = false;
            colCurrentStock.OptionsColumn.FixedWidth = true;
            colCurrentStock.Visible = true;

            // Configure Current Stock Value column
            colCurrentStockValue.FieldName = "CurrentStockValue";
            colCurrentStockValue.Caption = "Current Stock Value";
            colCurrentStockValue.Width = 150;
            colCurrentStockValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colCurrentStockValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colCurrentStockValue.DisplayFormat.FormatString = "n2";
            colCurrentStockValue.OptionsColumn.AllowEdit = false;
            colCurrentStockValue.OptionsColumn.AllowFocus = false;
            colCurrentStockValue.OptionsColumn.FixedWidth = true;
            colCurrentStockValue.Visible = true;

            // Configure Potential Profit column
            colPotentialProfit.FieldName = "PotentialProfit";
            colPotentialProfit.Caption = "Potential Profit";
            colPotentialProfit.Width = 120;
            colPotentialProfit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colPotentialProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPotentialProfit.DisplayFormat.FormatString = "n2";
            colPotentialProfit.OptionsColumn.AllowEdit = false;
            colPotentialProfit.OptionsColumn.AllowFocus = false;
            colPotentialProfit.OptionsColumn.FixedWidth = true;
            colPotentialProfit.Visible = true;

            // Configure Total Units Sold column
            colTotalUnitsSold.FieldName = "TotalUnitsSold";
            colTotalUnitsSold.Caption = "Total Units Sold";
            colTotalUnitsSold.Width = 120;
            colTotalUnitsSold.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTotalUnitsSold.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalUnitsSold.DisplayFormat.FormatString = "n2";
            colTotalUnitsSold.OptionsColumn.AllowEdit = false;
            colTotalUnitsSold.OptionsColumn.AllowFocus = false;
            colTotalUnitsSold.OptionsColumn.FixedWidth = true;
            colTotalUnitsSold.Visible = true;

            // Apply grid appearance styling
            gridViewStockReport.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewStockReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewStockReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewStockReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewStockReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewStockReport.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewStockReport.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewStockReport.ColumnPanelRowHeight = 44;
            gridViewStockReport.RowHeight = 44;

            // Grid view options
            gridViewStockReport.OptionsView.ShowGroupPanel = false;
            gridViewStockReport.OptionsView.ShowIndicator = false;
            gridViewStockReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewStockReport.OptionsView.ShowAutoFilterRow = false;
            
            gridViewStockReport.OptionsBehavior.Editable = true;
            gridViewStockReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewStockReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewStockReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewStockReport.OptionsSelection.MultiSelect = false;
            
            gridViewStockReport.OptionsCustomization.AllowFilter = false;
            gridViewStockReport.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Configures the View button editor
        /// </summary>
        private void ConfigureViewButton()
        {
            repositoryItemButtonEdit_View.Buttons.Clear();
            var viewButton = new EditorButton(ButtonPredefines.Glyph);
            viewButton.Caption = "View";
            viewButton.Kind = ButtonPredefines.Glyph;
            repositoryItemButtonEdit_View.Buttons.Add(viewButton);
            repositoryItemButtonEdit_View.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_View.ButtonClick += RepositoryItemButtonEdit_View_ButtonClick;

            colView.Caption = "View";
            colView.Width = 80;
            colView.ColumnEdit = repositoryItemButtonEdit_View;
            colView.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colView.OptionsColumn.AllowEdit = true;
            colView.OptionsColumn.AllowFocus = true;
            colView.OptionsColumn.FixedWidth = true;
            colView.Visible = true;
        }

        /// <summary>
        /// Handle View button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_View_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ViewProductStockHistory();
        }

        /// <summary>
        /// Opens the product stock history popup
        /// </summary>
        private void ViewProductStockHistory()
        {
            try
            {
                if (gridViewStockReport.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow selectedRow = gridViewStockReport.GetDataRow(gridViewStockReport.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int productId = Convert.ToInt32(selectedRow["product_id"]);
                string productName = selectedRow["Product"]?.ToString();

                // Open product stock history popup
                using (var historyForm = new Form_ProductStockHistory(productId, productName))
                {
                    historyForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error viewing product stock history: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Loads all stock report from database
        /// </summary>
        public void LoadStockReport()
        {
            try
            {
                // Load all stock report data
                stockReportTable = _bllProducts.GetStockReport();
                
                gridStockReport.DataSource = stockReportTable;
                gridViewStockReport.BestFitColumns();
                
                // Update summary labels after loading data
                UpdateSummaryLabels();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading stock report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports stock report data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockReportTable == null || stockReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock report data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"StockReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide View column
                    colView.Visible = false;

                    try
                    {
                        // Exports grid to CSV using DevExpress export functionality
                        gridViewStockReport.ExportToCsv(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Stock report data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export CSV",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore View column visibility
                        colView.Visible = true;
                    }
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
        /// Exports stock report data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockReportTable == null || stockReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock report data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"StockReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide View column
                    colView.Visible = false;

                    try
                    {
                        // Export grid to Excel using DevExpress export functionality
                        gridViewStockReport.ExportToXlsx(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Stock report data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export Excel",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore View column visibility
                        colView.Visible = true;
                    }
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
        /// Exports stock report data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockReportTable == null || stockReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock report data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"StockReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide View column
                    colView.Visible = false;

                    try
                    {
                        // Export grid to PDF using DevExpress export functionality
                        gridViewStockReport.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Stock report data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export PDF",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore View column visibility
                        colView.Visible = true;
                    }
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
        /// Prints the stock report data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockReportTable == null || stockReportTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No stock report data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Temporarily hide View column
                colView.Visible = false;

                try
                {
                    // Create a PrintableComponentLink to print the grid
                    DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                        new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                    
                    printLink.Component = gridStockReport;
                    
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
                            "Stock Report",
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
                    // Restore View column visibility
                    colView.Visible = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing stock report data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
