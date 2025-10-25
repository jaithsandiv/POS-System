namespace POS
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            pnlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)pnlMain).BeginInit();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Appearance.BackColor = System.Drawing.Color.White;
            pnlMain.Appearance.Options.UseBackColor = true;
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(1920, 1050);
            pnlMain.TabIndex = 0;
            // 
            // Main
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseFont = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1920, 1050);
            Controls.Add(pnlMain);
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            IconOptions.Image = (System.Drawing.Image)resources.GetObject("Main.IconOptions.Image");
            IconOptions.LargeImage = (System.Drawing.Image)resources.GetObject("Main.IconOptions.LargeImage");
            LookAndFeel.SkinName = "The Bezier";
            LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.False;
            LookAndFeel.UseDefaultLookAndFeel = false;
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(1920, 1080);
            Name = "Main";
            Text = "Serendib POS";
            ((System.ComponentModel.ISupportInitialize)pnlMain).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
    }
}

