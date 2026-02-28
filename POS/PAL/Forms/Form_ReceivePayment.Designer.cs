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

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            pnlHeader = new DevExpress.XtraEditors.PanelControl();
            lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            lblTitle = new DevExpress.XtraEditors.LabelControl();
            pnlDue = new DevExpress.XtraEditors.PanelControl();
            lblBalanceDue = new DevExpress.XtraEditors.LabelControl();
            pnlBody = new DevExpress.XtraEditors.PanelControl();
            txtReference = new DevExpress.XtraEditors.TextEdit();
            labelControl5 = new DevExpress.XtraEditors.LabelControl();
            txtAmount = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            cmbPaymentMethod = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            dtpDate = new DevExpress.XtraEditors.DateEdit();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            cmbInvoice = new DevExpress.XtraEditors.LookUpEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            pnlButtons = new DevExpress.XtraEditors.PanelControl();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)pnlHeader).BeginInit();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlDue).BeginInit();
            pnlDue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlBody).BeginInit();
            pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtReference.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAmount.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbPaymentMethod.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpDate.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpDate.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbInvoice.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pnlButtons).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            pnlHeader.Appearance.Options.UseBackColor = true;
            pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlHeader.Controls.Add(lblCustomerName);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(440, 80);
            pnlHeader.TabIndex = 0;
            // 
            // lblCustomerName
            // 
            lblCustomerName.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblCustomerName.Appearance.ForeColor = System.Drawing.Color.FromArgb(220, 255, 250);
            lblCustomerName.Appearance.Options.UseFont = true;
            lblCustomerName.Appearance.Options.UseForeColor = true;
            lblCustomerName.Location = new System.Drawing.Point(20, 46);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new System.Drawing.Size(68, 17);
            lblCustomerName.TabIndex = 1;
            lblCustomerName.Text = "Customer: -";
            // 
            // lblTitle
            // 
            lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            lblTitle.Appearance.Options.UseFont = true;
            lblTitle.Appearance.Options.UseForeColor = true;
            lblTitle.Location = new System.Drawing.Point(20, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(147, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Receive Payment";
            // 
            // pnlDue
            // 
            pnlDue.Appearance.BackColor = System.Drawing.Color.FromArgb(240, 253, 250);
            pnlDue.Appearance.Options.UseBackColor = true;
            pnlDue.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            pnlDue.Controls.Add(lblBalanceDue);
            pnlDue.Location = new System.Drawing.Point(20, 14);
            pnlDue.Name = "pnlDue";
            pnlDue.Size = new System.Drawing.Size(400, 38);
            pnlDue.TabIndex = 0;
            // 
            // lblBalanceDue
            // 
            lblBalanceDue.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            lblBalanceDue.Appearance.ForeColor = System.Drawing.Color.FromArgb(4, 140, 115);
            lblBalanceDue.Appearance.Options.UseFont = true;
            lblBalanceDue.Appearance.Options.UseForeColor = true;
            lblBalanceDue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblBalanceDue.Location = new System.Drawing.Point(10, 8);
            lblBalanceDue.Name = "lblBalanceDue";
            lblBalanceDue.Size = new System.Drawing.Size(376, 22);
            lblBalanceDue.TabIndex = 0;
            lblBalanceDue.Text = "Total Due: 0.00";
            // 
            // pnlBody
            // 
            pnlBody.Appearance.BackColor = System.Drawing.Color.White;
            pnlBody.Appearance.Options.UseBackColor = true;
            pnlBody.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlBody.Controls.Add(txtReference);
            pnlBody.Controls.Add(labelControl5);
            pnlBody.Controls.Add(txtAmount);
            pnlBody.Controls.Add(labelControl2);
            pnlBody.Controls.Add(cmbPaymentMethod);
            pnlBody.Controls.Add(labelControl4);
            pnlBody.Controls.Add(dtpDate);
            pnlBody.Controls.Add(labelControl3);
            pnlBody.Controls.Add(cmbInvoice);
            pnlBody.Controls.Add(labelControl1);
            pnlBody.Controls.Add(pnlDue);
            pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBody.Location = new System.Drawing.Point(0, 80);
            pnlBody.Name = "pnlBody";
            pnlBody.Size = new System.Drawing.Size(440, 436);
            pnlBody.TabIndex = 1;
            // 
            // txtReference
            // 
            txtReference.Location = new System.Drawing.Point(20, 371);
            txtReference.Name = "txtReference";
            txtReference.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtReference.Properties.Appearance.Options.UseFont = true;
            txtReference.Properties.Padding = new System.Windows.Forms.Padding(5);
            txtReference.Size = new System.Drawing.Size(400, 34);
            txtReference.TabIndex = 10;
            // 
            // labelControl5
            // 
            labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl5.Appearance.Options.UseFont = true;
            labelControl5.Location = new System.Drawing.Point(20, 351);
            labelControl5.Name = "labelControl5";
            labelControl5.Size = new System.Drawing.Size(102, 17);
            labelControl5.TabIndex = 9;
            labelControl5.Text = "Reference / Note";
            // 
            // txtAmount
            // 
            txtAmount.Location = new System.Drawing.Point(20, 302);
            txtAmount.Name = "txtAmount";
            txtAmount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtAmount.Properties.Appearance.Options.UseFont = true;
            txtAmount.Properties.Appearance.Options.UseTextOptions = true;
            txtAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtAmount.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtAmount.Properties.MaskSettings.Set("mask", "n2");
            txtAmount.Properties.Padding = new System.Windows.Forms.Padding(5);
            txtAmount.Size = new System.Drawing.Size(400, 34);
            txtAmount.TabIndex = 8;
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Location = new System.Drawing.Point(20, 282);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(79, 17);
            labelControl2.TabIndex = 7;
            labelControl2.Text = "Amount (Rs.)";
            // 
            // cmbPaymentMethod
            // 
            cmbPaymentMethod.Location = new System.Drawing.Point(20, 235);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            cmbPaymentMethod.Properties.Appearance.Options.UseFont = true;
            cmbPaymentMethod.Properties.Items.AddRange(new object[] { "CASH", "CARD", "BANK_TRANSFER", "CHEQUE" });
            cmbPaymentMethod.Properties.Padding = new System.Windows.Forms.Padding(5);
            cmbPaymentMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbPaymentMethod.Size = new System.Drawing.Size(400, 34);
            cmbPaymentMethod.TabIndex = 6;
            // 
            // labelControl4
            // 
            labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl4.Appearance.Options.UseFont = true;
            labelControl4.Location = new System.Drawing.Point(20, 215);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(106, 17);
            labelControl4.TabIndex = 5;
            labelControl4.Text = "Payment Method";
            // 
            // dtpDate
            // 
            dtpDate.EditValue = new System.DateTime(2026, 2, 28, 0, 0, 0, 0);
            dtpDate.Location = new System.Drawing.Point(20, 164);
            dtpDate.Name = "dtpDate";
            dtpDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dtpDate.Properties.Appearance.Options.UseFont = true;
            dtpDate.Properties.Padding = new System.Windows.Forms.Padding(5);
            dtpDate.Size = new System.Drawing.Size(400, 34);
            dtpDate.TabIndex = 4;
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Location = new System.Drawing.Point(20, 144);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(86, 17);
            labelControl3.TabIndex = 3;
            labelControl3.Text = "Payment Date";
            // 
            // cmbInvoice
            // 
            cmbInvoice.Location = new System.Drawing.Point(20, 96);
            cmbInvoice.Name = "cmbInvoice";
            cmbInvoice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            cmbInvoice.Properties.Appearance.Options.UseFont = true;
            cmbInvoice.Properties.NullText = "Select an invoice...";
            cmbInvoice.Properties.Padding = new System.Windows.Forms.Padding(5);
            cmbInvoice.Size = new System.Drawing.Size(400, 34);
            cmbInvoice.TabIndex = 2;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(20, 76);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(43, 17);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "Invoice";
            // 
            // pnlButtons
            // 
            pnlButtons.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            pnlButtons.Appearance.Options.UseBackColor = true;
            pnlButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlButtons.Location = new System.Drawing.Point(0, 516);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new System.Drawing.Size(440, 64);
            pnlButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            btnSave.Appearance.Options.UseBackColor = true;
            btnSave.Appearance.Options.UseFont = true;
            btnSave.Appearance.Options.UseForeColor = true;
            btnSave.Location = new System.Drawing.Point(155, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(140, 36);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save Payment";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.Appearance.ForeColor = System.Drawing.Color.White;
            btnCancel.Appearance.Options.UseBackColor = true;
            btnCancel.Appearance.Options.UseFont = true;
            btnCancel.Appearance.Options.UseForeColor = true;
            btnCancel.Location = new System.Drawing.Point(305, 13);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(110, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // Form_ReceivePayment
            // 
            ClientSize = new System.Drawing.Size(440, 580);
            Controls.Add(pnlBody);
            Controls.Add(pnlButtons);
            Controls.Add(pnlHeader);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_ReceivePayment";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Receive Payment";
            ((System.ComponentModel.ISupportInitialize)pnlHeader).EndInit();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pnlDue).EndInit();
            pnlDue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlBody).EndInit();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtReference.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAmount.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbPaymentMethod.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpDate.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpDate.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbInvoice.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlButtons).EndInit();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraEditors.PanelControl pnlDue;
        private DevExpress.XtraEditors.LabelControl lblBalanceDue;
        private DevExpress.XtraEditors.PanelControl pnlBody;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cmbInvoice;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPaymentMethod;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtAmount;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtReference;
        private DevExpress.XtraEditors.PanelControl pnlButtons;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}