using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace POS.PAL.USERCONTROL
{
    public partial class Form_SaleDetails : XtraForm
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private readonly int _saleId;
        private DataTable saleData;
        private DataTable saleItemsData;
        private DataTable salePaymentsData;

        public Form_SaleDetails(int saleId)
        {
            InitializeComponent();
            _saleId = saleId;
            LoadSaleDetails();
        }

        /// <summary>
        /// Loads all sale details including items and payments
        /// </summary>
        private void LoadSaleDetails()
        {
            try
            {
                // Load sale header
                saleData = _bllSalesTerminal.GetSale(_saleId);
                if (saleData == null || saleData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Sale not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load sale items
                saleItemsData = _bllSalesTerminal.GetSaleItems(_saleId);

                // Load sale payments
                salePaymentsData = _bllSalesTerminal.GetSalePayments(_saleId);

                // Display sale details
                DisplaySaleDetails();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading sale details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Displays the sale details in the form controls
        /// </summary>
        private void DisplaySaleDetails()
        {
            if (saleData == null || saleData.Rows.Count == 0)
                return;

            DataRow sale = saleData.Rows[0];

            // Sale header information
            lblInvoiceNumber.Text = sale["invoice_number"]?.ToString() ?? "N/A";
            lblSaleDate.Text = Convert.ToDateTime(sale["created_date"]).ToString("dd/MM/yyyy HH:mm");
            lblCustomer.Text = GetCustomerName(sale["customer_id"]);
            lblBiller.Text = GetBillerName(sale["biller_id"]);

            // Sale amounts
            lblTotalItems.Text = sale["total_items"]?.ToString() ?? "0";
            lblTotalAmount.Text = $"LKR {Convert.ToDecimal(sale["total_amount"]):N2}";
            lblDiscount.Text = $"{sale["discount_type"]?.ToString() ?? "NONE"} - {Convert.ToDecimal(sale["discount_value"]):N2}";
            lblGrandTotal.Text = $"LKR {Convert.ToDecimal(sale["grand_total"]):N2}";
            lblTotalPaid.Text = $"LKR {Convert.ToDecimal(sale["total_paid"]):N2}";
            lblChangeDue.Text = $"LKR {Convert.ToDecimal(sale["change_due"]):N2}";
            lblPaymentStatus.Text = sale["payment_status"]?.ToString() ?? "PENDING";
            lblNotes.Text = sale["notes"]?.ToString() ?? "N/A";

            // Sale items grid
            gridControlSaleItems.DataSource = saleItemsData;
            gridViewSaleItems.BestFitColumns();

            // Sale payments grid
            gridControlPayments.DataSource = salePaymentsData;
            gridViewPayments.BestFitColumns();
        }

        /// <summary>
        /// Gets customer name from customer_id
        /// </summary>
        private string GetCustomerName(object customerId)
        {
            if (customerId == null || customerId == DBNull.Value)
                return "Walk-In Customer";

            try
            {
                // You can use BLL_Contacts to get customer name if needed
                // For now, return a placeholder
                return $"Customer ID: {customerId}";
            }
            catch
            {
                return "Unknown Customer";
            }
        }

        /// <summary>
        /// Gets biller name from biller_id
        /// </summary>
        private string GetBillerName(object billerId)
        {
            if (billerId == null || billerId == DBNull.Value)
                return "Unknown";

            try
            {
                // You can use BLL to get user name if needed
                // For now, return a placeholder
                return $"User ID: {billerId}";
            }
            catch
            {
                return "Unknown Biller";
            }
        }

        /// <summary>
        /// Prints the sale receipt
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a simple print document
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // TODO: Implement receipt printing logic
                    XtraMessageBox.Show("Print functionality will be implemented based on your receipt template.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error printing receipt: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
