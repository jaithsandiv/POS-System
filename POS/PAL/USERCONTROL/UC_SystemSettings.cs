using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SystemSettings : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SystemSettings _bllSystemSettings = new BLL_SystemSettings();
        private readonly BLL_Store _bllStore = new BLL_Store();
        private DataTable _settingsTable;
        private bool _isSuperAdmin = false;

        public UC_SystemSettings()
        {
            InitializeComponent();
            
            // Check if current user is super admin
            CheckSuperAdminStatus();
            
            // Hide License tab for non-super admins
            ConfigureLicenseTabVisibility();
            
            LoadPrinters();
            LoadSettings();
        }

        /// <summary>
        /// Checks if the current logged-in user is a super admin
        /// </summary>
        private void CheckSuperAdminStatus()
        {
            if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
            {
                var userRow = Main.DataSetApp.User[0];
                if (!userRow.Isis_super_adminNull())
                {
                    _isSuperAdmin = Convert.ToBoolean(userRow.is_super_admin);
                }
            }
        }

        /// <summary>
        /// Configures visibility of the License tab based on user role
        /// </summary>
        private void ConfigureLicenseTabVisibility()
        {
            // Hide License group for regular users
            if (grpLicense != null)
            {
                grpLicense.Visible = _isSuperAdmin;
                grpLicense.Enabled = _isSuperAdmin;
                
                // Additionally, ensure all controls within the license group are disabled for non-super admins
                if (!_isSuperAdmin)
                {
                    tsEnableTrial.Enabled = false;
                    txtTrialStartDate.Enabled = false;
                    txtTrialEndDate.Enabled = false;
                    txtTrialDays.Enabled = false;
                    chkIsLicensed.Enabled = false;
                }
            }
        }

        private void LoadPrinters()
        {
            cmbThermalPrinter.Properties.Items.Clear();
            cmbKOTPrinter.Properties.Items.Clear();
            cmbBarcodePrinter.Properties.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cmbThermalPrinter.Properties.Items.Add(printer);
                cmbKOTPrinter.Properties.Items.Add(printer);
                cmbBarcodePrinter.Properties.Items.Add(printer);
            }
        }

        private void LoadSettings()
        {
            try
            {
                // 1. Load System Settings (Key/Value)
                _settingsTable = _bllSystemSettings.GetSystemSettings();

                if (_settingsTable != null)
                {
                    // Printing
                    bool enableThermal = GetBooleanSetting("ENABLE_THERMAL_PRINT");
                    rgPrintFormat.EditValue = enableThermal ? "THERMAL" : "A4";
                    
                    string printerName = GetStringSetting("thermal_printer_name");
                    if (!string.IsNullOrEmpty(printerName))
                        cmbThermalPrinter.SelectedItem = printerName;

                    tsAutoPrint.IsOn = GetBooleanSetting("auto_print");
                    txtInvoiceFooter.Text = GetStringSetting("invoice_footer");

                    // Features
                    tsKOT.IsOn = GetBooleanSetting("kot_enabled");
                    string kotPrinter = GetStringSetting("kot_printer_name");
                    if (!string.IsNullOrEmpty(kotPrinter))
                        cmbKOTPrinter.SelectedItem = kotPrinter;

                    // Barcode Printer
                    string barcodePrinter = GetStringSetting("barcode_printer_name");
                    if (!string.IsNullOrEmpty(barcodePrinter))
                        cmbBarcodePrinter.SelectedItem = barcodePrinter;

                    tsStockCheck.IsOn = GetBooleanSetting("stock_check_enabled");

                    // Store Website (stored in settings for now)
                    txtWebsite.Text = GetStringSetting("store_website");
                }

                // 2. Load Business Info
                if (Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];
                    txtBusinessName.Text = businessRow.Isbusiness_nameNull() ? "" : businessRow.business_name;
                    
                    if (!businessRow.IslogoNull())
                    {
                        using (MemoryStream ms = new MemoryStream(businessRow.logo))
                        {
                            picLogo.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        picLogo.Image = null;
                    }
                }

                // 3. Load Store Info
                DataTable storeDt = _bllStore.GetStores(); 
                if (storeDt.Rows.Count > 0)
                {
                    DataRow storeRow = storeDt.Rows[0]; // Assume first store
                    txtPhone.Text = storeRow["phone"]?.ToString();
                    txtEmail.Text = storeRow["email"]?.ToString();
                    txtAddress.Text = storeRow["address"]?.ToString();
                    txtCity.Text = storeRow["city"]?.ToString();
                    txtState.Text = storeRow["state"]?.ToString();
                    txtCountry.Text = storeRow["country"]?.ToString();
                    txtPostalCode.Text = storeRow["postal_code"]?.ToString();
                }

                // Load License & Trial settings (super admin only)
                if (_isSuperAdmin)
                {
                    LoadLicenseSettings();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads license and trial period settings from the Business table
        /// </summary>
        private void LoadLicenseSettings()
        {
            try
            {
                if (Main.DataSetApp?.Business != null && Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];
                    
                    // Load trial period status - check if dates exist
                    bool hasTrialDates = false;
                    if (!businessRow.Istrial_start_dateNull() && !businessRow.Istrial_end_dateNull())
                    {
                        string startDateStr = businessRow.trial_start_date;
                        string endDateStr = businessRow.trial_end_date;
                        hasTrialDates = !string.IsNullOrWhiteSpace(startDateStr) && !string.IsNullOrWhiteSpace(endDateStr);
                    }
                    tsEnableTrial.IsOn = hasTrialDates;
                    
                    // Load trial dates
                    if (!businessRow.Istrial_start_dateNull() && !string.IsNullOrWhiteSpace(businessRow.trial_start_date))
                    {
                        if (DateTime.TryParse(businessRow.trial_start_date, out DateTime startDate))
                        {
                            txtTrialStartDate.Text = startDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            txtTrialStartDate.Text = "";
                        }
                    }
                    else
                    {
                        txtTrialStartDate.Text = "";
                    }
                    
                    if (!businessRow.Istrial_end_dateNull() && !string.IsNullOrWhiteSpace(businessRow.trial_end_date))
                    {
                        if (DateTime.TryParse(businessRow.trial_end_date, out DateTime endDate))
                        {
                            txtTrialEndDate.Text = endDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            txtTrialEndDate.Text = "";
                        }
                    }
                    else
                    {
                        txtTrialEndDate.Text = "";
                    }
                    
                    // Calculate trial days
                    if (!businessRow.Istrial_start_dateNull() && !businessRow.Istrial_end_dateNull() &&
                        !string.IsNullOrWhiteSpace(businessRow.trial_start_date) && 
                        !string.IsNullOrWhiteSpace(businessRow.trial_end_date))
                    {
                        if (DateTime.TryParse(businessRow.trial_start_date, out DateTime startDate) &&
                            DateTime.TryParse(businessRow.trial_end_date, out DateTime endDate))
                        {
                            int days = (endDate - startDate).Days;
                            txtTrialDays.Text = days.ToString();
                        }
                        else
                        {
                            txtTrialDays.Text = "3"; // Default trial period
                        }
                    }
                    else
                    {
                        txtTrialDays.Text = "3"; // Default trial period
                    }
                    
                    // Load licensed status - handle string comparison
                    bool isLicensed = false;
                    if (!businessRow.Isis_licensedNull())
                    {
                        string licensedValue = businessRow.is_licensed?.ToString();
                        isLicensed = licensedValue == "True" || licensedValue == "1";
                    }
                    chkIsLicensed.Checked = isLicensed;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading license settings: {ex.Message}\n\nDetails: {ex.StackTrace}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool GetBooleanSetting(string key)
        {
            if (_settingsTable == null) return false;
            DataRow[] rows = _settingsTable.Select($"setting_key = '{key}'");
            if (rows.Length > 0)
            {
                string value = rows[0]["setting_value"]?.ToString();
                return string.Equals(value, "True", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private string GetStringSetting(string key)
        {
            if (_settingsTable == null) return "";
            DataRow[] rows = _settingsTable.Select($"setting_key = '{key}'");
            if (rows.Length > 0)
            {
                return rows[0]["setting_value"]?.ToString();
            }
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = 1; // Default to 1 if user not found
                if (Main.DataSetApp.User.Count > 0 && !Main.DataSetApp.User[0].Isuser_idNull())
                {
                     int.TryParse(Main.DataSetApp.User[0].user_id, out userId);
                }

                // 1. Save System Settings
                _bllSystemSettings.UpdateSystemSetting("ENABLE_THERMAL_PRINT", (rgPrintFormat.EditValue?.ToString() == "THERMAL").ToString(), userId);
                _bllSystemSettings.UpdateSystemSetting("thermal_printer_name", cmbThermalPrinter.Text, userId);
                _bllSystemSettings.UpdateSystemSetting("auto_print", tsAutoPrint.IsOn.ToString(), userId);
                _bllSystemSettings.UpdateSystemSetting("invoice_footer", txtInvoiceFooter.Text, userId);
                
                _bllSystemSettings.UpdateSystemSetting("kot_enabled", tsKOT.IsOn.ToString(), userId);
                _bllSystemSettings.UpdateSystemSetting("kot_printer_name", cmbKOTPrinter.Text, userId);
                _bllSystemSettings.UpdateSystemSetting("barcode_printer_name", cmbBarcodePrinter.Text, userId);
                _bllSystemSettings.UpdateSystemSetting("stock_check_enabled", tsStockCheck.IsOn.ToString(), userId);
                
                _bllSystemSettings.UpdateSystemSetting("store_website", txtWebsite.Text, userId);

                // 2. Save Business Info
                byte[] logoBytes = null;
                if (picLogo.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                        picLogo.Image.Save(ms, format);
                        logoBytes = ms.ToArray();
                    }
                }
                _bllSystemSettings.UpdateBusinessSettings(txtBusinessName.Text, logoBytes, userId);

                // 3. Save Store Info
                // Assuming Store ID 1
                _bllSystemSettings.UpdateStoreSettings(1, "Main Store", txtPhone.Text, txtEmail.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtCountry.Text, txtPostalCode.Text, userId);

                // Save License & Trial settings (super admin only)
                if (_isSuperAdmin)
                {
                    SaveLicenseSettings(userId);
                }

                XtraMessageBox.Show("Settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Reload global settings in Main to apply changes immediately
                Main.Instance?.LoadSystemSettings();
                Main.Instance?.LoadBusinessData();
                
                // Reload settings to refresh UI/State
                LoadSettings();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves license and trial period settings to the Business table
        /// </summary>
        private void SaveLicenseSettings(int userId)
        {
            try
            {
                // Double-check super admin permission before saving license settings
                if (!_isSuperAdmin)
                {
                    throw new UnauthorizedAccessException("Only Super Admin users can modify license settings.");
                }
                
                bool enableTrial = tsEnableTrial.IsOn;
                int trialDays = 3; // Default
                
                if (!string.IsNullOrWhiteSpace(txtTrialDays.Text))
                {
                    if (!int.TryParse(txtTrialDays.Text, out trialDays) || trialDays < 0)
                    {
                        trialDays = 3; // Fallback to default
                    }
                }
                
                bool isLicensed = chkIsLicensed.Checked;
                
                // Update business license settings
                DateTime? trialStartDate = null;
                DateTime? trialEndDate = null;
                
                if (enableTrial)
                {
                    // If enabling trial for the first time OR trial dates are null, set new dates
                    if (Main.DataSetApp.Business[0].Istrial_start_dateNull() || 
                        string.IsNullOrWhiteSpace(Main.DataSetApp.Business[0].trial_start_date))
                    {
                        trialStartDate = DateTime.Now;
                        trialEndDate = DateTime.Now.AddDays(trialDays);
                    }
                    else
                    {
                        // Keep existing dates if they exist
                        if (DateTime.TryParse(Main.DataSetApp.Business[0].trial_start_date, out DateTime parsedStart))
                        {
                            trialStartDate = parsedStart;
                        }
                        else
                        {
                            trialStartDate = DateTime.Now;
                        }
                        
                        if (DateTime.TryParse(Main.DataSetApp.Business[0].trial_end_date, out DateTime parsedEnd))
                        {
                            trialEndDate = parsedEnd;
                        }
                        else
                        {
                            trialEndDate = DateTime.Now.AddDays(trialDays);
                        }
                    }
                }
                else
                {
                    // Disabling trial - set dates to null
                    trialStartDate = null;
                    trialEndDate = null;
                }
                
                // Call BLL method to update license settings
                bool success = _bllSystemSettings.UpdateLicenseSettings(trialStartDate, trialEndDate, isLicensed, userId);
                
                if (!success)
                {
                    throw new Exception("Failed to update license settings in the database.");
                }
            }
            catch (UnauthorizedAccessException uaEx)
            {
                throw new Exception($"Access Denied: {uaEx.Message}", uaEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving license settings: {ex.Message}", ex);
            }
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picLogo.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void btnClearLogo_Click(object sender, EventArgs e)
        {
            picLogo.Image = null;
        }

        private void btnManageLocations_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Store_Management());
        }

        private void btnManageTables_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Table_Management());
        }

        private void btnManageAccount_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Profile_Management());
        }
    }
}
