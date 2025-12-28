namespace POS.PAL.Forms
{
    partial class Form_ReceivePayment
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbInvoice = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPaymentMethod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtReference = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblBalanceDue = new DevExpress.XtraEditors.LabelControl();

            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReference.Properties)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Text = "Receive Payment";

            // lblCustomerName
            this.lblCustomerName.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblCustomerName.Location = new System.Drawing.Point(20, 60);
            this.lblCustomerName.Text = "Customer: John Doe";

            // lblBalanceDue
            this.lblBalanceDue.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBalanceDue.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceDue.Location = new System.Drawing.Point(20, 90);
            this.lblBalanceDue.Text = "Total Due: 0.00";

            // Invoice Selection
            this.labelControl1.Location = new System.Drawing.Point(20, 130);
            this.labelControl1.Text = "Select Invoice:";
            
            this.cmbInvoice.Location = new System.Drawing.Point(20, 150);
            this.cmbInvoice.Size = new System.Drawing.Size(300, 30);
            this.cmbInvoice.Properties.NullText = "Select an invoice to pay...";

            // Payment Date
            this.labelControl3.Location = new System.Drawing.Point(20, 190);
            this.labelControl3.Text = "Payment Date:";

            this.dtpDate.Location = new System.Drawing.Point(20, 210);
            this.dtpDate.Size = new System.Drawing.Size(300, 30);

            // Payment Method
            this.labelControl4.Location = new System.Drawing.Point(20, 250);
            this.labelControl4.Text = "Payment Method:";

            this.cmbPaymentMethod.Location = new System.Drawing.Point(20, 270);
            this.cmbPaymentMethod.Size = new System.Drawing.Size(300, 30);
            this.cmbPaymentMethod.Properties.Items.AddRange(new object[] { "CASH", "CARD", "BANK_TRANSFER", "CHEQUE" });

            // Amount
            this.labelControl2.Location = new System.Drawing.Point(20, 310);
            this.labelControl2.Text = "Amount:";

            this.txtAmount.Location = new System.Drawing.Point(20, 330);
            this.txtAmount.Size = new System.Drawing.Size(300, 30);
            this.txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtAmount.Properties.Mask.EditMask = "n2";

            // Reference
            this.labelControl5.Location = new System.Drawing.Point(20, 370);
            this.labelControl5.Text = "Reference / Note:";

            this.txtReference.Location = new System.Drawing.Point(20, 390);
            this.txtReference.Size = new System.Drawing.Size(300, 30);

            // Buttons
            this.btnSave.Location = new System.Drawing.Point(130, 450);
            this.btnSave.Size = new System.Drawing.Size(90, 40);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(230, 450);
            this.btnCancel.Size = new System.Drawing.Size(90, 40);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(350, 520);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.lblBalanceDue);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbInvoice);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Receive Payment";

            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReference.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cmbInvoice;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPaymentMethod;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtReference;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblBalanceDue;
    }
}