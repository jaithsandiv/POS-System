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
            this.lblProduct = new DevExpress.XtraEditors.LabelControl();
            this.lueProduct = new DevExpress.XtraEditors.LookUpEdit();
            this.lblQuantity = new DevExpress.XtraEditors.LabelControl();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.chkName = new DevExpress.XtraEditors.CheckEdit();
            this.chkPrice = new DevExpress.XtraEditors.CheckEdit();
            this.chkExpiry = new DevExpress.XtraEditors.CheckEdit();
            this.chkManufacture = new DevExpress.XtraEditors.CheckEdit();
            this.chkPromo = new DevExpress.XtraEditors.CheckEdit();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
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
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Size = new System.Drawing.Size(800, 200);
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
            this.panelControl1.Controls.Add(this.lblTitle);

            // Controls
            this.lblTitle.Text = "Barcode Label Printing";
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);

            this.lblProduct.Text = "Select Product";
            this.lblProduct.Location = new System.Drawing.Point(20, 60);

            this.lueProduct.Location = new System.Drawing.Point(20, 80);
            this.lueProduct.Size = new System.Drawing.Size(300, 24);
            this.lueProduct.Properties.NullText = "Select Product...";

            this.lblQuantity.Text = "Quantity";
            this.lblQuantity.Location = new System.Drawing.Point(340, 60);

            this.numQuantity.Location = new System.Drawing.Point(340, 80);
            this.numQuantity.Size = new System.Drawing.Size(100, 24);
            this.numQuantity.Properties.MinValue = 1;
            this.numQuantity.Properties.MaxValue = 999;
            this.numQuantity.EditValue = 1;

            // Checkboxes
            int cx = 20;
            int cy = 120;

            this.chkName.Text = "Product Name";
            this.chkName.Location = new System.Drawing.Point(cx, cy);
            this.chkName.EditValue = true;

            this.chkPrice.Text = "Price";
            this.chkPrice.Location = new System.Drawing.Point(cx + 120, cy);
            this.chkPrice.EditValue = true;

            this.chkExpiry.Text = "Expiry Date";
            this.chkExpiry.Location = new System.Drawing.Point(cx + 200, cy);

            this.chkManufacture.Text = "Manufacture Date";
            this.chkManufacture.Location = new System.Drawing.Point(cx + 320, cy);

            this.chkPromo.Text = "Promo Price";
            this.chkPromo.Location = new System.Drawing.Point(cx + 460, cy);

            // Buttons
            this.btnAdd.Text = "Add to List";
            this.btnAdd.Location = new System.Drawing.Point(20, 160);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnClear.Text = "Clear List";
            this.btnClear.Location = new System.Drawing.Point(130, 160);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.btnPrint.Text = "Print Labels";
            this.btnPrint.Location = new System.Drawing.Point(650, 160);
            this.btnPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Appearance.Options.UseBackColor = true;
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Appearance.Options.UseForeColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);


            // Grid
            this.gridBarcodePrints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBarcodePrints.MainView = this.gridView1;
            this.gridBarcodePrints.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });

            this.gridView1.GridControl = this.gridBarcodePrints;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;

            // Main
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
        private DevExpress.XtraEditors.LabelControl lblProduct;
        private DevExpress.XtraEditors.LookUpEdit lueProduct;
        private DevExpress.XtraEditors.LabelControl lblQuantity;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.CheckEdit chkName;
        private DevExpress.XtraEditors.CheckEdit chkPrice;
        private DevExpress.XtraEditors.CheckEdit chkExpiry;
        private DevExpress.XtraEditors.CheckEdit chkManufacture;
        private DevExpress.XtraEditors.CheckEdit chkPromo;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraGrid.GridControl gridBarcodePrints;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
