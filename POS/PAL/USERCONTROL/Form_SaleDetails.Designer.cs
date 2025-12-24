namespace POS.PAL.USERCONTROL
{
    partial class Form_SaleDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlPayments = new DevExpress.XtraGrid.GridControl();
            this.gridViewPayments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlSaleItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewSaleItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblPaymentStatus = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.lblChangeDue = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalPaid = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblGrandTotal = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lblDiscount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalItems = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblBiller = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lblSaleDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lblInvoiceNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSaleItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSaleItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl3);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1000, 700);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.gridControlPayments);
            this.groupControl3.Location = new System.Drawing.Point(20, 515);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(960, 120);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "Payments";
            // 
            // gridControlPayments
            // 
            this.gridControlPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPayments.Location = new System.Drawing.Point(2, 23);
            this.gridControlPayments.MainView = this.gridViewPayments;
            this.gridControlPayments.Name = "gridControlPayments";
            this.gridControlPayments.Size = new System.Drawing.Size(956, 95);
            this.gridControlPayments.TabIndex = 0;
            this.gridControlPayments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPayments});
            // 
            // gridViewPayments
            // 
            this.gridViewPayments.GridControl = this.gridControlPayments;
            this.gridViewPayments.Name = "gridViewPayments";
            this.gridViewPayments.OptionsBehavior.Editable = false;
            this.gridViewPayments.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControlSaleItems);
            this.groupControl2.Location = new System.Drawing.Point(20, 320);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(960, 180);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Sale Items";
            // 
            // gridControlSaleItems
            // 
            this.gridControlSaleItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSaleItems.Location = new System.Drawing.Point(2, 23);
            this.gridControlSaleItems.MainView = this.gridViewSaleItems;
            this.gridControlSaleItems.Name = "gridControlSaleItems";
            this.gridControlSaleItems.Size = new System.Drawing.Size(956, 155);
            this.gridControlSaleItems.TabIndex = 0;
            this.gridControlSaleItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSaleItems});
            // 
            // gridViewSaleItems
            // 
            this.gridViewSaleItems.GridControl = this.gridControlSaleItems;
            this.gridViewSaleItems.Name = "gridViewSaleItems";
            this.gridViewSaleItems.OptionsBehavior.Editable = false;
            this.gridViewSaleItems.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblNotes);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.lblPaymentStatus);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.lblChangeDue);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.lblTotalPaid);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.lblGrandTotal);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.lblDiscount);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.lblTotalAmount);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lblTotalItems);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lblBiller);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.lblCustomer);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.lblSaleDate);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.lblInvoiceNumber);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(20, 60);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(960, 250);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Sale Information";
            // 
            // lblNotes
            // 
            this.lblNotes.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNotes.Appearance.Options.UseFont = true;
            this.lblNotes.Location = new System.Drawing.Point(610, 200);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(24, 17);
            this.lblNotes.TabIndex = 23;
            this.lblNotes.Text = "N/A";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(490, 200);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(42, 17);
            this.labelControl16.TabIndex = 22;
            this.labelControl16.Text = "Notes:";
            // 
            // lblPaymentStatus
            // 
            this.lblPaymentStatus.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblPaymentStatus.Appearance.Options.UseFont = true;
            this.lblPaymentStatus.Location = new System.Drawing.Point(610, 170);
            this.lblPaymentStatus.Name = "lblPaymentStatus";
            this.lblPaymentStatus.Size = new System.Drawing.Size(54, 17);
            this.lblPaymentStatus.TabIndex = 21;
            this.lblPaymentStatus.Text = "PENDING";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(490, 170);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(103, 17);
            this.labelControl14.TabIndex = 20;
            this.labelControl14.Text = "Payment Status:";
            // 
            // lblChangeDue
            // 
            this.lblChangeDue.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblChangeDue.Appearance.Options.UseFont = true;
            this.lblChangeDue.Location = new System.Drawing.Point(610, 140);
            this.lblChangeDue.Name = "lblChangeDue";
            this.lblChangeDue.Size = new System.Drawing.Size(56, 17);
            this.lblChangeDue.TabIndex = 19;
            this.lblChangeDue.Text = "LKR 0.00";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(490, 140);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(80, 17);
            this.labelControl12.TabIndex = 18;
            this.labelControl12.Text = "Change Due:";
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTotalPaid.Appearance.Options.UseFont = true;
            this.lblTotalPaid.Location = new System.Drawing.Point(610, 110);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(56, 17);
            this.lblTotalPaid.TabIndex = 17;
            this.lblTotalPaid.Text = "LKR 0.00";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(490, 110);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(68, 17);
            this.labelControl10.TabIndex = 16;
            this.labelControl10.Text = "Total Paid:";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblGrandTotal.Appearance.Options.UseFont = true;
            this.lblGrandTotal.Location = new System.Drawing.Point(610, 80);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(56, 17);
            this.lblGrandTotal.TabIndex = 15;
            this.lblGrandTotal.Text = "LKR 0.00";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(490, 80);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(77, 17);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Grand Total:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDiscount.Appearance.Options.UseFont = true;
            this.lblDiscount.Location = new System.Drawing.Point(610, 50);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(69, 17);
            this.lblDiscount.TabIndex = 13;
            this.lblDiscount.Text = "NONE - 0.00";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(490, 50);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(61, 17);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Discount:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTotalAmount.Appearance.Options.UseFont = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(150, 200);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(56, 17);
            this.lblTotalAmount.TabIndex = 11;
            this.lblTotalAmount.Text = "LKR 0.00";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(30, 200);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(89, 17);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "Total Amount:";
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTotalItems.Appearance.Options.UseFont = true;
            this.lblTotalItems.Location = new System.Drawing.Point(150, 170);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(7, 17);
            this.lblTotalItems.TabIndex = 9;
            this.lblTotalItems.Text = "0";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(30, 170);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(74, 17);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Total Items:";
            // 
            // lblBiller
            // 
            this.lblBiller.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblBiller.Appearance.Options.UseFont = true;
            this.lblBiller.Location = new System.Drawing.Point(150, 140);
            this.lblBiller.Name = "lblBiller";
            this.lblBiller.Size = new System.Drawing.Size(44, 17);
            this.lblBiller.TabIndex = 7;
            this.lblBiller.Text = "System";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(30, 140);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(37, 17);
            this.labelControl9.TabIndex = 6;
            this.labelControl9.Text = "Biller:";
            // 
            // lblCustomer
            // 
            this.lblCustomer.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCustomer.Appearance.Options.UseFont = true;
            this.lblCustomer.Location = new System.Drawing.Point(150, 110);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(109, 17);
            this.lblCustomer.TabIndex = 5;
            this.lblCustomer.Text = "Walk-In Customer";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(30, 110);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(64, 17);
            this.labelControl7.TabIndex = 4;
            this.labelControl7.Text = "Customer:";
            // 
            // lblSaleDate
            // 
            this.lblSaleDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSaleDate.Appearance.Options.UseFont = true;
            this.lblSaleDate.Location = new System.Drawing.Point(150, 80);
            this.lblSaleDate.Name = "lblSaleDate";
            this.lblSaleDate.Size = new System.Drawing.Size(112, 17);
            this.lblSaleDate.TabIndex = 3;
            this.lblSaleDate.Text = "01/01/2025 00:00";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(30, 80);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 17);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "Sale Date:";
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblInvoiceNumber.Appearance.Options.UseFont = true;
            this.lblInvoiceNumber.Location = new System.Drawing.Point(150, 50);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(24, 17);
            this.lblInvoiceNumber.TabIndex = 1;
            this.lblInvoiceNumber.Text = "N/A";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(30, 50);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(105, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Invoice Number:";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 650);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1000, 50);
            this.panelControl2.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Location = new System.Drawing.Point(880, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Appearance.Options.UseBackColor = true;
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Location = new System.Drawing.Point(770, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(107, 30);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Sale Details";
            // 
            // Form_SaleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SaleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sale Details";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSaleItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSaleItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblInvoiceNumber;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblSaleDate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lblBiller;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl lblTotalItems;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblTotalAmount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblDiscount;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblGrandTotal;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl lblTotalPaid;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl lblChangeDue;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl lblPaymentStatus;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControlSaleItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSaleItems;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraGrid.GridControl gridControlPayments;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPayments;
    }
}
