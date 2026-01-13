using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    /// <summary>
    /// User control to display trial expiration warnings
    /// </summary>
    public partial class UC_TrialExpirationWarning : DevExpress.XtraEditors.XtraUserControl
    {
        private BLL_TrialManager.TrialStatus _trialStatus;

        public UC_TrialExpirationWarning()
        {
            InitializeComponent();
            LoadTrialStatus();
        }

        private void LoadTrialStatus()
        {
            _trialStatus = BLL_TrialManager.GetTrialStatus();

            // Update UI based on trial status
            if (_trialStatus.IsLicensed)
            {
                this.Visible = false;
                return;
            }

            if (_trialStatus.IsTrialExpired)
            {
                ShowExpiredWarning();
            }
            else if (_trialStatus.IsExpiringSoon)
            {
                ShowExpiringSoonWarning();
            }
            else
            {
                this.Visible = false;
            }
        }

        private void ShowExpiredWarning()
        {
            lblWarningTitle.Text = "?? Trial Period Expired";
            lblWarningTitle.ForeColor = Color.DarkRed;
            
            lblWarningMessage.Text = BLL_TrialManager.GetWarningMessage();
            lblWarningMessage.ForeColor = Color.DarkRed;

            btnActivateLicense.Text = "Activate License Now";
            btnActivateLicense.Visible = true;

            this.BackColor = Color.FromArgb(255, 240, 240); // Light red background
        }

        private void ShowExpiringSoonWarning()
        {
            lblWarningTitle.Text = "?? Trial Period Expiring Soon";
            lblWarningTitle.ForeColor = Color.DarkOrange;

            lblWarningMessage.Text = BLL_TrialManager.GetWarningMessage();
            lblWarningMessage.ForeColor = Color.DarkOrange;

            btnActivateLicense.Text = "Activate License";
            btnActivateLicense.Visible = true;

            this.BackColor = Color.FromArgb(255, 250, 230); // Light yellow background
        }

        private void btnActivateLicense_Click(object sender, EventArgs e)
        {
            // Navigate to Business Settings - License section
            Main.Instance.LoadUserControl(new UC_SystemSettings());
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        #region Designer Code

        private void InitializeComponent()
        {
            this.lblWarningTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblWarningMessage = new DevExpress.XtraEditors.LabelControl();
            this.btnActivateLicense = new DevExpress.XtraEditors.SimpleButton();
            this.btnDismiss = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWarningTitle
            // 
            this.lblWarningTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWarningTitle.Appearance.Options.UseFont = true;
            this.lblWarningTitle.Location = new System.Drawing.Point(15, 15);
            this.lblWarningTitle.Name = "lblWarningTitle";
            this.lblWarningTitle.Size = new System.Drawing.Size(150, 21);
            this.lblWarningTitle.TabIndex = 0;
            this.lblWarningTitle.Text = "Trial Warning Title";
            // 
            // lblWarningMessage
            // 
            this.lblWarningMessage.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWarningMessage.Appearance.Options.UseFont = true;
            this.lblWarningMessage.Location = new System.Drawing.Point(15, 45);
            this.lblWarningMessage.Name = "lblWarningMessage";
            this.lblWarningMessage.Size = new System.Drawing.Size(500, 50);
            this.lblWarningMessage.TabIndex = 1;
            this.lblWarningMessage.Text = "Warning message goes here";
            this.lblWarningMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblWarningMessage.MaximumSize = new System.Drawing.Size(700, 0);
            // 
            // btnActivateLicense
            // 
            this.btnActivateLicense.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(167)))), ((int)(((byte)(140)))));
            this.btnActivateLicense.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnActivateLicense.Appearance.Options.UseBackColor = true;
            this.btnActivateLicense.Appearance.Options.UseFont = true;
            this.btnActivateLicense.Location = new System.Drawing.Point(15, 110);
            this.btnActivateLicense.Name = "btnActivateLicense";
            this.btnActivateLicense.Size = new System.Drawing.Size(150, 35);
            this.btnActivateLicense.TabIndex = 2;
            this.btnActivateLicense.Text = "Activate License";
            this.btnActivateLicense.Click += new System.EventHandler(this.btnActivateLicense_Click);
            // 
            // btnDismiss
            // 
            this.btnDismiss.Appearance.BackColor = System.Drawing.Color.White;
            this.btnDismiss.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnDismiss.Appearance.Options.UseBackColor = true;
            this.btnDismiss.Appearance.Options.UseFont = true;
            this.btnDismiss.Location = new System.Drawing.Point(175, 110);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(100, 35);
            this.btnDismiss.TabIndex = 3;
            this.btnDismiss.Text = "Dismiss";
            this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblWarningTitle);
            this.panelControl1.Controls.Add(this.lblWarningMessage);
            this.panelControl1.Controls.Add(this.btnActivateLicense);
            this.panelControl1.Controls.Add(this.btnDismiss);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 160);
            this.panelControl1.TabIndex = 4;
            // 
            // UC_TrialExpirationWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_TrialExpirationWarning";
            this.Size = new System.Drawing.Size(800, 160);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblWarningTitle;
        private DevExpress.XtraEditors.LabelControl lblWarningMessage;
        private DevExpress.XtraEditors.SimpleButton btnActivateLicense;
        private DevExpress.XtraEditors.SimpleButton btnDismiss;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
