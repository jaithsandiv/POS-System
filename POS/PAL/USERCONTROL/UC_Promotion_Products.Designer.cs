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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupControlAdd = new DevExpress.XtraEditors.GroupControl();
            this.btnAddProduct = new DevExpress.XtraEditors.SimpleButton();
            this.txtDiscountValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControlValue = new DevExpress.XtraEditors.LabelControl();
            this.cmbDiscountType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControlType = new DevExpress.XtraEditors.LabelControl();
            this.lookupProduct = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControlProduct = new DevExpress.XtraEditors.LabelControl();
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridViewProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlAdd)).BeginInit();
            this.groupControlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDiscountType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProducts)).BeginInit();
            this.SuspendLayout();
            //
            // panelControl1
            //
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnRemove);
            this.panelControl1.Controls.Add(this.gridControlProducts);
            this.panelControl1.Controls.Add(this.groupControlAdd);
            this.panelControl1.Controls.Add(this.btnBack);
            this.panelControl1.Controls.Add(this.labelControlTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1920, 1050);
            this.panelControl1.TabIndex = 0;
            //
            // labelControlTitle
            //
            this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlTitle.Appearance.Options.UseFont = true;
            this.labelControlTitle.Location = new System.Drawing.Point(30, 30);
            this.labelControlTitle.Name = "labelControlTitle";
            this.labelControlTitle.Size = new System.Drawing.Size(309, 40);
            this.labelControlTitle.TabIndex = 1;
            this.labelControlTitle.Text = "Manage Products: [Promotion Name]";
            //
            // btnBack
            //
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(30, 980);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(150, 44);
            this.btnBack.TabIndex = 16;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // groupControlAdd
            //
            this.groupControlAdd.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupControlAdd.AppearanceCaption.Options.UseFont = true;
            this.groupControlAdd.Controls.Add(this.btnAddProduct);
            this.groupControlAdd.Controls.Add(this.txtDiscountValue);
            this.groupControlAdd.Controls.Add(this.labelControlValue);
            this.groupControlAdd.Controls.Add(this.cmbDiscountType);
            this.groupControlAdd.Controls.Add(this.labelControlType);
            this.groupControlAdd.Controls.Add(this.lookupProduct);
            this.groupControlAdd.Controls.Add(this.labelControlProduct);
            this.groupControlAdd.Location = new System.Drawing.Point(30, 100);
            this.groupControlAdd.Name = "groupControlAdd";
            this.groupControlAdd.Size = new System.Drawing.Size(1860, 120);
            this.groupControlAdd.TabIndex = 17;
            this.groupControlAdd.Text = "Add Product to Promotion";
            //
            // btnAddProduct
            //
            this.btnAddProduct.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnAddProduct.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.Appearance.Options.UseBackColor = true;
            this.btnAddProduct.Appearance.Options.UseFont = true;
            this.btnAddProduct.Location = new System.Drawing.Point(1710, 50);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(120, 40);
            this.btnAddProduct.TabIndex = 6;
            this.btnAddProduct.Text = "Add / Update";
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            //
            // txtDiscountValue
            //
            this.txtDiscountValue.Location = new System.Drawing.Point(1450, 55);
            this.txtDiscountValue.Name = "txtDiscountValue";
            this.txtDiscountValue.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiscountValue.Properties.Appearance.Options.UseFont = true;
            this.txtDiscountValue.Size = new System.Drawing.Size(200, 26);
            this.txtDiscountValue.TabIndex = 5;
            //
            // labelControlValue
            //
            this.labelControlValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelControlValue.Appearance.Options.UseFont = true;
            this.labelControlValue.Location = new System.Drawing.Point(1450, 32);
            this.labelControlValue.Name = "labelControlValue";
            this.labelControlValue.Size = new System.Drawing.Size(32, 17);
            this.labelControlValue.TabIndex = 4;
            this.labelControlValue.Text = "Value";
            //
            // cmbDiscountType
            //
            this.cmbDiscountType.Location = new System.Drawing.Point(1200, 55);
            this.cmbDiscountType.Name = "cmbDiscountType";
            this.cmbDiscountType.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbDiscountType.Properties.Appearance.Options.UseFont = true;
            this.cmbDiscountType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDiscountType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDiscountType.Size = new System.Drawing.Size(200, 26);
            this.cmbDiscountType.TabIndex = 3;
            //
            // labelControlType
            //
            this.labelControlType.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelControlType.Appearance.Options.UseFont = true;
            this.labelControlType.Location = new System.Drawing.Point(1200, 32);
            this.labelControlType.Name = "labelControlType";
            this.labelControlType.Size = new System.Drawing.Size(83, 17);
            this.labelControlType.TabIndex = 2;
            this.labelControlType.Text = "Discount Type";
            //
            // lookupProduct
            //
            this.lookupProduct.Location = new System.Drawing.Point(20, 55);
            this.lookupProduct.Name = "lookupProduct";
            this.lookupProduct.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lookupProduct.Properties.Appearance.Options.UseFont = true;
            this.lookupProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupProduct.Properties.NullText = "Select a Product...";
            this.lookupProduct.Size = new System.Drawing.Size(1150, 26);
            this.lookupProduct.TabIndex = 1;
            //
            // labelControlProduct
            //
            this.labelControlProduct.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelControlProduct.Appearance.Options.UseFont = true;
            this.labelControlProduct.Location = new System.Drawing.Point(20, 32);
            this.labelControlProduct.Name = "labelControlProduct";
            this.labelControlProduct.Size = new System.Drawing.Size(46, 17);
            this.labelControlProduct.TabIndex = 0;
            this.labelControlProduct.Text = "Product";
            //
            // gridControlProducts
            //
            this.gridControlProducts.Location = new System.Drawing.Point(30, 240);
            this.gridControlProducts.MainView = this.gridViewProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.Size = new System.Drawing.Size(1860, 720);
            this.gridControlProducts.TabIndex = 18;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProducts});
            //
            // gridViewProducts
            //
            this.gridViewProducts.GridControl = this.gridControlProducts;
            this.gridViewProducts.Name = "gridViewProducts";
            this.gridViewProducts.OptionsBehavior.Editable = false;
            this.gridViewProducts.OptionsView.ShowGroupPanel = false;
            //
            // btnRemove
            //
            this.btnRemove.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnRemove.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRemove.Appearance.Options.UseBackColor = true;
            this.btnRemove.Appearance.Options.UseFont = true;
            this.btnRemove.Location = new System.Drawing.Point(1740, 980);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(150, 44);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "Remove Selected";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            //
            // UC_Promotion_Products
            //
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_Promotion_Products";
            this.Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlAdd)).EndInit();
            this.groupControlAdd.ResumeLayout(false);
            this.groupControlAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDiscountType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProducts)).EndInit();
            this.ResumeLayout(false);

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
