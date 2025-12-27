namespace POS.PAL.USERCONTROL
{
    partial class UC_Stock_Report
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
            xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            xtraScrollableControl2 = new DevExpress.XtraEditors.XtraScrollableControl();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            lblProfitMargin = new DevExpress.XtraEditors.LabelControl();
            lblPotentialProfit = new DevExpress.XtraEditors.LabelControl();
            lblClosingStockBySalePrice = new DevExpress.XtraEditors.LabelControl();
            lblClosingStockByPurchasePrice = new DevExpress.XtraEditors.LabelControl();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            gridStockReport = new DevExpress.XtraGrid.GridControl();
            gridViewStockReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            colView = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemButtonEdit_View = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            colUnitSellingPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            colCurrentStock = new DevExpress.XtraGrid.Columns.GridColumn();
            colCurrentStockValue = new DevExpress.XtraGrid.Columns.GridColumn();
            colPotentialProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalUnitsSold = new DevExpress.XtraGrid.Columns.GridColumn();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            xtraScrollableControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridStockReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewStockReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit_View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            SuspendLayout();
            // 
            // xtraScrollableControl1
            // 
            xtraScrollableControl1.Location = new System.Drawing.Point(485, 610);
            xtraScrollableControl1.Name = "xtraScrollableControl1";
            xtraScrollableControl1.Size = new System.Drawing.Size(75, 23);
            xtraScrollableControl1.TabIndex = 0;
            // 
            // xtraScrollableControl2
            // 
            xtraScrollableControl2.Appearance.BackColor = System.Drawing.Color.White;
            xtraScrollableControl2.Appearance.Options.UseBackColor = true;
            xtraScrollableControl2.Controls.Add(panelControl1);
            xtraScrollableControl2.Controls.Add(separatorControl1);
            xtraScrollableControl2.Controls.Add(panelControl2);
            xtraScrollableControl2.Controls.Add(labelControl14);
            xtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            xtraScrollableControl2.Location = new System.Drawing.Point(0, 0);
            xtraScrollableControl2.Name = "xtraScrollableControl2";
            xtraScrollableControl2.Size = new System.Drawing.Size(1920, 1001);
            xtraScrollableControl2.TabIndex = 1;
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(lblProfitMargin);
            panelControl1.Controls.Add(lblPotentialProfit);
            panelControl1.Controls.Add(lblClosingStockBySalePrice);
            panelControl1.Controls.Add(lblClosingStockByPurchasePrice);
            panelControl1.Controls.Add(labelControl4);
            panelControl1.Controls.Add(labelControl3);
            panelControl1.Controls.Add(labelControl2);
            panelControl1.Controls.Add(labelControl1);
            panelControl1.Location = new System.Drawing.Point(13, 77);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1874, 99);
            panelControl1.TabIndex = 132;
            // 
            // lblProfitMargin
            // 
            lblProfitMargin.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblProfitMargin.Appearance.Options.UseFont = true;
            lblProfitMargin.Location = new System.Drawing.Point(1706, 45);
            lblProfitMargin.Name = "lblProfitMargin";
            lblProfitMargin.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            lblProfitMargin.Size = new System.Drawing.Size(35, 45);
            lblProfitMargin.TabIndex = 140;
            lblProfitMargin.Text = "10%";
            // 
            // lblPotentialProfit
            // 
            lblPotentialProfit.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblPotentialProfit.Appearance.Options.UseFont = true;
            lblPotentialProfit.Location = new System.Drawing.Point(1156, 45);
            lblPotentialProfit.Name = "lblPotentialProfit";
            lblPotentialProfit.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            lblPotentialProfit.Size = new System.Drawing.Size(98, 45);
            lblPotentialProfit.TabIndex = 139;
            lblPotentialProfit.Text = "Rs. 1000.00";
            // 
            // lblClosingStockBySalePrice
            // 
            lblClosingStockBySalePrice.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblClosingStockBySalePrice.Appearance.Options.UseFont = true;
            lblClosingStockBySalePrice.Location = new System.Drawing.Point(638, 45);
            lblClosingStockBySalePrice.Name = "lblClosingStockBySalePrice";
            lblClosingStockBySalePrice.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            lblClosingStockBySalePrice.Size = new System.Drawing.Size(98, 45);
            lblClosingStockBySalePrice.TabIndex = 138;
            lblClosingStockBySalePrice.Text = "Rs. 1000.00";
            // 
            // lblClosingStockByPurchasePrice
            // 
            lblClosingStockByPurchasePrice.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblClosingStockByPurchasePrice.Appearance.Options.UseFont = true;
            lblClosingStockByPurchasePrice.Location = new System.Drawing.Point(120, 45);
            lblClosingStockByPurchasePrice.Name = "lblClosingStockByPurchasePrice";
            lblClosingStockByPurchasePrice.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            lblClosingStockByPurchasePrice.Size = new System.Drawing.Size(98, 45);
            lblClosingStockByPurchasePrice.TabIndex = 137;
            lblClosingStockByPurchasePrice.Text = "Rs. 1000.00";
            // 
            // labelControl4
            // 
            labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl4.Appearance.Options.UseFont = true;
            labelControl4.Location = new System.Drawing.Point(1666, 9);
            labelControl4.Name = "labelControl4";
            labelControl4.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl4.Size = new System.Drawing.Size(115, 45);
            labelControl4.TabIndex = 136;
            labelControl4.Text = "Profit Margin";
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Location = new System.Drawing.Point(1140, 9);
            labelControl3.Name = "labelControl3";
            labelControl3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl3.Size = new System.Drawing.Size(130, 45);
            labelControl3.TabIndex = 135;
            labelControl3.Text = "Potential Profit";
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Location = new System.Drawing.Point(568, 9);
            labelControl2.Name = "labelControl2";
            labelControl2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl2.Size = new System.Drawing.Size(238, 45);
            labelControl2.TabIndex = 134;
            labelControl2.Text = "Closing Stock (By sale price)";
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(28, 9);
            labelControl1.Name = "labelControl1";
            labelControl1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl1.Size = new System.Drawing.Size(282, 45);
            labelControl1.TabIndex = 133;
            labelControl1.Text = "Closing Stock (By purchase price)";
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(13, 60);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1885, 23);
            separatorControl1.TabIndex = 131;
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            panelControl2.Appearance.BorderColor = System.Drawing.Color.Black;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.Appearance.Options.UseBorderColor = true;
            panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl2.Controls.Add(separatorControl2);
            panelControl2.Controls.Add(btnExportPDF);
            panelControl2.Controls.Add(btnPrint);
            panelControl2.Controls.Add(btnExportExcel);
            panelControl2.Controls.Add(btnExportCSV);
            panelControl2.Controls.Add(gridStockReport);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Location = new System.Drawing.Point(13, 182);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1874, 989);
            panelControl2.TabIndex = 130;
            // 
            // separatorControl2
            // 
            separatorControl2.BackColor = System.Drawing.Color.Transparent;
            separatorControl2.Location = new System.Drawing.Point(0, -7);
            separatorControl2.Name = "separatorControl2";
            separatorControl2.Size = new System.Drawing.Size(1885, 24);
            separatorControl2.TabIndex = 132;
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
            // gridStockReport
            // 
            gridStockReport.Location = new System.Drawing.Point(28, 78);
            gridStockReport.MainView = gridViewStockReport;
            gridStockReport.Name = "gridStockReport";
            gridStockReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemButtonEdit_View });
            gridStockReport.Size = new System.Drawing.Size(1840, 805);
            gridStockReport.TabIndex = 18;
            gridStockReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewStockReport });
            // 
            // gridViewStockReport
            // 
            gridViewStockReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewStockReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewStockReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewStockReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewStockReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewStockReport.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewStockReport.Appearance.Row.Options.UseFont = true;
            gridViewStockReport.ColumnPanelRowHeight = 44;
            gridViewStockReport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colView, colProduct, colCategory, colLocation, colUnitSellingPrice, colCurrentStock, colCurrentStockValue, colPotentialProfit, colTotalUnitsSold });
            gridViewStockReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewStockReport.GridControl = gridStockReport;
            gridViewStockReport.Name = "gridViewStockReport";
            gridViewStockReport.OptionsCustomization.AllowFilter = false;
            gridViewStockReport.OptionsCustomization.AllowGroup = false;
            gridViewStockReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewStockReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewStockReport.OptionsView.ShowGroupPanel = false;
            gridViewStockReport.OptionsView.ShowIndicator = false;
            gridViewStockReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewStockReport.RowHeight = 44;
            // 
            // colView
            // 
            colView.Caption = "View";
            colView.ColumnEdit = repositoryItemButtonEdit_View;
            colView.Name = "colView";
            colView.Visible = true;
            colView.VisibleIndex = 0;
            colView.Width = 80;
            // 
            // repositoryItemButtonEdit_View
            // 
            repositoryItemButtonEdit_View.AutoHeight = false;
            repositoryItemButtonEdit_View.Name = "repositoryItemButtonEdit_View";
            repositoryItemButtonEdit_View.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colProduct
            // 
            colProduct.Caption = "Product";
            colProduct.FieldName = "Product";
            colProduct.Name = "colProduct";
            colProduct.Visible = true;
            colProduct.VisibleIndex = 1;
            colProduct.Width = 250;
            // 
            // colCategory
            // 
            colCategory.Caption = "Category";
            colCategory.FieldName = "Category";
            colCategory.Name = "colCategory";
            colCategory.Visible = true;
            colCategory.VisibleIndex = 2;
            colCategory.Width = 150;
            // 
            // colLocation
            // 
            colLocation.Caption = "Location";
            colLocation.FieldName = "Location";
            colLocation.Name = "colLocation";
            colLocation.Visible = true;
            colLocation.VisibleIndex = 3;
            colLocation.Width = 150;
            // 
            // colUnitSellingPrice
            // 
            colUnitSellingPrice.Caption = "Unit Selling Price";
            colUnitSellingPrice.DisplayFormat.FormatString = "n2";
            colUnitSellingPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colUnitSellingPrice.FieldName = "UnitSellingPrice";
            colUnitSellingPrice.Name = "colUnitSellingPrice";
            colUnitSellingPrice.Visible = true;
            colUnitSellingPrice.VisibleIndex = 4;
            colUnitSellingPrice.Width = 120;
            // 
            // colCurrentStock
            // 
            colCurrentStock.Caption = "Current Stock";
            colCurrentStock.DisplayFormat.FormatString = "n2";
            colCurrentStock.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colCurrentStock.FieldName = "CurrentStock";
            colCurrentStock.Name = "colCurrentStock";
            colCurrentStock.Visible = true;
            colCurrentStock.VisibleIndex = 5;
            colCurrentStock.Width = 100;
            // 
            // colCurrentStockValue
            // 
            colCurrentStockValue.Caption = "Current Stock Value";
            colCurrentStockValue.DisplayFormat.FormatString = "n2";
            colCurrentStockValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colCurrentStockValue.FieldName = "CurrentStockValue";
            colCurrentStockValue.Name = "colCurrentStockValue";
            colCurrentStockValue.Visible = true;
            colCurrentStockValue.VisibleIndex = 6;
            colCurrentStockValue.Width = 150;
            // 
            // colPotentialProfit
            // 
            colPotentialProfit.Caption = "Potential Profit";
            colPotentialProfit.DisplayFormat.FormatString = "n2";
            colPotentialProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPotentialProfit.FieldName = "PotentialProfit";
            colPotentialProfit.Name = "colPotentialProfit";
            colPotentialProfit.Visible = true;
            colPotentialProfit.VisibleIndex = 7;
            colPotentialProfit.Width = 120;
            // 
            // colTotalUnitsSold
            // 
            colTotalUnitsSold.Caption = "Total Units Sold";
            colTotalUnitsSold.DisplayFormat.FormatString = "n2";
            colTotalUnitsSold.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalUnitsSold.FieldName = "TotalUnitsSold";
            colTotalUnitsSold.Name = "colTotalUnitsSold";
            colTotalUnitsSold.Visible = true;
            colTotalUnitsSold.VisibleIndex = 8;
            colTotalUnitsSold.Width = 120;
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
            labelControl14.Size = new System.Drawing.Size(128, 50);
            labelControl14.TabIndex = 129;
            labelControl14.Text = "Stock Report";
            // 
            // UC_Stock_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(xtraScrollableControl2);
            Controls.Add(xtraScrollableControl1);
            Name = "UC_Stock_Report";
            Size = new System.Drawing.Size(1920, 1001);
            xtraScrollableControl2.ResumeLayout(false);
            xtraScrollableControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)separatorControl2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridStockReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewStockReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit_View).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraGrid.GridControl gridStockReport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStockReport;
        private DevExpress.XtraGrid.Columns.GridColumn colView;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit_View;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitSellingPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentStock;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentStockValue;
        private DevExpress.XtraGrid.Columns.GridColumn colPotentialProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalUnitsSold;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.LabelControl lblProfitMargin;
        private DevExpress.XtraEditors.LabelControl lblPotentialProfit;
        private DevExpress.XtraEditors.LabelControl lblClosingStockBySalePrice;
        private DevExpress.XtraEditors.LabelControl lblClosingStockByPurchasePrice;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
