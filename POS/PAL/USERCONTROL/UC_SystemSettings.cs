using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SystemSettings : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SystemSettings _bllSystemSettings = new BLL_SystemSettings();
        private DataTable _settingsTable;

        public UC_SystemSettings()
        {
            InitializeComponent();
            LoadPrinters();
            LoadSettings();
        }

        private void LoadPrinters()
        {
            cmbThermalPrinter.Properties.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                cmbThermalPrinter.Properties.Items.Add(printer);
            }
        }

        private void LoadSettings()
        {
            _settingsTable = _bllSystemSettings.GetSystemSettings();

            if (_settingsTable != null)
            {
                bool enableThermal = GetBooleanSetting("ENABLE_THERMAL_PRINT");
                if (enableThermal)
                {
                    rgPrintFormat.EditValue = "THERMAL";
                }
                else
                {
                    rgPrintFormat.EditValue = "A4";
                }

                tsKOT.IsOn = GetBooleanSetting("kot_enabled");
                
                string printerName = GetStringSetting("thermal_printer_name");
                if (!string.IsNullOrEmpty(printerName))
                {
                    cmbThermalPrinter.SelectedItem = printerName;
                }
            }
        }

        private bool GetBooleanSetting(string key)
        {
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
            DataRow[] rows = _settingsTable.Select($"setting_key = '{key}'");
            if (rows.Length > 0)
            {
                return rows[0]["setting_value"]?.ToString();
            }
            return string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Main.DataSetApp.User.Count > 0 ? int.Parse(Main.DataSetApp.User[0].user_id) : 1;

                string printFormat = rgPrintFormat.EditValue?.ToString();
                bool enableThermal = printFormat == "THERMAL";
                bool enableA4 = printFormat == "A4";

                _bllSystemSettings.UpdateSystemSetting("ENABLE_THERMAL_PRINT", enableThermal.ToString(), userId);
                _bllSystemSettings.UpdateSystemSetting("ENABLE_A4_PRINT", enableA4.ToString(), userId);
                _bllSystemSettings.UpdateSystemSetting("AUTO_PRINT_ON_COMPLETION", "True", userId);
                _bllSystemSettings.UpdateSystemSetting("kot_enabled", tsKOT.IsOn.ToString(), userId);
                
                if (cmbThermalPrinter.SelectedItem != null)
                {
                    _bllSystemSettings.UpdateSystemSetting("thermal_printer_name", cmbThermalPrinter.SelectedItem.ToString(), userId);
                }

                // Reload settings in Main to apply changes immediately
                if (Main.Instance != null)
                {
                    Main.Instance.LoadSystemSettings();
                }
                
                // Refresh the local table
                LoadSettings();

                MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
