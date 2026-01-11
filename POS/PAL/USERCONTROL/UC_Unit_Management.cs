using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using POS.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Unit_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;
        private DataTable unitsTable;

        public UC_Unit_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadData();

            // Wire up search events
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
            if (txtSearch != null)
                txtSearch.KeyDown += txtSearch_KeyDown;
                
            // Initialize export buttons
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
                DataTable dt = _bllProducts.SearchUnits(keyword);
                gridUnits.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeRepositoryItems()
        {
            repositoryItemButtonEdit_Edit = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit_Delete = new RepositoryItemButtonEdit();

            repositoryItemButtonEdit_Edit.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Edit.Buttons.Clear();
            repositoryItemButtonEdit_Edit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph) { Caption = "Edit" });
            repositoryItemButtonEdit_Edit.ButtonClick += RepositoryItemButtonEdit_Edit_ButtonClick;

            repositoryItemButtonEdit_Delete.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Delete.Buttons.Clear();
            repositoryItemButtonEdit_Delete.Buttons.Add(new EditorButton(ButtonPredefines.Glyph) { Caption = "Delete" });
            repositoryItemButtonEdit_Delete.ButtonClick += RepositoryItemButtonEdit_Delete_ButtonClick;

            gridUnits.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
            gridUnits.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
        }

        private void ConfigureGrid()
        {
            gridView1.Columns.Clear();

            var colId = gridView1.Columns.AddVisible("unit_id", "ID");
            colId.OptionsColumn.AllowEdit = false;
            colId.Width = 50;

            var colCode = gridView1.Columns.AddVisible("code", "Code");
            colCode.OptionsColumn.AllowEdit = false;

            var colName = gridView1.Columns.AddVisible("name", "Name");
            colName.OptionsColumn.AllowEdit = false;

            var colStatus = gridView1.Columns.AddVisible("status", "Status");
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.Width = 50;

            var colEdit = gridView1.Columns.AddVisible("Edit", "");
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.Width = 50;
            colEdit.OptionsColumn.FixedWidth = true;

            var colDelete = gridView1.Columns.AddVisible("Delete", "");
            colDelete.ColumnEdit = repositoryItemButtonEdit_Delete;
            colDelete.Width = 60;
            colDelete.OptionsColumn.FixedWidth = true;
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _bllProducts.GetUnits();
                unitsTable = dt;
                gridUnits.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Unit_Registration());
        }

        private void RepositoryItemButtonEdit_Edit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
             DataRow row = gridView1.GetFocusedDataRow();
             if (row != null)
             {
                 int id = Convert.ToInt32(row["unit_id"]);
                 Main.Instance.LoadUserControl(new UC_Unit_Registration(id));
             }
        }

        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
            if (row != null)
            {
                if (XtraMessageBox.Show("Are you sure you want to delete this unit?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(row["unit_id"]);
                    int userId = 1; // Default
                    if (_bllProducts.DeleteUnit(id, userId))
                    {
                        LoadData();
                    }
                }
            }
        }
        
        /// <summary>
        /// Exports units data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (unitsTable == null || unitsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No units data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"Units_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        gridView1.ExportToCsv(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Units data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export CSV",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
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
        /// Exports units data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (unitsTable == null || unitsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No units data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"Units_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        gridView1.ExportToXlsx(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Units data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export Excel",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
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
        /// Exports units data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (unitsTable == null || unitsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No units data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Units_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide Edit and Delete columns and save column widths
                    var editColumn = gridView1.Columns["Edit"];
                    var deleteColumn = gridView1.Columns["Delete"];
                    bool editVisible = editColumn?.Visible ?? false;
                    bool deleteVisible = deleteColumn?.Visible ?? false;
                    
                    // Store original column settings
                    Dictionary<GridColumn, int> originalWidths = new Dictionary<GridColumn, int>();
                    Dictionary<GridColumn, bool> originalFixedWidth = new Dictionary<GridColumn, bool>();
                    
                    foreach (GridColumn column in gridView1.Columns)
                    {
                        if (column.Visible)
                        {
                            originalWidths[column] = column.Width;
                            originalFixedWidth[column] = column.OptionsColumn.FixedWidth;
                        }
                    }

                    if (editColumn != null) editColumn.Visible = false;
                    if (deleteColumn != null) deleteColumn.Visible = false;

                    try
                    {
                        // Make columns auto-fit for export
                        foreach (GridColumn column in gridView1.Columns)
                        {
                            if (column.Visible)
                            {
                                column.OptionsColumn.FixedWidth = false;
                            }
                        }
                        
                        gridView1.BestFitColumns();
                        
                        // Create print link for better control over PDF export
                        PrintableComponentLink printLink = new PrintableComponentLink(new PrintingSystem());
                        printLink.Component = gridUnits;
                        printLink.Landscape = true;
                        printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                        
                        // Set margins
                        printLink.Margins.Left = 50;
                        printLink.Margins.Right = 50;
                        printLink.Margins.Top = 50;
                        printLink.Margins.Bottom = 50;
                        
                        // Create document and fit to page width
                        printLink.CreateDocument();
                        printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                        
                        // Export to PDF
                        printLink.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Units data exported successfully to:\n{saveFileDialog.FileName}",
                            "Export PDF",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    finally
                    {
                        // Restore column visibility and widths
                        if (editColumn != null) editColumn.Visible = editVisible;
                        if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                        
                        foreach (var kvp in originalWidths)
                        {
                            kvp.Key.Width = kvp.Value;
                            kvp.Key.OptionsColumn.FixedWidth = originalFixedWidth[kvp.Key];
                        }
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
        /// Prints the units data with dynamic column fitting
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (unitsTable == null || unitsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No units data to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Temporarily hide Edit and Delete columns
                var editColumn = gridView1.Columns["Edit"];
                var deleteColumn = gridView1.Columns["Delete"];
                bool editVisible = editColumn?.Visible ?? false;
                bool deleteVisible = deleteColumn?.Visible ?? false;

                if (editColumn != null) editColumn.Visible = false;
                if (deleteColumn != null) deleteColumn.Visible = false;

                try
                {
                    // Create print link using ReportHelper
                    PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                        gridUnits, 
                        "Unit List", 
                        landscape: false);

                    // Show print preview dialog
                    printLink.ShowPreviewDialog();
                }
                finally
                {
                    // Restore column visibility
                    if (editColumn != null) editColumn.Visible = editVisible;
                    if (deleteColumn != null) deleteColumn.Visible = deleteVisible;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing units data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
