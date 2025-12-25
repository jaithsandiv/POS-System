namespace POS.PAL.USERCONTROL
{
    partial class UC_SupplierCustomer_Report
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
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalPurchase = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalPurchaseReturn = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalSale = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalSellReturn = new DevExpress.XtraGrid.Columns.GridColumn();
            colOpeningBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            colDue = new DevExpress.XtraGrid.Columns.GridColumn();
            gridViewSupplierCustomerReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSupplierCustomerReport = new DevExpress.XtraGrid.GridControl();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewSupplierCustomerReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridSupplierCustomerReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
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
            // colCustomerName
            // 
            colCustomerName.Caption = "Customer/Supplier Name";
            colCustomerName.FieldName = "customer_name";
            colCustomerName.Name = "colCustomerName";
            colCustomerName.OptionsColumn.AllowEdit = false;
            colCustomerName.OptionsColumn.AllowFocus = false;
            colCustomerName.Visible = true;
            colCustomerName.VisibleIndex = 0;
            colCustomerName.Width = 300;
            // 
            // colTotalPurchase
            // 
            colTotalPurchase.AppearanceCell.Options.UseTextOptions = true;
            colTotalPurchase.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalPurchase.Caption = "Total Purchase";
            colTotalPurchase.DisplayFormat.FormatString = "n2";
            colTotalPurchase.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalPurchase.FieldName = "total_purchase";
            colTotalPurchase.Name = "colTotalPurchase";
            colTotalPurchase.OptionsColumn.AllowEdit = false;
            colTotalPurchase.OptionsColumn.AllowFocus = false;
            colTotalPurchase.OptionsColumn.FixedWidth = true;
            colTotalPurchase.Visible = true;
            colTotalPurchase.VisibleIndex = 1;
            colTotalPurchase.Width = 150;
            // 
            // colTotalPurchaseReturn
            // 
            colTotalPurchaseReturn.AppearanceCell.Options.UseTextOptions = true;
            colTotalPurchaseReturn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalPurchaseReturn.Caption = "Total Purchase Return";
            colTotalPurchaseReturn.DisplayFormat.FormatString = "n2";
            colTotalPurchaseReturn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalPurchaseReturn.FieldName = "total_purchase_return";
            colTotalPurchaseReturn.Name = "colTotalPurchaseReturn";
            colTotalPurchaseReturn.OptionsColumn.AllowEdit = false;
            colTotalPurchaseReturn.OptionsColumn.AllowFocus = false;
            colTotalPurchaseReturn.OptionsColumn.FixedWidth = true;
            colTotalPurchaseReturn.Visible = true;
            colTotalPurchaseReturn.VisibleIndex = 2;
            colTotalPurchaseReturn.Width = 150;
            // 
            // colTotalSale
            // 
            colTotalSale.AppearanceCell.Options.UseTextOptions = true;
            colTotalSale.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalSale.Caption = "Total Sale";
            colTotalSale.DisplayFormat.FormatString = "n2";
            colTotalSale.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalSale.FieldName = "total_sale";
            colTotalSale.Name = "colTotalSale";
            colTotalSale.OptionsColumn.AllowEdit = false;
            colTotalSale.OptionsColumn.AllowFocus = false;
            colTotalSale.OptionsColumn.FixedWidth = true;
            colTotalSale.Visible = true;
            colTotalSale.VisibleIndex = 3;
            colTotalSale.Width = 150;
            // 
            // colTotalSellReturn
            // 
            colTotalSellReturn.AppearanceCell.Options.UseTextOptions = true;
            colTotalSellReturn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalSellReturn.Caption = "Total Sell Return";
            colTotalSellReturn.DisplayFormat.FormatString = "n2";
            colTotalSellReturn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalSellReturn.FieldName = "total_sell_return";
            colTotalSellReturn.Name = "colTotalSellReturn";
            colTotalSellReturn.OptionsColumn.AllowEdit = false;
            colTotalSellReturn.OptionsColumn.AllowFocus = false;
            colTotalSellReturn.OptionsColumn.FixedWidth = true;
            colTotalSellReturn.Visible = true;
            colTotalSellReturn.VisibleIndex = 4;
            colTotalSellReturn.Width = 150;
            // 
            // colOpeningBalance
            // 
            colOpeningBalance.AppearanceCell.Options.UseTextOptions = true;
            colOpeningBalance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colOpeningBalance.Caption = "Opening Balance";
            colOpeningBalance.DisplayFormat.FormatString = "n2";
            colOpeningBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colOpeningBalance.FieldName = "opening_balance";
            colOpeningBalance.Name = "colOpeningBalance";
            colOpeningBalance.OptionsColumn.AllowEdit = false;
            colOpeningBalance.OptionsColumn.AllowFocus = false;
            colOpeningBalance.OptionsColumn.FixedWidth = true;
            colOpeningBalance.Visible = true;
            colOpeningBalance.VisibleIndex = 5;
            colOpeningBalance.Width = 150;
            // 
            // colDue
            // 
            colDue.AppearanceCell.Options.UseTextOptions = true;
            colDue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colDue.Caption = "Due";
            colDue.DisplayFormat.FormatString = "n2";
            colDue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colDue.FieldName = "due";
            colDue.Name = "colDue";
            colDue.OptionsColumn.AllowEdit = false;
            colDue.OptionsColumn.AllowFocus = false;
            colDue.OptionsColumn.FixedWidth = true;
            colDue.Visible = true;
            colDue.VisibleIndex = 6;
            colDue.Width = 150;
            // 
            // gridViewSupplierCustomerReport
            // 
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSupplierCustomerReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewSupplierCustomerReport.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewSupplierCustomerReport.Appearance.Row.Options.UseFont = true;
            gridViewSupplierCustomerReport.ColumnPanelRowHeight = 44;
            gridViewSupplierCustomerReport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colCustomerName, colTotalPurchase, colTotalPurchaseReturn, colTotalSale, colTotalSellReturn, colOpeningBalance, colDue });
            gridViewSupplierCustomerReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSupplierCustomerReport.GridControl = gridSupplierCustomerReport;
            gridViewSupplierCustomerReport.Name = "gridViewSupplierCustomerReport";
            gridViewSupplierCustomerReport.OptionsBehavior.Editable = false;
            gridViewSupplierCustomerReport.OptionsCustomization.AllowFilter = false;
            gridViewSupplierCustomerReport.OptionsCustomization.AllowGroup = false;
            gridViewSupplierCustomerReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSupplierCustomerReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSupplierCustomerReport.OptionsView.ShowGroupPanel = false;
            gridViewSupplierCustomerReport.OptionsView.ShowIndicator = false;
            gridViewSupplierCustomerReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSupplierCustomerReport.RowHeight = 44;
            // 
            // gridSupplierCustomerReport
            // 
            gridSupplierCustomerReport.Location = new System.Drawing.Point(28, 78);
            gridSupplierCustomerReport.MainView = gridViewSupplierCustomerReport;
            gridSupplierCustomerReport.Name = "gridSupplierCustomerReport";
            gridSupplierCustomerReport.Size = new System.Drawing.Size(1840, 805);
            gridSupplierCustomerReport.TabIndex = 18;
            gridSupplierCustomerReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSupplierCustomerReport });
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
            panelControl2.Controls.Add(gridSupplierCustomerReport);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Location = new System.Drawing.Point(13, 86);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 905);
            panelControl2.TabIndex = 130;
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
            // labelControl14
            // 
            labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            labelControl14.Appearance.Options.UseFont = true;
            labelControl14.Location = new System.Drawing.Point(41, 9);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(298, 50);
            labelControl14.TabIndex = 129;
            labelControl14.Text = "Supplier and Customer Report";
            // 
            // UC_SupplierCustomer_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_SupplierCustomer_Report";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridViewSupplierCustomerReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridSupplierCustomerReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
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
        private DevExpress.XtraGrid.GridControl gridSupplierCustomerReport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSupplierCustomerReport;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPurchase;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPurchaseReturn;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSale;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSellReturn;
        private DevExpress.XtraGrid.Columns.GridColumn colOpeningBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colDue;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl14;
    }
}
