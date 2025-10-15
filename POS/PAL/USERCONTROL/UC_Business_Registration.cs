using DevExpress.XtraEditors;
using POS.DAL.DataSource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Business_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private static DAL_DS_Initialize _dataSet = new DAL_DS_Initialize();

        public static DAL_DS_Initialize RegistrationDataSet 
        { 
            get { return _dataSet; } 
        }

        public UC_Business_Registration()
        {
            InitializeComponent();
            LoadPreviousData();
        }

        private void LoadPreviousData()
        {
            // Load previously entered data if navigating back
            if (_dataSet.Business.Count > 0)
            {
                var businessRow = _dataSet.Business[0];
                txtBusinessName.Text = businessRow.business_name;
                
                if (!businessRow.IslogoNull() && businessRow.logo != null)
                {
                    using (MemoryStream ms = new MemoryStream(businessRow.logo))
                    {
                        Logo.Image = Image.FromStream(ms);
                    }
                }
            }

            if (_dataSet.Store.Count > 0)
            {
                var storeRow = _dataSet.Store[0];
                txtPhoneNumber.Text = storeRow.phone;
                txtEmail.Text = storeRow.email;
                txtAddress.Text = storeRow.address;
                txtCity.Text = storeRow.city;
                txtState.Text = storeRow.state;
                txtCountry.Text = storeRow.country;
                txtPostalCode.Text = storeRow.postal_code;
            }
        }

        private void backBtn1_Click(object sender, EventArgs e)
        {
            // Clear registration data when going back to login
            _dataSet.Clear();

            UC_Login login = new UC_Login();
            Control parentPanel = this.Parent;

            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(login);
                login.Dock = DockStyle.Fill;
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            // Validate Business Name is required
            if (string.IsNullOrWhiteSpace(txtBusinessName.Text))
            {
                XtraMessageBox.Show("Business Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBusinessName.Focus();
                return;
            }

            // Save business data to DataSet
            _dataSet.Business.Clear();
            var businessRow = _dataSet.Business.NewBusinessRow();
            businessRow.business_name = txtBusinessName.Text.Trim();
            
            // Convert logo to byte array if exists
            if (Logo.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Logo.Image.Save(ms, Logo.Image.RawFormat);
                    businessRow.logo = ms.ToArray();
                }
            }
            
            _dataSet.Business.AddBusinessRow(businessRow);

            // Save store data to DataSet - use business name as store name
            _dataSet.Store.Clear();
            var storeRow = _dataSet.Store.NewStoreRow();
            storeRow.store_name = txtBusinessName.Text.Trim(); // Use business name for store name
            storeRow.phone = txtPhoneNumber.Text.Trim();
            storeRow.email = txtEmail.Text.Trim();
            storeRow.address = txtAddress.Text.Trim();
            storeRow.city = txtCity.Text.Trim();
            storeRow.state = txtState.Text.Trim();
            storeRow.country = txtCountry.Text.Trim();
            storeRow.postal_code = txtPostalCode.Text.Trim();
            
            _dataSet.Store.AddStoreRow(storeRow);

            // Navigate to User Registration
            UC_User_Registration userRegistration = new UC_User_Registration();
            Control parentPanel = this.Parent;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(userRegistration);
                userRegistration.Dock = DockStyle.Fill;
            }
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    Logo.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("An error occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
