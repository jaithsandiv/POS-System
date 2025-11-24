namespace POS.PAL.USERCONTROL
{
    partial class UC_SystemSettings
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.cmbThermalPrinter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tsKOT = new DevExpress.XtraEditors.ToggleSwitch();
            this.rgPrintFormat = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThermalPrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsKOT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgPrintFormat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1000, 60);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(145, 25);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "System Settings";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.cmbThermalPrinter);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.tsKOT);
            this.groupControl1.Controls.Add(this.rgPrintFormat);
            this.groupControl1.Location = new System.Drawing.Point(20, 80);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(600, 400);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "General Settings";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(450, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Changes";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbThermalPrinter
            // 
            this.cmbThermalPrinter.Location = new System.Drawing.Point(200, 250);
            this.cmbThermalPrinter.Name = "cmbThermalPrinter";
            this.cmbThermalPrinter.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbThermalPrinter.Properties.Appearance.Options.UseFont = true;
            this.cmbThermalPrinter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbThermalPrinter.Size = new System.Drawing.Size(300, 24);
            this.cmbThermalPrinter.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(30, 253);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(130, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Thermal Printer Name:";
            // 
            // tsKOT
            // 
            this.tsKOT.Location = new System.Drawing.Point(30, 200);
            this.tsKOT.Name = "tsKOT";
            this.tsKOT.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tsKOT.Properties.Appearance.Options.UseFont = true;
            this.tsKOT.Properties.OffText = "KOT (Kitchen Order Ticket) Disabled";
            this.tsKOT.Properties.OnText = "KOT (Kitchen Order Ticket) Enabled";
            this.tsKOT.Size = new System.Drawing.Size(400, 22);
            this.tsKOT.TabIndex = 3;
            // 
            // rgPrintFormat
            // 
            this.rgPrintFormat.Location = new System.Drawing.Point(30, 50);
            this.rgPrintFormat.Name = "rgPrintFormat";
            this.rgPrintFormat.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rgPrintFormat.Properties.Appearance.Options.UseFont = true;
            this.rgPrintFormat.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("THERMAL", "Thermal Receipt (80mm)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("A4", "A4 Invoice")});
            this.rgPrintFormat.Size = new System.Drawing.Size(400, 80);
            this.rgPrintFormat.TabIndex = 0;
            // 
            // UC_SystemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_SystemSettings";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThermalPrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsKOT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgPrintFormat.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.RadioGroup rgPrintFormat;
        private DevExpress.XtraEditors.ToggleSwitch tsKOT;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbThermalPrinter;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}
