namespace POS.PAL.USERCONTROL
{
    partial class UC_Brand_Registration
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
            this.lblBrandName = new DevExpress.XtraEditors.LabelControl();
            this.txtBrandName = new DevExpress.XtraEditors.TextEdit();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.lueSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrandName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(167, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Brand Registration";
            //
            // lblBrandName
            //
            this.lblBrandName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblBrandName.Appearance.Options.UseFont = true;
            this.lblBrandName.Location = new System.Drawing.Point(30, 70);
            this.lblBrandName.Name = "lblBrandName";
            this.lblBrandName.Size = new System.Drawing.Size(79, 17);
            this.lblBrandName.TabIndex = 1;
            this.lblBrandName.Text = "Brand Name *";
            //
            // txtBrandName
            //
            this.txtBrandName.Location = new System.Drawing.Point(30, 95);
            this.txtBrandName.Name = "txtBrandName";
            this.txtBrandName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtBrandName.Properties.Appearance.Options.UseFont = true;
            this.txtBrandName.Size = new System.Drawing.Size(300, 24);
            this.txtBrandName.TabIndex = 2;
            //
            // lblSupplier
            //
            this.lblSupplier.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSupplier.Appearance.Options.UseFont = true;
            this.lblSupplier.Location = new System.Drawing.Point(30, 135);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(49, 17);
            this.lblSupplier.TabIndex = 3;
            this.lblSupplier.Text = "Supplier";
            //
            // lueSupplier
            //
            this.lueSupplier.Location = new System.Drawing.Point(30, 160);
            this.lueSupplier.Name = "lueSupplier";
            this.lueSupplier.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lueSupplier.Properties.Appearance.Options.UseFont = true;
            this.lueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSupplier.Properties.NullText = "Select Supplier";
            this.lueSupplier.Size = new System.Drawing.Size(300, 24);
            this.lueSupplier.TabIndex = 4;
            //
            // btnSave
            //
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(30, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(140, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // UC_Brand_Registration
            //
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lueSupplier);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.txtBrandName);
            this.Controls.Add(this.lblBrandName);
            this.Controls.Add(this.lblTitle);
            this.Name = "UC_Brand_Registration";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.txtBrandName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblBrandName;
        private DevExpress.XtraEditors.TextEdit txtBrandName;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.LookUpEdit lueSupplier;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
