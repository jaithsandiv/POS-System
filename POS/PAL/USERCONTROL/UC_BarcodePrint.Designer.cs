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
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();

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
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            this.SuspendLayout();

            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 300);
            this.panelControl1.TabIndex = 0;
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
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Controls.Add(this.btnSearch);

            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(806, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Barcode Label Printing";

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.lblSubtitle.Appearance.Options.UseFont = true;
            this.lblSubtitle.Location = new System.Drawing.Point(20, 20);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(200, 40);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Print Labels";

            // 
            // lblProduct
            // 
            this.lblProduct.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblProduct.Appearance.Options.UseFont = true;
            this.lblProduct.Location = new System.Drawing.Point(20, 100);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(80, 17);
            this.lblProduct.TabIndex = 2;
            this.lblProduct.Text = "Select Product";

            // 
            // lueProduct
            // 
            this.lueProduct.Location = new System.Drawing.Point(20, 123);
            this.lueProduct.Name = "lueProduct";
            this.lueProduct.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lueProduct.Properties.Appearance.Options.UseFont = true;
            this.lueProduct.Properties.AutoHeight = false;
            this.lueProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProduct.Properties.NullText = "Select Product...";
            this.lueProduct.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.lueProduct.Size = new System.Drawing.Size(300, 44);
            this.lueProduct.TabIndex = 3;

            // 
            // lblQuantity
            // 
            this.lblQuantity.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblQuantity.Appearance.Options.UseFont = true;
            this.lblQuantity.Location = new System.Drawing.Point(340, 100);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(50, 17);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity";

            // 
            // numQuantity
            // 
            this.numQuantity.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuantity.Location = new System.Drawing.Point(340, 123);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.numQuantity.Properties.Appearance.Options.UseFont = true;
            this.numQuantity.Properties.AutoHeight = false;
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numQuantity.Properties.MaxValue = new decimal(new int[] { 999, 0, 0, 0 });
            this.numQuantity.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuantity.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.numQuantity.Size = new System.Drawing.Size(100, 44);
            this.numQuantity.TabIndex = 5;

            // 
            // chkName
            // 
            this.chkName.EditValue = true;
            this.chkName.Location = new System.Drawing.Point(20, 180);
            this.chkName.Name = "chkName";
            this.chkName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkName.Properties.Appearance.Options.UseFont = true;
            this.chkName.Properties.Caption = "Product Name";
            this.chkName.Size = new System.Drawing.Size(110, 22);
            this.chkName.TabIndex = 6;

            // 
            // chkPrice
            // 
            this.chkPrice.EditValue = true;
            this.chkPrice.Location = new System.Drawing.Point(140, 180);
            this.chkPrice.Name = "chkPrice";
            this.chkPrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkPrice.Properties.Appearance.Options.UseFont = true;
            this.chkPrice.Properties.Caption = "Price";
            this.chkPrice.Size = new System.Drawing.Size(60, 22);
            this.chkPrice.TabIndex = 7;

            // 
            // chkExpiry
            // 
            this.chkExpiry.Location = new System.Drawing.Point(220, 180);
            this.chkExpiry.Name = "chkExpiry";
            this.chkExpiry.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkExpiry.Properties.Appearance.Options.UseFont = true;
            this.chkExpiry.Properties.Caption = "Expiry Date";
            this.chkExpiry.Size = new System.Drawing.Size(100, 22);
            this.chkExpiry.TabIndex = 8;

            // 
            // chkManufacture
            // 
            this.chkManufacture.Location = new System.Drawing.Point(340, 180);
            this.chkManufacture.Name = "chkManufacture";
            this.chkManufacture.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkManufacture.Properties.Appearance.Options.UseFont = true;
            this.chkManufacture.Properties.Caption = "Manufacture Date";
            this.chkManufacture.Size = new System.Drawing.Size(130, 22);
            this.chkManufacture.TabIndex = 9;

            // 
            // chkPromo
            // 
            this.chkPromo.Location = new System.Drawing.Point(480, 180);
            this.chkPromo.Name = "chkPromo";
            this.chkPromo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkPromo.Properties.Appearance.Options.UseFont = true;
            this.chkPromo.Properties.Caption = "Promo Price";
            this.chkPromo.Size = new System.Drawing.Size(100, 22);
            this.chkPromo.TabIndex = 10;

            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 220);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 44);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add to List";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnClear.Location = new System.Drawing.Point(180, 220);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 44);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear List";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(340, 220);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 44);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "Print Labels";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(520, 220);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.NullValuePrompt = "Search products...";
            this.txtSearch.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSearch.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtSearch.Size = new System.Drawing.Size(180, 44);
            this.txtSearch.TabIndex = 14;

            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Appearance.Options.UseBackColor = true;
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Appearance.Options.UseForeColor = true;
            this.btnSearch.Location = new System.Drawing.Point(710, 220);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 44);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "Search";

            // 
            // gridBarcodePrints
            // 
            this.gridBarcodePrints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBarcodePrints.Location = new System.Drawing.Point(0, 300);
            this.gridBarcodePrints.MainView = this.gridView1;
            this.gridBarcodePrints.Name = "gridBarcodePrints";
            this.gridBarcodePrints.Size = new System.Drawing.Size(800, 300);
            this.gridBarcodePrints.TabIndex = 1;
            this.gridBarcodePrints.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });

            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.ColumnPanelRowHeight = 44;
            this.gridView1.GridControl = this.gridBarcodePrints;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 44;

            // 
            // UC_BarcodePrint
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}
