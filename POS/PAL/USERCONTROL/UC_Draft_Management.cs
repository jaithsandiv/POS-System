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

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Draft_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable draftsTable;

        public UC_Draft_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadDrafts();
            
            // Hide old controls if they still exist in the designer file (best effort)
            HideOldControls();

            // Wire up search events
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
            if (txtSearch != null)
                txtSearch.KeyDown += txtSearch_KeyDown;

            // Wire up export button events
            InitializeExportButtons();
        }

        /// <summary>
        /// Initializes the export button event handlers
        /// </summary>
        private void InitializeExportButtons()
        {
            // Wire up export button events
            if (btnExportCSV != null)
                btnExportCSV.Click += BtnExportCSV_Click;
            
            if (btnExportExcel != null)
                btnExportExcel.Click += BtnExportExcel_Click;
            
            if (btnExportPDF != null)
                btnExportPDF.Click += BtnExportPDF_Click;
            
            if (btnPrint != null)
                btnPrint.Click += BtnPrint_Click;
        }

        /// <summary>
        /// Exports draft data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (draftsTable == null || draftsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No draft data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Drafts_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gridViewDrafts.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Draft data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports draft data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (draftsTable == null || draftsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No draft data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Drafts_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gridViewDrafts.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Draft data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports draft data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (draftsTable == null || draftsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No draft data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Drafts_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gridViewDrafts.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Draft data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the draft data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (draftsTable == null || draftsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No draft data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a PrintableComponentLink to print the grid
                DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                    new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                
                printLink.Component = gridControlDrafts;
                
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
                        "Draft List",
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
                    $"Error printing draft data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
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
                draftsTable = _bllSalesTerminal.SearchSales("DRAFT", keyword);
                gridControlDrafts.DataSource = draftsTable;
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
            if (gridViewDrafts == null) return;

            // Clear existing columns
            gridViewDrafts.Columns.Clear();

            // Add columns matching the database schema
            var colSaleId = gridViewDrafts.Columns.AddVisible("sale_id", "Draft ID");
            colSaleId.FieldName = "sale_id";
            colSaleId.Width = 80;
            colSaleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleId.OptionsColumn.AllowEdit = false;
            colSaleId.OptionsColumn.AllowFocus = false;
            colSaleId.OptionsColumn.FixedWidth = true;

            var colInvoiceNumber = gridViewDrafts.Columns.AddVisible("invoice_number", "Reference No");
            colInvoiceNumber.FieldName = "invoice_number";
            colInvoiceNumber.Width = 150;
            colInvoiceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNumber.OptionsColumn.AllowEdit = false;
            colInvoiceNumber.OptionsColumn.AllowFocus = false;
            colInvoiceNumber.OptionsColumn.FixedWidth = true;

            var colCustomer = gridViewDrafts.Columns.AddVisible("customer_name", "Customer");
            colCustomer.FieldName = "customer_name";
            colCustomer.Width = 200;
            colCustomer.OptionsColumn.AllowEdit = false;
            colCustomer.OptionsColumn.AllowFocus = false;

            var colDate = gridViewDrafts.Columns.AddVisible("sale_date", "Date");
            colDate.FieldName = "sale_date";
            colDate.Width = 150;
            colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            colDate.OptionsColumn.FixedWidth = true;

            var colTotal = gridViewDrafts.Columns.AddVisible("grand_total", "Total");
            colTotal.FieldName = "grand_total";
            colTotal.Width = 120;
            colTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotal.DisplayFormat.FormatString = "n2";
            colTotal.OptionsColumn.AllowEdit = false;
            colTotal.OptionsColumn.AllowFocus = false;
            colTotal.OptionsColumn.FixedWidth = true;

            var colPaymentStatus = gridViewDrafts.Columns.AddVisible("payment_status", "Payment Status");
            colPaymentStatus.FieldName = "payment_status";
            colPaymentStatus.Width = 120;
            colPaymentStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaymentStatus.OptionsColumn.AllowEdit = false;
            colPaymentStatus.OptionsColumn.AllowFocus = false;
            colPaymentStatus.OptionsColumn.FixedWidth = true;

            var colBiller = gridViewDrafts.Columns.AddVisible("biller_name", "Biller");
            colBiller.FieldName = "biller_name";
            colBiller.Width = 150;
            colBiller.OptionsColumn.AllowEdit = false;
            colBiller.OptionsColumn.AllowFocus = false;

            // Apply grid appearance styling
            gridViewDrafts.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewDrafts.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewDrafts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewDrafts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewDrafts.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewDrafts.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewDrafts.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewDrafts.ColumnPanelRowHeight = 44;
            gridViewDrafts.RowHeight = 44;

            // Grid view options
            gridViewDrafts.OptionsView.ShowGroupPanel = false;
            gridViewDrafts.OptionsView.ShowIndicator = false;
            gridViewDrafts.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewDrafts.OptionsView.ShowAutoFilterRow = false;
            
            gridViewDrafts.OptionsBehavior.Editable = true;
            gridViewDrafts.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDrafts.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewDrafts.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewDrafts.OptionsSelection.MultiSelect = false;
            
            gridViewDrafts.OptionsCustomization.AllowFilter = false;
            gridViewDrafts.OptionsCustomization.AllowGroup = false;

            // Set default sort order by sale_id descending
            colSaleId.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
        }

        /// <summary>
        /// Loads all drafts from database
        /// </summary>
        public void LoadDrafts()
        {
            try
            {
                draftsTable = _bllSalesTerminal.GetSales("DRAFT");
                gridControlDrafts.DataSource = draftsTable;
                gridViewDrafts.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading drafts: {ex.Message}",
                    "Error",
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
