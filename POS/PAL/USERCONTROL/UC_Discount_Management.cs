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
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Discount_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Discount _bllDiscount = new BLL_Discount();
        private DataTable discountsTable;

        public UC_Discount_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadDiscounts();

            // Wire up search events
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
            if (txtSearch != null)
                txtSearch.KeyDown += txtSearch_KeyDown;

            // Initialize export buttons
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
                discountsTable = _bllDiscount.SearchDiscounts(keyword);
                gridControlDiscounts.DataSource = discountsTable;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exports discount data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (discountsTable == null || discountsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No discount data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Discounts_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridViewDiscounts.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        "Discount data exported successfully.",
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
        /// Exports discount data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (discountsTable == null || discountsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No discount data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Discounts_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridViewDiscounts.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        "Discount data exported successfully.",
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
        /// Exports discount data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (discountsTable == null || discountsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No discount data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Discounts_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridViewDiscounts.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        "Discount data exported successfully.",
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
        /// Prints the discount data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (discountsTable == null || discountsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No discount data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create print link using ReportHelper
                PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                    gridControlDiscounts, 
                    "Discount List", 
                    landscape: true);

                // Show print preview dialog with print options
                printLink.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing discount data: {ex.Message}",
                    "Print Error",
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
            if (gridViewDiscounts == null) return;

            // Clear existing columns
            gridViewDiscounts.Columns.Clear();

            // Add columns matching the database schema
            var colId = gridViewDiscounts.Columns.AddVisible("discount_id", "ID");
            colId.FieldName = "discount_id";
            colId.Width = 80;
            colId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colId.OptionsColumn.AllowEdit = false;
            colId.OptionsColumn.AllowFocus = false;
            colId.OptionsColumn.FixedWidth = true;

            var colName = gridViewDiscounts.Columns.AddVisible("name", "Name");
            colName.FieldName = "name";
            colName.Width = 200;
            colName.OptionsColumn.AllowEdit = false;
            colName.OptionsColumn.AllowFocus = false;

            var colStartDate = gridViewDiscounts.Columns.AddVisible("start_date", "Start Date");
            colStartDate.FieldName = "start_date";
            colStartDate.Width = 120;
            colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStartDate.OptionsColumn.AllowEdit = false;
            colStartDate.OptionsColumn.AllowFocus = false;

            var colEndDate = gridViewDiscounts.Columns.AddVisible("end_date", "End Date");
            colEndDate.FieldName = "end_date";
            colEndDate.Width = 120;
            colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEndDate.OptionsColumn.AllowEdit = false;
            colEndDate.OptionsColumn.AllowFocus = false;

            var colStatus = gridViewDiscounts.Columns.AddVisible("status", "Status");
            colStatus.FieldName = "status";
            colStatus.Width = 100;
            colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.OptionsColumn.AllowFocus = false;

            // Apply grid appearance styling
            gridViewDiscounts.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewDiscounts.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewDiscounts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewDiscounts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewDiscounts.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewDiscounts.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewDiscounts.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewDiscounts.ColumnPanelRowHeight = 44;
            gridViewDiscounts.RowHeight = 44;

            // Grid view options
            gridViewDiscounts.OptionsView.ShowGroupPanel = false;
            gridViewDiscounts.OptionsView.ShowIndicator = false;
            gridViewDiscounts.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewDiscounts.OptionsView.ShowAutoFilterRow = false;
            
            gridViewDiscounts.OptionsBehavior.Editable = true;
            gridViewDiscounts.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDiscounts.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridViewDiscounts.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewDiscounts.OptionsSelection.MultiSelect = false;
            
            gridViewDiscounts.OptionsCustomization.AllowFilter = false;
            gridViewDiscounts.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all discounts from database
        /// </summary>
        public void LoadDiscounts()
        {
            try
            {
                discountsTable = _bllDiscount.GetDiscounts();
                gridControlDiscounts.DataSource = discountsTable;
                gridViewDiscounts.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading discounts: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Discount_Registration());
        }

        private void btnAssignProducts_Click(object sender, EventArgs e)
        {
            var selectedRows = gridViewDiscounts.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Please select a discount to assign products.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int rowHandle = selectedRows[0];
            int discountId = Convert.ToInt32(gridViewDiscounts.GetRowCellValue(rowHandle, "discount_id"));
            string discountName = gridViewDiscounts.GetRowCellValue(rowHandle, "name").ToString();

            Main.Instance.LoadUserControl(new UC_Promotion_Products(discountId, discountName));
        }
    }
}
