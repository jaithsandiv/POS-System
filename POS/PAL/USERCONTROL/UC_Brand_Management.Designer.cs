namespace POS.PAL.USERCONTROL
{
    partial class UC_Brand_Management
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            labelControl8 = new DevExpress.XtraEditors.LabelControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            gridBrands = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            btnAddBrand = new DevExpress.XtraEditors.SimpleButton();
            labelControl10 = new DevExpress.XtraEditors.LabelControl();
            labelControl9 = new DevExpress.XtraEditors.LabelControl();
            popupContainerEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridBrands).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)popupContainerEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(labelControl8);
            panelControl1.Controls.Add(panelControl2);
            panelControl1.Controls.Add(labelControl7);
            panelControl1.Controls.Add(labelControl14);
            panelControl1.Controls.Add(separatorControl1);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1050);
            panelControl1.TabIndex = 0;
            // 
            // labelControl8
            // 
            labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl8.Appearance.Options.UseFont = true;
            labelControl8.Location = new System.Drawing.Point(12, 55);
            labelControl8.Name = "labelControl8";
            labelControl8.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl8.Size = new System.Drawing.Size(100, 40);
            labelControl8.TabIndex = 117;
            labelControl8.Text = "All your brands";
            // 
            // panelControl2
            // 
            panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            panelControl2.Appearance.BorderColor = System.Drawing.Color.Black;
            panelControl2.Appearance.Options.UseBackColor = true;
            panelControl2.Appearance.Options.UseBorderColor = true;
            panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl2.Controls.Add(gridBrands);
            panelControl2.Controls.Add(txtSearch);
            panelControl2.Controls.Add(btnSearch);
            panelControl2.Controls.Add(btnAddBrand);
            panelControl2.Controls.Add(labelControl10);
            panelControl2.Controls.Add(labelControl9);
            panelControl2.Controls.Add(popupContainerEdit1);
            panelControl2.Location = new System.Drawing.Point(13, 101);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(1894, 927);
            panelControl2.TabIndex = 116;
            // 
            // gridBrands
            // 
            gridBrands.Location = new System.Drawing.Point(28, 78);
            gridBrands.MainView = gridView1;
            gridBrands.Name = "gridBrands";
            gridBrands.Size = new System.Drawing.Size(1840, 846);
            gridBrands.TabIndex = 18;
            gridBrands.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.ColumnPanelRowHeight = 44;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.GridControl = gridBrands;
            gridView1.Name = "gridView1";
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.RowHeight = 44;
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
            // btnAddBrand
            // 
            btnAddBrand.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnAddBrand.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnAddBrand.Appearance.Options.UseBackColor = true;
            btnAddBrand.Appearance.Options.UseFont = true;
            btnAddBrand.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAddBrand.Location = new System.Drawing.Point(1779, 5);
            btnAddBrand.Name = "btnAddBrand";
            btnAddBrand.Size = new System.Drawing.Size(89, 29);
            btnAddBrand.TabIndex = 10;
            btnAddBrand.Text = "Add";
            btnAddBrand.Click += btnAddBrand_Click;
            // 
            // labelControl10
            // 
            labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl10.Appearance.Options.UseFont = true;
            labelControl10.Location = new System.Drawing.Point(127, 43);
            labelControl10.Name = "labelControl10";
            labelControl10.Size = new System.Drawing.Size(41, 17);
            labelControl10.TabIndex = 11;
            labelControl10.Text = "entries";
            // 
            // labelControl9
            // 
            labelControl9.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            labelControl9.Appearance.Options.UseFont = true;
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
            popupContainerEdit1.Size = new System.Drawing.Size(54, 20);
            popupContainerEdit1.TabIndex = 1;
            // 
            // labelControl7
            // 
            labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl7.Appearance.Options.UseFont = true;
            labelControl7.Location = new System.Drawing.Point(87, 20);
            labelControl7.Name = "labelControl7";
            labelControl7.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl7.Size = new System.Drawing.Size(136, 40);
            labelControl7.TabIndex = 115;
            labelControl7.Text = "Manage your brands";
            // 
            // labelControl14
            // 
            labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            labelControl14.Appearance.Options.UseFont = true;
            labelControl14.Location = new System.Drawing.Point(14, 10);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(67, 50);
            labelControl14.TabIndex = 114;
            labelControl14.Text = "Brands";
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(140, 64);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1767, 23);
            separatorControl1.TabIndex = 118;
            // 
            // UC_Brand_Management
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_Brand_Management";
            Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridBrands).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)popupContainerEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridBrands;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddBrand;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}
