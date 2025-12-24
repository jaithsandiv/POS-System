namespace POS.PAL.USERCONTROL
{
    partial class UC_SellPayment_Report
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
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            gridSellPayments = new DevExpress.XtraGrid.GridControl();
            gridViewSellPayments = new DevExpress.XtraGrid.Views.Grid.GridView();
            colReferenceNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            colPaidOn = new DevExpress.XtraGrid.Columns.GridColumn();
            colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            colCustomerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            colPaymentMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            colView = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemButtonEdit_View = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridSellPayments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSellPayments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit_View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(separatorControl1);
            panelControl1.Controls.Add(panelControl2);
            panelControl1.Controls.Add(labelControl14);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1001);
            panelControl1.TabIndex = 0;
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(13, 60);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1894, 23);
            separatorControl1.TabIndex = 131;
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            panelControl2.Appearance.BorderColor = System.Drawing.Color.Black;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.Appearance.Options.UseBorderColor = true;
            panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl2.Controls.Add(btnExportPDF);
            panelControl2.Controls.Add(btnPrint);
            panelControl2.Controls.Add(btnExportExcel);
            panelControl2.Controls.Add(btnExportCSV);
            panelControl2.Controls.Add(gridSellPayments);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Location = new System.Drawing.Point(13, 86);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 905);
            panelControl2.TabIndex = 130;
            // 
            // btnExportPDF
            // 
            btnExportPDF.Appearance.BackColor = System.Drawing.Color.White;
            btnExportPDF.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportPDF.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportPDF.Appearance.Options.UseBackColor = true;
            btnExportPDF.Appearance.Options.UseFont = true;
            btnExportPDF.Appearance.Options.UseForeColor = true;
            btnExportPDF.Location = new System.Drawing.Point(1768, 23);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new System.Drawing.Size(100, 29);
            btnExportPDF.TabIndex = 118;
            btnExportPDF.Text = "Export PDF";
            // 
            // btnPrint
            // 
            btnPrint.Appearance.BackColor = System.Drawing.Color.White;
            btnPrint.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnPrint.Appearance.ForeColor = System.Drawing.Color.Black;
            btnPrint.Appearance.Options.UseBackColor = true;
            btnPrint.Appearance.Options.UseFont = true;
            btnPrint.Appearance.Options.UseForeColor = true;
            btnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnPrint.Location = new System.Drawing.Point(1450, 23);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(100, 29);
            btnPrint.TabIndex = 117;
            btnPrint.Text = "Print";
            // 
            // btnExportExcel
            // 
            btnExportExcel.Appearance.BackColor = System.Drawing.Color.White;
            btnExportExcel.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportExcel.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportExcel.Appearance.Options.UseBackColor = true;
            btnExportExcel.Appearance.Options.UseFont = true;
            btnExportExcel.Appearance.Options.UseForeColor = true;
            btnExportExcel.Location = new System.Drawing.Point(1662, 23);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new System.Drawing.Size(100, 29);
            btnExportExcel.TabIndex = 116;
            btnExportExcel.Text = "Export Excel";
            // 
            // btnExportCSV
            // 
            btnExportCSV.Appearance.BackColor = System.Drawing.Color.White;
            btnExportCSV.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportCSV.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportCSV.Appearance.Options.UseBackColor = true;
            btnExportCSV.Appearance.Options.UseFont = true;
            btnExportCSV.Appearance.Options.UseForeColor = true;
            btnExportCSV.Location = new System.Drawing.Point(1556, 23);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new System.Drawing.Size(100, 29);
            btnExportCSV.TabIndex = 115;
            btnExportCSV.Text = "Export CSV";
            // 
            // gridSellPayments
            // 
            gridSellPayments.Location = new System.Drawing.Point(28, 78);
            gridSellPayments.MainView = gridViewSellPayments;
            gridSellPayments.Name = "gridSellPayments";
            gridSellPayments.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemButtonEdit_View });
            gridSellPayments.Size = new System.Drawing.Size(1840, 805);
            gridSellPayments.TabIndex = 18;
            gridSellPayments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSellPayments });
            // 
            // gridViewSellPayments
            // 
            gridViewSellPayments.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewSellPayments.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSellPayments.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSellPayments.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSellPayments.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewSellPayments.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewSellPayments.Appearance.Row.Options.UseFont = true;
            gridViewSellPayments.ColumnPanelRowHeight = 44;
            gridViewSellPayments.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colReferenceNumber, colPaidOn, colAmount, colCustomer, colCustomerGroup, colPaymentMethod, colView });
            gridViewSellPayments.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSellPayments.GridControl = gridSellPayments;
            gridViewSellPayments.Name = "gridViewSellPayments";
            gridViewSellPayments.OptionsBehavior.Editable = true;
            gridViewSellPayments.OptionsCustomization.AllowFilter = false;
            gridViewSellPayments.OptionsCustomization.AllowGroup = false;
            gridViewSellPayments.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSellPayments.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSellPayments.OptionsView.ShowGroupPanel = false;
            gridViewSellPayments.OptionsView.ShowIndicator = false;
            gridViewSellPayments.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSellPayments.RowHeight = 44;
            // 
            // colReferenceNumber
            // 
            colReferenceNumber.AppearanceCell.Options.UseTextOptions = true;
            colReferenceNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colReferenceNumber.Caption = "Reference Number";
            colReferenceNumber.FieldName = "reference_number";
            colReferenceNumber.Name = "colReferenceNumber";
            colReferenceNumber.OptionsColumn.AllowEdit = false;
            colReferenceNumber.OptionsColumn.AllowFocus = false;
            colReferenceNumber.OptionsColumn.FixedWidth = true;
            colReferenceNumber.Visible = true;
            colReferenceNumber.VisibleIndex = 0;
            colReferenceNumber.Width = 150;
            // 
            // colPaidOn
            // 
            colPaidOn.AppearanceCell.Options.UseTextOptions = true;
            colPaidOn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaidOn.Caption = "Paid On";
            colPaidOn.FieldName = "paid_on";
            colPaidOn.Name = "colPaidOn";
            colPaidOn.OptionsColumn.AllowEdit = false;
            colPaidOn.OptionsColumn.AllowFocus = false;
            colPaidOn.OptionsColumn.FixedWidth = true;
            colPaidOn.Visible = true;
            colPaidOn.VisibleIndex = 1;
            colPaidOn.Width = 150;
            // 
            // colAmount
            // 
            colAmount.AppearanceCell.Options.UseTextOptions = true;
            colAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colAmount.Caption = "Amount";
            colAmount.DisplayFormat.FormatString = "n2";
            colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colAmount.FieldName = "amount";
            colAmount.Name = "colAmount";
            colAmount.OptionsColumn.AllowEdit = false;
            colAmount.OptionsColumn.AllowFocus = false;
            colAmount.OptionsColumn.FixedWidth = true;
            colAmount.Visible = true;
            colAmount.VisibleIndex = 2;
            colAmount.Width = 120;
            // 
            // colCustomer
            // 
            colCustomer.AppearanceCell.Options.UseTextOptions = true;
            colCustomer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomer.Caption = "Customer";
            colCustomer.FieldName = "customer";
            colCustomer.Name = "colCustomer";
            colCustomer.OptionsColumn.AllowEdit = false;
            colCustomer.OptionsColumn.AllowFocus = false;
            colCustomer.Visible = true;
            colCustomer.VisibleIndex = 3;
            colCustomer.Width = 200;
            // 
            // colCustomerGroup
            // 
            colCustomerGroup.AppearanceCell.Options.UseTextOptions = true;
            colCustomerGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomerGroup.Caption = "Customer Group";
            colCustomerGroup.FieldName = "customer_group";
            colCustomerGroup.Name = "colCustomerGroup";
            colCustomerGroup.OptionsColumn.AllowEdit = false;
            colCustomerGroup.OptionsColumn.AllowFocus = false;
            colCustomerGroup.OptionsColumn.FixedWidth = true;
            colCustomerGroup.Visible = true;
            colCustomerGroup.VisibleIndex = 4;
            colCustomerGroup.Width = 150;
            // 
            // colPaymentMethod
            // 
            colPaymentMethod.AppearanceCell.Options.UseTextOptions = true;
            colPaymentMethod.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPaymentMethod.Caption = "Payment Method";
            colPaymentMethod.FieldName = "payment_method";
            colPaymentMethod.Name = "colPaymentMethod";
            colPaymentMethod.OptionsColumn.AllowEdit = false;
            colPaymentMethod.OptionsColumn.AllowFocus = false;
            colPaymentMethod.OptionsColumn.FixedWidth = true;
            colPaymentMethod.Visible = true;
            colPaymentMethod.VisibleIndex = 5;
            colPaymentMethod.Width = 150;
            // 
            // colView
            // 
            colView.Caption = "View";
            colView.ColumnEdit = repositoryItemButtonEdit_View;
            colView.Name = "colView";
            colView.OptionsColumn.AllowEdit = true;
            colView.OptionsColumn.AllowFocus = true;
            colView.OptionsColumn.FixedWidth = true;
            colView.Visible = true;
            colView.VisibleIndex = 6;
            colView.Width = 80;
            // 
            // repositoryItemButtonEdit_View
            // 
            repositoryItemButtonEdit_View.AutoHeight = false;
            repositoryItemButtonEdit_View.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph) });
            repositoryItemButtonEdit_View.Name = "repositoryItemButtonEdit_View";
            repositoryItemButtonEdit_View.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(27, 23);
            txtSearch.Name = "txtSearch";
            txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtSearch.Properties.Appearance.Options.UseFont = true;
            txtSearch.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtSearch.Size = new System.Drawing.Size(343, 44);
            txtSearch.TabIndex = 114;
            // 
            // btnSearch
            // 
            btnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnSearch.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnSearch.Appearance.Options.UseBackColor = true;
            btnSearch.Appearance.Options.UseFont = true;
            btnSearch.AppearanceHovered.Options.UseBackColor = true;
            btnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSearch.Location = new System.Drawing.Point(370, 23);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(80, 44);
            btnSearch.TabIndex = 113;
            btnSearch.Text = "Search";
            // 
            // labelControl14
            // 
            labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            labelControl14.Appearance.Options.UseFont = true;
            labelControl14.Location = new System.Drawing.Point(41, 9);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(136, 50);
            labelControl14.TabIndex = 129;
            labelControl14.Text = "Sell Payments";
            // 
            // UC_SellPayment_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_SellPayment_Report";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridSellPayments).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSellPayments).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit_View).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraGrid.GridControl gridSellPayments;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSellPayments;
        private DevExpress.XtraGrid.Columns.GridColumn colReferenceNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPaidOn;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentMethod;
        private DevExpress.XtraGrid.Columns.GridColumn colView;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit_View;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl14;
    }
}
