using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Unit_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private int? _unitId = null;

        public UC_Unit_Registration(int? unitId = null)
        {
            InitializeComponent();
            _unitId = unitId;

            if (_unitId.HasValue)
            {
                lblTitle.Text = "Edit Unit";
                lblSubtitle.Text = "Edit unit details";
                LoadUnitData();
            }
            else
            {
                lblTitle.Text = "Unit Registration";
                lblSubtitle.Text = "Add a new unit";
            }
        }

        private void LoadUnitData()
        {
            DataTable dt = _bllProducts.GetUnitById(_unitId.Value);
            if (dt.Rows.Count > 0)
            {
                txtUnitCode.Text = dt.Rows[0]["code"].ToString();
                txtUnitName.Text = dt.Rows[0]["name"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtUnitCode.Text.Trim();
                string name = txtUnitName.Text.Trim();
                int userId = 1; // Default

                if (_unitId.HasValue)
                {
                    bool success = _bllProducts.UpdateUnit(_unitId.Value, code, name, userId);
                    if (success)
                    {
                        XtraMessageBox.Show("Unit updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToManagement();
                    }
                }
                else
                {
                    int newId = _bllProducts.InsertUnit(code, name, userId);
                    if (newId > 0)
                    {
                        XtraMessageBox.Show("Unit added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToManagement();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToManagement();
        }

        private void ReturnToManagement()
        {
            Main.Instance.LoadUserControl(new UC_Unit_Management());
        }
    }
}
