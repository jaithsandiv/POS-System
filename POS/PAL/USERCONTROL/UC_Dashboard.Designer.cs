namespace POS.PAL.USERCONTROL
{
    partial class UC_Dashboard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Dashboard));
            sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            topPanel1 = new System.Windows.Forms.Panel();
            btnPOS = new DevExpress.XtraEditors.SimpleButton();
            btnAcc = new DevExpress.XtraEditors.SimpleButton();
            panel1 = new System.Windows.Forms.Panel();
            DateFilterComboBox = new DevExpress.XtraEditors.ComboBoxEdit();
            htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();
            panel5 = new System.Windows.Forms.Panel();
            lblExpense = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            panel4 = new System.Windows.Forms.Panel();
            lblTotalSalesReturn = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            panel6 = new System.Windows.Forms.Panel();
            lblTotalPurchaseReturn = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            panel3 = new System.Windows.Forms.Panel();
            lblInvoiceDue = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            panel7 = new System.Windows.Forms.Panel();
            lblPurchaseDue = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            lblNet = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            panel8 = new System.Windows.Forms.Panel();
            lblTotalPurchase = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            totalSalesPanel = new System.Windows.Forms.Panel();
            lblTotalSales = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel9 = new System.Windows.Forms.Panel();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            chartControl2 = new DevExpress.XtraCharts.ChartControl();
            label4 = new System.Windows.Forms.Label();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            chartControl1 = new DevExpress.XtraCharts.ChartControl();
            label3 = new System.Windows.Forms.Label();
            panelControl3 = new DevExpress.XtraEditors.PanelControl();
            lblBusinessName = new DevExpress.XtraEditors.LabelControl();
            sidePanel1.SuspendLayout();
            topPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DateFilterComboBox.Properties).BeginInit();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            panel3.SuspendLayout();
            panel7.SuspendLayout();
            panel2.SuspendLayout();
            panel8.SuspendLayout();
            totalSalesPanel.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl3).BeginInit();
            panelControl3.SuspendLayout();
            SuspendLayout();
            // 
            // sidePanel1
            // 
            sidePanel1.Appearance.BackColor = System.Drawing.Color.White;
            sidePanel1.Appearance.Options.UseBackColor = true;
            sidePanel1.BorderThickness = 2;
            sidePanel1.Controls.Add(panelControl3);
            sidePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            sidePanel1.Location = new System.Drawing.Point(0, 0);
            sidePanel1.Name = "sidePanel1";
            sidePanel1.Size = new System.Drawing.Size(250, 1050);
            sidePanel1.TabIndex = 0;
            sidePanel1.Text = "sidePanel1";
            // 
            // topPanel1
            // 
            topPanel1.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            topPanel1.Controls.Add(btnPOS);
            topPanel1.Controls.Add(btnAcc);
            topPanel1.Location = new System.Drawing.Point(249, 0);
            topPanel1.Name = "topPanel1";
            topPanel1.Size = new System.Drawing.Size(1671, 49);
            topPanel1.TabIndex = 1;
            // 
            // btnPOS
            // 
            btnPOS.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnPOS.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnPOS.Appearance.Options.UseBackColor = true;
            btnPOS.Appearance.Options.UseFont = true;
            btnPOS.AppearanceHovered.Options.UseBackColor = true;
            btnPOS.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            btnPOS.Location = new System.Drawing.Point(1478, 9);
            btnPOS.Name = "btnPOS";
            btnPOS.Size = new System.Drawing.Size(48, 30);
            btnPOS.TabIndex = 5;
            btnPOS.Text = "POS";
            btnPOS.Click += btnPOS_Click;
            // 
            // btnAcc
            // 
            btnAcc.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnAcc.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnAcc.Appearance.Options.UseBackColor = true;
            btnAcc.Appearance.Options.UseFont = true;
            btnAcc.AppearanceHovered.Options.UseBackColor = true;
            btnAcc.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnAcc.ImageOptions.Image");
            btnAcc.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            btnAcc.Location = new System.Drawing.Point(1541, 9);
            btnAcc.Name = "btnAcc";
            btnAcc.Size = new System.Drawing.Size(48, 30);
            btnAcc.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            panel1.Controls.Add(DateFilterComboBox);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel8);
            panel1.Controls.Add(totalSalesPanel);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(249, 45);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1671, 246);
            panel1.TabIndex = 2;
            // 
            // DateFilterComboBox
            // 
            DateFilterComboBox.EditValue = "FIlter By Date";
            DateFilterComboBox.Location = new System.Drawing.Point(1430, 21);
            DateFilterComboBox.Name = "DateFilterComboBox";
            DateFilterComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            DateFilterComboBox.Properties.HtmlTemplates.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] { htmlTemplate1 });
            DateFilterComboBox.Properties.Items.AddRange(new object[] { "Yesterday", "Last 7 Days", "Last 30 Days", "This Month", "Last Month", "This Year", "Last Year" });
            DateFilterComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            DateFilterComboBox.Size = new System.Drawing.Size(159, 20);
            DateFilterComboBox.TabIndex = 8;
            // 
            // htmlTemplate1
            // 
            htmlTemplate1.Name = "htmlTemplate1";
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.White;
            panel5.Controls.Add(lblExpense);
            panel5.Controls.Add(label17);
            panel5.Location = new System.Drawing.Point(1210, 146);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(379, 80);
            panel5.TabIndex = 7;
            // 
            // lblExpense
            // 
            lblExpense.AutoSize = true;
            lblExpense.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblExpense.ForeColor = System.Drawing.Color.Black;
            lblExpense.Location = new System.Drawing.Point(81, 45);
            lblExpense.Name = "lblExpense";
            lblExpense.Size = new System.Drawing.Size(104, 19);
            lblExpense.TabIndex = 1;
            lblExpense.Text = "Rs. 1000.00";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label17.ForeColor = System.Drawing.Color.Gray;
            label17.Location = new System.Drawing.Point(81, 15);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(60, 16);
            label17.TabIndex = 0;
            label17.Text = "Expense";
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.White;
            panel4.Controls.Add(lblTotalSalesReturn);
            panel4.Controls.Add(label15);
            panel4.Location = new System.Drawing.Point(1210, 60);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(379, 80);
            panel4.TabIndex = 6;
            // 
            // lblTotalSalesReturn
            // 
            lblTotalSalesReturn.AutoSize = true;
            lblTotalSalesReturn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblTotalSalesReturn.ForeColor = System.Drawing.Color.Black;
            lblTotalSalesReturn.Location = new System.Drawing.Point(81, 45);
            lblTotalSalesReturn.Name = "lblTotalSalesReturn";
            lblTotalSalesReturn.Size = new System.Drawing.Size(104, 19);
            lblTotalSalesReturn.TabIndex = 1;
            lblTotalSalesReturn.Text = "Rs. 1000.00";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label15.ForeColor = System.Drawing.Color.Gray;
            label15.Location = new System.Drawing.Point(81, 15);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(114, 16);
            label15.TabIndex = 0;
            label15.Text = "Total Sell Return";
            // 
            // panel6
            // 
            panel6.BackColor = System.Drawing.Color.White;
            panel6.Controls.Add(lblTotalPurchaseReturn);
            panel6.Controls.Add(label13);
            panel6.Location = new System.Drawing.Point(816, 146);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(379, 80);
            panel6.TabIndex = 6;
            // 
            // lblTotalPurchaseReturn
            // 
            lblTotalPurchaseReturn.AutoSize = true;
            lblTotalPurchaseReturn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblTotalPurchaseReturn.ForeColor = System.Drawing.Color.Black;
            lblTotalPurchaseReturn.Location = new System.Drawing.Point(81, 45);
            lblTotalPurchaseReturn.Name = "lblTotalPurchaseReturn";
            lblTotalPurchaseReturn.Size = new System.Drawing.Size(104, 19);
            lblTotalPurchaseReturn.TabIndex = 1;
            lblTotalPurchaseReturn.Text = "Rs. 1000.00";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label13.ForeColor = System.Drawing.Color.Gray;
            label13.Location = new System.Drawing.Point(81, 15);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(152, 16);
            label13.TabIndex = 0;
            label13.Text = "Total Purchase Return";
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.White;
            panel3.Controls.Add(lblInvoiceDue);
            panel3.Controls.Add(label11);
            panel3.Location = new System.Drawing.Point(816, 60);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(379, 80);
            panel3.TabIndex = 5;
            // 
            // lblInvoiceDue
            // 
            lblInvoiceDue.AutoSize = true;
            lblInvoiceDue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblInvoiceDue.ForeColor = System.Drawing.Color.Black;
            lblInvoiceDue.Location = new System.Drawing.Point(81, 45);
            lblInvoiceDue.Name = "lblInvoiceDue";
            lblInvoiceDue.Size = new System.Drawing.Size(104, 19);
            lblInvoiceDue.TabIndex = 1;
            lblInvoiceDue.Text = "Rs. 1000.00";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label11.ForeColor = System.Drawing.Color.Gray;
            label11.Location = new System.Drawing.Point(81, 15);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(83, 16);
            label11.TabIndex = 0;
            label11.Text = "Invoice Due";
            // 
            // panel7
            // 
            panel7.BackColor = System.Drawing.Color.White;
            panel7.Controls.Add(lblPurchaseDue);
            panel7.Controls.Add(label9);
            panel7.Location = new System.Drawing.Point(422, 146);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(379, 80);
            panel7.TabIndex = 5;
            // 
            // lblPurchaseDue
            // 
            lblPurchaseDue.AutoSize = true;
            lblPurchaseDue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblPurchaseDue.ForeColor = System.Drawing.Color.Black;
            lblPurchaseDue.Location = new System.Drawing.Point(81, 45);
            lblPurchaseDue.Name = "lblPurchaseDue";
            lblPurchaseDue.Size = new System.Drawing.Size(104, 19);
            lblPurchaseDue.TabIndex = 1;
            lblPurchaseDue.Text = "Rs. 1000.00";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label9.ForeColor = System.Drawing.Color.Gray;
            label9.Location = new System.Drawing.Point(81, 15);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(96, 16);
            label9.TabIndex = 0;
            label9.Text = "Purchase Due";
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.White;
            panel2.Controls.Add(lblNet);
            panel2.Controls.Add(label7);
            panel2.Location = new System.Drawing.Point(422, 60);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(379, 80);
            panel2.TabIndex = 4;
            // 
            // lblNet
            // 
            lblNet.AutoSize = true;
            lblNet.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblNet.ForeColor = System.Drawing.Color.Black;
            lblNet.Location = new System.Drawing.Point(81, 45);
            lblNet.Name = "lblNet";
            lblNet.Size = new System.Drawing.Size(104, 19);
            lblNet.TabIndex = 1;
            lblNet.Text = "Rs. 1000.00";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label7.ForeColor = System.Drawing.Color.Gray;
            label7.Location = new System.Drawing.Point(81, 15);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(29, 16);
            label7.TabIndex = 0;
            label7.Text = "Net";
            // 
            // panel8
            // 
            panel8.BackColor = System.Drawing.Color.White;
            panel8.Controls.Add(lblTotalPurchase);
            panel8.Controls.Add(label5);
            panel8.Location = new System.Drawing.Point(28, 146);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(379, 80);
            panel8.TabIndex = 4;
            // 
            // lblTotalPurchase
            // 
            lblTotalPurchase.AutoSize = true;
            lblTotalPurchase.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblTotalPurchase.ForeColor = System.Drawing.Color.Black;
            lblTotalPurchase.Location = new System.Drawing.Point(81, 45);
            lblTotalPurchase.Name = "lblTotalPurchase";
            lblTotalPurchase.Size = new System.Drawing.Size(104, 19);
            lblTotalPurchase.TabIndex = 1;
            lblTotalPurchase.Text = "Rs. 1000.00";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.Gray;
            label5.Location = new System.Drawing.Point(81, 15);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(103, 16);
            label5.TabIndex = 0;
            label5.Text = "Total Purchase";
            // 
            // totalSalesPanel
            // 
            totalSalesPanel.BackColor = System.Drawing.Color.White;
            totalSalesPanel.Controls.Add(lblTotalSales);
            totalSalesPanel.Controls.Add(label2);
            totalSalesPanel.Location = new System.Drawing.Point(28, 60);
            totalSalesPanel.Name = "totalSalesPanel";
            totalSalesPanel.Size = new System.Drawing.Size(379, 80);
            totalSalesPanel.TabIndex = 3;
            // 
            // lblTotalSales
            // 
            lblTotalSales.AutoSize = true;
            lblTotalSales.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblTotalSales.ForeColor = System.Drawing.Color.Black;
            lblTotalSales.Location = new System.Drawing.Point(81, 45);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new System.Drawing.Size(104, 19);
            lblTotalSales.TabIndex = 1;
            lblTotalSales.Text = "Rs. 1000.00";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.Color.Gray;
            label2.Location = new System.Drawing.Point(81, 15);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(77, 16);
            label2.TabIndex = 0;
            label2.Text = "Total Sales";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(23, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(187, 25);
            label1.TabIndex = 0;
            label1.Text = "Welcome Admin,";
            // 
            // panel9
            // 
            panel9.AutoScroll = true;
            panel9.Controls.Add(panelControl2);
            panel9.Controls.Add(panelControl1);
            panel9.Location = new System.Drawing.Point(249, 287);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(1671, 763);
            panel9.TabIndex = 3;
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panelControl2.Controls.Add(chartControl2);
            panelControl2.Controls.Add(label4);
            panelControl2.Location = new System.Drawing.Point(28, 388);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1625, 363);
            panelControl2.TabIndex = 13;
            // 
            // chartControl2
            // 
            chartControl2.Location = new System.Drawing.Point(16, 57);
            chartControl2.Name = "chartControl2";
            chartControl2.Size = new System.Drawing.Size(1590, 287);
            chartControl2.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.Black;
            label4.Location = new System.Drawing.Point(16, 24);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(239, 19);
            label4.TabIndex = 11;
            label4.Text = "Sales Current Financial Year";
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panelControl1.Controls.Add(chartControl1);
            panelControl1.Controls.Add(label3);
            panelControl1.Location = new System.Drawing.Point(28, 10);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1625, 363);
            panelControl1.TabIndex = 12;
            // 
            // chartControl1
            // 
            chartControl1.Location = new System.Drawing.Point(16, 57);
            chartControl1.Name = "chartControl1";
            chartControl1.Size = new System.Drawing.Size(1590, 287);
            chartControl1.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.Color.Black;
            label3.Location = new System.Drawing.Point(16, 24);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(160, 19);
            label3.TabIndex = 11;
            label3.Text = "Sales Last 30 Days";
            // 
            // panelControl3
            // 
            panelControl3.Controls.Add(lblBusinessName);
            panelControl3.Location = new System.Drawing.Point(0, 0);
            panelControl3.Name = "panelControl3";
            panelControl3.Size = new System.Drawing.Size(250, 49);
            panelControl3.TabIndex = 0;
            // 
            // lblBusinessName
            // 
            lblBusinessName.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblBusinessName.Appearance.Options.UseFont = true;
            lblBusinessName.Location = new System.Drawing.Point(53, 13);
            lblBusinessName.Name = "lblBusinessName";
            lblBusinessName.Size = new System.Drawing.Size(145, 23);
            lblBusinessName.TabIndex = 0;
            lblBusinessName.Text = "Business Name";
            // 
            // UC_Dashboard
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.ForeColor = System.Drawing.Color.LightGray;
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseForeColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel9);
            Controls.Add(panel1);
            Controls.Add(topPanel1);
            Controls.Add(sidePanel1);
            Name = "UC_Dashboard";
            Size = new System.Drawing.Size(1920, 1050);
            Tag = "";
            sidePanel1.ResumeLayout(false);
            topPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DateFilterComboBox.Properties).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            totalSalesPanel.ResumeLayout(false);
            totalSalesPanel.PerformLayout();
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl2).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl3).EndInit();
            panelControl3.ResumeLayout(false);
            panelControl3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private System.Windows.Forms.Panel topPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel totalSalesPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblExpense;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalSalesReturn;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTotalPurchaseReturn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblInvoiceDue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblPurchaseDue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblTotalPurchase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalSales;
        private DevExpress.XtraEditors.ComboBoxEdit DateFilterComboBox;
        private DevExpress.Utils.Html.HtmlTemplate htmlTemplate1;
        private System.Windows.Forms.Panel panel9;
        private DevExpress.XtraEditors.SimpleButton btnAcc;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraCharts.ChartControl chartControl2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton btnPOS;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblBusinessName;
    }
}
