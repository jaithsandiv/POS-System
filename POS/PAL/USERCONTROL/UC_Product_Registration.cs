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
    public partial class UC_Product_Registration : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private int? _productId = null;

        public UC_Product_Registration(int? productId = null)
        {
            InitializeComponent();
            _productId = productId;
            LoadDropdowns();

            if (_productId.HasValue)
            {
                lblTitle.Text = "Edit Product";
                lblSubtitle.Text = "Edit product details";
                LoadProductData();
            }
            else
            {
                lblTitle.Text = "Product Registration";
                lblSubtitle.Text = "Add a new product";
            }
        }

        private void LoadDropdowns()
        {
            try
            {
                // Categories
                DataTable dtCategories = _bllProducts.GetCategories();
                lueCategory.Properties.DataSource = dtCategories;
                lueCategory.Properties.DisplayMember = "category_name";
                lueCategory.Properties.ValueMember = "category_id";
                lueCategory.Properties.Columns.Clear();
                lueCategory.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("category_name", "Category"));

                // Brands
                DataTable dtBrands = _bllProducts.GetBrands();
                lueBrand.Properties.DataSource = dtBrands;
                lueBrand.Properties.DisplayMember = "brand_name";
                lueBrand.Properties.ValueMember = "brand_id";
                lueBrand.Properties.Columns.Clear();
                lueBrand.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("brand_name", "Brand"));

                // Units
                DataTable dtUnits = _bllProducts.GetUnits();
                lueUnit.Properties.DataSource = dtUnits;
                lueUnit.Properties.DisplayMember = "name";
                lueUnit.Properties.ValueMember = "unit_id";
                lueUnit.Properties.Columns.Clear();
                lueUnit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code"));
                lueUnit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "Name"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadProductData()
        {
            DataTable dt = _bllProducts.GetProductById(_productId.Value);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtProductName.Text = row["product_name"].ToString();
                txtProductCode.Text = row["product_code"].ToString();
                txtBarcode.Text = row["barcode"]?.ToString();

                if (row["product_type"] != DBNull.Value)
                    cmbProductType.SelectedItem = row["product_type"].ToString();
                else
                    cmbProductType.SelectedItem = "Standard";

                if (row["category_id"] != DBNull.Value)
                    lueCategory.EditValue = Convert.ToInt32(row["category_id"]);

                if (row["brand_id"] != DBNull.Value)
                    lueBrand.EditValue = Convert.ToInt32(row["brand_id"]);

                if (row["unit_id"] != DBNull.Value)
                    lueUnit.EditValue = Convert.ToInt32(row["unit_id"]);

                txtPurchaseCost.Text = row["purchase_cost"]?.ToString();
                txtSellingPrice.Text = row["selling_price"]?.ToString();
                txtStockQty.Text = row["stock_quantity"]?.ToString();

                if (row["expiry_date"] != DBNull.Value)
                    dtExpiryDate.DateTime = Convert.ToDateTime(row["expiry_date"]);

                if (row["manufacture_date"] != DBNull.Value)
                    dtManufactureDate.DateTime = Convert.ToDateTime(row["manufacture_date"]);

                memoDescription.Text = row["description"]?.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = txtProductName.Text.Trim();
                string productCode = txtProductCode.Text.Trim();
                string barcode = txtBarcode.Text.Trim();
                string productType = cmbProductType.SelectedItem?.ToString();
                int? categoryId = lueCategory.EditValue != null ? (int?)Convert.ToInt32(lueCategory.EditValue) : null;
                int? brandId = lueBrand.EditValue != null ? (int?)Convert.ToInt32(lueBrand.EditValue) : null;
                int unitId = lueUnit.EditValue != null ? Convert.ToInt32(lueUnit.EditValue) : 0;

                decimal purchaseCost = 0;
                decimal.TryParse(txtPurchaseCost.Text, out purchaseCost);

                decimal sellingPrice = 0;
                decimal.TryParse(txtSellingPrice.Text, out sellingPrice);

                decimal stockQty = 0;
                decimal.TryParse(txtStockQty.Text, out stockQty);

                DateTime? expiryDate = dtExpiryDate.EditValue != null ? (DateTime?)dtExpiryDate.DateTime : null;
                DateTime? manufactureDate = dtManufactureDate.EditValue != null ? (DateTime?)dtManufactureDate.DateTime : null;
                string description = memoDescription.Text.Trim();

                int userId = 1; // Default

                if (_productId.HasValue)
                {
                    bool success = _bllProducts.UpdateProduct(_productId.Value, productName, productCode, barcode, productType,
                                                              categoryId, brandId, unitId,
                                                              purchaseCost, sellingPrice, stockQty,
                                                              expiryDate, manufactureDate, description,
                                                              userId);
                    if (success)
                    {
                        XtraMessageBox.Show("Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReturnToManagement();
                    }
                }
                else
                {
                    int newId = _bllProducts.InsertProduct(productName, productCode, barcode, productType,
                                                           categoryId, brandId, unitId,
                                                           purchaseCost, sellingPrice, stockQty,
                                                           expiryDate, manufactureDate, description,
                                                           userId);
                    if (newId > 0)
                    {
                        XtraMessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Main.Instance.LoadUserControl(new UC_Product_Management());
        }
    }
}
