namespace POS.PAL.USERCONTROL
{
    partial class UC_ProductSell_Report
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
            gridProductSell = new DevExpress.XtraGrid.GridControl();
            gridViewProductSell = new DevExpress.XtraGrid.Views.Grid.GridView();
            colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            colPayment = new DevExpress.XtraGrid.Columns.GridColumn();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridProductSell).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProductSell).BeginInit();
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
            panelControl2.Controls.Add(gridProductSell);
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
            // gridProductSell
            // 
            gridProductSell.Location = new System.Drawing.Point(28, 78);
            gridProductSell.MainView = gridViewProductSell;
            gridProductSell.Name = "gridProductSell";
            gridProductSell.Size = new System.Drawing.Size(1840, 805);
            gridProductSell.TabIndex = 18;
            gridProductSell.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewProductSell });
            // 
            // gridViewProductSell
            // 
            gridViewProductSell.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewProductSell.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewProductSell.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewProductSell.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewProductSell.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewProductSell.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewProductSell.Appearance.Row.Options.UseFont = true;
            gridViewProductSell.ColumnPanelRowHeight = 44;
            gridViewProductSell.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colProduct, colCustomerName, colCustomerID, colInvoiceNo, colDate, colQuantity, colUnitPrice, colDiscount, colTotalAmount, colPayment });
            gridViewProductSell.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewProductSell.GridControl = gridProductSell;
            gridViewProductSell.Name = "gridViewProductSell";
            gridViewProductSell.OptionsBehavior.Editable = false;
            gridViewProductSell.OptionsCustomization.AllowFilter = false;
            gridViewProductSell.OptionsCustomization.AllowGroup = false;
            gridViewProductSell.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewProductSell.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewProductSell.OptionsView.ShowGroupPanel = false;
            gridViewProductSell.OptionsView.ShowIndicator = false;
            gridViewProductSell.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewProductSell.RowHeight = 44;
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
            // colCustomerName
            // 
            colCustomerName.Caption = "Customer Name";
            colCustomerName.FieldName = "CustomerName";
            colCustomerName.Name = "colCustomerName";
            colCustomerName.OptionsColumn.AllowEdit = false;
            colCustomerName.OptionsColumn.AllowFocus = false;
            colCustomerName.Visible = true;
            colCustomerName.VisibleIndex = 1;
            colCustomerName.Width = 180;
            // 
            // colCustomerID
            // 
            colCustomerID.AppearanceCell.Options.UseTextOptions = true;
            colCustomerID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCustomerID.Caption = "Customer ID";
            colCustomerID.FieldName = "CustomerID";
            colCustomerID.Name = "colCustomerID";
            colCustomerID.OptionsColumn.AllowEdit = false;
            colCustomerID.OptionsColumn.AllowFocus = false;
            colCustomerID.OptionsColumn.FixedWidth = true;
            colCustomerID.Visible = true;
            colCustomerID.VisibleIndex = 2;
            colCustomerID.Width = 100;
            // 
            // colInvoiceNo
            // 
            colInvoiceNo.AppearanceCell.Options.UseTextOptions = true;
            colInvoiceNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colInvoiceNo.Caption = "Invoice No.";
            colInvoiceNo.FieldName = "InvoiceNo";
            colInvoiceNo.Name = "colInvoiceNo";
            colInvoiceNo.OptionsColumn.AllowEdit = false;
            colInvoiceNo.OptionsColumn.AllowFocus = false;
            colInvoiceNo.OptionsColumn.FixedWidth = true;
            colInvoiceNo.Visible = true;
            colInvoiceNo.VisibleIndex = 3;
            colInvoiceNo.Width = 150;
            // 
            // colDate
            // 
            colDate.AppearanceCell.Options.UseTextOptions = true;
            colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDate.Caption = "Date";
            colDate.FieldName = "Date";
            colDate.Name = "colDate";
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            colDate.OptionsColumn.FixedWidth = true;
            colDate.Visible = true;
            colDate.VisibleIndex = 4;
            colDate.Width = 150;
            // 
            // colQuantity
            // 
            colQuantity.AppearanceCell.Options.UseTextOptions = true;
            colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colQuantity.Caption = "Quantity";
            colQuantity.DisplayFormat.FormatString = "n2";
            colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colQuantity.FieldName = "Quantity";
            colQuantity.Name = "colQuantity";
            colQuantity.OptionsColumn.AllowEdit = false;
            colQuantity.OptionsColumn.AllowFocus = false;
            colQuantity.OptionsColumn.FixedWidth = true;
            colQuantity.Visible = true;
            colQuantity.VisibleIndex = 5;
            colQuantity.Width = 100;
            // 
            // colUnitPrice
            // 
            colUnitPrice.AppearanceCell.Options.UseTextOptions = true;
            colUnitPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colUnitPrice.Caption = "Unit Price";
            colUnitPrice.DisplayFormat.FormatString = "n2";
            colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colUnitPrice.FieldName = "UnitPrice";
            colUnitPrice.Name = "colUnitPrice";
            colUnitPrice.OptionsColumn.AllowEdit = false;
            colUnitPrice.OptionsColumn.AllowFocus = false;
            colUnitPrice.OptionsColumn.FixedWidth = true;
            colUnitPrice.Visible = true;
            colUnitPrice.VisibleIndex = 6;
            colUnitPrice.Width = 120;
            // 
            // colDiscount
            // 
            colDiscount.AppearanceCell.Options.UseTextOptions = true;
            colDiscount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colDiscount.Caption = "Discount";
            colDiscount.DisplayFormat.FormatString = "n2";
            colDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colDiscount.FieldName = "Discount";
            colDiscount.Name = "colDiscount";
            colDiscount.OptionsColumn.AllowEdit = false;
            colDiscount.OptionsColumn.AllowFocus = false;
            colDiscount.OptionsColumn.FixedWidth = true;
            colDiscount.Visible = true;
            colDiscount.VisibleIndex = 7;
            colDiscount.Width = 100;
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
            colTotalAmount.VisibleIndex = 8;
            colTotalAmount.Width = 150;
            // 
            // colPayment
            // 
            colPayment.AppearanceCell.Options.UseTextOptions = true;
            colPayment.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPayment.Caption = "Payment";
            colPayment.FieldName = "Payment";
            colPayment.Name = "colPayment";
            colPayment.OptionsColumn.AllowEdit = false;
            colPayment.OptionsColumn.AllowFocus = false;
            colPayment.OptionsColumn.FixedWidth = true;
            colPayment.Visible = true;
            colPayment.VisibleIndex = 9;
            colPayment.Width = 150;
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
            labelControl14.Size = new System.Drawing.Size(125, 50);
            labelControl14.TabIndex = 129;
            labelControl14.Text = "Product Sell ";
            // 
            // UC_ProductSell_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_ProductSell_Report";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridProductSell).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProductSell).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridProductSell;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProductSell;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPayment;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl14;
    }
}
