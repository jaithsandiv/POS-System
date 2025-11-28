namespace POS.PAL.USERCONTROL
{
    partial class UC_BarcodePrint
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblSubtitle = new DevExpress.XtraEditors.LabelControl();
            this.lblProduct = new DevExpress.XtraEditors.LabelControl();
            this.lueProduct = new DevExpress.XtraEditors.LookUpEdit();
            this.lblQuantity = new DevExpress.XtraEditors.LabelControl();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.chkName = new DevExpress.XtraEditors.CheckEdit();
            this.chkPrice = new DevExpress.XtraEditors.CheckEdit();
            this.chkExpiry = new DevExpress.XtraEditors.CheckEdit();
            this.chkManufacture = new DevExpress.XtraEditors.CheckEdit();
            this.chkPromo = new DevExpress.XtraEditors.CheckEdit();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.gridBarcodePrints = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();

            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpiry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkManufacture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPromo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBarcodePrints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();

            // panelControl1 (Top Input Area)
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Size = new System.Drawing.Size(800, 300);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.chkPromo);
            this.panelControl1.Controls.Add(this.chkManufacture);
            this.panelControl1.Controls.Add(this.chkExpiry);
            this.panelControl1.Controls.Add(this.chkPrice);
            this.panelControl1.Controls.Add(this.chkName);
            this.panelControl1.Controls.Add(this.numQuantity);
            this.panelControl1.Controls.Add(this.lblQuantity);
            this.panelControl1.Controls.Add(this.lueProduct);
            this.panelControl1.Controls.Add(this.lblProduct);
            this.panelControl1.Controls.Add(this.lblSubtitle);
            this.panelControl1.Controls.Add(this.lblTitle);

            // Title
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(806, 20); // Centered roughly
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 40);
            this.lblTitle.Text = "Barcode Label Printing";

            // Subtitle
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.lblSubtitle.Appearance.Options.UseFont = true;
            this.lblSubtitle.Location = new System.Drawing.Point(20, 20);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(200, 40);
            this.lblSubtitle.Text = "Print Labels";

            int y = 100;
            this.lblProduct.Text = "Select Product";
            this.lblProduct.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblProduct.Appearance.Options.UseFont = true;
            this.lblProduct.Location = new System.Drawing.Point(20, y);

            this.lueProduct.Location = new System.Drawing.Point(20, y + 23);
            this.lueProduct.Size = new System.Drawing.Size(300, 44);
            this.lueProduct.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lueProduct.Properties.Appearance.Options.UseFont = true;
            this.lueProduct.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.lueProduct.Properties.NullText = "Select Product...";

            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblQuantity.Appearance.Options.UseFont = true;
            this.lblQuantity.Location = new System.Drawing.Point(340, y);

            this.numQuantity.Location = new System.Drawing.Point(340, y + 23);
            this.numQuantity.Size = new System.Drawing.Size(100, 44);
            this.numQuantity.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.numQuantity.Properties.Appearance.Options.UseFont = true;
            this.numQuantity.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.numQuantity.Properties.MinValue = 1;
            this.numQuantity.Properties.MaxValue = 999;
            this.numQuantity.EditValue = 1;

            // Checkboxes
            int cx = 20;
            int cy = y + 80;

            this.chkName.Text = "Product Name";
            this.chkName.Location = new System.Drawing.Point(cx, cy);
            this.chkName.EditValue = true;
            this.chkName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkName.Properties.Appearance.Options.UseFont = true;

            this.chkPrice.Text = "Price";
            this.chkPrice.Location = new System.Drawing.Point(cx + 120, cy);
            this.chkPrice.EditValue = true;
            this.chkPrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkPrice.Properties.Appearance.Options.UseFont = true;

            this.chkExpiry.Text = "Expiry Date";
            this.chkExpiry.Location = new System.Drawing.Point(cx + 200, cy);
            this.chkExpiry.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkExpiry.Properties.Appearance.Options.UseFont = true;

            this.chkManufacture.Text = "Manufacture Date";
            this.chkManufacture.Location = new System.Drawing.Point(cx + 320, cy);
            this.chkManufacture.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkManufacture.Properties.Appearance.Options.UseFont = true;

            this.chkPromo.Text = "Promo Price";
            this.chkPromo.Location = new System.Drawing.Point(cx + 460, cy);
            this.chkPromo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkPromo.Properties.Appearance.Options.UseFont = true;

            // Buttons
            int btnY = cy + 40;
            this.btnAdd.Text = "Add to List";
            this.btnAdd.Location = new System.Drawing.Point(20, btnY);
            this.btnAdd.Size = new System.Drawing.Size(150, 44);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnClear.Text = "Clear List";
            this.btnClear.Location = new System.Drawing.Point(180, btnY);
            this.btnClear.Size = new System.Drawing.Size(150, 44);
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.btnPrint.Text = "Print Labels";
            this.btnPrint.Location = new System.Drawing.Point(340, btnY);
            this.btnPrint.Size = new System.Drawing.Size(150, 44);
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);


            // Grid
            this.gridBarcodePrints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBarcodePrints.MainView = this.gridView1;
            this.gridBarcodePrints.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });

            this.gridView1.GridControl = this.gridBarcodePrints;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.ColumnPanelRowHeight = 44;
            this.gridView1.RowHeight = 44;

            // Main
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.Controls.Add(this.gridBarcodePrints);
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_BarcodePrint";
            this.Size = new System.Drawing.Size(800, 600);

            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpiry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkManufacture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPromo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBarcodePrints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSubtitle;
        private DevExpress.XtraEditors.LabelControl lblProduct;
        private DevExpress.XtraEditors.LookUpEdit lueProduct;
        private DevExpress.XtraEditors.LabelControl lblQuantity;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.CheckEdit chkName;
        private DevExpress.XtraEditors.CheckEdit chkPrice;
        private DevExpress.XtraEditors.CheckEdit chkExpiry;
        private DevExpress.XtraEditors.CheckEdit chkManufacture;
        private DevExpress.XtraEditors.CheckEdit chkPromo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrint;
        private DevExpress.XtraGrid.GridControl gridBarcodePrints;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
