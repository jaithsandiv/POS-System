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
    public partial class UC_Brand_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private readonly BLL_Contacts _bllContacts = new BLL_Contacts();
        private int? _brandId = null;

        public UC_Brand_Registration(int? brandId = null)
        {
            InitializeComponent();
            _brandId = brandId;
            LoadSuppliers();

            if (_brandId.HasValue)
            {
                lblTitle.Text = "Edit Brand";
                lblSubtitle.Text = "Edit brand details";
                LoadBrandData();
            }
            else
            {
                lblTitle.Text = "Brand Registration";
                lblSubtitle.Text = "Add a new brand";
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = _bllContacts.GetSuppliers();
                lueSupplier.Properties.DataSource = dt;
                lueSupplier.Properties.DisplayMember = "supplier_name";
                lueSupplier.Properties.ValueMember = "supplier_id";

                lueSupplier.Properties.Columns.Clear();
                lueSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("supplier_name", "Supplier"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadBrandData()
        {
            DataTable dt = _bllProducts.GetBrandById(_brandId.Value);
            if (dt.Rows.Count > 0)
            {
                txtBrandName.Text = dt.Rows[0]["brand_name"].ToString();
                if (dt.Rows[0]["supplier_id"] != DBNull.Value)
                {
                    lueSupplier.EditValue = Convert.ToInt32(dt.Rows[0]["supplier_id"]);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtBrandName.Text.Trim();
                int? supplierId = null;
                if (lueSupplier.EditValue != null)
                {
                    supplierId = Convert.ToInt32(lueSupplier.EditValue);
                }

                int userId = 1; // Default

                if (_brandId.HasValue)
                {
                    bool success = _bllProducts.UpdateBrand(_brandId.Value, name, supplierId, userId);
                    if (success)
                    {
                        XtraMessageBox.Show("Brand updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToManagement();
                    }
                }
                else
                {
                    int newId = _bllProducts.InsertBrand(name, supplierId, userId);
                    if (newId > 0)
                    {
                        XtraMessageBox.Show("Brand added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Main.Instance.LoadUserControl(new UC_Brand_Management());
        }
    }
}
