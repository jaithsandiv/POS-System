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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtUnitName = new DevExpress.XtraEditors.TextEdit();
            this.lblUnitName = new DevExpress.XtraEditors.LabelControl();
            this.txtUnitCode = new DevExpress.XtraEditors.TextEdit();
            this.lblUnitCode = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblSubtitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).BeginInit();
            this.SuspendLayout();
            //
            // panelControl1
            //
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.txtUnitName);
            this.panelControl1.Controls.Add(this.lblUnitName);
            this.panelControl1.Controls.Add(this.txtUnitCode);
            this.panelControl1.Controls.Add(this.lblUnitCode);
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Controls.Add(this.lblSubtitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1920, 1050);
            this.panelControl1.TabIndex = 0;
            //
            // btnSave
            //
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(167)))), ((int)(((byte)(140)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1496, 860);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(250, 44);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(175, 860);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(250, 44);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Back";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // txtUnitName
            //
            this.txtUnitName.Location = new System.Drawing.Point(381, 452);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtUnitName.Properties.Appearance.Options.UseFont = true;
            this.txtUnitName.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtUnitName.Size = new System.Drawing.Size(480, 44);
            this.txtUnitName.TabIndex = 4;
            //
            // lblUnitName
            //
            this.lblUnitName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblUnitName.Appearance.Options.UseFont = true;
            this.lblUnitName.Location = new System.Drawing.Point(381, 429);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(75, 17);
            this.lblUnitName.TabIndex = 3;
            this.lblUnitName.Text = "Unit Name *";
            //
            // txtUnitCode
            //
            this.txtUnitCode.Location = new System.Drawing.Point(381, 366);
            this.txtUnitCode.Name = "txtUnitCode";
            this.txtUnitCode.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtUnitCode.Properties.Appearance.Options.UseFont = true;
            this.txtUnitCode.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtUnitCode.Size = new System.Drawing.Size(480, 44);
            this.txtUnitCode.TabIndex = 2;
            //
            // lblUnitCode
            //
            this.lblUnitCode.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblUnitCode.Appearance.Options.UseFont = true;
            this.lblUnitCode.Location = new System.Drawing.Point(381, 343);
            this.lblUnitCode.Name = "lblUnitCode";
            this.lblUnitCode.Size = new System.Drawing.Size(71, 17);
            this.lblUnitCode.TabIndex = 1;
            this.lblUnitCode.Text = "Unit Code *";
            //
            // lblTitle
            //
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(806, 98);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Unit Registration";
            //
            // lblSubtitle
            //
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.lblSubtitle.Appearance.Options.UseFont = true;
            this.lblSubtitle.Location = new System.Drawing.Point(189, 237);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(200, 40);
            this.lblSubtitle.TabIndex = 5;
            this.lblSubtitle.Text = "Add a new unit";
            //
            // UC_Unit_Registration
            //
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_Unit_Registration";
            this.Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnitCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private DevExpress.XtraEditors.TextEdit txtUnitName;
        private DevExpress.XtraEditors.LabelControl lblUnitName;
        private DevExpress.XtraEditors.TextEdit txtUnitCode;
        private DevExpress.XtraEditors.LabelControl lblUnitCode;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSubtitle;
    }
}
