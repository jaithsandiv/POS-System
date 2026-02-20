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
        private readonly BLL_AutoBackup _bllAutoBackup = new BLL_AutoBackup();
        private DataTable _settingsTable;
        private DataTable _storesTable;
        private bool _isSuperAdmin = false;

        public UC_SystemSettings()
        {
            InitializeComponent();
            
            // Check if current user is super admin
            CheckSuperAdminStatus();
            
            // Hide License tab for non-super admins
            ConfigureLicenseTabVisibility();
            
            LoadPrinters();
            LoadStoresDropdown();
            LoadSettings();
            
            // Wire up trial toggle event to validate trial days
            if (tsEnableTrial != null)
            {
                tsEnableTrial.Toggled += tsEnableTrial_Toggled;
            }
            
            // Wire up license checkbox event to enforce mutual exclusivity
            if (chkIsLicensed != null)
            {
                chkIsLicensed.CheckedChanged += chkIsLicensed_CheckedChanged;
            }
            
            // Wire up store location selection event
            if (cmbStoreLocation != null)
            {
                cmbStoreLocation.SelectedIndexChanged += CmbStoreLocation_SelectedIndexChanged;
            }
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
            // Make License group visible to all users
            if (grpLicense != null)
            {
                grpLicense.Visible = true;
                
                // Enable controls for all users - PIN verification will protect changes
                grpLicense.Enabled = true;
                tsEnableTrial.Enabled = true;
                txtTrialStartDate.Enabled = true;
                txtTrialEndDate.Enabled = true;
                txtTrialDays.Enabled = true;
                chkIsLicensed.Enabled = true;
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

        /// <summary>
        /// Loads all stores into the cmbStoreLocation dropdown
        /// </summary>
        private void LoadStoresDropdown()
        {
            try
            {
                // Temporarily unsubscribe from the event to prevent triggering during load
                if (cmbStoreLocation != null)
                {
                    cmbStoreLocation.SelectedIndexChanged -= CmbStoreLocation_SelectedIndexChanged;
                }

                // Get all stores
                _storesTable = _bllStore.GetStores();

                // Clear existing items
                cmbStoreLocation.Properties.Items.Clear();

                // Add each store to the dropdown
                if (_storesTable != null && _storesTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _storesTable.Rows)
                    {
                        string storeName = row["store_name"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(storeName))
                        {
                            cmbStoreLocation.Properties.Items.Add(storeName);
                        }
                    }
                }

                // Don't set a default selection here - let LoadSettings handle it
                
                // Re-subscribe to the event
                if (cmbStoreLocation != null)
                {
                    cmbStoreLocation.SelectedIndexChanged += CmbStoreLocation_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading stores: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadSettings()
        {
            try
            {
                // Temporarily unsubscribe from store location event
                if (cmbStoreLocation != null)
                {
                    cmbStoreLocation.SelectedIndexChanged -= CmbStoreLocation_SelectedIndexChanged;
                }

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

                // 3. Load Store Info - Get selected store from settings or use current user's store
                string selectedStoreName = GetStringSetting("selected_store_location");
                int selectedStoreId = -1;
                
                // If no saved selection, use current user's store
                if (string.IsNullOrEmpty(selectedStoreName) && Main.DataSetApp?.Store != null && Main.DataSetApp.Store.Rows.Count > 0)
                {
                    selectedStoreName = Main.DataSetApp.Store[0]["store_name"]?.ToString();
                    selectedStoreId = Convert.ToInt32(Main.DataSetApp.Store[0]["store_id"]);
                }
                else if (!string.IsNullOrEmpty(selectedStoreName))
                {
                    // Find store ID by name
                    DataTable storeDt = _bllStore.GetStores();
                    foreach (DataRow row in storeDt.Rows)
                    {
                        if (row["store_name"]?.ToString() == selectedStoreName)
                        {
                            selectedStoreId = Convert.ToInt32(row["store_id"]);
                            break;
                        }
                    }
                }

                // Set the selected store in the dropdown
                if (!string.IsNullOrEmpty(selectedStoreName) && cmbStoreLocation.Properties.Items.Contains(selectedStoreName))
                {
                    cmbStoreLocation.SelectedItem = selectedStoreName;
                }
                else if (cmbStoreLocation.Properties.Items.Count > 0)
                {
                    // Fallback to first store if saved selection is not found
                    cmbStoreLocation.SelectedIndex = 0;
                    selectedStoreName = cmbStoreLocation.Text;
                }

                // Load the selected store's data
                if (selectedStoreId > 0)
                {
                    DataTable storeData = _bllStore.GetStoreById(selectedStoreId);
                    if (storeData != null && storeData.Rows.Count > 0)
                    {
                        DataRow storeRow = storeData.Rows[0];
                        txtPhone.Text = storeRow["phone"]?.ToString();
                        txtEmail.Text = storeRow["email"]?.ToString();
                        txtAddress.Text = storeRow["address"]?.ToString();
                        txtCity.Text = storeRow["city"]?.ToString();
                        txtState.Text = storeRow["state"]?.ToString();
                        txtCountry.Text = storeRow["country"]?.ToString();
                        txtPostalCode.Text = storeRow["postal_code"]?.ToString();
                    }
                }
                else
                {
                    // Fallback: load first available store
                    DataTable storeDt = _bllStore.GetStores();
                    if (storeDt.Rows.Count > 0)
                    {
                        DataRow storeRow = storeDt.Rows[0];
                        txtPhone.Text = storeRow["phone"]?.ToString();
                        txtEmail.Text = storeRow["email"]?.ToString();
                        txtAddress.Text = storeRow["address"]?.ToString();
                        txtCity.Text = storeRow["city"]?.ToString();
                        txtState.Text = storeRow["state"]?.ToString();
                        txtCountry.Text = storeRow["country"]?.ToString();
                        txtPostalCode.Text = storeRow["postal_code"]?.ToString();
                    }
                }

                // 4. Load License & Trial settings (visible to all users now)
                LoadLicenseSettings();

                // 5. Load backup information
                LoadBackupInfo();

                // Re-subscribe to store location event
                if (cmbStoreLocation != null)
                {
                    cmbStoreLocation.SelectedIndexChanged += CmbStoreLocation_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                // Re-subscribe to event even if error occurs
                if (cmbStoreLocation != null)
                {
                    cmbStoreLocation.SelectedIndexChanged += CmbStoreLocation_SelectedIndexChanged;
                }
                XtraMessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads and displays backup information
        /// </summary>
        private void LoadBackupInfo()
        {
            try
            {
                DateTime? lastBackupDate = _bllAutoBackup.GetLastBackupDate();

                if (lastBackupDate.HasValue)
                {
                    labelControl7.Text = $"Last Backup: {lastBackupDate.Value:yyyy-MM-dd} | Auto-backup Location: C:\\POS_Backups";
                }
                else
                {
                    labelControl7.Text = "Last Backup: Never | Auto-backup Location: C:\\POS_Backups";
                }
            }
            catch
            {
                labelControl7.Text = "Backup Directory: C:\\POS_Backups";
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
                    
                    // Update visual indicators based on trial status
                    UpdateTrialStatusVisualIndicators();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading license settings: {ex.Message}\n\nDetails: {ex.StackTrace}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates visual indicators for trial status (color coding and messages)
        /// </summary>
        private void UpdateTrialStatusVisualIndicators()
        {
            var trialStatus = BLL_TrialManager.GetTrialStatus();
            
            if (grpLicense != null)
            {
                // Change license group appearance based on status
                if (trialStatus.IsLicensed)
                {
                    grpLicense.AppearanceCaption.ForeColor = System.Drawing.Color.Green;
                    grpLicense.Text = "License and Trial Settings - LICENSED";
                }
                else if (trialStatus.IsTrialExpired)
                {
                    grpLicense.AppearanceCaption.ForeColor = System.Drawing.Color.Red;
                    grpLicense.Text = "License and Trial Settings - TRIAL EXPIRED";
                    
                    // Show urgent message
                    XtraMessageBox.Show(
                        "Your trial period has expired!\n\n" +
                        "Please activate your license by checking the 'Is Licensed' checkbox " +
                        "to continue using all features of the system.\n\n" +
                        "You currently have restricted access to the system.",
                        "Trial Expired - Action Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else if (trialStatus.IsExpiringSoon)
                {
                    grpLicense.AppearanceCaption.ForeColor = System.Drawing.Color.Orange;
                    grpLicense.Text = $"License and Trial Settings - {trialStatus.DaysRemaining} Day(s) Remaining";
                }
                else if (trialStatus.IsTrialActive)
                {
                    grpLicense.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
                    grpLicense.Text = $"License and Trial Settings - {trialStatus.DaysRemaining} Day(s) Remaining";
                }
                else
                {
                    grpLicense.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
                    grpLicense.Text = "License and Trial Settings";
                }
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

                // Check if license settings were modified (for non-super admins)
                bool licenseSettingsModified = false;
                
                if (!_isSuperAdmin)
                {
                    licenseSettingsModified = CheckLicenseSettingsModified();
                    
                    if (licenseSettingsModified)
                    {
                        // Prompt for Super Admin PIN
                        var (verified, pin) = UC_SuperAdminPinDialog.ShowDialog();
                        
                        if (!verified || string.IsNullOrEmpty(pin))
                        {
                            XtraMessageBox.Show("License settings modification was cancelled.", "Cancelled", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                        // Verify the PIN against super admin users
                        if (!BLL_User.VerifySuperAdminPin(pin))
                        {
                            XtraMessageBox.Show("Invalid Super Admin PIN. License settings were not saved.", 
                                "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // 1. Save System Settings
                bool isThermal = rgPrintFormat.EditValue?.ToString() == "THERMAL";
                _bllSystemSettings.UpdateSystemSetting("ENABLE_THERMAL_PRINT", isThermal.ToString(), userId);
                _bllSystemSettings.UpdateSystemSetting("ENABLE_A4_PRINT", (!isThermal).ToString(), userId);

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

                // 4. Save License & Trial settings (if modified and PIN verified OR if super admin)
                if (_isSuperAdmin || licenseSettingsModified)
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
        /// Checks if license settings have been modified from their original values
        /// </summary>
        private bool CheckLicenseSettingsModified()
        {
            if (Main.DataSetApp?.Business == null || Main.DataSetApp.Business.Rows.Count == 0)
                return false;

            var businessRow = Main.DataSetApp.Business[0];
            
            // Check trial enabled status
            bool hasTrialDates = false;
            if (!businessRow.Istrial_start_dateNull() && !businessRow.Istrial_end_dateNull())
            {
                string startDateStr = businessRow.trial_start_date;
                string endDateStr = businessRow.trial_end_date;
                hasTrialDates = !string.IsNullOrWhiteSpace(startDateStr) && !string.IsNullOrWhiteSpace(endDateStr);
            }
            
            if (tsEnableTrial.IsOn != hasTrialDates)
                return true;
            
            // Check trial days
            if (tsEnableTrial.IsOn && !string.IsNullOrWhiteSpace(txtTrialDays.Text))
            {
                if (!businessRow.Istrial_start_dateNull() && !businessRow.Istrial_end_dateNull() &&
                    !string.IsNullOrWhiteSpace(businessRow.trial_start_date) && 
                    !string.IsNullOrWhiteSpace(businessRow.trial_end_date))
                {
                    if (DateTime.TryParse(businessRow.trial_start_date, out DateTime startDate) &&
                        DateTime.TryParse(businessRow.trial_end_date, out DateTime endDate))
                    {
                        int originalDays = (endDate - startDate).Days;
                        if (int.TryParse(txtTrialDays.Text, out int newDays) && originalDays != newDays)
                            return true;
                    }
                }
            }
            
            // Check licensed status
            bool isLicensed = false;
            if (!businessRow.Isis_licensedNull())
            {
                string licensedValue = businessRow.is_licensed?.ToString();
                isLicensed = licensedValue == "True" || licensedValue == "1";
            }
            
            if (chkIsLicensed.Checked != isLicensed)
                return true;
            
            return false;
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

        /// <summary>
        /// Saves license and trial period settings to the Business table
        /// </summary>
        private void SaveLicenseSettings(int userId)
        {
            try
            {
                // PIN verification is already done in btnSave_Click
                // No need to check _isSuperAdmin here
                
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
                    // Validate that trial days is provided
                    if (string.IsNullOrWhiteSpace(txtTrialDays.Text))
                    {
                        throw new Exception("Trial Days must be specified when enabling trial.");
                    }
                    
                    // Always use current date as start date when enabling trial
                    trialStartDate = DateTime.Now.Date; // Use date only (no time component)
                    trialEndDate = trialStartDate.Value.AddDays(trialDays);
                    
                    // Update the UI to show the calculated dates
                    txtTrialStartDate.Text = trialStartDate.Value.ToString("yyyy-MM-dd");
                    txtTrialEndDate.Text = trialEndDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    // Disabling trial - set dates to null
                    trialStartDate = null;
                    trialEndDate = null;
                    
                    // Clear the UI date fields
                    txtTrialStartDate.Text = "";
                    txtTrialEndDate.Text = "";
                }
                
                // Call BLL method to update license settings
                bool success = _bllSystemSettings.UpdateLicenseSettings(trialStartDate, trialEndDate, isLicensed, userId);
                
                if (!success)
                {
                    throw new Exception("Failed to update license settings in the database.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving license settings: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validates trial configuration when trial toggle is changed
        /// </summary>
        private void tsEnableTrial_Toggled(object sender, EventArgs e)
        {
            // If user is trying to enable trial while licensed, prevent it
            if (tsEnableTrial.IsOn && chkIsLicensed.Checked)
            {
                XtraMessageBox.Show(
                    "Cannot enable trial on a licensed system.\n\n" +
                    "A system can either be Licensed OR on Trial, but not both.\n\n" +
                    "Please uncheck 'Is Licensed' first if you want to enable trial mode.",
                    "License/Trial Conflict",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                // Reset toggle back to off
                tsEnableTrial.IsOn = false;
                return;
            }
            
            if (tsEnableTrial.IsOn)
            {
                // Check if trial days is provided
                if (string.IsNullOrWhiteSpace(txtTrialDays.Text))
                {
                    XtraMessageBox.Show("Please enter the number of trial days before enabling trial.", 
                        "Trial Days Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Reset toggle back to off
                    tsEnableTrial.IsOn = false;
                    txtTrialDays.Focus();
                    return;
                }
                
                // Validate trial days is a valid number
                if (!int.TryParse(txtTrialDays.Text, out int days) || days <= 0)
                {
                    XtraMessageBox.Show("Please enter a valid positive number for trial days.", 
                        "Invalid Trial Days", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Reset toggle back to off
                    tsEnableTrial.IsOn = false;
                    txtTrialDays.Focus();
                    return;
                }
                
                // Calculate and display trial dates
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddDays(days);
                
                txtTrialStartDate.Text = startDate.ToString("yyyy-MM-dd");
                txtTrialEndDate.Text = endDate.ToString("yyyy-MM-dd");
                
                XtraMessageBox.Show(
                    $"Trial will be enabled with the following dates:\n\n" +
                    $"Start Date: {startDate:yyyy-MM-dd}\n" +
                    $"End Date: {endDate:yyyy-MM-dd}\n" +
                    $"Duration: {days} day(s)\n\n" +
                    $"Click Save to apply these changes.",
                    "Trial Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Clear trial dates when disabling
                txtTrialStartDate.Text = "";
                txtTrialEndDate.Text = "";
            }
        }
        
        /// <summary>
        /// Handles license checkbox change to ensure mutual exclusivity with trial
        /// </summary>
        private void chkIsLicensed_CheckedChanged(object sender, EventArgs e)
        {
            // If user is checking "Is Licensed" while trial is enabled, warn them
            if (chkIsLicensed.Checked && tsEnableTrial.IsOn)
            {
                var result = XtraMessageBox.Show(
                    "You are activating a license while trial mode is enabled.\n\n" +
                    "When licensed, the trial period will be ignored.\n" +
                    "A system can either be Licensed OR on Trial, but not both.\n\n" +
                    "Do you want to proceed with license activation?\n" +
                    "(Trial will be automatically disabled)",
                    "License Activation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // Disable trial when activating license
                    tsEnableTrial.IsOn = false;
                    txtTrialStartDate.Text = "";
                    txtTrialEndDate.Text = "";
                }
                else
                {
                    // User cancelled, uncheck the license
                    chkIsLicensed.Checked = false;
                }
            }
        }

        /// <summary>
        /// Handles backup database button click - Automatically backs up to C:\POS_Backups\
        /// </summary>
        private void btnBackupDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                // Fixed backup directory
                string backupDirectory = @"C:\POS_Backups";

                // Ensure backup directory exists
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                // Generate timestamped filename
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFileName = $"POS_Backup_{timestamp}.bak";
                string backupFilePath = Path.Combine(backupDirectory, backupFileName);

                // Show progress message
                XtraMessageBox.Show("Database backup in progress. Please wait...",
                    "Backup Started", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DAL.DAL_DatabaseBackup backupService = new DAL.DAL_DatabaseBackup();
                bool success = backupService.BackupDatabase(backupFilePath);

                if (success)
                {
                    // Update last backup date
                    Properties.Settings.Default.LastBackupDate = DateTime.Now.ToString("yyyy-MM-dd");
                    Properties.Settings.Default.Save();

                    // Reload backup info to update UI
                    LoadBackupInfo();

                    XtraMessageBox.Show(
                        $"Database backup completed successfully!\n\nBackup saved to:\n{backupFilePath}",
                        "Backup Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Database backup failed.",
                        "Backup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error during backup: {ex.Message}",
                    "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles restore database button click - Opens OpenFileDialog and restores database
        /// </summary>
        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm restore action
                var confirmResult = XtraMessageBox.Show(
                    "WARNING: This will restore the database from a backup file.\n\n" +
                    "All current data will be replaced with the backup data.\n" +
                    "This action cannot be undone.\n\n" +
                    "Do you want to continue?",
                    "Confirm Database Restore",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                    return;

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Database Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                    ofd.Title = "Select Backup File to Restore";
                    ofd.CheckFileExists = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Show progress message
                        XtraMessageBox.Show(
                            "Database restore in progress. Please wait...\n\n" +
                            "The application may need to restart after restore.",
                            "Restore Started",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        DAL.DAL_DatabaseBackup backupService = new DAL.DAL_DatabaseBackup();
                        bool success = backupService.RestoreDatabase(ofd.FileName);

                        if (success)
                        {
                            var restartResult = XtraMessageBox.Show(
                                $"Database restore completed successfully!\n\n" +
                                $"Restored from:\n{ofd.FileName}\n\n" +
                                $"The application should be restarted to ensure data consistency.\n\n" +
                                $"Do you want to restart the application now?",
                                "Restore Successful",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);

                            if (restartResult == DialogResult.Yes)
                            {
                                Application.Restart();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Database restore failed.",
                                "Restore Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error during restore: {ex.Message}",
                    "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles store location selection change and populates address fields
        /// </summary>
        private void CmbStoreLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbStoreLocation.SelectedIndex < 0 || string.IsNullOrWhiteSpace(cmbStoreLocation.Text))
                {
                    return;
                }

                string selectedStoreName = cmbStoreLocation.Text.Trim();

                // Find the store by name in the stores table
                if (_storesTable != null && _storesTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _storesTable.Rows)
                    {
                        if (row["store_name"]?.ToString()?.Trim() == selectedStoreName)
                        {
                            // Populate the address fields with the selected store's data
                            txtAddress.Text = row["address"]?.ToString() ?? "";
                            txtCity.Text = row["city"]?.ToString() ?? "";
                            txtState.Text = row["state"]?.ToString() ?? "";
                            txtPostalCode.Text = row["postal_code"]?.ToString() ?? "";
                            txtCountry.Text = row["country"]?.ToString() ?? "";
                            
                            // Also populate contact fields if they exist in the store data
                            txtPhone.Text = row["phone"]?.ToString() ?? "";
                            txtEmail.Text = row["email"]?.ToString() ?? "";
                            
                            // Save the selected store location to system settings for persistence
                            try
                            {
                                int userId = 1;
                                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Count > 0 && !Main.DataSetApp.User[0].Isuser_idNull())
                                {
                                    int.TryParse(Main.DataSetApp.User[0].user_id, out userId);
                                }
                                
                                _bllSystemSettings.UpdateSystemSetting("selected_store_location", selectedStoreName, userId);
                            }
                            catch
                            {
                                // Silently fail if saving preference fails - not critical
                            }
                            
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading store address: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
