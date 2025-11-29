namespace POS.PAL.USERCONTROL
{
    partial class UC_Promotion_Products
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
            btnRemove = new DevExpress.XtraEditors.SimpleButton();
            gridControlProducts = new DevExpress.XtraGrid.GridControl();
            gridViewProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            groupControlAdd = new DevExpress.XtraEditors.GroupControl();
            btnAddProduct = new DevExpress.XtraEditors.SimpleButton();
            txtDiscountValue = new DevExpress.XtraEditors.TextEdit();
            labelControlValue = new DevExpress.XtraEditors.LabelControl();
            cmbDiscountType = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControlType = new DevExpress.XtraEditors.LabelControl();
            lookupProduct = new DevExpress.XtraEditors.LookUpEdit();
            labelControlProduct = new DevExpress.XtraEditors.LabelControl();
            btnBack = new DevExpress.XtraEditors.SimpleButton();
            labelControlTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupControlAdd).BeginInit();
            groupControlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtDiscountValue.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbDiscountType.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lookupProduct.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(btnRemove);
            panelControl1.Controls.Add(gridControlProducts);
            panelControl1.Controls.Add(groupControlAdd);
            panelControl1.Controls.Add(btnBack);
            panelControl1.Controls.Add(labelControlTitle);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1050);
            panelControl1.TabIndex = 0;
            // 
            // btnRemove
            // 
            btnRemove.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 80, 80);
            btnRemove.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnRemove.Appearance.Options.UseBackColor = true;
            btnRemove.Appearance.Options.UseFont = true;
            btnRemove.Location = new System.Drawing.Point(1740, 933);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(150, 44);
            btnRemove.TabIndex = 19;
            btnRemove.Text = "Remove Selected";
            btnRemove.Click += btnRemove_Click;
            // 
            // gridControlProducts
            // 
            gridControlProducts.Location = new System.Drawing.Point(30, 240);
            gridControlProducts.MainView = gridViewProducts;
            gridControlProducts.Name = "gridControlProducts";
            gridControlProducts.Size = new System.Drawing.Size(1860, 671);
            gridControlProducts.TabIndex = 18;
            gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewProducts });
            // 
            // gridViewProducts
            // 
            gridViewProducts.GridControl = gridControlProducts;
            gridViewProducts.Name = "gridViewProducts";
            gridViewProducts.OptionsBehavior.Editable = false;
            gridViewProducts.OptionsView.ShowGroupPanel = false;
            // 
            // groupControlAdd
            // 
            groupControlAdd.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            groupControlAdd.AppearanceCaption.Options.UseFont = true;
            groupControlAdd.Controls.Add(btnAddProduct);
            groupControlAdd.Controls.Add(txtDiscountValue);
            groupControlAdd.Controls.Add(labelControlValue);
            groupControlAdd.Controls.Add(cmbDiscountType);
            groupControlAdd.Controls.Add(labelControlType);
            groupControlAdd.Controls.Add(lookupProduct);
            groupControlAdd.Controls.Add(labelControlProduct);
            groupControlAdd.Location = new System.Drawing.Point(30, 100);
            groupControlAdd.Name = "groupControlAdd";
            groupControlAdd.Size = new System.Drawing.Size(1860, 120);
            groupControlAdd.TabIndex = 17;
            groupControlAdd.Text = "Add Product to Promotion";
            // 
            // btnAddProduct
            // 
            btnAddProduct.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnAddProduct.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnAddProduct.Appearance.Options.UseBackColor = true;
            btnAddProduct.Appearance.Options.UseFont = true;
            btnAddProduct.Location = new System.Drawing.Point(1710, 50);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new System.Drawing.Size(120, 40);
            btnAddProduct.TabIndex = 6;
            btnAddProduct.Text = "Add / Update";
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // txtDiscountValue
            // 
            txtDiscountValue.Location = new System.Drawing.Point(1450, 55);
            txtDiscountValue.Name = "txtDiscountValue";
            txtDiscountValue.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtDiscountValue.Properties.Appearance.Options.UseFont = true;
            txtDiscountValue.Size = new System.Drawing.Size(200, 26);
            txtDiscountValue.TabIndex = 5;
            // 
            // labelControlValue
            // 
            labelControlValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            labelControlValue.Appearance.Options.UseFont = true;
            labelControlValue.Location = new System.Drawing.Point(1450, 32);
            labelControlValue.Name = "labelControlValue";
            labelControlValue.Size = new System.Drawing.Size(32, 17);
            labelControlValue.TabIndex = 4;
            labelControlValue.Text = "Value";
            // 
            // cmbDiscountType
            // 
            cmbDiscountType.Location = new System.Drawing.Point(1200, 55);
            cmbDiscountType.Name = "cmbDiscountType";
            cmbDiscountType.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            cmbDiscountType.Properties.Appearance.Options.UseFont = true;
            cmbDiscountType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbDiscountType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbDiscountType.Size = new System.Drawing.Size(200, 26);
            cmbDiscountType.TabIndex = 3;
            // 
            // labelControlType
            // 
            labelControlType.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            labelControlType.Appearance.Options.UseFont = true;
            labelControlType.Location = new System.Drawing.Point(1200, 32);
            labelControlType.Name = "labelControlType";
            labelControlType.Size = new System.Drawing.Size(82, 17);
            labelControlType.TabIndex = 2;
            labelControlType.Text = "Discount Type";
            // 
            // lookupProduct
            // 
            lookupProduct.Location = new System.Drawing.Point(20, 55);
            lookupProduct.Name = "lookupProduct";
            lookupProduct.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            lookupProduct.Properties.Appearance.Options.UseFont = true;
            lookupProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            lookupProduct.Properties.NullText = "Select a Product...";
            lookupProduct.Size = new System.Drawing.Size(1150, 26);
            lookupProduct.TabIndex = 1;
            // 
            // labelControlProduct
            // 
            labelControlProduct.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            labelControlProduct.Appearance.Options.UseFont = true;
            labelControlProduct.Location = new System.Drawing.Point(20, 32);
            labelControlProduct.Name = "labelControlProduct";
            labelControlProduct.Size = new System.Drawing.Size(45, 17);
            labelControlProduct.TabIndex = 0;
            labelControlProduct.Text = "Product";
            // 
            // btnBack
            // 
            btnBack.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnBack.Appearance.Options.UseFont = true;
            btnBack.Location = new System.Drawing.Point(30, 933);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(150, 44);
            btnBack.TabIndex = 16;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // labelControlTitle
            // 
            labelControlTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControlTitle.Appearance.Options.UseFont = true;
            labelControlTitle.Location = new System.Drawing.Point(30, 30);
            labelControlTitle.Name = "labelControlTitle";
            labelControlTitle.Size = new System.Drawing.Size(518, 40);
            labelControlTitle.TabIndex = 1;
            labelControlTitle.Text = "Manage Products: [Promotion Name]";
            // 
            // UC_Promotion_Products
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_Promotion_Products";
            Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupControlAdd).EndInit();
            groupControlAdd.ResumeLayout(false);
            groupControlAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtDiscountValue.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbDiscountType.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)lookupProduct.Properties).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControlTitle;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.GroupControl groupControlAdd;
        private DevExpress.XtraEditors.LookUpEdit lookupProduct;
        private DevExpress.XtraEditors.LabelControl labelControlProduct;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDiscountType;
        private DevExpress.XtraEditors.LabelControl labelControlType;
        private DevExpress.XtraEditors.TextEdit txtDiscountValue;
        private DevExpress.XtraEditors.LabelControl labelControlValue;
        private DevExpress.XtraEditors.SimpleButton btnAddProduct;
        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProducts;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
    }
}
