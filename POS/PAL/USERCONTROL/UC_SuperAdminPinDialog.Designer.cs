namespace POS.PAL.USERCONTROL
{
    partial class UC_SuperAdminPinDialog
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
            components = new System.ComponentModel.Container();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtPin = new DevExpress.XtraEditors.TextEdit();
            btnVerify = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(components);
            ((System.ComponentModel.ISupportInitialize)txtPin.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)behaviorManager1).BeginInit();
            SuspendLayout();
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(20, 20);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(204, 21);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Super Admin PIN Required";
            // 
            // txtPin
            // 
            txtPin.Location = new System.Drawing.Point(20, 90);
            txtPin.Name = "txtPin";
            txtPin.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            txtPin.Properties.Appearance.Options.UseFont = true;
            txtPin.Properties.Appearance.Options.UseTextOptions = true;
            txtPin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtPin.Properties.MaxLength = 6;
            txtPin.Properties.PasswordChar = '*';
            txtPin.Size = new System.Drawing.Size(360, 32);
            txtPin.TabIndex = 1;
            // 
            // btnVerify
            // 
            btnVerify.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnVerify.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnVerify.Appearance.ForeColor = System.Drawing.Color.White;
            btnVerify.Appearance.Options.UseBackColor = true;
            btnVerify.Appearance.Options.UseFont = true;
            btnVerify.Appearance.Options.UseForeColor = true;
            btnVerify.Location = new System.Drawing.Point(20, 140);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new System.Drawing.Size(170, 40);
            btnVerify.TabIndex = 2;
            btnVerify.Text = "Verify";
            btnVerify.Click += btnVerify_Click;
            // 
            // btnCancel
            // 
            btnCancel.Appearance.BackColor = System.Drawing.Color.IndianRed;
            btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.Appearance.ForeColor = System.Drawing.Color.White;
            btnCancel.Appearance.Options.UseBackColor = true;
            btnCancel.Appearance.Options.UseFont = true;
            btnCancel.Appearance.Options.UseForeColor = true;
            btnCancel.Location = new System.Drawing.Point(210, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(170, 40);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Location = new System.Drawing.Point(20, 55);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(288, 17);
            labelControl2.TabIndex = 4;
            labelControl2.Text = "Enter Super Admin PIN to modify license settings:";
            // 
            // UC_SuperAdminPinDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(labelControl2);
            Controls.Add(btnCancel);
            Controls.Add(btnVerify);
            Controls.Add(txtPin);
            Controls.Add(labelControl1);
            Name = "UC_SuperAdminPinDialog";
            Size = new System.Drawing.Size(400, 200);
            ((System.ComponentModel.ISupportInitialize)txtPin.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)behaviorManager1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPin;
        private DevExpress.XtraEditors.SimpleButton btnVerify;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}
