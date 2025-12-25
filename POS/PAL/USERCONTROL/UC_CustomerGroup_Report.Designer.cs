namespace POS.PAL.USERCONTROL
{
    partial class UC_CustomerGroup_Report
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
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            gridCustoemrGroupReport = new DevExpress.XtraGrid.GridControl();
            gridViewCustomerGroupReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            colCustomerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalSales = new DevExpress.XtraGrid.Columns.GridColumn();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCustoemrGroupReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewCustomerGroupReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
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
            labelControl14.Size = new System.Drawing.Size(235, 50);
            labelControl14.TabIndex = 135;
            labelControl14.Text = "Customer Group Report";
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
            panelControl2.Controls.Add(gridCustoemrGroupReport);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Location = new System.Drawing.Point(13, 86);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 905);
            panelControl2.TabIndex = 136;
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
            // gridCustoemrGroupReport
            // 
            gridCustoemrGroupReport.Location = new System.Drawing.Point(28, 78);
            gridCustoemrGroupReport.MainView = gridViewCustomerGroupReport;
            gridCustoemrGroupReport.Name = "gridCustoemrGroupReport";
            gridCustoemrGroupReport.Size = new System.Drawing.Size(1840, 805);
            gridCustoemrGroupReport.TabIndex = 18;
            gridCustoemrGroupReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewCustomerGroupReport });
            // 
            // gridViewCustomerGroupReport
            // 
            gridViewCustomerGroupReport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewCustomerGroupReport.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewCustomerGroupReport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewCustomerGroupReport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewCustomerGroupReport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewCustomerGroupReport.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewCustomerGroupReport.Appearance.Row.Options.UseFont = true;
            gridViewCustomerGroupReport.ColumnPanelRowHeight = 44;
            gridViewCustomerGroupReport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colCustomerGroup, colTotalSales });
            gridViewCustomerGroupReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewCustomerGroupReport.GridControl = gridCustoemrGroupReport;
            gridViewCustomerGroupReport.Name = "gridViewCustomerGroupReport";
            gridViewCustomerGroupReport.OptionsBehavior.Editable = false;
            gridViewCustomerGroupReport.OptionsCustomization.AllowFilter = false;
            gridViewCustomerGroupReport.OptionsCustomization.AllowGroup = false;
            gridViewCustomerGroupReport.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewCustomerGroupReport.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewCustomerGroupReport.OptionsSelection.MultiSelect = false;
            gridViewCustomerGroupReport.OptionsView.ShowAutoFilterRow = false;
            gridViewCustomerGroupReport.OptionsView.ShowGroupPanel = false;
            gridViewCustomerGroupReport.OptionsView.ShowIndicator = false;
            gridViewCustomerGroupReport.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewCustomerGroupReport.RowHeight = 44;
            // 
            // colCustomerGroup
            // 
            colCustomerGroup.Caption = "Customer Group";
            colCustomerGroup.FieldName = "customer_group";
            colCustomerGroup.Name = "colCustomerGroup";
            colCustomerGroup.OptionsColumn.AllowEdit = false;
            colCustomerGroup.OptionsColumn.AllowFocus = false;
            colCustomerGroup.Visible = true;
            colCustomerGroup.VisibleIndex = 0;
            colCustomerGroup.Width = 300;
            // 
            // colTotalSales
            // 
            colTotalSales.AppearanceCell.Options.UseTextOptions = true;
            colTotalSales.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalSales.Caption = "Total Sales";
            colTotalSales.DisplayFormat.FormatString = "n2";
            colTotalSales.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalSales.FieldName = "total_sales";
            colTotalSales.Name = "colTotalSales";
            colTotalSales.OptionsColumn.AllowEdit = false;
            colTotalSales.OptionsColumn.AllowFocus = false;
            colTotalSales.OptionsColumn.FixedWidth = true;
            colTotalSales.Visible = true;
            colTotalSales.VisibleIndex = 1;
            colTotalSales.Width = 150;
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
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(13, 60);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1894, 23);
            separatorControl1.TabIndex = 137;
            // 
            // UC_CustomerGroup_Report
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_CustomerGroup_Report";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCustoemrGroupReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewCustomerGroupReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridCustoemrGroupReport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCustomerGroupReport;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSales;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}
