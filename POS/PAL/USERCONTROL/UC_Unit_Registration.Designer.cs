namespace POS.PAL.USERCONTROL
{
    partial class UC_Unit_Registration
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
            this.lblUnitCode = new DevExpress.XtraEditors.LabelControl();
            this.txtUnitCode = new DevExpress.XtraEditors.TextEdit();
            this.lblUnitName = new DevExpress.XtraEditors.LabelControl();
            this.txtUnitName = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitName.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(155, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Unit Registration";
            //
            // lblUnitCode
            //
            this.lblUnitCode.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblUnitCode.Appearance.Options.UseFont = true;
            this.lblUnitCode.Location = new System.Drawing.Point(30, 70);
            this.lblUnitCode.Name = "lblUnitCode";
            this.lblUnitCode.Size = new System.Drawing.Size(71, 17);
            this.lblUnitCode.TabIndex = 1;
            this.lblUnitCode.Text = "Unit Code *";
            //
            // txtUnitCode
            //
            this.txtUnitCode.Location = new System.Drawing.Point(30, 95);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtUnitCode.Properties.Appearance.Options.UseFont = true;
            this.txtUnitCode.Size = new System.Drawing.Size(300, 24);
            this.txtUnitCode.TabIndex = 2;
            //
            // lblUnitName
            //
            this.lblUnitName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblUnitName.Appearance.Options.UseFont = true;
            this.lblUnitName.Location = new System.Drawing.Point(30, 135);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(75, 17);
            this.lblUnitName.TabIndex = 3;
            this.lblUnitName.Text = "Unit Name *";
            //
            // txtUnitName
            //
            this.txtUnitName.Location = new System.Drawing.Point(30, 160);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtUnitName.Properties.Appearance.Options.UseFont = true;
            this.txtUnitName.Size = new System.Drawing.Size(300, 24);
            this.txtUnitName.TabIndex = 4;
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
            // UC_Unit_Registration
            //
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtUnitName);
            this.Controls.Add(this.lblUnitName);
            this.Controls.Add(this.txtUnitCode);
            this.Controls.Add(this.lblUnitCode);
            this.Controls.Add(this.lblTitle);
            this.Name = "UC_Unit_Registration";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblUnitCode;
        private DevExpress.XtraEditors.TextEdit txtUnitCode;
        private DevExpress.XtraEditors.LabelControl lblUnitName;
        private DevExpress.XtraEditors.TextEdit txtUnitName;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
