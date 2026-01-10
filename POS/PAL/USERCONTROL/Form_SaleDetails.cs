using DevExpress.XtraEditors;
using POS.BLL;
using POS.PAL.REPORT;
using POS.PAL.REPORT.SUBREPORT;
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
        /// Prints the sale invoice (same as original invoice)
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (saleData == null || saleData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No sale data to print.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (saleItemsData == null || saleItemsData.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No sale items to print.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow sale = saleData.Rows[0];

                // Get sale details
                string invoiceNumber = sale["invoice_number"]?.ToString() ?? "N/A";
                decimal totalAmount = Convert.ToDecimal(sale["total_amount"]);
                decimal discountValue = Convert.ToDecimal(sale["discount_value"]);
                decimal grandTotal = Convert.ToDecimal(sale["grand_total"]);
                decimal totalPaid = Convert.ToDecimal(sale["total_paid"]);
                decimal changeDue = Convert.ToDecimal(sale["change_due"]);
                decimal due = grandTotal - totalPaid;
                if (due < 0) due = 0;

                // Get customer details
                string customerName = GetCustomerName(sale["customer_id"]);

                // Check print settings
                bool enableThermal = Main.GetSetting("ENABLE_THERMAL_PRINT", "False").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool enableA4 = Main.GetSetting("ENABLE_A4_PRINT", "True").Equals("True", StringComparison.OrdinalIgnoreCase);

                if (enableThermal)
                {
                    // Print thermal invoice
                    PrintThermalInvoice(invoiceNumber, grandTotal, totalPaid, due, customerName);
                }
                else if (enableA4)
                {
                    // Print A4 invoice
                    PrintA4Invoice(invoiceNumber, totalAmount, discountValue, grandTotal, totalPaid, due, customerName);
                }
                else
                {
                    // Default to A4 if neither is explicitly enabled
                    PrintA4Invoice(invoiceNumber, totalAmount, discountValue, grandTotal, totalPaid, due, customerName);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error printing invoice: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Prints the A4 invoice report
        /// </summary>
        private void PrintA4Invoice(string invoiceNumber, decimal totalAmount, decimal discountValue, 
            decimal grandTotal, decimal totalPaid, decimal due, string customerName)
        {
            // Create A4 invoice report
            Invoice invoiceReport = new Invoice();

            // Set footer text from settings
            string footerText = Main.GetSetting("invoice_footer", "Thank You For Your Business!");
            invoiceReport.Parameters["p_footer"].Value = footerText;

            // Bind main report data (sale items)
            invoiceReport.DataSource = saleItemsData;

            // Set main invoice parameters
            invoiceReport.Parameters["p_invoice_no"].Value = invoiceNumber;
            invoiceReport.Parameters["p_date"].Value = Convert.ToDateTime(saleData.Rows[0]["created_date"]).ToString("yyyy-MM-dd HH:mm:ss");
            invoiceReport.Parameters["p_total"].Value = totalAmount.ToString("F2");
            invoiceReport.Parameters["p_discount"].Value = discountValue.ToString("F2");
            invoiceReport.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

            // Set customer details
            invoiceReport.Parameters["p_customer_name"].Value = customerName;
            
            // Get store details for contact info
            if (Main.DataSetApp.Store.Rows.Count > 0)
            {
                var storeRow = Main.DataSetApp.Store[0];
                invoiceReport.Parameters["p_email"].Value = storeRow.IsemailNull() ? "" : storeRow.email;
                invoiceReport.Parameters["p_contact"].Value = storeRow.IsphoneNull() ? "" : storeRow.phone;
                invoiceReport.Parameters["p_address"].Value = storeRow.IsaddressNull() ? "" : storeRow.address;
            }

            // Configure subreport for payments
            if (invoiceReport.PaymentSubreport?.ReportSource is SR_Payment subreport)
            {
                subreport.DataSource = salePaymentsData;
                subreport.Parameters["p_total_paid"].Value = totalPaid.ToString("F2");
                subreport.Parameters["p_due"].Value = due.ToString("F2");
            }

            // Print directly
            DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(invoiceReport);
            printTool.Print();
        }

        /// <summary>
        /// Prints the thermal (80mm) invoice report
        /// </summary>
        private void PrintThermalInvoice(string invoiceNumber, decimal grandTotal, 
            decimal totalPaid, decimal due, string customerName)
        {
            // Create thermal invoice report
            ThermalInvoice thermalInvoice = new ThermalInvoice();

            // Set footer text from settings
            string footerText = Main.GetSetting("invoice_footer", "Thank You!");
            thermalInvoice.Parameters["p_footer"].Value = footerText;

            // Set main invoice parameters
            thermalInvoice.Parameters["p_invoice_no"].Value = invoiceNumber;
            thermalInvoice.Parameters["p_date"].Value = Convert.ToDateTime(saleData.Rows[0]["created_date"]).ToString("yyyy-MM-dd HH:mm:ss");
            thermalInvoice.Parameters["p_customer_name"].Value = customerName;
            thermalInvoice.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

            // Get store details from Main.DataSetApp
            if (Main.DataSetApp.Store.Rows.Count > 0)
            {
                var storeRow = Main.DataSetApp.Store[0];
                thermalInvoice.Parameters["p_contact"].Value = storeRow.IsphoneNull() ? "" : storeRow.phone;
                thermalInvoice.Parameters["p_email"].Value = storeRow.IsemailNull() ? "" : storeRow.email;
                thermalInvoice.Parameters["p_address"].Value = storeRow.IsaddressNull() ? "" : storeRow.address;
            }

            // Set data source for items
            thermalInvoice.DataSource = saleItemsData;

            // Calculate totals from saleItemsData
            decimal subtotal = 0m;
            foreach (DataRow item in saleItemsData.Rows)
            {
                if (decimal.TryParse(item["subtotal"]?.ToString(), out decimal itemTotal))
                {
                    subtotal += itemTotal;
                }
            }

            // Get discount from sale data
            decimal discountAmount = 0m;
            if (saleData.Rows.Count > 0)
            {
                DataRow saleRow = saleData.Rows[0];
                if (decimal.TryParse(saleRow["discount_value"]?.ToString(), out decimal discountValue))
                {
                    string discountType = saleRow["discount_type"]?.ToString();
                    if (discountType == "PERCENTAGE")
                    {
                        discountAmount = subtotal * (discountValue / 100m);
                    }
                    else
                    {
                        discountAmount = discountValue;
                    }
                }
            }

            thermalInvoice.Parameters["p_total"].Value = subtotal.ToString("F2");
            thermalInvoice.Parameters["p_discount"].Value = "-" + discountAmount.ToString("F2");

            // Bind payment subreport (thermal version)
            if (thermalInvoice.PaymentSubreport != null)
            {
                SR_ThermalPayment paymentReport = new SR_ThermalPayment();
                paymentReport.DataSource = salePaymentsData;

                // Calculate change
                decimal change = Math.Max(0, totalPaid - grandTotal);

                paymentReport.Parameters["p_total_paid"].Value = totalPaid.ToString("F2");
                paymentReport.Parameters["p_due"].Value = due.ToString("F2");
                paymentReport.Parameters["p_change"].Value = change.ToString("F2");

                thermalInvoice.PaymentSubreport.ReportSource = paymentReport;
            }

            // Get thermal printer name from system settings
            string printerName = Main.GetSetting("thermal_printer_name", null);

            // Configure printer if specified
            if (!string.IsNullOrEmpty(printerName))
            {
                thermalInvoice.PrinterName = printerName;
            }

            // Print directly
            thermalInvoice.Print();
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
