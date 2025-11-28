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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Brand_Management));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAddBrand = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.gridBrands = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            //
            // panelControl1
            //
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnAddBrand);
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 60);
            this.panelControl1.TabIndex = 0;
            //
            // btnAddBrand
            //
            this.btnAddBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBrand.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnAddBrand.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddBrand.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnAddBrand.Appearance.Options.UseBackColor = true;
            this.btnAddBrand.Appearance.Options.UseFont = true;
            this.btnAddBrand.Appearance.Options.UseForeColor = true;
            this.btnAddBrand.Location = new System.Drawing.Point(680, 15);
            this.btnAddBrand.Name = "btnAddBrand";
            this.btnAddBrand.Size = new System.Drawing.Size(100, 30);
            this.btnAddBrand.TabIndex = 1;
            this.btnAddBrand.Text = "Add Brand";
            this.btnAddBrand.Click += new System.EventHandler(this.btnAddBrand_Click);
            //
            // lblTitle
            //
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(173, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Brand Management";
            //
            // gridBrands
            //
            this.gridBrands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBrands.Location = new System.Drawing.Point(0, 60);
            this.gridBrands.MainView = this.gridView1;
            this.gridBrands.Name = "gridBrands";
            this.gridBrands.Size = new System.Drawing.Size(800, 540);
            this.gridBrands.TabIndex = 1;
            this.gridBrands.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            //
            // gridView1
            //
            this.gridView1.GridControl = this.gridBrands;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            //
            // UC_Brand_Management
            //
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridBrands);
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_Brand_Management";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAddBrand;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraGrid.GridControl gridBrands;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
