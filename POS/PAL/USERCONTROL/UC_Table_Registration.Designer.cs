namespace POS.PAL.USERCONTROL
{
    partial class UC_Table_Registration
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
            this.regBtn = new DevExpress.XtraEditors.SimpleButton();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlSubHeader = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHeader = new DevExpress.XtraEditors.LabelControl();
            this.txtTableNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControlTableNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtCapacity = new DevExpress.XtraEditors.TextEdit();
            this.labelControlCapacity = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCapacity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControlCapacity);
            this.panelControl1.Controls.Add(this.txtCapacity);
            this.panelControl1.Controls.Add(this.labelControlTableNumber);
            this.panelControl1.Controls.Add(this.txtTableNumber);
            this.panelControl1.Controls.Add(this.regBtn);
            this.panelControl1.Controls.Add(this.btnBack);
            this.panelControl1.Controls.Add(this.labelControlSubHeader);
            this.panelControl1.Controls.Add(this.labelControlHeader);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1920, 1050);
            this.panelControl1.TabIndex = 0;
            // 
            // regBtn
            // 
            this.regBtn.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(167)))), ((int)(((byte)(140)))));
            this.regBtn.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regBtn.Appearance.ForeColor = System.Drawing.Color.White;
            this.regBtn.Appearance.Options.UseBackColor = true;
            this.regBtn.Appearance.Options.UseFont = true;
            this.regBtn.Appearance.Options.UseForeColor = true;
            this.regBtn.Location = new System.Drawing.Point(1496, 860);
            this.regBtn.Name = "regBtn";
            this.regBtn.Size = new System.Drawing.Size(250, 44);
            this.regBtn.TabIndex = 94;
            this.regBtn.Text = "Register";
            this.regBtn.Click += new System.EventHandler(this.RegBtn_Click);
            // 
            // btnBack
            // 
            this.btnBack.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Appearance.Options.UseFont = true;
            this.btnBack.Location = new System.Drawing.Point(175, 860);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(250, 44);
            this.btnBack.TabIndex = 81;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // labelControlSubHeader
            // 
            this.labelControlSubHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlSubHeader.Appearance.Options.UseFont = true;
            this.labelControlSubHeader.Location = new System.Drawing.Point(175, 133);
            this.labelControlSubHeader.Name = "labelControlSubHeader";
            this.labelControlSubHeader.Size = new System.Drawing.Size(143, 17);
            this.labelControlSubHeader.TabIndex = 77;
            this.labelControlSubHeader.Text = "Add a new table profile.";
            // 
            // labelControlHeader
            // 
            this.labelControlHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlHeader.Appearance.Options.UseFont = true;
            this.labelControlHeader.Location = new System.Drawing.Point(175, 102);
            this.labelControlHeader.Name = "labelControlHeader";
            this.labelControlHeader.Size = new System.Drawing.Size(165, 25);
            this.labelControlHeader.TabIndex = 76;
            this.labelControlHeader.Text = "Table Registration";
            // 
            // txtTableNumber
            // 
            this.txtTableNumber.Location = new System.Drawing.Point(381, 452);
            this.txtTableNumber.Name = "txtTableNumber";
            this.txtTableNumber.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableNumber.Properties.Appearance.Options.UseFont = true;
            this.txtTableNumber.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtTableNumber.Size = new System.Drawing.Size(480, 44);
            this.txtTableNumber.TabIndex = 79;
            // 
            // labelControlTableNumber
            // 
            this.labelControlTableNumber.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlTableNumber.Appearance.Options.UseFont = true;
            this.labelControlTableNumber.Location = new System.Drawing.Point(381, 429);
            this.labelControlTableNumber.Name = "labelControlTableNumber";
            this.labelControlTableNumber.Size = new System.Drawing.Size(88, 17);
            this.labelControlTableNumber.TabIndex = 80;
            this.labelControlTableNumber.Text = "Table Number:";
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(995, 452);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapacity.Properties.Appearance.Options.UseFont = true;
            this.txtCapacity.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.txtCapacity.Size = new System.Drawing.Size(480, 44);
            this.txtCapacity.TabIndex = 82;
            // 
            // labelControlCapacity
            // 
            this.labelControlCapacity.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlCapacity.Appearance.Options.UseFont = true;
            this.labelControlCapacity.Location = new System.Drawing.Point(995, 429);
            this.labelControlCapacity.Name = "labelControlCapacity";
            this.labelControlCapacity.Size = new System.Drawing.Size(54, 17);
            this.labelControlCapacity.TabIndex = 83;
            this.labelControlCapacity.Text = "Capacity:";
            // 
            // UC_Table_Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_Table_Registration";
            this.Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCapacity.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton regBtn;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.LabelControl labelControlSubHeader;
        private DevExpress.XtraEditors.LabelControl labelControlHeader;
        private DevExpress.XtraEditors.TextEdit txtTableNumber;
        private DevExpress.XtraEditors.LabelControl labelControlTableNumber;
        private DevExpress.XtraEditors.TextEdit txtCapacity;
        private DevExpress.XtraEditors.LabelControl labelControlCapacity;
    }
}
