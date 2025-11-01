namespace POS.PAL.USERCONTROL
{
    partial class UC_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Login));
            pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            txtUsername = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            btnSignIn = new DevExpress.XtraEditors.SimpleButton();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUsername.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            SuspendLayout();
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            pictureEdit1.Location = new System.Drawing.Point(180, 250);
            pictureEdit1.Margin = new System.Windows.Forms.Padding(4);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Size = new System.Drawing.Size(800, 520);
            pictureEdit1.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.Location = new System.Drawing.Point(1284, 446);
            txtUsername.Name = "txtUsername";
            txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtUsername.Properties.Appearance.Options.UseFont = true;
            txtUsername.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtUsername.Size = new System.Drawing.Size(480, 44);
            txtUsername.TabIndex = 2;
            txtUsername.KeyPress += txtUsername_KeyPress;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(1284, 423);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(121, 17);
            labelControl1.TabIndex = 4;
            labelControl1.Text = "Enter your username";
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Location = new System.Drawing.Point(1461, 358);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(94, 40);
            labelControl3.TabIndex = 7;
            labelControl3.Text = "Sign in";
            // 
            // labelControl4
            // 
            labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl4.Appearance.Options.UseFont = true;
            labelControl4.Location = new System.Drawing.Point(1284, 506);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(121, 17);
            labelControl4.TabIndex = 8;
            labelControl4.Text = "Enter your password";
            // 
            // btnSignIn
            // 
            btnSignIn.Appearance.BackColor = System.Drawing.Color.FromArgb(3, 167, 140);
            btnSignIn.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnSignIn.Appearance.Options.UseBackColor = true;
            btnSignIn.Appearance.Options.UseFont = true;
            btnSignIn.Location = new System.Drawing.Point(1284, 605);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new System.Drawing.Size(480, 44);
            btnSignIn.TabIndex = 9;
            btnSignIn.Text = "Sign in";
            btnSignIn.Click += btnSignIn_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(1284, 529);
            txtPassword.Name = "txtPassword";
            txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtPassword.Properties.Appearance.Options.UseFont = true;
            txtPassword.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtPassword.Properties.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(480, 44);
            txtPassword.TabIndex = 10;
            txtPassword.KeyPress += txtPassword_KeyPress;
            // 
            // UC_Login
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseFont = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(txtPassword);
            Controls.Add(btnSignIn);
            Controls.Add(labelControl4);
            Controls.Add(labelControl3);
            Controls.Add(labelControl1);
            Controls.Add(txtUsername);
            Controls.Add(pictureEdit1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "UC_Login";
            Size = new System.Drawing.Size(1920, 1050);
            Load += UC_Login_Load;
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUsername.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnSignIn;
        private DevExpress.XtraEditors.TextEdit txtPassword;
    }
}
