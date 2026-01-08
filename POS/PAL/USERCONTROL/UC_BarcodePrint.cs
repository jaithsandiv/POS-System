using DevExpress.XtraEditors;
using POS.BLL;
using POS.PAL.REPORT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_BarcodePrint : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private DataTable _allBarcodePrints; // Store all data for local filtering

        public UC_BarcodePrint()
        {
            InitializeComponent();
            LoadProducts();
            ConfigureGrid();
            LoadData();

            // Wire up search events
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
            if (txtSearch != null)
            {
                txtSearch.KeyDown += txtSearch_KeyDown;
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
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void PerformSearch()
        {
            try
            {
                string keyword = txtSearch.Text.Trim().ToLower();
                
                if (_allBarcodePrints == null || _allBarcodePrints.Rows.Count == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(keyword))
                {
                    // If search is empty, show all data
                    gridBarcodePrints.DataSource = _allBarcodePrints;
                    gridView1.RefreshData();
                    return;
                }

                // Filter across all columns
                DataTable filteredTable = _allBarcodePrints.Clone();
                
                foreach (DataRow row in _allBarcodePrints.Rows)
                {
                    bool match = false;
                    
                    // Check each column for a match
                    foreach (DataColumn column in _allBarcodePrints.Columns)
                    {
                        if (row[column] != null && row[column] != DBNull.Value)
                        {
                            string cellValue = row[column].ToString().ToLower();
                            if (cellValue.Contains(keyword))
                            {
                                match = true;
                                break;
                            }
                        }
                    }
                    
                    if (match)
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                gridBarcodePrints.DataSource = filteredTable;
                gridView1.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                DataTable dt = _bllProducts.GetProducts();
                lueProduct.Properties.DataSource = dt;
                lueProduct.Properties.DisplayMember = "product_name";
                lueProduct.Properties.ValueMember = "product_id";
                lueProduct.Properties.Columns.Clear();
                lueProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("product_code", "Code"));
                lueProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("product_name", "Name"));
                lueProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("selling_price", "Price"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ConfigureGrid()
        {
            gridView1.Columns.Clear();

            var colProduct = gridView1.Columns.AddVisible("product_name", "Product");
            var colCode = gridView1.Columns.AddVisible("product_code", "Code");
            var colQty = gridView1.Columns.AddVisible("quantity_printed", "Qty");
            var colName = gridView1.Columns.AddVisible("include_name", "Inc. Name");
            var colPrice = gridView1.Columns.AddVisible("include_price", "Inc. Price");
            var colExpiry = gridView1.Columns.AddVisible("include_expiry", "Inc. Expiry");
        }

        private void LoadData()
        {
            try
            {
                _allBarcodePrints = _bllProducts.GetBarcodePrints();
                gridBarcodePrints.DataSource = _allBarcodePrints;
                gridView1.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lueProduct.EditValue == null)
            {
                XtraMessageBox.Show("Please select a product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int productId = Convert.ToInt32(lueProduct.EditValue);
                int qty = Convert.ToInt32(numQuantity.Value);
                bool incName = chkName.Checked;
                bool incPrice = chkPrice.Checked;
                bool incExpiry = chkExpiry.Checked;
                bool incMan = chkManufacture.Checked;
                bool incPromo = chkPromo.Checked;
                int userId = 1;

                _bllProducts.InsertBarcodePrint(productId, qty, incName, incPrice, incExpiry, incMan, incPromo, userId);
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to clear the list?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _bllProducts.ClearBarcodePrints();
                LoadData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Get barcode print queue with product details
                DataTable printQueue = _bllProducts.GetBarcodePrintDetails();

                if (printQueue == null || printQueue.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No labels in the print queue. Please add products first.", 
                        "Empty Queue", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create a DataTable to hold expanded rows (one row per label copy)
                DataTable expandedData = printQueue.Clone();

                foreach (DataRow row in printQueue.Rows)
                {
                    int quantity = Convert.ToInt32(row["quantity_printed"]);
                    
                    // Create 'quantity' number of copies for each product
                    for (int i = 0; i < quantity; i++)
                    {
                        DataRow newRow = expandedData.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];
                        expandedData.Rows.Add(newRow);
                    }
                }

                if (expandedData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No labels to print.", "Empty Queue", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create and configure the barcode label report
                BarcodeLabel labelReport = new BarcodeLabel();
                labelReport.DataSource = expandedData;

                // Get the first row to check configuration options
                bool includeName = true;
                bool includePrice = true;
                bool includeExpiry = false;
                bool includeManufacture = false;
                bool includePromo = false;

                if (printQueue.Rows.Count > 0)
                {
                    DataRow firstRow = printQueue.Rows[0];
                    includeName = firstRow["include_name"] != DBNull.Value && Convert.ToBoolean(firstRow["include_name"]);
                    includePrice = firstRow["include_price"] != DBNull.Value && Convert.ToBoolean(firstRow["include_price"]);
                    includeExpiry = firstRow["include_expiry"] != DBNull.Value && Convert.ToBoolean(firstRow["include_expiry"]);
                    includeManufacture = firstRow["include_manufacture"] != DBNull.Value && Convert.ToBoolean(firstRow["include_manufacture"]);
                    includePromo = firstRow["include_promo_price"] != DBNull.Value && Convert.ToBoolean(firstRow["include_promo_price"]);
                }

                // Configure visibility based on flags
                labelReport.ConfigureLabel(includeName, includePrice, includeExpiry, includeManufacture, includePromo);

                // Get barcode printer name from system settings (optional)
                string printerName = Main.GetSetting("barcode_printer_name", null);

                if (!string.IsNullOrEmpty(printerName))
                {
                    labelReport.PrinterName = printerName;
                }

                // Show print preview dialog
                DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(labelReport);
                printTool.ShowPreviewDialog();

                // After successful preview/print, ask user if they want to clear the list
                var result = XtraMessageBox.Show(
                    $"{expandedData.Rows.Count} barcode label(s) were prepared for printing.\n\nDo you want to clear the print queue?", 
                    "Print Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Clear the barcode print list
                    _bllProducts.ClearBarcodePrints();
                    LoadData();
                    txtSearch.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error printing labels: {ex.Message}", 
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
