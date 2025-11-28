namespace POS.PAL.USERCONTROL
{
    partial class UC_Product_Registration
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
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();

            // Column 1
            this.lblProductName = new DevExpress.XtraEditors.LabelControl();
            this.txtProductName = new DevExpress.XtraEditors.TextEdit();

            this.lblProductCode = new DevExpress.XtraEditors.LabelControl();
            this.txtProductCode = new DevExpress.XtraEditors.TextEdit();

            this.lblBarcode = new DevExpress.XtraEditors.LabelControl();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();

            this.lblProductType = new DevExpress.XtraEditors.LabelControl();
            this.cmbProductType = new DevExpress.XtraEditors.ComboBoxEdit();

            this.lblCategory = new DevExpress.XtraEditors.LabelControl();
            this.lueCategory = new DevExpress.XtraEditors.LookUpEdit();

            this.lblBrand = new DevExpress.XtraEditors.LabelControl();
            this.lueBrand = new DevExpress.XtraEditors.LookUpEdit();

            // Column 2
            this.lblUnit = new DevExpress.XtraEditors.LabelControl();
            this.lueUnit = new DevExpress.XtraEditors.LookUpEdit();

            this.lblPurchaseCost = new DevExpress.XtraEditors.LabelControl();
            this.txtPurchaseCost = new DevExpress.XtraEditors.TextEdit(); // Using TextEdit for simplicity, usually CalcEdit

            this.lblSellingPrice = new DevExpress.XtraEditors.LabelControl();
            this.txtSellingPrice = new DevExpress.XtraEditors.TextEdit();

            this.lblStockQty = new DevExpress.XtraEditors.LabelControl();
            this.txtStockQty = new DevExpress.XtraEditors.TextEdit();

            this.lblExpiryDate = new DevExpress.XtraEditors.LabelControl();
            this.dtExpiryDate = new DevExpress.XtraEditors.DateEdit();

            this.lblManufactureDate = new DevExpress.XtraEditors.LabelControl();
            this.dtManufactureDate = new DevExpress.XtraEditors.DateEdit();

            // Description
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();

            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();

            // Initializations
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProductType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBrand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurchaseCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSellingPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStockQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtManufactureDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtManufactureDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            this.SuspendLayout();

            // Title
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(185, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product Registration";

            // -- Col 1 --
            int c1x = 30;
            int y = 70;
            int gap = 60;

            this.lblProductName.Location = new System.Drawing.Point(c1x, y);
            this.lblProductName.Text = "Product Name *";
            this.txtProductName.Location = new System.Drawing.Point(c1x, y + 25);
            this.txtProductName.Size = new System.Drawing.Size(300, 24);

            y += gap;
            this.lblProductCode.Location = new System.Drawing.Point(c1x, y);
            this.lblProductCode.Text = "Product Code *";
            this.txtProductCode.Location = new System.Drawing.Point(c1x, y + 25);
            this.txtProductCode.Size = new System.Drawing.Size(300, 24);

            y += gap;
            this.lblBarcode.Location = new System.Drawing.Point(c1x, y);
            this.lblBarcode.Text = "Barcode";
            this.txtBarcode.Location = new System.Drawing.Point(c1x, y + 25);
            this.txtBarcode.Size = new System.Drawing.Size(300, 24);

            y += gap;
            this.lblProductType.Location = new System.Drawing.Point(c1x, y);
            this.lblProductType.Text = "Product Type";
            this.cmbProductType.Location = new System.Drawing.Point(c1x, y + 25);
            this.cmbProductType.Size = new System.Drawing.Size(300, 24);
            this.cmbProductType.Properties.Items.AddRange(new object[] { "Standard", "Combo", "Digital" });

            y += gap;
            this.lblCategory.Location = new System.Drawing.Point(c1x, y);
            this.lblCategory.Text = "Category";
            this.lueCategory.Location = new System.Drawing.Point(c1x, y + 25);
            this.lueCategory.Size = new System.Drawing.Size(300, 24);
            this.lueCategory.Properties.NullText = "Select Category";

            y += gap;
            this.lblBrand.Location = new System.Drawing.Point(c1x, y);
            this.lblBrand.Text = "Brand";
            this.lueBrand.Location = new System.Drawing.Point(c1x, y + 25);
            this.lueBrand.Size = new System.Drawing.Size(300, 24);
            this.lueBrand.Properties.NullText = "Select Brand";

            // -- Col 2 --
            int c2x = 400;
            y = 70;

            this.lblUnit.Location = new System.Drawing.Point(c2x, y);
            this.lblUnit.Text = "Unit *";
            this.lueUnit.Location = new System.Drawing.Point(c2x, y + 25);
            this.lueUnit.Size = new System.Drawing.Size(300, 24);
            this.lueUnit.Properties.NullText = "Select Unit";

            y += gap;
            this.lblPurchaseCost.Location = new System.Drawing.Point(c2x, y);
            this.lblPurchaseCost.Text = "Purchase Cost";
            this.txtPurchaseCost.Location = new System.Drawing.Point(c2x, y + 25);
            this.txtPurchaseCost.Size = new System.Drawing.Size(300, 24);
            this.txtPurchaseCost.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtPurchaseCost.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtPurchaseCost.Properties.MaskSettings.Set("mask", "n2");

            y += gap;
            this.lblSellingPrice.Location = new System.Drawing.Point(c2x, y);
            this.lblSellingPrice.Text = "Selling Price *";
            this.txtSellingPrice.Location = new System.Drawing.Point(c2x, y + 25);
            this.txtSellingPrice.Size = new System.Drawing.Size(300, 24);
            this.txtSellingPrice.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSellingPrice.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtSellingPrice.Properties.MaskSettings.Set("mask", "n2");

            y += gap;
            this.lblStockQty.Location = new System.Drawing.Point(c2x, y);
            this.lblStockQty.Text = "Stock Quantity";
            this.txtStockQty.Location = new System.Drawing.Point(c2x, y + 25);
            this.txtStockQty.Size = new System.Drawing.Size(300, 24);
            this.txtStockQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtStockQty.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtStockQty.Properties.MaskSettings.Set("mask", "n3");

            y += gap;
            this.lblExpiryDate.Location = new System.Drawing.Point(c2x, y);
            this.lblExpiryDate.Text = "Expiry Date";
            this.dtExpiryDate.Location = new System.Drawing.Point(c2x, y + 25);
            this.dtExpiryDate.Size = new System.Drawing.Size(140, 24);

            this.lblManufactureDate.Location = new System.Drawing.Point(c2x + 160, y);
            this.lblManufactureDate.Text = "Manufacture Date";
            this.dtManufactureDate.Location = new System.Drawing.Point(c2x + 160, y + 25);
            this.dtManufactureDate.Size = new System.Drawing.Size(140, 24);

            y += gap;
            this.lblDescription.Location = new System.Drawing.Point(30, y);
            this.lblDescription.Text = "Description";
            this.memoDescription.Location = new System.Drawing.Point(30, y + 25);
            this.memoDescription.Size = new System.Drawing.Size(670, 80);

            y += 120;
            this.btnSave.Location = new System.Drawing.Point(30, y);
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Text = "Save";
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(140, y);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Controls Add
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.memoDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.dtManufactureDate);
            this.Controls.Add(this.lblManufactureDate);
            this.Controls.Add(this.dtExpiryDate);
            this.Controls.Add(this.lblExpiryDate);
            this.Controls.Add(this.txtStockQty);
            this.Controls.Add(this.lblStockQty);
            this.Controls.Add(this.txtSellingPrice);
            this.Controls.Add(this.lblSellingPrice);
            this.Controls.Add(this.txtPurchaseCost);
            this.Controls.Add(this.lblPurchaseCost);
            this.Controls.Add(this.lueUnit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lueBrand);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lueCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbProductType);
            this.Controls.Add(this.lblProductType);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.lblProductCode);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblTitle);

            this.Name = "UC_Product_Registration";
            this.Size = new System.Drawing.Size(800, 650);
            this.AutoScroll = true;

            ((System.ComponentModel.ISupportInitialize)(this.txtProductName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProductType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBrand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurchaseCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSellingPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStockQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpiryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtManufactureDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtManufactureDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblProductName;
        private DevExpress.XtraEditors.TextEdit txtProductName;
        private DevExpress.XtraEditors.LabelControl lblProductCode;
        private DevExpress.XtraEditors.TextEdit txtProductCode;
        private DevExpress.XtraEditors.LabelControl lblBarcode;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
        private DevExpress.XtraEditors.LabelControl lblProductType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbProductType;
        private DevExpress.XtraEditors.LabelControl lblCategory;
        private DevExpress.XtraEditors.LookUpEdit lueCategory;
        private DevExpress.XtraEditors.LabelControl lblBrand;
        private DevExpress.XtraEditors.LookUpEdit lueBrand;
        private DevExpress.XtraEditors.LabelControl lblUnit;
        private DevExpress.XtraEditors.LookUpEdit lueUnit;
        private DevExpress.XtraEditors.LabelControl lblPurchaseCost;
        private DevExpress.XtraEditors.TextEdit txtPurchaseCost;
        private DevExpress.XtraEditors.LabelControl lblSellingPrice;
        private DevExpress.XtraEditors.TextEdit txtSellingPrice;
        private DevExpress.XtraEditors.LabelControl lblStockQty;
        private DevExpress.XtraEditors.TextEdit txtStockQty;
        private DevExpress.XtraEditors.LabelControl lblExpiryDate;
        private DevExpress.XtraEditors.DateEdit dtExpiryDate;
        private DevExpress.XtraEditors.LabelControl lblManufactureDate;
        private DevExpress.XtraEditors.DateEdit dtManufactureDate;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
