namespace POS.PAL.USERCONTROL
{
    partial class UC_Table_Report
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
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            gridTableReport = new DevExpress.XtraGrid.GridControl();
            gridViewTableReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            colTableNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalOrders = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalItems = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            colGrandTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            colFirstOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colLastOrderDate = new DevExpress.XtraGrid.Columns.GridColumn();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridTableReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTableReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(labelControl14);
            panelControl1.Controls.Add(separatorControl1);
            panelControl1.Controls.Add(panelControl2);
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
            labelControl14.Location = new System.Drawing.Point(41, 8);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(126, 50);
            labelControl14.TabIndex = 126;
            labelControl14.Text = "Table Report";
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(13, 59);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1894, 23);
            separatorControl1.TabIndex = 128;
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
            panelControl2.Controls.Add(gridTableReport);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Location = new System.Drawing.Point(13, 85);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 905);
            panelControl2.TabIndex = 127;
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
            // gridTableReport
            // 
            gridTableReport.Location = new System.Drawing.Point(28, 78);
            gridTableReport.MainView = gridViewTableReport;
            gridTableReport.Name = "gridTableReport";
            gridTableReport.Size = new System.Drawing.Size(1840, 805);
            gridTableReport.TabIndex = 18;
            gridTableReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTableReport });
            // 
            // gridViewTableReport
            // 
            gridViewTableReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewTableReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewTableReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewTableReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewTableReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewTableReport.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewTableReport.Appearance.Row.Options.UseFont = true;
            gridViewTableReport.ColumnPanelRowHeight = 44;
            gridViewTableReport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colTableNumber, colTotalOrders, colTotalItems, colTotalAmount, colGrandTotal, colFirstOrderDate, colLastOrderDate });
            gridViewTableReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewTableReport.GridControl = gridTableReport;
            gridViewTableReport.Name = "gridViewTableReport";
            gridViewTableReport.OptionsBehavior.Editable = false;
            gridViewTableReport.OptionsCustomization.AllowFilter = false;
            gridViewTableReport.OptionsCustomization.AllowGroup = false;
            gridViewTableReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewTableReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewTableReport.OptionsView.ShowAutoFilterRow = false;
            gridViewTableReport.OptionsView.ShowGroupPanel = false;
            gridViewTableReport.OptionsView.ShowIndicator = false;
            gridViewTableReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewTableReport.RowHeight = 44;
            // 
            // colTableNumber
            // 
            colTableNumber.AppearanceCell.Options.UseTextOptions = true;
            colTableNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTableNumber.Caption = "Table Number";
            colTableNumber.FieldName = "table_number";
            colTableNumber.Name = "colTableNumber";
            colTableNumber.OptionsColumn.AllowEdit = false;
            colTableNumber.OptionsColumn.AllowFocus = false;
            colTableNumber.OptionsColumn.FixedWidth = true;
            colTableNumber.Visible = true;
            colTableNumber.VisibleIndex = 0;
            colTableNumber.Width = 150;
            // 
            // colTotalOrders
            // 
            colTotalOrders.AppearanceCell.Options.UseTextOptions = true;
            colTotalOrders.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTotalOrders.Caption = "Total Orders";
            colTotalOrders.FieldName = "total_orders";
            colTotalOrders.Name = "colTotalOrders";
            colTotalOrders.OptionsColumn.AllowEdit = false;
            colTotalOrders.OptionsColumn.AllowFocus = false;
            colTotalOrders.OptionsColumn.FixedWidth = true;
            colTotalOrders.Visible = true;
            colTotalOrders.VisibleIndex = 1;
            colTotalOrders.Width = 120;
            // 
            // colTotalItems
            // 
            colTotalItems.AppearanceCell.Options.UseTextOptions = true;
            colTotalItems.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTotalItems.Caption = "Total Items";
            colTotalItems.FieldName = "total_items";
            colTotalItems.Name = "colTotalItems";
            colTotalItems.OptionsColumn.AllowEdit = false;
            colTotalItems.OptionsColumn.AllowFocus = false;
            colTotalItems.OptionsColumn.FixedWidth = true;
            colTotalItems.Visible = true;
            colTotalItems.VisibleIndex = 2;
            colTotalItems.Width = 120;
            // 
            // colTotalAmount
            // 
            colTotalAmount.AppearanceCell.Options.UseTextOptions = true;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.FieldName = "total_amount";
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;
            colTotalAmount.VisibleIndex = 3;
            colTotalAmount.Width = 150;
            // 
            // colGrandTotal
            // 
            colGrandTotal.AppearanceCell.Options.UseTextOptions = true;
            colGrandTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colGrandTotal.Caption = "Grand Total";
            colGrandTotal.DisplayFormat.FormatString = "n2";
            colGrandTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colGrandTotal.FieldName = "grand_total";
            colGrandTotal.Name = "colGrandTotal";
            colGrandTotal.OptionsColumn.AllowEdit = false;
            colGrandTotal.OptionsColumn.AllowFocus = false;
            colGrandTotal.OptionsColumn.FixedWidth = true;
            colGrandTotal.Visible = true;
            colGrandTotal.VisibleIndex = 4;
            colGrandTotal.Width = 150;
            // 
            // colFirstOrderDate
            // 
            colFirstOrderDate.AppearanceCell.Options.UseTextOptions = true;
            colFirstOrderDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFirstOrderDate.Caption = "First Order";
            colFirstOrderDate.FieldName = "first_order_date";
            colFirstOrderDate.Name = "colFirstOrderDate";
            colFirstOrderDate.OptionsColumn.AllowEdit = false;
            colFirstOrderDate.OptionsColumn.AllowFocus = false;
            colFirstOrderDate.OptionsColumn.FixedWidth = true;
            colFirstOrderDate.Visible = true;
            colFirstOrderDate.VisibleIndex = 5;
            colFirstOrderDate.Width = 150;
            // 
            // colLastOrderDate
            // 
            colLastOrderDate.AppearanceCell.Options.UseTextOptions = true;
            colLastOrderDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colLastOrderDate.Caption = "Last Order";
            colLastOrderDate.FieldName = "last_order_date";
            colLastOrderDate.Name = "colLastOrderDate";
            colLastOrderDate.OptionsColumn.AllowEdit = false;
            colLastOrderDate.OptionsColumn.AllowFocus = false;
            colLastOrderDate.OptionsColumn.FixedWidth = true;
            colLastOrderDate.Visible = true;
            colLastOrderDate.VisibleIndex = 6;
            colLastOrderDate.Width = 150;
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
            // UC_Table_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_Table_Report";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridTableReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTableReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraGrid.GridControl gridTableReport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTableReport;
        private DevExpress.XtraGrid.Columns.GridColumn colTableNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalOrders;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalItems;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colGrandTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstOrderDate;
        private DevExpress.XtraGrid.Columns.GridColumn colLastOrderDate;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}
