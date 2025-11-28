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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblSubtitle = new DevExpress.XtraEditors.LabelControl();

            // Col 1
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

            // Col 2
            this.lblUnit = new DevExpress.XtraEditors.LabelControl();
            this.lueUnit = new DevExpress.XtraEditors.LookUpEdit();
            this.lblPurchaseCost = new DevExpress.XtraEditors.LabelControl();
            this.txtPurchaseCost = new DevExpress.XtraEditors.TextEdit();
            this.lblSellingPrice = new DevExpress.XtraEditors.LabelControl();
            this.txtSellingPrice = new DevExpress.XtraEditors.TextEdit();
            this.lblStockQty = new DevExpress.XtraEditors.LabelControl();
            this.txtStockQty = new DevExpress.XtraEditors.TextEdit();
            this.lblExpiryDate = new DevExpress.XtraEditors.LabelControl();
            this.dtExpiryDate = new DevExpress.XtraEditors.DateEdit();
            this.lblManufactureDate = new DevExpress.XtraEditors.LabelControl();
            this.dtManufactureDate = new DevExpress.XtraEditors.DateEdit();

            // Desc
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();

            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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

            // panelControl1
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Size = new System.Drawing.Size(1920, 1050);
            this.panelControl1.AutoScroll = true;

            // Title
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(806, 98);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 40);
            this.lblTitle.Text = "Product Registration";

            // Subtitle
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.lblSubtitle.Appearance.Options.UseFont = true;
            this.lblSubtitle.Location = new System.Drawing.Point(189, 237);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(260, 40);
            this.lblSubtitle.Text = "Add a new product";

            // Layout Vars
            int col1X = 381;
            int col2X = 995;
            int startY = 343;
            int gap = 75; // reduced spacing
            int currentY = startY;

            // -- Col 1 --
            this.lblProductName.Location = new System.Drawing.Point(col1X, currentY);
            this.lblProductName.Text = "Product Name *";
            this.txtProductName.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.txtProductName.Size = new System.Drawing.Size(480, 44);
            this.txtProductName.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtProductName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtProductName.Properties.Appearance.Options.UseFont = true;

            currentY += gap;
            this.lblProductCode.Location = new System.Drawing.Point(col1X, currentY);
            this.lblProductCode.Text = "Product Code *";
            this.txtProductCode.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.txtProductCode.Size = new System.Drawing.Size(480, 44);
            this.txtProductCode.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtProductCode.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtProductCode.Properties.Appearance.Options.UseFont = true;

            currentY += gap;
            this.lblBarcode.Location = new System.Drawing.Point(col1X, currentY);
            this.lblBarcode.Text = "Barcode";
            this.txtBarcode.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.txtBarcode.Size = new System.Drawing.Size(480, 44);
            this.txtBarcode.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;

            currentY += gap;
            this.lblProductType.Location = new System.Drawing.Point(col1X, currentY);
            this.lblProductType.Text = "Product Type";
            this.cmbProductType.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.cmbProductType.Size = new System.Drawing.Size(480, 44);
            this.cmbProductType.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.cmbProductType.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbProductType.Properties.Appearance.Options.UseFont = true;
            this.cmbProductType.Properties.Items.AddRange(new object[] { "Standard", "Combo", "Digital" });

            currentY += gap;
            this.lblCategory.Location = new System.Drawing.Point(col1X, currentY);
            this.lblCategory.Text = "Category";
            this.lueCategory.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.lueCategory.Size = new System.Drawing.Size(480, 44);
            this.lueCategory.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.lueCategory.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lueCategory.Properties.Appearance.Options.UseFont = true;
            this.lueCategory.Properties.NullText = "Select Category";

            currentY += gap;
            this.lblBrand.Location = new System.Drawing.Point(col1X, currentY);
            this.lblBrand.Text = "Brand";
            this.lueBrand.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.lueBrand.Size = new System.Drawing.Size(480, 44);
            this.lueBrand.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.lueBrand.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lueBrand.Properties.Appearance.Options.UseFont = true;
            this.lueBrand.Properties.NullText = "Select Brand";

            // -- Col 2 --
            currentY = startY;

            this.lblUnit.Location = new System.Drawing.Point(col2X, currentY);
            this.lblUnit.Text = "Unit *";
            this.lueUnit.Location = new System.Drawing.Point(col2X, currentY + 23);
            this.lueUnit.Size = new System.Drawing.Size(480, 44);
            this.lueUnit.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.lueUnit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lueUnit.Properties.Appearance.Options.UseFont = true;
            this.lueUnit.Properties.NullText = "Select Unit";

            currentY += gap;
            this.lblPurchaseCost.Location = new System.Drawing.Point(col2X, currentY);
            this.lblPurchaseCost.Text = "Purchase Cost";
            this.txtPurchaseCost.Location = new System.Drawing.Point(col2X, currentY + 23);
            this.txtPurchaseCost.Size = new System.Drawing.Size(480, 44);
            this.txtPurchaseCost.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtPurchaseCost.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPurchaseCost.Properties.Appearance.Options.UseFont = true;
            this.txtPurchaseCost.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtPurchaseCost.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtPurchaseCost.Properties.MaskSettings.Set("mask", "n2");

            currentY += gap;
            this.lblSellingPrice.Location = new System.Drawing.Point(col2X, currentY);
            this.lblSellingPrice.Text = "Selling Price *";
            this.txtSellingPrice.Location = new System.Drawing.Point(col2X, currentY + 23);
            this.txtSellingPrice.Size = new System.Drawing.Size(480, 44);
            this.txtSellingPrice.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtSellingPrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSellingPrice.Properties.Appearance.Options.UseFont = true;
            this.txtSellingPrice.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSellingPrice.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtSellingPrice.Properties.MaskSettings.Set("mask", "n2");

            currentY += gap;
            this.lblStockQty.Location = new System.Drawing.Point(col2X, currentY);
            this.lblStockQty.Text = "Stock Quantity";
            this.txtStockQty.Location = new System.Drawing.Point(col2X, currentY + 23);
            this.txtStockQty.Size = new System.Drawing.Size(480, 44);
            this.txtStockQty.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtStockQty.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtStockQty.Properties.Appearance.Options.UseFont = true;
            this.txtStockQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtStockQty.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtStockQty.Properties.MaskSettings.Set("mask", "n3");

            currentY += gap;
            this.lblExpiryDate.Location = new System.Drawing.Point(col2X, currentY);
            this.lblExpiryDate.Text = "Expiry Date";
            this.dtExpiryDate.Location = new System.Drawing.Point(col2X, currentY + 23);
            this.dtExpiryDate.Size = new System.Drawing.Size(230, 44);
            this.dtExpiryDate.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.dtExpiryDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtExpiryDate.Properties.Appearance.Options.UseFont = true;

            this.lblManufactureDate.Location = new System.Drawing.Point(col2X + 250, currentY);
            this.lblManufactureDate.Text = "Manufacture Date";
            this.dtManufactureDate.Location = new System.Drawing.Point(col2X + 250, currentY + 23);
            this.dtManufactureDate.Size = new System.Drawing.Size(230, 44);
            this.dtManufactureDate.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.dtManufactureDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtManufactureDate.Properties.Appearance.Options.UseFont = true;

            currentY += gap;
            // Description
            this.lblDescription.Location = new System.Drawing.Point(col1X, currentY);
            this.lblDescription.Text = "Description";
            this.memoDescription.Location = new System.Drawing.Point(col1X, currentY + 23);
            this.memoDescription.Size = new System.Drawing.Size(1094, 80);
            this.memoDescription.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.memoDescription.Properties.Appearance.Options.UseFont = true;

            int btnY = currentY + 120; // 860 roughly or more

            // Buttons
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(167)))), ((int)(((byte)(140)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1496, btnY);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(250, 44);
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(175, btnY);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(250, 44);
            this.btnCancel.Text = "Back";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Adding controls
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.memoDescription);
            this.panelControl1.Controls.Add(this.lblDescription);
            this.panelControl1.Controls.Add(this.dtManufactureDate);
            this.panelControl1.Controls.Add(this.lblManufactureDate);
            this.panelControl1.Controls.Add(this.dtExpiryDate);
            this.panelControl1.Controls.Add(this.lblExpiryDate);
            this.panelControl1.Controls.Add(this.txtStockQty);
            this.panelControl1.Controls.Add(this.lblStockQty);
            this.panelControl1.Controls.Add(this.txtSellingPrice);
            this.panelControl1.Controls.Add(this.lblSellingPrice);
            this.panelControl1.Controls.Add(this.txtPurchaseCost);
            this.panelControl1.Controls.Add(this.lblPurchaseCost);
            this.panelControl1.Controls.Add(this.lueUnit);
            this.panelControl1.Controls.Add(this.lblUnit);
            this.panelControl1.Controls.Add(this.lueBrand);
            this.panelControl1.Controls.Add(this.lblBrand);
            this.panelControl1.Controls.Add(this.lueCategory);
            this.panelControl1.Controls.Add(this.lblCategory);
            this.panelControl1.Controls.Add(this.cmbProductType);
            this.panelControl1.Controls.Add(this.lblProductType);
            this.panelControl1.Controls.Add(this.txtBarcode);
            this.panelControl1.Controls.Add(this.lblBarcode);
            this.panelControl1.Controls.Add(this.txtProductCode);
            this.panelControl1.Controls.Add(this.lblProductCode);
            this.panelControl1.Controls.Add(this.txtProductName);
            this.panelControl1.Controls.Add(this.lblProductName);
            this.panelControl1.Controls.Add(this.lblSubtitle);
            this.panelControl1.Controls.Add(this.lblTitle);

            // Add panel to form
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_Product_Registration";
            this.Size = new System.Drawing.Size(1920, 1050);

            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
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
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSubtitle;
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
    }
}
