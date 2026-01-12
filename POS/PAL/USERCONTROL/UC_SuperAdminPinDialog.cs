using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SuperAdminPinDialog : DevExpress.XtraEditors.XtraUserControl
    {
        public bool PinVerified { get; private set; }
        public string EnteredPin { get; private set; }

        public UC_SuperAdminPinDialog()
        {
            InitializeComponent();
            PinVerified = false;
            
            // Allow Enter key to verify
            txtPin.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    e.Handled = true;
                    btnVerify_Click(null, null);
                }
            };
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            EnteredPin = txtPin.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(EnteredPin))
            {
                XtraMessageBox.Show("Please enter a PIN.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPin.Focus();
                return;
            }

            PinVerified = true;
            CloseDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PinVerified = false;
            EnteredPin = null;
            CloseDialog();
        }

        private void CloseDialog()
        {
            // Find parent form and close it
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                parentForm.DialogResult = PinVerified ? DialogResult.OK : DialogResult.Cancel;
                parentForm.Close();
            }
        }

        /// <summary>
        /// Shows the PIN dialog and returns verification result
        /// </summary>
        public static (bool verified, string pin) ShowDialog()
        {
            using (XtraForm form = new XtraForm())
            {
                form.Text = "Super Admin Verification";
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ClientSize = new System.Drawing.Size(400, 200);
                
                UC_SuperAdminPinDialog dialog = new UC_SuperAdminPinDialog();
                dialog.Dock = DockStyle.Fill;
                form.Controls.Add(dialog);
                
                form.ShowDialog();
                
                return (dialog.PinVerified, dialog.EnteredPin);
            }
        }
    }
}
