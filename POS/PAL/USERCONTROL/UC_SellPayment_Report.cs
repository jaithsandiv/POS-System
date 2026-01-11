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
using POS.PAL.Helpers;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SellPayment_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable salePaymentsTable;

        public UC_SellPayment_Report()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadSalePayments();

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
                txtSearch.Properties.NullValuePrompt = "Search sale payments...";
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
        /// Performs search filtering on the sale payments grid
        /// </summary>
        private void PerformSearch(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // If search is empty, show all records
                    LoadSalePayments();
                    return;
                }

                // Use BLL search method
                salePaymentsTable = _bllSalesTerminal.SearchSalePayments(searchText);
                gridSellPayments.DataSource = salePaymentsTable;
                gridViewSellPayments.RefreshData();
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
            if (gridViewSellPayments == null) return;

            // Configure Reference Number column
            colReferenceNumber.FieldName = "reference_number";
            colReferenceNumber.Caption = "Reference Number";
            colReferenceNumber.Width = 150;
            colReferenceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colReferenceNumber.OptionsColumn.AllowEdit = false;
            colReferenceNumber.OptionsColumn.AllowFocus = false;
            colReferenceNumber.OptionsColumn.FixedWidth = true;
            colReferenceNumber.Visible = true;

            // Configure Paid On column
            colPaidOn.FieldName = "paid_on";
            colPaidOn.Caption = "Paid On";
            colPaidOn.Width = 150;
            colPaidOn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaidOn.OptionsColumn.AllowEdit = false;
            colPaidOn.OptionsColumn.AllowFocus = false;
            colPaidOn.OptionsColumn.FixedWidth = true;
            colPaidOn.Visible = true;

            // Configure Amount column
            colAmount.FieldName = "amount";
            colAmount.Caption = "Amount";
            colAmount.Width = 120;
            colAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colAmount.DisplayFormat.FormatString = "n2";
            colAmount.OptionsColumn.AllowEdit = false;
            colAmount.OptionsColumn.AllowFocus = false;
            colAmount.OptionsColumn.FixedWidth = true;
            colAmount.Visible = true;

            // Configure Customer column
            colCustomer.FieldName = "customer";
            colCustomer.Caption = "Customer";
            colCustomer.Width = 200;
            colCustomer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomer.OptionsColumn.AllowEdit = false;
            colCustomer.OptionsColumn.AllowFocus = false;
            colCustomer.Visible = true;

            // Configure Customer Group column
            colCustomerGroup.FieldName = "customer_group";
            colCustomerGroup.Caption = "Customer Group";
            colCustomerGroup.Width = 150;
            colCustomerGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomerGroup.OptionsColumn.AllowEdit = false;
            colCustomerGroup.OptionsColumn.AllowFocus = false;
            colCustomerGroup.OptionsColumn.FixedWidth = true;
            colCustomerGroup.Visible = true;

            // Configure Payment Method column
            colPaymentMethod.FieldName = "payment_method";
            colPaymentMethod.Caption = "Payment Method";
            colPaymentMethod.Width = 150;
            colPaymentMethod.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaymentMethod.OptionsColumn.AllowEdit = false;
            colPaymentMethod.OptionsColumn.AllowFocus = false;
            colPaymentMethod.OptionsColumn.FixedWidth = true;
            colPaymentMethod.Visible = true;

            // Configure View button column
            ConfigureViewButton();

            // Apply grid appearance styling
            gridViewSellPayments.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewSellPayments.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSellPayments.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSellPayments.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSellPayments.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            
            gridViewSellPayments.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gridViewSellPayments.Appearance.Row.Options.UseFont = true;
            
            // Set row and column panel heights
            gridViewSellPayments.ColumnPanelRowHeight = 44;
            gridViewSellPayments.RowHeight = 44;

            // Grid view options
            gridViewSellPayments.OptionsView.ShowGroupPanel = false;
            gridViewSellPayments.OptionsView.ShowIndicator = false;
            gridViewSellPayments.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSellPayments.OptionsView.ShowAutoFilterRow = false;
            
            gridViewSellPayments.OptionsBehavior.Editable = true;
            gridViewSellPayments.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSellPayments.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSellPayments.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSellPayments.OptionsSelection.MultiSelect = false;
            
            gridViewSellPayments.OptionsCustomization.AllowFilter = false;
            gridViewSellPayments.OptionsCustomization.AllowGroup = false;
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
            ViewSaleDetails();
        }

        /// <summary>
        /// Opens the sale details popup
        /// </summary>
        private void ViewSaleDetails()
        {
            try
            {
                if (gridViewSellPayments.FocusedRowHandle < 0)
                {
                    return;
                }

                DataRow selectedRow = gridViewSellPayments.GetDataRow(gridViewSellPayments.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int saleId = Convert.ToInt32(selectedRow["sale_id"]);

                // Open sale details popup
                using (var detailsForm = new Form_SaleDetails(saleId))
                {
                    detailsForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error viewing sale details: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Loads all sale payments from database
        /// </summary>
        public void LoadSalePayments()
        {
            try
            {
                salePaymentsTable = _bllSalesTerminal.GetSalePayments();
                gridSellPayments.DataSource = salePaymentsTable;
                gridViewSellPayments.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading sale payments: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Exports sale payments data to CSV format
        /// </summary>
        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if (salePaymentsTable == null || salePaymentsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale payments data to export.",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"SalePayments_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide View column
                    colView.Visible = false;

                    try
                    {
                        // Export grid to CSV using DevExpress export functionality
                        gridViewSellPayments.ExportToCsv(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Sale payments data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports sale payments data to Excel format
        /// </summary>
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (salePaymentsTable == null || salePaymentsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale payments data to export.",
                        "Export Excel",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FileName = $"SalePayments_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide View column
                    colView.Visible = false;

                    try
                    {
                        // Export grid to Excel using DevExpress export functionality
                        gridViewSellPayments.ExportToXlsx(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Sale payments data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Exports sale payments data to PDF format
        /// </summary>
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (salePaymentsTable == null || salePaymentsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale payments data to export.",
                        "Export PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"SalePayments_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                    DefaultExt = "pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Temporarily hide View column
                    colView.Visible = false;

                    try
                    {
                        // Export grid to PDF using DevExpress export functionality
                        gridViewSellPayments.ExportToPdf(saveFileDialog.FileName);

                        XtraMessageBox.Show(
                            $"Sale payments data exported successfully to:\n{saveFileDialog.FileName}",
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
        /// Prints the sale payments data using Windows default print dialog with preview
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (salePaymentsTable == null || salePaymentsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sale payments data to print.",
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
                    // Create print link using ReportHelper
                    PrintableComponentLink printLink = ReportHelper.CreatePrintLink(
                        gridSellPayments, 
                        "Sale Payments Report", 
                        landscape: true);

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
                    $"Error printing sale payments data: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
