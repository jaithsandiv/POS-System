using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_BusinessLogo : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_BusinessLogo()
        {
            InitializeComponent();
            LoadBusinessLogo();
        }

        private void InitializeComponent()
        {
            pictureBoxLogo = new PictureBox();
            lblBusinessName = new LabelControl();
            panelContainer = new PanelControl();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelContainer).BeginInit();
            panelContainer.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Anchor = AnchorStyles.None;
            pictureBoxLogo.Location = new Point(760, 141);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(400, 400);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // lblBusinessName
            // 
            lblBusinessName.Anchor = AnchorStyles.None;
            lblBusinessName.Appearance.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBusinessName.Appearance.ForeColor = Color.FromArgb(64, 64, 64);
            lblBusinessName.Appearance.Options.UseFont = true;
            lblBusinessName.Appearance.Options.UseForeColor = true;
            lblBusinessName.Appearance.Options.UseTextOptions = true;
            lblBusinessName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblBusinessName.AutoSizeMode = LabelAutoSizeMode.None;
            lblBusinessName.Location = new Point(660, 561);
            lblBusinessName.Name = "lblBusinessName";
            lblBusinessName.Size = new Size(600, 40);
            lblBusinessName.TabIndex = 1;
            lblBusinessName.Text = "Business Name";
            // 
            // panelContainer
            // 
            panelContainer.Appearance.BackColor = Color.White;
            panelContainer.Appearance.Options.UseBackColor = true;
            panelContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelContainer.Controls.Add(pictureBoxLogo);
            panelContainer.Controls.Add(lblBusinessName);
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(0, 0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1920, 1001);
            panelContainer.TabIndex = 2;
            // 
            // UC_BusinessLogo
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelContainer);
            Name = "UC_BusinessLogo";
            Size = new Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelContainer).EndInit();
            panelContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private DevExpress.XtraEditors.LabelControl lblBusinessName;
        private DevExpress.XtraEditors.PanelControl panelContainer;

        /// <summary>
        /// Load and display the business logo from the database
        /// </summary>
        private void LoadBusinessLogo()
        {
            try
            {
                if (Main.DataSetApp?.Business != null && Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];

                    // Load business name
                    if (!businessRow.Isbusiness_nameNull() && !string.IsNullOrWhiteSpace(businessRow.business_name))
                    {
                        lblBusinessName.Text = businessRow.business_name;
                    }
                    else
                    {
                        lblBusinessName.Text = "Welcome";
                    }

                    // Load logo image
                    if (!businessRow.IslogoNull() && businessRow.logo != null && businessRow.logo.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(businessRow.logo))
                        {
                            pictureBoxLogo.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // Set a default placeholder if no logo exists
                        pictureBoxLogo.Image = null;
                        // Optionally, you could set a default image here
                    }

                    // Center the controls
                    CenterControls();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading business logo: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Centers the logo and business name on the form
        /// </summary>
        private void CenterControls()
        {
            this.SizeChanged += (s, e) =>
            {
                // Center logo horizontally and vertically
                pictureBoxLogo.Left = (this.Width - pictureBoxLogo.Width) / 2;
                pictureBoxLogo.Top = (this.Height - pictureBoxLogo.Height) / 2 - 50;

                // Center business name below logo
                lblBusinessName.Left = (this.Width - lblBusinessName.Width) / 2;
                lblBusinessName.Top = pictureBoxLogo.Bottom + 20;
            };

            // Trigger initial centering
            pictureBoxLogo.Left = (this.Width - pictureBoxLogo.Width) / 2;
            pictureBoxLogo.Top = (this.Height - pictureBoxLogo.Height) / 2 - 50;
            lblBusinessName.Left = (this.Width - lblBusinessName.Width) / 2;
            lblBusinessName.Top = pictureBoxLogo.Bottom + 20;
        }
    }
}
