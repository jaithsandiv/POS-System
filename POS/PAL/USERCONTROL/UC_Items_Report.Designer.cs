namespace POS.PAL.USERCONTROL
{
    partial class UC_Items_Report
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
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            gridViewItemsReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            colPurchaseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colPurchase = new DevExpress.XtraGrid.Columns.GridColumn();
            colSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            colPurchasePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            colSellDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colSaleID = new DevExpress.XtraGrid.Columns.GridColumn();
            colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            colSellQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            colSellPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            gridItemsReport = new DevExpress.XtraGrid.GridControl();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewItemsReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridItemsReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(labelControl14);
            panelControl1.Controls.Add(panelControl2);
            panelControl1.Controls.Add(separatorControl1);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1001);
            panelControl1.TabIndex = 0;
            // 
            // labelControl14
            // 
            labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            labelControl14.Appearance.Options.UseFont = true;
            labelControl14.Location = new System.Drawing.Point(41, 9);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(127, 50);
            labelControl14.TabIndex = 132;
            labelControl14.Text = "Items Report";
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
            // gridViewItemsReport
            // 
            gridViewItemsReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewItemsReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewItemsReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewItemsReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewItemsReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewItemsReport.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewItemsReport.Appearance.Row.Options.UseFont = true;
            gridViewItemsReport.ColumnPanelRowHeight = 44;
            gridViewItemsReport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { 
                colProduct, colPurchaseDate, colPurchase, colSupplier, colPurchasePrice,
                colSellDate, colSaleID, colCustomerName, colLocation, colSellQuantity,
                colSellPrice, colTotalAmount });
            gridViewItemsReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewItemsReport.GridControl = gridItemsReport;
            gridViewItemsReport.Name = "gridViewItemsReport";
            gridViewItemsReport.OptionsBehavior.Editable = false;
            gridViewItemsReport.OptionsCustomization.AllowFilter = false;
            gridViewItemsReport.OptionsCustomization.AllowGroup = false;
            gridViewItemsReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewItemsReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewItemsReport.OptionsView.ShowGroupPanel = false;
            gridViewItemsReport.OptionsView.ShowIndicator = false;
            gridViewItemsReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewItemsReport.RowHeight = 44;
            // 
            // colProduct
            // 
            colProduct.Caption = "Product";
            colProduct.FieldName = "Product";
            colProduct.Name = "colProduct";
            colProduct.OptionsColumn.AllowEdit = false;
            colProduct.OptionsColumn.AllowFocus = false;
            colProduct.Visible = true;
            colProduct.VisibleIndex = 0;
            colProduct.Width = 200;
            // 
            // colPurchaseDate
            // 
            colPurchaseDate.AppearanceCell.Options.UseTextOptions = true;
            colPurchaseDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPurchaseDate.Caption = "Purchase Date";
            colPurchaseDate.FieldName = "PurchaseDate";
            colPurchaseDate.Name = "colPurchaseDate";
            colPurchaseDate.OptionsColumn.AllowEdit = false;
            colPurchaseDate.OptionsColumn.AllowFocus = false;
            colPurchaseDate.OptionsColumn.FixedWidth = true;
            colPurchaseDate.Visible = true;
            colPurchaseDate.VisibleIndex = 1;
            colPurchaseDate.Width = 120;
            // 
            // colPurchase
            // 
            colPurchase.AppearanceCell.Options.UseTextOptions = true;
            colPurchase.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPurchase.Caption = "Purchase";
            colPurchase.FieldName = "Purchase";
            colPurchase.Name = "colPurchase";
            colPurchase.OptionsColumn.AllowEdit = false;
            colPurchase.OptionsColumn.AllowFocus = false;
            colPurchase.Visible = true;
            colPurchase.VisibleIndex = 2;
            colPurchase.Width = 150;
            // 
            // colSupplier
            // 
            colSupplier.AppearanceCell.Options.UseTextOptions = true;
            colSupplier.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSupplier.Caption = "Supplier";
            colSupplier.FieldName = "Supplier";
            colSupplier.Name = "colSupplier";
            colSupplier.OptionsColumn.AllowEdit = false;
            colSupplier.OptionsColumn.AllowFocus = false;
            colSupplier.Visible = true;
            colSupplier.VisibleIndex = 3;
            colSupplier.Width = 150;
            // 
            // colPurchasePrice
            // 
            colPurchasePrice.AppearanceCell.Options.UseTextOptions = true;
            colPurchasePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colPurchasePrice.Caption = "Purchase Price";
            colPurchasePrice.DisplayFormat.FormatString = "n2";
            colPurchasePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPurchasePrice.FieldName = "PurchasePrice";
            colPurchasePrice.Name = "colPurchasePrice";
            colPurchasePrice.OptionsColumn.AllowEdit = false;
            colPurchasePrice.OptionsColumn.AllowFocus = false;
            colPurchasePrice.OptionsColumn.FixedWidth = true;
            colPurchasePrice.Visible = true;
            colPurchasePrice.VisibleIndex = 4;
            colPurchasePrice.Width = 100;
            // 
            // colSellDate
            // 
            colSellDate.AppearanceCell.Options.UseTextOptions = true;
            colSellDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSellDate.Caption = "Sell Date";
            colSellDate.FieldName = "SellDate";
            colSellDate.Name = "colSellDate";
            colSellDate.OptionsColumn.AllowEdit = false;
            colSellDate.OptionsColumn.AllowFocus = false;
            colSellDate.OptionsColumn.FixedWidth = true;
            colSellDate.Visible = true;
            colSellDate.VisibleIndex = 5;
            colSellDate.Width = 120;
            // 
            // colSaleID
            // 
            colSaleID.AppearanceCell.Options.UseTextOptions = true;
            colSaleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleID.Caption = "Sale ID";
            colSaleID.FieldName = "SaleID";
            colSaleID.Name = "colSaleID";
            colSaleID.OptionsColumn.AllowEdit = false;
            colSaleID.OptionsColumn.AllowFocus = false;
            colSaleID.OptionsColumn.FixedWidth = true;
            colSaleID.Visible = true;
            colSaleID.VisibleIndex = 6;
            colSaleID.Width = 80;
            // 
            // colCustomerName
            // 
            colCustomerName.Caption = "Customer Name";
            colCustomerName.FieldName = "CustomerName";
            colCustomerName.Name = "colCustomerName";
            colCustomerName.OptionsColumn.AllowEdit = false;
            colCustomerName.OptionsColumn.AllowFocus = false;
            colCustomerName.Visible = true;
            colCustomerName.VisibleIndex = 7;
            colCustomerName.Width = 180;
            // 
            // colLocation
            // 
            colLocation.AppearanceCell.Options.UseTextOptions = true;
            colLocation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLocation.Caption = "Location";
            colLocation.FieldName = "Location";
            colLocation.Name = "colLocation";
            colLocation.OptionsColumn.AllowEdit = false;
            colLocation.OptionsColumn.AllowFocus = false;
            colLocation.Visible = true;
            colLocation.VisibleIndex = 8;
            colLocation.Width = 150;
            // 
            // colSellQuantity
            // 
            colSellQuantity.AppearanceCell.Options.UseTextOptions = true;
            colSellQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSellQuantity.Caption = "Sell Quantity";
            colSellQuantity.DisplayFormat.FormatString = "n2";
            colSellQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSellQuantity.FieldName = "SellQuantity";
            colSellQuantity.Name = "colSellQuantity";
            colSellQuantity.OptionsColumn.AllowEdit = false;
            colSellQuantity.OptionsColumn.AllowFocus = false;
            colSellQuantity.OptionsColumn.FixedWidth = true;
            colSellQuantity.Visible = true;
            colSellQuantity.VisibleIndex = 9;
            colSellQuantity.Width = 80;
            // 
            // colSellPrice
            // 
            colSellPrice.AppearanceCell.Options.UseTextOptions = true;
            colSellPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colSellPrice.Caption = "Sell Price";
            colSellPrice.DisplayFormat.FormatString = "n2";
            colSellPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSellPrice.FieldName = "SellPrice";
            colSellPrice.Name = "colSellPrice";
            colSellPrice.OptionsColumn.AllowEdit = false;
            colSellPrice.OptionsColumn.AllowFocus = false;
            colSellPrice.OptionsColumn.FixedWidth = true;
            colSellPrice.Visible = true;
            colSellPrice.VisibleIndex = 10;
            colSellPrice.Width = 100;
            // 
            // colTotalAmount
            // 
            colTotalAmount.AppearanceCell.Options.UseTextOptions = true;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.FieldName = "TotalAmount";
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;
            colTotalAmount.VisibleIndex = 11;
            colTotalAmount.Width = 100;
            // 
            // gridItemsReport
            // 
            gridItemsReport.Location = new System.Drawing.Point(28, 78);
            gridItemsReport.MainView = gridViewItemsReport;
            gridItemsReport.Name = "gridItemsReport";
            gridItemsReport.Size = new System.Drawing.Size(1840, 805);
            gridItemsReport.TabIndex = 18;
            gridItemsReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewItemsReport });
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
            panelControl2.Controls.Add(gridItemsReport);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Location = new System.Drawing.Point(13, 86);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 905);
            panelControl2.TabIndex = 133;
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(13, 60);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1894, 23);
            separatorControl1.TabIndex = 134;
            // 
            // UC_Items_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_Items_Report";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewItemsReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridItemsReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraGrid.GridControl gridItemsReport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemsReport;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchaseDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchase;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colPurchasePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colSellDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleID;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colSellQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colSellPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}
