using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using POS.BLL;

namespace POS.PAL.Forms
{
    public partial class Form_ReceivePayment : DevExpress.XtraEditors.XtraForm
    {
        private readonly int _customerId;
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private readonly BLL_SalesTerminal _bllSales = new BLL_SalesTerminal();
        private bool _hasInvoices = true;

        public bool PaymentSaved { get; private set; } = false;

        public Form_ReceivePayment(int customerId, string customerName)
        {
            InitializeComponent();
            _customerId = customerId;
            lblCustomerName.Text = $"Customer: {customerName}";
            
            LoadUnpaidInvoices();
            dtpDate.DateTime = DateTime.Today;
            cmbPaymentMethod.SelectedIndex = 0; // Default to CASH
            
            // Wire up event
            cmbInvoice.EditValueChanged += cmbInvoice_EditValueChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!_hasInvoices)
                this.Close(); // Close after ShowDialog starts but before the form becomes visible
        }

        private void LoadUnpaidInvoices()
        {
            try
            {
                DataTable dt = _bllContacts.GetUnpaidInvoices(_customerId);
                
                // Add a display column
                if (!dt.Columns.Contains("DisplayMember"))
                {
                    dt.Columns.Add("DisplayMember", typeof(string));
                }

                decimal totalDue = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string invoice = row["invoice_number"].ToString();
                    decimal due = Convert.ToDecimal(row["balance_due"]);
                    string date = Convert.ToDateTime(row["created_date"]).ToString("dd/MM/yyyy");
                    
                    row["DisplayMember"] = $"{invoice} ({date}) - Due: {due:N2}";
                    totalDue += due;
                }

                lblBalanceDue.Text = $"Total Due: {totalDue:N2}";

                cmbInvoice.Properties.DataSource = dt;
                cmbInvoice.Properties.DisplayMember = "DisplayMember";
                cmbInvoice.Properties.ValueMember = "sale_id";
                
                // Add columns to dropdown
                cmbInvoice.Properties.Columns.Clear();
                cmbInvoice.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("invoice_number", "Invoice #"));
                cmbInvoice.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("balance_due", "Due Amount"));

                if (dt.Rows.Count > 0)
                {
                    cmbInvoice.ItemIndex = 0;
                    UpdateAmountFromSelection();
                }
                else
                {
                    _hasInvoices = false;
                    XtraMessageBox.Show("No unpaid invoices found for this customer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbInvoice_EditValueChanged(object sender, EventArgs e)
        {
            UpdateAmountFromSelection();
        }

        private void UpdateAmountFromSelection()
        {
            if (cmbInvoice.EditValue != null)
            {
                DataRowView row = cmbInvoice.GetSelectedDataRow() as DataRowView;
                if (row != null)
                {
                    txtAmount.Text = row["balance_due"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbInvoice.EditValue == null)
            {
                XtraMessageBox.Show("Please select an invoice.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                XtraMessageBox.Show("Please enter a valid amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Handle overpayment — cap the recorded amount at balance_due, give change back
            DataRowView selectedRow = cmbInvoice.GetSelectedDataRow() as DataRowView;
            decimal balanceDue = amount; // fallback: record exactly what was entered
            decimal changeToGive = 0;
            if (selectedRow != null && decimal.TryParse(selectedRow["balance_due"]?.ToString(), out decimal parsedDue))
            {
                balanceDue = parsedDue;
                if (amount > balanceDue + 0.01m)
                {
                    changeToGive = amount - balanceDue;
                    var result = XtraMessageBox.Show(
                        $"Customer paid Rs. {amount:N2} but the outstanding balance is Rs. {balanceDue:N2}.\n\n" +
                        $"⚠ Give change of Rs. {changeToGive:N2} back to the customer.\n\n" +
                        $"The payment will be recorded as Rs. {balanceDue:N2} (the exact due amount).\n" +
                        $"Click OK to confirm.",
                        "Overpayment — Give Change",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);
                    if (result == DialogResult.Cancel)
                        return;
                    // Cap the recorded payment to the actual due amount
                    amount = balanceDue;
                    txtAmount.Text = amount.ToString("F2");
                }
            }

            int saleId = Convert.ToInt32(cmbInvoice.EditValue);
            string paymentMethod = cmbPaymentMethod.Text;
            string reference = txtReference.Text;
            DateTime paymentDate = dtpDate.DateTime;

            try
            {
                DataTable payments = new DataTable();
                payments.Columns.Add("payment_method", typeof(string));
                payments.Columns.Add("amount", typeof(decimal));
                payments.Columns.Add("card_last_four_digits", typeof(string));
                payments.Columns.Add("card_holder_name", typeof(string));
                payments.Columns.Add("card_transaction_number", typeof(string));
                payments.Columns.Add("card_type", typeof(string));
                payments.Columns.Add("bank_reference_number", typeof(string));

                DataRow row = payments.NewRow();
                row["payment_method"] = paymentMethod;
                row["amount"] = amount;
                
                if (paymentMethod == "BANK_TRANSFER" || paymentMethod == "CHEQUE")
                {
                    row["bank_reference_number"] = reference;
                }
                
                payments.Rows.Add(row);

                // Use logged-in user ID from the active session
                int userId = 1; // fallback
                try
                {
                    if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                        userId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }
                catch { /* keep fallback */ }

                _bllSales.SavePayments(saleId, payments, userId);

                XtraMessageBox.Show("Payment received successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PaymentSaved = true;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error saving payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}