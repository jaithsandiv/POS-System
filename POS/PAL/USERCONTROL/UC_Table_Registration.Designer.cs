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
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            labelControlCapacity = new DevExpress.XtraEditors.LabelControl();
            txtCapacity = new DevExpress.XtraEditors.TextEdit();
            labelControlTableNumber = new DevExpress.XtraEditors.LabelControl();
            txtTableNumber = new DevExpress.XtraEditors.TextEdit();
            regBtn = new DevExpress.XtraEditors.SimpleButton();
            btnBack = new DevExpress.XtraEditors.SimpleButton();
            labelControlSubHeader = new DevExpress.XtraEditors.LabelControl();
            labelControlHeader = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtCapacity.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTableNumber.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(labelControlCapacity);
            panelControl1.Controls.Add(txtCapacity);
            panelControl1.Controls.Add(labelControlTableNumber);
            panelControl1.Controls.Add(txtTableNumber);
            panelControl1.Controls.Add(regBtn);
            panelControl1.Controls.Add(btnBack);
            panelControl1.Controls.Add(labelControlSubHeader);
            panelControl1.Controls.Add(labelControlHeader);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1050);
            panelControl1.TabIndex = 0;
            // 
            // labelControlCapacity
            // 
            labelControlCapacity.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControlCapacity.Appearance.Options.UseFont = true;
            labelControlCapacity.Location = new System.Drawing.Point(995, 429);
            labelControlCapacity.Name = "labelControlCapacity";
            labelControlCapacity.Size = new System.Drawing.Size(52, 17);
            labelControlCapacity.TabIndex = 83;
            labelControlCapacity.Text = "Capacity:";
            // 
            // txtCapacity
            // 
            txtCapacity.Location = new System.Drawing.Point(995, 452);
            txtCapacity.Name = "txtCapacity";
            txtCapacity.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtCapacity.Properties.Appearance.Options.UseFont = true;
            txtCapacity.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtCapacity.Size = new System.Drawing.Size(480, 44);
            txtCapacity.TabIndex = 82;
            // 
            // labelControlTableNumber
            // 
            labelControlTableNumber.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControlTableNumber.Appearance.Options.UseFont = true;
            labelControlTableNumber.Location = new System.Drawing.Point(381, 429);
            labelControlTableNumber.Name = "labelControlTableNumber";
            labelControlTableNumber.Size = new System.Drawing.Size(87, 17);
            labelControlTableNumber.TabIndex = 80;
            labelControlTableNumber.Text = "Table Number:";
            // 
            // txtTableNumber
            // 
            txtTableNumber.Location = new System.Drawing.Point(381, 452);
            txtTableNumber.Name = "txtTableNumber";
            txtTableNumber.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtTableNumber.Properties.Appearance.Options.UseFont = true;
            txtTableNumber.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtTableNumber.Size = new System.Drawing.Size(480, 44);
            txtTableNumber.TabIndex = 79;
            // 
            // regBtn
            // 
            regBtn.Appearance.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            regBtn.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            regBtn.Appearance.ForeColor = System.Drawing.Color.White;
            regBtn.Appearance.Options.UseBackColor = true;
            regBtn.Appearance.Options.UseFont = true;
            regBtn.Appearance.Options.UseForeColor = true;
            regBtn.Location = new System.Drawing.Point(1496, 860);
            regBtn.Name = "regBtn";
            regBtn.Size = new System.Drawing.Size(250, 44);
            regBtn.TabIndex = 94;
            regBtn.Text = "Register";
            regBtn.Click += RegBtn_Click;
            // 
            // btnBack
            // 
            btnBack.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnBack.Appearance.Options.UseFont = true;
            btnBack.Location = new System.Drawing.Point(175, 860);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(250, 44);
            btnBack.TabIndex = 81;
            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;
            // 
            // labelControlSubHeader
            // 
            labelControlSubHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            labelControlSubHeader.Appearance.Options.UseFont = true;
            labelControlSubHeader.Location = new System.Drawing.Point(189, 237);
            labelControlSubHeader.Name = "labelControlSubHeader";
            labelControlSubHeader.Size = new System.Drawing.Size(304, 40);
            labelControlSubHeader.TabIndex = 77;
            labelControlSubHeader.Text = "Add a new table profile.";
            // 
            // labelControlHeader
            // 
            labelControlHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold);
            labelControlHeader.Appearance.Options.UseFont = true;
            labelControlHeader.Location = new System.Drawing.Point(806, 98);
            labelControlHeader.Name = "labelControlHeader";
            labelControlHeader.Size = new System.Drawing.Size(251, 40);
            labelControlHeader.TabIndex = 76;
            labelControlHeader.Text = "Table Registration";
            // 
            // UC_Table_Registration
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_Table_Registration";
            Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtCapacity.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTableNumber.Properties).EndInit();
            ResumeLayout(false);

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
