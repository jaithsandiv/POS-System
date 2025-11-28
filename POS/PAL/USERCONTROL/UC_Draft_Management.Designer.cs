namespace POS.PAL.USERCONTROL
{
    partial class UC_Draft_Management
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Draft_Management));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlDrafts = new DevExpress.XtraGrid.GridControl();
            this.gridViewDrafts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.btnAddDraft = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            this.btnColumnVisibilty = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDrafts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDrafts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl14);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.separatorControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1920, 1050);
            this.panelControl1.TabIndex = 4;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(13, 126);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.labelControl8.Size = new System.Drawing.Size(93, 40);
            this.labelControl8.TabIndex = 117;
            this.labelControl8.Text = "All your drafts";
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.BorderColor = System.Drawing.Color.Black;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.Appearance.Options.UseBorderColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gridControlDrafts);
            this.panelControl2.Controls.Add(this.searchControl1);
            this.panelControl2.Controls.Add(this.btnAddDraft);
            this.panelControl2.Controls.Add(this.btnExportPDF);
            this.panelControl2.Controls.Add(this.btnColumnVisibilty);
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Controls.Add(this.btnExportExcel);
            this.panelControl2.Controls.Add(this.btnExportCSV);
            this.panelControl2.Controls.Add(this.labelControl10);
            this.panelControl2.Controls.Add(this.labelControl9);
            this.panelControl2.Controls.Add(this.popupContainerEdit1);
            this.panelControl2.Location = new System.Drawing.Point(14, 172);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1894, 818);
            this.panelControl2.TabIndex = 116;
            // 
            // gridControlDrafts
            // 
            this.gridControlDrafts.Location = new System.Drawing.Point(28, 78);
            this.gridControlDrafts.MainView = this.gridViewDrafts;
            this.gridControlDrafts.Name = "gridControlDrafts";
            this.gridControlDrafts.Size = new System.Drawing.Size(1840, 716);
            this.gridControlDrafts.TabIndex = 18;
            this.gridControlDrafts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDrafts});
            // 
            // gridViewDrafts
            // 
            this.gridViewDrafts.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewDrafts.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewDrafts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewDrafts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewDrafts.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridViewDrafts.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDrafts.Appearance.Row.Options.UseFont = true;
            this.gridViewDrafts.ColumnPanelRowHeight = 44;
            this.gridViewDrafts.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewDrafts.GridControl = this.gridControlDrafts;
            this.gridViewDrafts.Name = "gridViewDrafts";
            this.gridViewDrafts.OptionsBehavior.Editable = false;
            this.gridViewDrafts.OptionsCustomization.AllowFilter = false;
            this.gridViewDrafts.OptionsCustomization.AllowGroup = false;
            this.gridViewDrafts.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDrafts.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewDrafts.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewDrafts.OptionsView.ShowGroupPanel = false;
            this.gridViewDrafts.OptionsView.ShowIndicator = false;
            this.gridViewDrafts.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewDrafts.RowHeight = 44;
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(1745, 40);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(123, 20);
            this.searchControl1.TabIndex = 17;
            // 
            // btnAddDraft
            // 
            this.btnAddDraft.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnAddDraft.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDraft.Appearance.Options.UseBackColor = true;
            this.btnAddDraft.Appearance.Options.UseFont = true;
            this.btnAddDraft.AppearanceHovered.Options.UseBackColor = true;
            this.btnAddDraft.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAddDraft.Location = new System.Drawing.Point(1779, 5);
            this.btnAddDraft.Name = "btnAddDraft";
            this.btnAddDraft.Size = new System.Drawing.Size(89, 29);
            this.btnAddDraft.TabIndex = 10;
            this.btnAddDraft.Text = "Add";
            this.btnAddDraft.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(1033, 37);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(75, 23);
            this.btnExportPDF.TabIndex = 16;
            this.btnExportPDF.Text = "Export PDF";
            // 
            // btnColumnVisibilty
            // 
            this.btnColumnVisibilty.Location = new System.Drawing.Point(928, 37);
            this.btnColumnVisibilty.Name = "btnColumnVisibilty";
            this.btnColumnVisibilty.Size = new System.Drawing.Size(99, 23);
            this.btnColumnVisibilty.TabIndex = 15;
            this.btnColumnVisibilty.Text = "Column Visibilty";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(872, 37);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 23);
            this.btnPrint.TabIndex = 14;
            this.btnPrint.Text = "Print";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(791, 37);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 13;
            this.btnExportExcel.Text = "Export Excel";
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(710, 37);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(75, 23);
            this.btnExportCSV.TabIndex = 12;
            this.btnExportCSV.Text = "Export CSV";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Location = new System.Drawing.Point(127, 43);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(41, 17);
            this.labelControl10.TabIndex = 11;
            this.labelControl10.Text = "entries";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(28, 43);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(33, 17);
            this.labelControl9.TabIndex = 10;
            this.labelControl9.Text = "Show";
            // 
            // popupContainerEdit1
            // 
            this.popupContainerEdit1.Location = new System.Drawing.Point(67, 43);
            this.popupContainerEdit1.Name = "popupContainerEdit1";
            this.popupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit1.Properties.PopupFormMinSize = new System.Drawing.Size(43, 184);
            this.popupContainerEdit1.Properties.PopupFormSize = new System.Drawing.Size(43, 184);
            this.popupContainerEdit1.Properties.PopupSizeable = false;
            this.popupContainerEdit1.Properties.UsePopupControlMinSize = true;
            this.popupContainerEdit1.Size = new System.Drawing.Size(54, 20);
            this.popupContainerEdit1.TabIndex = 1;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(123, 18);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.labelControl7.Size = new System.Drawing.Size(129, 40);
            this.labelControl7.TabIndex = 115;
            this.labelControl7.Text = "Manage your drafts";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Location = new System.Drawing.Point(14, 10);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.labelControl14.Size = new System.Drawing.Size(62, 50);
            this.labelControl14.TabIndex = 114;
            this.labelControl14.Text = "Drafts";
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Location = new System.Drawing.Point(14, 59);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1894, 61);
            this.panelControl3.TabIndex = 113;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(10, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.simpleButton1.Size = new System.Drawing.Size(231, 51);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Filter";
            // 
            // separatorControl1
            // 
            this.separatorControl1.BackColor = System.Drawing.Color.Transparent;
            this.separatorControl1.Location = new System.Drawing.Point(141, 135);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(1767, 23);
            this.separatorControl1.TabIndex = 118;
            // 
            // UC_Draft_Management
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_Draft_Management";
            this.Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDrafts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDrafts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControlDrafts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDrafts;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.SimpleButton btnAddDraft;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnColumnVisibilty;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}
