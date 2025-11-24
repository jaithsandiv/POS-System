using POS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BLL
{
    internal class BLL_SystemSettings
    {
        private readonly DAL_SystemSettings _dalSystemSettings = new DAL_SystemSettings();

        public DataTable GetSystemSettings()
        {
            return _dalSystemSettings.GetSystemSettings();
        }

        public bool UpdateSystemSetting(string key, string value, int updatedBy)
        {
            return _dalSystemSettings.UpdateSystemSetting(key, value, updatedBy);
        }

        public bool UpdateBusinessSettings(string businessName, byte[] logo, int updatedBy)
        {
            return _dalSystemSettings.UpdateBusinessSettings(businessName, logo, updatedBy);
        }
    }
}
