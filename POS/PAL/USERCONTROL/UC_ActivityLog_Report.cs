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
    public partial class UC_ActivityLog_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SystemLog _bllSystemLog = new BLL_SystemLog();
        private DataTable activityLogsTable;

        public UC_ActivityLog_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadActivityLogs();

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
                txtSearch.Properties.NullValuePrompt = "Search activity logs...";
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
        /// Performs search filtering on the activity logs grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadActivityLogs();
                    return;
                }

                // Use BLL search method
                activityLogsTable = _bllSystemLog.SearchSystemLogs(searchText);
                gridActivityLog.DataSource = activityLogsTable;
                gridView1.RefreshData();
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
            if (gridView1 == null) return;

            // Configure Log ID column
            colLogId.FieldName = "log_id";
            colLogId.Caption = "Log ID";
            colLogId.Width = 80;
            colLogId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLogId.OptionsColumn.AllowEdit = false;
            colLogId.OptionsColumn.AllowFocus = false;
            colLogId.OptionsColumn.FixedWidth = true;
            colLogId.Visible = true;
            colLogId.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            // Configure Log Type column
            colLogType.FieldName = "log_type";
            colLogType.Caption = "Type";
            colLogType.Width = 100;
            colLogType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLogType.OptionsColumn.AllowEdit = false;
            colLogType.OptionsColumn.AllowFocus = false;
            colLogType.OptionsColumn.FixedWidth = true;
            colLogType.Visible = true;

            // Configure Source column
            colSource.FieldName = "source";
            colSource.Caption = "Source";
            colSource.Width = 120;
            colSource.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSource.OptionsColumn.AllowEdit = false;
            colSource.OptionsColumn.AllowFocus = false;
            colSource.OptionsColumn.FixedWidth = true;
            colSource.Visible = true;

            // Configure Reference ID column
            colReferenceId.FieldName = "reference_id";
            colReferenceId.Caption = "Reference ID";
            colReferenceId.Width = 100;
            colReferenceId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colReferenceId.OptionsColumn.AllowEdit = false;
            colReferenceId.OptionsColumn.AllowFocus = false;
            colReferenceId.OptionsColumn.FixedWidth = true;
            colReferenceId.Visible = true;

            // Configure Message column
            colMessage.FieldName = "message";
            colMessage.Caption = "Message";
            colMessage.Width = 500;
            colMessage.OptionsColumn.AllowEdit = false;
            colMessage.OptionsColumn.AllowFocus = false;
            colMessage.Visible = true;

            // Configure Username column
            colUsername.FieldName = "username";
            colUsername.Caption = "User";
            colUsername.Width = 120;
            colUsername.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colUsername.OptionsColumn.AllowEdit = false;
            colUsername.OptionsColumn.AllowFocus = false;
            colUsername.OptionsColumn.FixedWidth = true;
            colUsername.Visible = true;

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
            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridView1.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridView1.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridView1.ColumnPanelRowHeight = 44;
            gridView1.RowHeight = 44;

            // Grid view options
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.OptionsSelection.MultiSelect = false;
            
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;
        }

        /// <summary>
        /// Loads all activity logs from database
        /// </summary>
        public void LoadActivityLogs()
        {
            try
            {
                activityLogsTable = _bllSystemLog.GetSystemLogs();
                gridActivityLog.DataSource = activityLogsTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading activity logs: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports activity logs data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (activityLogsTable == null || activityLogsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No activity logs data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"ActivityLogs_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to CSV using DevExpress export functionality
                    gridView1.ExportToCsv(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Activity logs data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports activity logs data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (activityLogsTable == null || activityLogsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No activity logs data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"ActivityLogs_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to Excel using DevExpress export functionality
                    gridView1.ExportToXlsx(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Activity logs data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports activity logs data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (activityLogsTable == null || activityLogsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No activity logs data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"ActivityLogs_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export grid to PDF using DevExpress export functionality
                    gridView1.ExportToPdf(saveFileDialog.FileName);

                    XtraMessageBox.Show(
                        $"Activity logs data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the activity logs data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (activityLogsTable == null || activityLogsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No activity logs data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a PrintableComponentLink to print the grid
                DevExpress.XtraPrinting.PrintableComponentLink printLink = 
                    new DevExpress.XtraPrinting.PrintableComponentLink(new DevExpress.XtraPrinting.PrintingSystem());
                
                printLink.Component = gridActivityLog;
                
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
                        "Activity Log Report",
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
                    $"Error printing activity logs data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
