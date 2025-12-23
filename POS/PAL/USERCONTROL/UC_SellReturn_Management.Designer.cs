namespace POS.PAL.USERCONTROL
{
    partial class UC_SellReturn_Management
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
            panelControl3 = new DevExpress.XtraEditors.PanelControl();
            btnFilter = new DevExpress.XtraEditors.SimpleButton();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            gridControlSellReturns = new DevExpress.XtraGrid.GridControl();
            gridViewSellReturns = new DevExpress.XtraGrid.Views.Grid.GridView();
            colReturnId = new DevExpress.XtraGrid.Columns.GridColumn();
            colSaleId = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            colProcessedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            btnAddReturn = new DevExpress.XtraEditors.SimpleButton();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            labelControl10 = new DevExpress.XtraEditors.LabelControl();
            labelControl9 = new DevExpress.XtraEditors.LabelControl();
            popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
            labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl3).BeginInit();
            panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlSellReturns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSellReturns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)popupContainerEdit1.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(panelControl3);
            panelControl1.Controls.Add(separatorControl1);
            panelControl1.Controls.Add(labelControl14);
            panelControl1.Controls.Add(labelControl7);
            panelControl1.Controls.Add(panelControl2);
            panelControl1.Controls.Add(labelControl8);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1001);
            panelControl1.TabIndex = 0;
            // 
            // panelControl3
            // 
            panelControl3.Appearance.BackColor = System.Drawing.Color.White;
            panelControl3.Appearance.Options.UseBackColor = true;
            panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl3.Controls.Add(btnFilter);
            panelControl3.Location = new System.Drawing.Point(14, 59);
            panelControl3.Name = "panelControl3";
            panelControl3.Size = new System.Drawing.Size(1894, 61);
            panelControl3.TabIndex = 119;
            // 
            // btnFilter
            // 
            btnFilter.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnFilter.Appearance.Options.UseFont = true;
            btnFilter.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnFilter.Location = new System.Drawing.Point(10, 5);
            btnFilter.Name = "btnFilter";
            btnFilter.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            btnFilter.Size = new System.Drawing.Size(231, 51);
            btnFilter.TabIndex = 0;
            btnFilter.Text = "Filter";
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(119, 135);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1789, 23);
            separatorControl1.TabIndex = 124;
            // 
            // labelControl14
            // 
            labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl14.Appearance.Options.UseFont = true;
            labelControl14.Location = new System.Drawing.Point(14, 10);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(117, 50);
            labelControl14.TabIndex = 120;
            labelControl14.Text = "Sell Returns";
            // 
            // labelControl7
            // 
            labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl7.Appearance.Options.UseFont = true;
            labelControl7.Location = new System.Drawing.Point(137, 18);
            labelControl7.Name = "labelControl7";
            labelControl7.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl7.Size = new System.Drawing.Size(136, 40);
            labelControl7.TabIndex = 121;
            labelControl7.Text = "Manage your returns";
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            panelControl2.Appearance.BorderColor = System.Drawing.Color.Black;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.Appearance.Options.UseBorderColor = true;
            panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl2.Controls.Add(gridControlSellReturns);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Controls.Add(btnAddReturn);
            panelControl2.Controls.Add(btnExportPDF);
            panelControl2.Controls.Add(btnPrint);
            panelControl2.Controls.Add(btnExportExcel);
            panelControl2.Controls.Add(btnExportCSV);
            panelControl2.Controls.Add(labelControl10);
            panelControl2.Controls.Add(labelControl9);
            panelControl2.Controls.Add(popupContainerEdit1);
            panelControl2.Location = new System.Drawing.Point(14, 172);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 818);
            panelControl2.TabIndex = 122;
            // 
            // gridControlSellReturns
            // 
            gridControlSellReturns.Location = new System.Drawing.Point(28, 78);
            gridControlSellReturns.MainView = gridViewSellReturns;
            gridControlSellReturns.Name = "gridControlSellReturns";
            gridControlSellReturns.Size = new System.Drawing.Size(1840, 716);
            gridControlSellReturns.TabIndex = 18;
            gridControlSellReturns.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSellReturns });
            // 
            // gridViewSellReturns
            // 
            gridViewSellReturns.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridViewSellReturns.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSellReturns.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewSellReturns.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewSellReturns.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridViewSellReturns.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridViewSellReturns.Appearance.Row.Options.UseFont = true;
            gridViewSellReturns.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colReturnId, colSaleId, colTotalAmount, colReason, colProcessedBy, colCreatedDate });
            gridViewSellReturns.ColumnPanelRowHeight = 44;
            gridViewSellReturns.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridViewSellReturns.GridControl = gridControlSellReturns;
            gridViewSellReturns.Name = "gridViewSellReturns";
            gridViewSellReturns.OptionsBehavior.Editable = false;
            gridViewSellReturns.OptionsCustomization.AllowFilter = false;
            gridViewSellReturns.OptionsCustomization.AllowGroup = false;
            gridViewSellReturns.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSellReturns.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewSellReturns.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            gridViewSellReturns.OptionsView.ShowGroupPanel = false;
            gridViewSellReturns.OptionsView.ShowIndicator = false;
            gridViewSellReturns.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridViewSellReturns.RowHeight = 44;
            // 
            // colReturnId
            // 
            colReturnId.AppearanceCell.Options.UseTextOptions = true;
            colReturnId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colReturnId.Caption = "Return ID";
            colReturnId.FieldName = "return_id";
            colReturnId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            colReturnId.Name = "colReturnId";
            colReturnId.OptionsColumn.AllowEdit = false;
            colReturnId.OptionsColumn.AllowFocus = false;
            colReturnId.OptionsColumn.FixedWidth = true;
            colReturnId.Visible = true;
            colReturnId.VisibleIndex = 0;
            colReturnId.Width = 100;
            // 
            // colSaleId
            // 
            colSaleId.AppearanceCell.Options.UseTextOptions = true;
            colSaleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colSaleId.Caption = "Sale ID";
            colSaleId.FieldName = "sale_id";
            colSaleId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            colSaleId.Name = "colSaleId";
            colSaleId.OptionsColumn.AllowEdit = false;
            colSaleId.OptionsColumn.AllowFocus = false;
            colSaleId.OptionsColumn.FixedWidth = true;
            colSaleId.Visible = true;
            colSaleId.VisibleIndex = 1;
            colSaleId.Width = 100;
            // 
            // colTotalAmount
            // 
            colTotalAmount.AppearanceCell.Options.UseTextOptions = true;
            colTotalAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colTotalAmount.Caption = "Total Amount";
            colTotalAmount.DisplayFormat.FormatString = "n2";
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotalAmount.FieldName = "total_amount";
            colTotalAmount.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            colTotalAmount.OptionsColumn.AllowFocus = false;
            colTotalAmount.OptionsColumn.FixedWidth = true;
            colTotalAmount.Visible = true;
            colTotalAmount.VisibleIndex = 2;
            colTotalAmount.Width = 120;
            // 
            // colReason
            // 
            colReason.Caption = "Reason";
            colReason.FieldName = "reason";
            colReason.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            colReason.Name = "colReason";
            colReason.OptionsColumn.AllowEdit = false;
            colReason.OptionsColumn.AllowFocus = false;
            colReason.Visible = true;
            colReason.VisibleIndex = 3;
            colReason.Width = 300;
            // 
            // colProcessedBy
            // 
            colProcessedBy.AppearanceCell.Options.UseTextOptions = true;
            colProcessedBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colProcessedBy.Caption = "Processed By";
            colProcessedBy.FieldName = "processed_by";
            colProcessedBy.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            colProcessedBy.Name = "colProcessedBy";
            colProcessedBy.OptionsColumn.AllowEdit = false;
            colProcessedBy.OptionsColumn.AllowFocus = false;
            colProcessedBy.OptionsColumn.FixedWidth = true;
            colProcessedBy.Visible = true;
            colProcessedBy.VisibleIndex = 4;
            colProcessedBy.Width = 120;
            // 
            // colCreatedDate
            // 
            colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colCreatedDate.Caption = "Date";
            colCreatedDate.FieldName = "created_date";
            colCreatedDate.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None;
            colCreatedDate.Name = "colCreatedDate";
            colCreatedDate.OptionsColumn.AllowEdit = false;
            colCreatedDate.OptionsColumn.AllowFocus = false;
            colCreatedDate.OptionsColumn.FixedWidth = true;
            colCreatedDate.Visible = true;
            colCreatedDate.VisibleIndex = 5;
            colCreatedDate.Width = 150;
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
            btnSearch.Size = new System.Drawing.Size(60, 44);
            btnSearch.TabIndex = 113;
            btnSearch.Text = "Search";
            // 
            // btnAddReturn
            // 
            btnAddReturn.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnAddReturn.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnAddReturn.Appearance.Options.UseBackColor = true;
            btnAddReturn.Appearance.Options.UseFont = true;
            btnAddReturn.AppearanceHovered.Options.UseBackColor = true;
            btnAddReturn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAddReturn.Location = new System.Drawing.Point(1779, 23);
            btnAddReturn.Name = "btnAddReturn";
            btnAddReturn.Size = new System.Drawing.Size(89, 29);
            btnAddReturn.TabIndex = 10;
            btnAddReturn.Text = "Add";
            // 
            // btnExportPDF
            // 
            btnExportPDF.Appearance.BackColor = System.Drawing.Color.White;
            btnExportPDF.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportPDF.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportPDF.Appearance.Options.UseBackColor = true;
            btnExportPDF.Appearance.Options.UseFont = true;
            btnExportPDF.Appearance.Options.UseForeColor = true;
            btnExportPDF.Location = new System.Drawing.Point(1673, 23);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new System.Drawing.Size(100, 29);
            btnExportPDF.TabIndex = 16;
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
            btnPrint.Location = new System.Drawing.Point(1355, 23);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(100, 29);
            btnPrint.TabIndex = 14;
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
            btnExportExcel.Location = new System.Drawing.Point(1567, 23);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new System.Drawing.Size(100, 29);
            btnExportExcel.TabIndex = 13;
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
            btnExportCSV.Location = new System.Drawing.Point(1461, 23);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new System.Drawing.Size(100, 29);
            btnExportCSV.TabIndex = 12;
            btnExportCSV.Text = "Export CSV";
            // 
            // labelControl10
            // 
            labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            labelControl10.Appearance.Options.UseFont = true;
            labelControl10.Appearance.Options.UseForeColor = true;
            labelControl10.Location = new System.Drawing.Point(127, 43);
            labelControl10.Name = "labelControl10";
            labelControl10.Size = new System.Drawing.Size(41, 17);
            labelControl10.TabIndex = 11;
            labelControl10.Text = "entries";
            // 
            // labelControl9
            // 
            labelControl9.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            labelControl9.Appearance.Options.UseFont = true;
            labelControl9.Appearance.Options.UseForeColor = true;
            labelControl9.Location = new System.Drawing.Point(28, 43);
            labelControl9.Name = "labelControl9";
            labelControl9.Size = new System.Drawing.Size(33, 17);
            labelControl9.TabIndex = 10;
            labelControl9.Text = "Show";
            // 
            // popupContainerEdit1
            // 
            popupContainerEdit1.Location = new System.Drawing.Point(67, 43);
            popupContainerEdit1.Name = "popupContainerEdit1";
            popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            popupContainerEdit1.Properties.PopupFormMinSize = new System.Drawing.Size(43, 184);
            popupContainerEdit1.Properties.PopupFormSize = new System.Drawing.Size(43, 184);
            popupContainerEdit1.Properties.PopupSizeable = false;
            popupContainerEdit1.Properties.UsePopupControlMinSize = true;
            popupContainerEdit1.Size = new System.Drawing.Size(54, 20);
            popupContainerEdit1.TabIndex = 1;
            // 
            // labelControl8
            // 
            labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl8.Appearance.Options.UseFont = true;
            labelControl8.Location = new System.Drawing.Point(13, 126);
            labelControl8.Name = "labelControl8";
            labelControl8.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl8.Size = new System.Drawing.Size(100, 40);
            labelControl8.TabIndex = 123;
            labelControl8.Text = "All your returns";
            // 
            // UC_SellReturn_Management
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_SellReturn_Management";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl3).EndInit();
            panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlSellReturns).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSellReturns).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)popupContainerEdit1.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControlSellReturns;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSellReturns;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnId;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleId;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddReturn;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}
