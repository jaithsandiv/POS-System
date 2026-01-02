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

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SellReturn_Addition : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private readonly BLL_SalesReturn _bllSalesReturn = new BLL_SalesReturn();
        
        private DataTable saleItemsTable;
        private int currentSaleId = 0;

        public UC_SellReturn_Addition()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeDataTable();
        }

        /// <summary>
        /// Initialize event handlers for controls
        /// </summary>
        private void InitializeEventHandlers()
        {
            btnLoadSale.Click += BtnLoadSale_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            txtSaleId.KeyDown += TxtSaleId_KeyDown;
            
            // Wire up grid view events for real-time calculation
            gridViewSaleItems.CellValueChanged += GridViewSaleItems_CellValueChanged;
        }

        /// <summary>
        /// Initialize the data table for sale items
        /// </summary>
        private void InitializeDataTable()
        {
            saleItemsTable = new DataTable();
            saleItemsTable.Columns.Add("sale_item_id", typeof(int));
            saleItemsTable.Columns.Add("product_id", typeof(int));
            saleItemsTable.Columns.Add("product_name", typeof(string));
            saleItemsTable.Columns.Add("product_code", typeof(string));
            saleItemsTable.Columns.Add("unit", typeof(string));
            saleItemsTable.Columns.Add("quantity", typeof(decimal));
            saleItemsTable.Columns.Add("return_quantity", typeof(decimal));
            saleItemsTable.Columns.Add("unit_price", typeof(decimal));
            saleItemsTable.Columns.Add("refund_amount", typeof(decimal));
            saleItemsTable.Columns.Add("selected", typeof(bool));

            gridControlSaleItems.DataSource = saleItemsTable;
        }

        /// <summary>
        /// Handle cell value changed in grid view
        /// </summary>
        private void GridViewSaleItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            DataRow row = gridViewSaleItems.GetDataRow(e.RowHandle);
            if (row == null) return;

            // Handle selection checkbox change
            if (e.Column.FieldName == "selected")
            {
                bool isSelected = Convert.ToBoolean(row["selected"]);
                
                if (!isSelected)
                {
                    // If unchecked, reset return quantity to 0
                    row["return_quantity"] = 0;
                    row["refund_amount"] = 0;
                }
                else
                {
                    // If checked, set return quantity to quantity sold
                    row["return_quantity"] = row["quantity"];
                    CalculateRefundAmount(row);
                }

                CalculateTotalRefund();
            }
            // Handle return quantity change
            else if (e.Column.FieldName == "return_quantity")
            {
                decimal returnQty = Convert.ToDecimal(row["return_quantity"] ?? 0);
                decimal quantitySold = Convert.ToDecimal(row["quantity"]);

                // Validate return quantity
                if (returnQty > quantitySold)
                {
                    XtraMessageBox.Show(
                        $"Return quantity cannot exceed quantity sold ({quantitySold:N2}).",
                        "Invalid Quantity",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    row["return_quantity"] = quantitySold;
                    returnQty = quantitySold;
                }

                if (returnQty < 0)
                {
                    row["return_quantity"] = 0;
                    returnQty = 0;
                }

                // Auto-check if return quantity > 0
                if (returnQty > 0)
                {
                    row["selected"] = true;
                }
                else
                {
                    row["selected"] = false;
                }

                CalculateRefundAmount(row);
                CalculateTotalRefund();
            }
        }

        /// <summary>
        /// Calculate refund amount for a row
        /// </summary>
        private void CalculateRefundAmount(DataRow row)
        {
            decimal returnQty = Convert.ToDecimal(row["return_quantity"] ?? 0);
            decimal unitPrice = Convert.ToDecimal(row["unit_price"]);
            decimal refundAmount = returnQty * unitPrice;
            row["refund_amount"] = refundAmount;
        }

        /// <summary>
        /// Calculate total refund amount
        /// </summary>
        private void CalculateTotalRefund()
        {
            decimal totalRefund = 0;

            foreach (DataRow row in saleItemsTable.Rows)
            {
                bool isSelected = Convert.ToBoolean(row["selected"]);
                if (isSelected)
                {
                    totalRefund += Convert.ToDecimal(row["refund_amount"]);
                }
            }

            txtTotalRefund.Text = $"Rs. {totalRefund:N2}";
        }

        /// <summary>
        /// Handle Enter key press in Sale ID textbox
        /// </summary>
        private void TxtSaleId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                LoadSale();
            }
        }

        /// <summary>
        /// Handle Load Sale button click
        /// </summary>
        private void BtnLoadSale_Click(object sender, EventArgs e)
        {
            LoadSale();
        }

        /// <summary>
        /// Load sale information and items
        /// </summary>
        private void LoadSale()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSaleId.Text))
                {
                    XtraMessageBox.Show(
                        "Please enter a Sale ID.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtSaleId.Focus();
                    return;
                }

                if (!int.TryParse(txtSaleId.Text, out int saleId) || saleId <= 0)
                {
                    XtraMessageBox.Show(
                        "Please enter a valid Sale ID.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtSaleId.Focus();
                    return;
                }

                // Load sale information
                DataTable saleData = _bllSalesTerminal.GetSale(saleId);

                if (saleData == null || saleData.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        $"Sale ID {saleId} not found.",
                        "Sale Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DataRow saleRow = saleData.Rows[0];

                // Check if sale is valid for return (status must be A and sale_type must be SALE)
                string status = saleRow["status"]?.ToString();
                string saleType = saleRow["sale_type"]?.ToString();

                if (status != "A")
                {
                    XtraMessageBox.Show(
                        "This sale is not active and cannot be returned.",
                        "Invalid Sale",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (saleType != "SALE")
                {
                    XtraMessageBox.Show(
                        $"Only completed sales can be returned. This is a {saleType}.",
                        "Invalid Sale Type",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Display sale information
                currentSaleId = saleId;
                txtInvoiceNumber.Text = saleRow["invoice_number"]?.ToString() ?? "N/A";
                txtSaleDate.Text = saleRow["created_date"]?.ToString() ?? "N/A";
                txtGrandTotal.Text = $"Rs. {Convert.ToDecimal(saleRow["grand_total"] ?? 0):N2}";

                // Load customer information
                int? customerId = saleRow["customer_id"] != DBNull.Value 
                    ? Convert.ToInt32(saleRow["customer_id"]) 
                    : (int?)null;

                if (customerId.HasValue)
                {
                    DataTable customers = _bllSalesTerminal.GetCustomers();
                    DataRow[] customerRows = customers.Select($"customer_id = {customerId.Value}");
                    if (customerRows.Length > 0)
                    {
                        txtCustomer.Text = customerRows[0]["full_name"]?.ToString();
                    }
                    else
                    {
                        txtCustomer.Text = "Walk-In Customer";
                    }
                }
                else
                {
                    txtCustomer.Text = "Walk-In Customer";
                }

                // Load sale items
                DataTable items = _bllSalesTerminal.GetSaleItems(saleId);

                if (items == null || items.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No items found for this sale.",
                        "No Items",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Clear existing items
                saleItemsTable.Clear();

                // Populate items table
                foreach (DataRow item in items.Rows)
                {
                    DataRow newRow = saleItemsTable.NewRow();
                    newRow["sale_item_id"] = item["sale_item_id"];
                    newRow["product_id"] = item["product_id"];
                    newRow["product_name"] = item["product_name"];
                    newRow["product_code"] = item["product_code"];
                    newRow["unit"] = item["unit"];
                    newRow["quantity"] = item["quantity"];
                    newRow["return_quantity"] = 0;
                    newRow["unit_price"] = item["unit_price"];
                    newRow["refund_amount"] = 0;
                    newRow["selected"] = false;
                    saleItemsTable.Rows.Add(newRow);
                }

                gridViewSaleItems.RefreshData();
                gridViewSaleItems.BestFitColumns();

                // Reset total refund
                txtTotalRefund.Text = "Rs. 0.00";

                XtraMessageBox.Show(
                    $"Sale loaded successfully with {items.Rows.Count} items.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading sale: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Save button click
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate sale is loaded
                if (currentSaleId == 0)
                {
                    XtraMessageBox.Show(
                        "Please load a sale first.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Get selected items
                DataRow[] selectedRows = saleItemsTable.Select("selected = True AND return_quantity > 0");

                if (selectedRows.Length == 0)
                {
                    XtraMessageBox.Show(
                        "Please select at least one item to return.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Calculate total refund
                decimal totalRefund = 0;
                foreach (DataRow row in selectedRows)
                {
                    totalRefund += Convert.ToDecimal(row["refund_amount"]);
                }

                // Confirm return
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to process this return?\n\n" +
                    $"Items to return: {selectedRows.Length}\n" +
                    $"Total refund amount: Rs. {totalRefund:N2}",
                    "Confirm Return",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Create return items table
                DataTable returnItems = new DataTable();
                returnItems.Columns.Add("sale_item_id", typeof(int));
                returnItems.Columns.Add("product_id", typeof(int));
                returnItems.Columns.Add("quantity", typeof(decimal));
                returnItems.Columns.Add("refund_amount", typeof(decimal));

                foreach (DataRow row in selectedRows)
                {
                    DataRow newRow = returnItems.NewRow();
                    newRow["sale_item_id"] = row["sale_item_id"];
                    newRow["product_id"] = row["product_id"];
                    newRow["quantity"] = row["return_quantity"];
                    newRow["refund_amount"] = row["refund_amount"];
                    returnItems.Rows.Add(newRow);
                }

                // Get current user ID
                int processedBy = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    processedBy = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Get return reason
                string reason = memoReason.Text.Trim();
                if (string.IsNullOrWhiteSpace(reason))
                {
                    reason = "No reason provided";
                }

                // Save the return
                int returnId = _bllSalesReturn.SaveSaleReturn(
                    currentSaleId,
                    totalRefund,
                    reason,
                    processedBy,
                    returnItems
                );

                XtraMessageBox.Show(
                    $"Sale return processed successfully!\n\n" +
                    $"Return ID: {returnId}\n" +
                    $"Refund Amount: Rs. {totalRefund:N2}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Navigate back to management screen
                Main.Instance.LoadUserControl(new UC_SellReturn_Management());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error processing return: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Cancel button click
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var result = XtraMessageBox.Show(
                "Are you sure you want to cancel? Any unsaved changes will be lost.",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Main.Instance.LoadUserControl(new UC_SellReturn_Management());
            }
        }
    }
}
