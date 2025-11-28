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
    public partial class UC_Category_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private int? _categoryId = null;

        public UC_Category_Registration(int? categoryId = null)
        {
            InitializeComponent();
            _categoryId = categoryId;

            if (_categoryId.HasValue)
            {
                lblTitle.Text = "Edit Category";
                LoadCategoryData();
            }
            else
            {
                lblTitle.Text = "Add Category";
            }
        }

        private void LoadCategoryData()
        {
            DataTable dt = _bllProducts.GetCategoryById(_categoryId.Value);
            if (dt.Rows.Count > 0)
            {
                txtCategoryName.Text = dt.Rows[0]["category_name"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtCategoryName.Text.Trim();
                int userId = 1; // Default Admin user for now

                if (Main.DataSetApp != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                     // Attempt to get logged in user ID
                     // userId = ...
                }

                if (_categoryId.HasValue)
                {
                    bool success = _bllProducts.UpdateCategory(_categoryId.Value, name, userId);
                    if (success)
                    {
                        XtraMessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToManagement();
                    }
                }
                else
                {
                    int newId = _bllProducts.InsertCategory(name, userId);
                    if (newId > 0)
                    {
                        XtraMessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Main.Instance.LoadUserControl(new UC_Category_Management());
        }
    }
}
