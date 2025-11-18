using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.BLL
{
    internal class BLL_Initialize
    {
        private readonly DAL.DAL_Initialize dal = new DAL.DAL_Initialize();
        internal DataTable GetBusiness()
        {
            return dal.GetBusiness();
        }

        internal DataTable GetSystemSettings()
        {
            return dal.GetSystemSettings();
        }
    }
}
